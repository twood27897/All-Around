Shader "Hidden/palette"
{
	// Changeable variables
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		
		// Texture to store the different palette variations
		_PaletteTex("PaletteTexture", 2D) = "white" {}

		// Float to choose palette with
		_PalettePicker("PalettePicker", float) = 1.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _PaletteTex;
			uniform float _PalettePicker;

			fixed4 frag (v2f i) : SV_Target
			{
				// Sample the main texture for a colour value
				fixed x = tex2D(_MainTex, i.uv).r;

				// Use sampled colour value as the x component and the _PalettePicker variable as the y component to sample from the palette texture 
				fixed4 col = tex2D(_PaletteTex, float2(x, _PalettePicker));

				return col;
			}
			ENDCG
		}
	}
}
