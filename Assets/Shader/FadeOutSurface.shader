Shader "Custom/FadeOutSurface" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
        _FadePos("Fade Position" , float ) = 0.5
        _FadeRange("Fade Range" , float ) = 0.1
	}
	SubShader {
	    Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
	    Blend SrcAlpha OneMinusSrcAlpha
	    Cull Off
	    LOD 200

//		Tags { "RenderType"="Opaque" }
//		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _FadePos;
		float _FadeRange;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = _Color;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			float height = IN.worldPos.y;

			if ( height < _FadePos) 
			{
				o.Alpha = 0;
			}else if ( height < _FadePos + _FadeRange)
			{
				float a = 1 - (  _FadePos + _FadeRange - height ) / _FadeRange;
				o.Alpha = a;
			}else
			{
				o.Alpha = 1;
			}
		}
		ENDCG
	}
	FallBack "Diffuse"
}
