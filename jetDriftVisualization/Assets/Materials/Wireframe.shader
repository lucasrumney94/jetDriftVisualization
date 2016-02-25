Shader "HARDYSHADER/Wireframe"
{
	Properties
	{
		_WireColor("WireColor", Color) = (1, 0, 0, 1)
		_Color("Color", Color) = (1, 1, 1, 0)
		_Thickness("Thickness", float) = 1
	}

	SubShader
	{
		Cull Back
		Zwrite On
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "Rendertype" = "Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{

			CGPROGRAM
			#pragma target 4.0
			#pragma vertex vert
			#pragma geometry geom
			#pragma fragment frag
			// include file that contains UnityObjectToWorldNormal helper function
			#include "UnityCG.cginc"

			half4 _WireColor, _Color;
			float _Thickness;
			
			struct v2g {
				float4 pos : SV_POSITION;
			};

			struct g2f {
				float4 pos : SV_POSITION;
				float3 dist : TEXCOORD1;
			};

			// vertex shader: takes object space normal as input too
			v2g vert(appdata_base v)
			{
				v2g o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}

			[maxvertexcount(3)]
			void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream) 
			{
				float2 WIN_SCALE = float2(_ScreenParams.x / 2.0, _ScreenParams.y / 2.0);

				float2 p0 = WIN_SCALE * IN[0].pos.xy / IN[0].pos.w;
				float2 p1 = WIN_SCALE * IN[1].pos.xy / IN[1].pos.w;
				float2 p2 = WIN_SCALE * IN[2].pos.xy / IN[2].pos.w;

				float2 v0 = p2 - p1;
				float2 v1 = p2 - p0;
				float2 v2 = p1 - p0;

				float area = abs(v1.x * v2.y - v1.y * v2.x);

				g2f OUT;
				OUT.pos = IN[0].pos;
				OUT.dist = float3(area / length(v0), 0, 0);
				triStream.Append(OUT);

				OUT.pos = IN[1].pos;
				OUT.dist = float3(0, area / length(v0), 0);
				triStream.Append(OUT);

				OUT.pos = IN[2].pos;
				OUT.dist = float3(0, 0, area / length(v0));
				triStream.Append(OUT);
			}

			half4 frag(g2f IN) : SV_Target
			{
				/*if (min(IN.dist.x, min(IN.dist.y, IN.dist.z)) < _Thickness)
				{
					return _WireColor;
				}
				else 
				{
					return _Color;
				}*/
				float d = min(IN.dist.x, min(IN.dist.y, IN.dist.z));
				float I = exp2(-4.0 * d * d);
				return lerp(_Color, _WireColor, I);
			}
			ENDCG
		}
	}
}
