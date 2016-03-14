Shader "LUCASSHADER/LucasTestBW"
{
	Properties
	{
		_MinIntensity("MinIntensity", Range(0, 1)) = 0.2
		_MaxIntensity("MaxIntensity", Range(0, 1)) = 0.6
		_Step("Step", int) = 0
	}
	// no Properties block this time!
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// include file that contains UnityObjectToWorldNormal helper function
			#include "UnityCG.cginc"

			float _MinIntensity;
			float _MaxIntensity;
			int _Step;

			struct v2f {
				// we'll output world space normal as one of regular ("texcoord") interpolators
				half3 worldNormal : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			// vertex shader: takes object space normal as input too
			v2f vert(float4 vertex : POSITION, float3 normal : NORMAL)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, vertex);
				// UnityCG.cginc file contains function to transform
				// normal from object to world space, use that
				o.worldNormal = UnityObjectToWorldNormal(normal);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 c = 0;
			// normal is a 3D vector with xyz components; in -1..1
			// range. To display it as color, bring the range into 0..1
			// and put into red, green, blue components
			float3 normals = i.worldNormal*0.5 + 0.5;
			float magnitude = (normals.x + normals.y + normals.z) / 3.0;
			if (_Step) {
				float breakpoint = (_MaxIntensity + _MinIntensity) / 2.0;
				if (magnitude < breakpoint) magnitude = _MinIntensity;
				else magnitude = _MaxIntensity;
			}
			else
			{
				magnitude = ((_MaxIntensity - _MinIntensity) * magnitude) + _MinIntensity;
			}
			c.rgb = magnitude;
			return c;
			}
			ENDCG
		}
	}
}
