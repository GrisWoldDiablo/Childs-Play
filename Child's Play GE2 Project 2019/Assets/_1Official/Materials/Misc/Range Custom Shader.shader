// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// Starting point of script:
// https://forum.unity.com/threads/approach-for-displaying-intersection-between-plane-and-sphere.344079/

Shader "Custom/Intersection Additive" {

	Properties{
		_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex("Main Texture", 2D) = "white" {}
		_InvFade("Soft Particles Factor", Range(0.1,1.0)) = 0.5
		_Edge("Edge (intensity, cutoff)", Vector) = (0.1, 0.375, 0.0, 0.0)
		_EdgeTex("Edge Texture", 2D) = "white" {}
		_BumpMap("Normals ", 2D) = "bump" {}
		_Distort("Distort Factor", Range(0.1,100.0)) = 0.5
		_Displace("Diplace Factor", Range(0.1,100.0)) = 0.5
		_BumpDirection("Bump Direction & Speed", Vector) = (1.0 ,1.0, -1.0, 1.0)
	}

	Category{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off Lighting Off ZWrite Off ZTest LESS
			
		SubShader{
			Tags
			{
				"RenderType" = "Transparent"
				"Queue" = "Transparent"
			}

			GrabPass
			{
				"_BumpMap"
			}
			Pass{
				CGPROGRAM
				#include "UnityCG.cginc"
				half4 Edge(sampler2D edgeTex, half4 coords)
				{
					half4 edge = (tex2D(edgeTex, coords.xy) * tex2D(edgeTex, coords.zw)) - 0.125;
					return edge;
				}
				half3 PerPixelNormal(sampler2D bumpMap, half4 coords, half3 vertexNormal, half bumpStrength)
				{
					half3 bump = (UnpackNormal(tex2D(bumpMap, coords.xy)) + UnpackNormal(tex2D(bumpMap, coords.zw))) * 0.5;
					half3 worldNormal = vertexNormal + bump.xxy * bumpStrength * half3(1,0,1);
					return normalize(worldNormal);
				}
				inline void ComputeScreenAndGrabPassPos(float4 pos, out float4 screenPos, out float4 grabPassPos)
				{
					#if UNITY_UV_STARTS_AT_TOP
						float scale = -1.0;
					#else
						float scale = 1.0f;
					#endif
				
					screenPos = ComputeNonStereoScreenPos(pos);
					grabPassPos.xy = (float2(pos.x, pos.y*scale) + pos.w) * 0.5;
					grabPassPos.zw = pos.zw;
				}
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_particles
				#pragma multi_compile_fog


				sampler2D _MainTex;
				sampler2D _EdgeTex;
				fixed4 _TintColor;
				uniform float4 _Edge;
				sampler2D _BumpMap;
				float4 _BumpDirection;

				struct appdata_t {
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float2 texcoord : TEXCOORD0;
				};

				struct v2f {
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					float4 viewInterpolator : TEXCOORD1;
					//UNITY_FOG_COORDS(1)
					float4 grabPos : TEXCOORD2;
					float4 screenPos : TEXCOORD3;
					float4 grabPassPos : TEXCOORD4;
					float3 normal : NORMAL;
				#ifdef SOFTPARTICLES_ON
				#endif
				};

				float4 _MainTex_ST;

				//v2f vert(appdata_t v)
				v2f vert(appdata_full v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.screenPos = ComputeGrabScreenPos(o.vertex);
					o.texcoord.xyzw = (_Time.xxxx * _BumpDirection.xyzw);
					o.texcoord.xy += TRANSFORM_TEX(v.texcoord,_MainTex);
					o.grabPos = ComputeScreenPos(o.vertex);
					COMPUTE_EYEDEPTH(o.grabPos.z);
					o.color = v.color;
					o.normal = UnityObjectToWorldNormal(v.normal);
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				sampler2D_float _CameraDepthTexture;
				float _InvFade;
				float _Distort;
				float _Displace;

				fixed4 frag(v2f i) : SV_Target
				{
					half3 worldNormal = PerPixelNormal(_BumpMap, i.grabPos, i.texcoord.xyz, _Displace);
					//half3 viewVector = normalize(i.viewInterpolator.xyz);

					half4 distortOffset = half4(worldNormal.xz * _Distort * 10.0, 0, 0);
					half4 screenWithOffset = i.screenPos + distortOffset;
					half4 grabWithOffset = i.grabPassPos + distortOffset;
					half4 rtRefractions = tex2Dproj(_MainTex, UNITY_PROJ_COORD(grabWithOffset));

				#ifdef SOFTPARTICLES_ON
					float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.grabPos)));
					float partZ = i.grabPos.z;
					float fade = saturate(_InvFade * (sceneZ - partZ));
					//i.color.a += saturate(1 - fade) * (1 - i.color.a);
					_TintColor.a += saturate(1 - fade) * (1 - _TintColor.a);
				#endif

					fixed4 col = 2.0f * i.color * _TintColor * tex2D(_MainTex, i.texcoord);
					half4 edge = Edge(_EdgeTex, i.grabPos * 2.0);
					float4 offset = tex2D(_MainTex, i.texcoord - _Time.xy * _Distort);
					i.grabPos.xy -= offset.xy * _Displace;
					col *= tex2Dproj(_BumpMap, i.grabPos);
					//_TintColor.rgb += edge.rgb * _Edge.x * (/*edgeBlendFactors.y +*/ saturate(i.viewInterpolator.w - _Edge.y));
					//_TintColor = lerp(rtRefractions, _TintColor, _TintColor.a);
					//col *= _TintColor;

					//UNITY_APPLY_FOG_COLOR(i.fogCoord, col, fixed4(0,0,0,0)); // fog towards black due to our blend mode
					return col;
				}
				ENDCG
			}
		}

	}
}