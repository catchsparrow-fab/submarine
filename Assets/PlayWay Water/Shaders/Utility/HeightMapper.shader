Shader "PlayWay Water/Utility/HeightMapper"
{
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			BlendOp Max
			ZTest Always
			ZWrite Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex	: POSITION;
			};

			struct v2f
			{
				float4 vertex	: SV_POSITION;
				float worldPosY	: TEXCOORD0;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.worldPosY = mul(_Object2World, v.vertex).y;
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				return i.worldPosY;
			}
			ENDCG
		}
	}
}
