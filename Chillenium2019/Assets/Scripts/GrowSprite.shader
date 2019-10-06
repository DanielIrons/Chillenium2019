Shader "Unlit/GrowSprite"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		 [PerRendererData]_on("on",Range(0,1)) = 0.0
	}
		SubShader
		{

		Tags { "RenderType" = "Transparent" "Queue" = "Transparent+100"}
			Cull Off
			Lighting Off
			ZWrite Off
			ZTest Always
			Fog { Mode Off }
			Blend One OneMinusSrcAlpha

			Pass{

				CGPROGRAM

				#pragma vertex vertF
				#pragma fragment fragF
				#include "UnityCG.cginc"

				sampler2D _MainTex;

				struct v2f {
					float4 pos : SV_POSITION;
					half2 uv : TEXCOORD0;
					};

				v2f vertF(appdata_base v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.texcoord;

					return o;
						}

				fixed4 _Color;
				float4 _MainTex_TexelSize;
				float _on;

				fixed4 fragF(v2f i) : COLOR {
					half4 c = tex2D(_MainTex, i.uv);
					c.rgb *= c.a;
					half4 outC = _Color;
					outC.a *= ceil(c.a);
					outC.rgb *= outC.a;

					fixed up = tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y)).a;
					fixed down = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y)).a;
					fixed left = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0)).a;
					fixed right = tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x, 0)).a;


					if (_on <= 0.1) {
						return c;
					}
					else return lerp(outC, c, ceil(up * down * left * right));
				}

				ENDCG
			}
    }

	FallBack Off
}
