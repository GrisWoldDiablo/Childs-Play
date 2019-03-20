Shader "Custom/highlightVolume"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,.2)
		_IntersectionMax("Intersection Max", Range(0.01,5)) = 1
		_IntersectionDamper("Intersection Damper", Range(0.01,5)) = 1
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		Pass
		{
			Stencil
			{
				Ref 172
				Comp Always
				Pass Replace
				ZFail Zero
			}
			Blend Zero One
			Cull Front
			ZTest  GEqual
			//ZTest  Always
			ZWrite Off

		}// end stencil pass

			
		Pass
		{
			//
				/*Cull Off
				ZTest Off*/
			//ZTest NotEqual
			Blend SrcAlpha OneMinusSrcAlpha
			Stencil
			{
				Ref 172
				Comp Equal
				Pass Replace
		//Comp notequal
			}
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			float4 _Color;
			sampler2D _MainTex;
			sampler2D _CameraDepthTexture;
			half _IntersectionMax;
			half _IntersectionDamper;

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 screenPos : TEXCOORD1;
				float4 vertex : SV_POSITION;
				float2 uv2 : TEXCOORD3;

			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			fixed4 frag(v2f i) : SV_Target
			{
				half highlight_mask = max(0, sign(_IntersectionMax));
				highlight_mask *= 1 - _IntersectionMax * _IntersectionDamper;

				fixed4 col = tex2D(_MainTex, i.uv2);
				highlight_mask *= _Color.a;

				//col *= _MainColor * (1 - highlight_mask);
				col += _Color * highlight_mask;

				col.a = max(highlight_mask, col.a);

				
				return col;
				//return _Color;
			}
			
			ENDCG
			
		}//end color pass
		//Fallback "Diffuse"
	}
}