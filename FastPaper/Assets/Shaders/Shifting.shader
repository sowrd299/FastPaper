Shader "Cards/ShiftBackground"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_background("background",2D)="red"{}
		_noise("noise",2D)="White"{}
		
		
		_Mitigation ("Distortion mitigation", Range(1, 30)) = 1
        _SpeedX("Speed along X", Range(0, 5)) = 1
        _SpeedY("Speed along Y", Range(0, 5)) = 1
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			
			#include "UnityCG.cginc"
			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};
			sampler2D _background,_noise;
			float4 _background_ST,_noise_ST;
			float _SpeedX,_SpeedY,_Mitigation;

			v2f vert(appdata_base v)	
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _background);
				return o;
			}
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				half2 uv = i.uv;
                half noiseVal = tex2D(_noise, uv).r;
				uv.x += (_Time.y* _SpeedX) / _Mitigation;
				uv.y = uv.y + noiseVal * sin(_Time.y* _SpeedY) / _Mitigation;
				fixed4 col = tex2D(_background,uv);
				// apply fog
				return col;
			}
			ENDCG

		}


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

			sampler2D _MainTex;
			float4 _MainTex_ST;
		
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv.y+=.1;
				o.uv.x+=.05;
				return o;
			}
			
			
			float GetAlpha(v2f i){
				return tex2D(_MainTex,i.uv.xy).a;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float alpha =GetAlpha(i);
				clip(alpha-0.5);
				
				fixed4 col =fixed4(0,0,0,0);
				// apply fog
				return col;
			}


			ENDCG
		}


		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			
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

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			
			float GetAlpha(v2f i){
				return tex2D(_MainTex,i.uv.xy).a;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float alpha =GetAlpha(i);
				clip(alpha-0.5);
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				return col;
			}
			ENDCG
		}
	}
}
