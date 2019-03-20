Shader "Render Depth" {
	Properties{
		_Color("Color", Color) = (1,1,1,.2)
	}
	SubShader{
		Tags { "RenderType" = "Transparent" }
		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
				float2 depth : TEXCOORD0;
			};

			v2f vert(appdata_base v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				UNITY_TRANSFER_DEPTH(o.depth);
				return o;
			}

			half4 frag(v2f i) : SV_Target {
				UNITY_OUTPUT_DEPTH(i.depth);
			}
			ENDCG
		}
	}
}