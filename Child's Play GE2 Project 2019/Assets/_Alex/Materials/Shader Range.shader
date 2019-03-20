Shader "Custom/RangeEdge"{

	Properties
	{
		_Color("Color", Color) = (1,1,1,.2)
		_IntersectionMax("Intersection Max", Range(0.01,5)) = 1
		_IntersectionDamper("Intersection Damper", Range(0.01,5)) = 1
		_MainTex("Texture", 2D) = "white" {}
		_Edge("Edge (intensity, cutoff)", Vector) = (0.1, 0.375, 0.0, 0.0)
		_EdgeTex("Edge texture ", 2D) = "black" {}
		_EdgeFadeParam("Auto blend parameter (Edge, Shore, Distance scale)", Vector) = (0.15 ,0.15, 0.5, 1.0)
		_DistortParams("Distortions (Bump waves, Reflection, Fresnel power, Fresnel bias)", Vector) = (1.0 ,1.0, 2.0, 1.15)
		_BumpMap("Normals ", 2D) = "bump" {}
		_BaseColor("Base color", COLOR) = (.54, .95, .99, 0.5)
	}

		CGINCLUDE

#include "UnityCG.cginc"
		struct appdata
	{
		float4 vertex : POSITION;
	};

	struct v2f
	{
		float4 uv : TEXCOORD0;
		float4 viewInterpolator : TEXCOORD1;
		float4 bumpCoords : TEXCOORD2;
		float4 vertex : SV_POSITION;
		float4 screenPos : TEXCOORD3;
		float4 grabPassPos : TEXCOORD4;
	};

//#pragma vertex vert
//#pragma fragment frag
	float4 _Color;
	sampler2D _MainTex;
	half _IntersectionMax;
	half _IntersectionDamper;

	//Edge
	uniform float4 _Edge;
	sampler2D _EdgeTex;
	uniform float4 _EdgeFadeParam;
	sampler2D_float _CameraDepthTexture;
	uniform float4 _DistortParams;
	sampler2D _BumpMap;
	sampler2D _RefractionTex;
	uniform float4 _BaseColor;
#define PER_PIXEL_DISPLACE _DistortParams.x
#define REALTIME_DISTORTION _DistortParams.y
#define VERTEX_WORLD_NORMAL i.uv.xyz



	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		return o;
	}

	inline half3 PerPixelNormal(sampler2D bumpMap, half4 coords, half3 vertexNormal, half bumpStrength)
	{
		half3 bump = (UnpackNormal(tex2D(bumpMap, coords.xy)) + UnpackNormal(tex2D(bumpMap, coords.zw))) * 0.5;
		half3 worldNormal = vertexNormal + bump.xxy * bumpStrength * half3(1, 0, 1);
		return normalize(worldNormal);
	}

	inline half4 Foam(sampler2D shoreTex, half4 coords, half amount)
	{
		half4 foam = (tex2D(shoreTex, coords.xy) * tex2D(shoreTex, coords.zw)) - 0.125;
		foam.a = amount;
		return foam;
	}

	inline half4 Foam(sampler2D shoreTex, half4 coords)
	{
		half4 foam = (tex2D(shoreTex, coords.xy) * tex2D(shoreTex, coords.zw)) - 0.125;
		return foam;
	}

	half4  ExtinctColor(half4 baseColor, half extinctionAmount)
	{
		// tweak the extinction coefficient for different coloring
		return baseColor - extinctionAmount * half4(0.15, 0.03, 0.01, 0.0);
	}

	half4 frag(v2f i) : SV_Target
	{

		//Edge
		half3 worldNormal = PerPixelNormal(_BumpMap, i.bumpCoords, VERTEX_WORLD_NORMAL, PER_PIXEL_DISPLACE);
		half4 distortOffset = half4(worldNormal.xz * REALTIME_DISTORTION * 10.0, 0, 0);
		half4 screenWithOffset = i.screenPos + distortOffset;
		half4 grabWithOffset = i.grabPassPos + distortOffset;
		half4 rtRefractionsNoDistort = tex2Dproj(_RefractionTex, UNITY_PROJ_COORD(i.grabPassPos));
		half refrFix = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(grabWithOffset));
		half4 rtRefractions = tex2Dproj(_RefractionTex, UNITY_PROJ_COORD(grabWithOffset));

		if (LinearEyeDepth(refrFix) < i.screenPos.z)
		rtRefractions = rtRefractionsNoDistort;

		half4 edgeBlendFactors = half4(1.0, 0.0, 0.0, 0.0);

		float depth = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos));
		depth = LinearEyeDepth(depth);
		edgeBlendFactors = saturate(_EdgeFadeParam * (depth - i.screenPos.w));
		edgeBlendFactors.y = 1.0 - edgeBlendFactors.y;

		half4 baseColor = ExtinctColor(_BaseColor, i.viewInterpolator.w * _EdgeFadeParam.w);

		half4 foam = Foam(_EdgeTex, i.bumpCoords * 2.0);
		baseColor.rgb += foam.rgb * _Edge.x * (edgeBlendFactors.y + saturate(i.viewInterpolator.w - _Edge.y));

		//Edge

		half highlight_mask = max(0, sign(_IntersectionMax));
		highlight_mask *= 1 - _IntersectionMax * _IntersectionDamper;

		fixed4 col = tex2D(_MainTex, i.screenPos);
		highlight_mask *= _Color.a;

		//col *= _MainColor * (1 - highlight_mask);
		col *= baseColor;// *highlight_mask;
		baseColor *=col ;// *highlight_mask;

		//col.a = max(highlight_mask, col.a);
		//baseColor.a = edgeBlendFactors.x;
		//col *= baseColor
		return baseColor;
		//return baseColor;
		//return _Color;
	}
		ENDCG
	//	SubShader
	//{
	//	Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
	//		Pass
	//	{
	//		Stencil
	//		{
	//			Ref 172
	//			Comp Always
	//			Pass Replace
	//			ZFail Zero
	//		}
	//		Blend Zero One
	//		Cull Front
	//		ZTest  GEqual
	//		//ZTest  Always
	//		ZWrite Off

	//	}// end stencil pass


	//		Pass
	//	{
	//		//
	//			/*Cull Off
	//			ZTest Off*/
	//			//ZTest NotEqual
	//			Blend SrcAlpha OneMinusSrcAlpha

	//			
	//		ZTest LEqual
	//		ZWrite Off
	//		Cull Off

	//			Stencil
	//			{
	//				Ref 172
	//				Comp Equal
	//				Pass Replace
	//		//Comp notequal
	//			}
	//			CGPROGRAM




	//			ENDCG
	//	}//end color pass
	//		//Fallback "Transparent/Diffuse"
	//}
	Subshader
	{
		Tags {"RenderType" = "Transparent" "Queue" = "Transparent"}

		ColorMask RGB

		GrabPass { "_RefractionTex" }

		Pass {
				Blend SrcAlpha OneMinusSrcAlpha
				ZTest LEqual
				ZWrite Off
				Cull Off

				CGPROGRAM

				#pragma target 3.0

				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog

				//#pragma multi_compile WATER_VERTEX_DISPLACEMENT_ON WATER_VERTEX_DISPLACEMENT_OFF
				//#pragma multi_compile WATER_EDGEBLEND_ON WATER_EDGEBLEND_OFF
				//#pragma multi_compile WATER_REFLECTIVE WATER_SIMPLE

				ENDCG
		}
	}
	Fallback "Transparent/Diffuse"
}

