Shader "Unlit/GaborPatch"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Frequency ("Frequency", Float) = 15.0
        _Sigma ("Sigma (Spread)", Float) = 0.15
        _Contrast ("Contrast", Range(0, 1)) = 1.0
        _Angle ("Orientation Angle", Float) = 0.0
        _Phase ("Phase Shift", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

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

            float _Frequency;
            float _Sigma;
            float _Contrast;
            float _Angle;
            float _Phase;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // UV를 -0.5 ~ 0.5 범위로 정규화
                float2 pos = i.uv - 0.5;

                // 회전 행렬 적용
                float rad = radians(_Angle);
                float x_rot = pos.x * cos(rad) - pos.y * sin(rad);
                
                // 1. Sinusoidal Grating (정현파 격자)
                float grating = sin(2.0 * 3.14159 * _Frequency * x_rot + _Phase);

                // 2. Gaussian Envelope (가우시안 포락선)
                float dist_sq = dot(pos, pos);
                float envelope = exp(-dist_sq / (2.0 * _Sigma * _Sigma));

                // 3. 결합 및 대비 조절
                float gray = 0.5 + 0.5 * grating * envelope * _Contrast;

                return fixed4(gray, gray, gray, envelope); // 투명도는 가우시안 포락선 기준
            }
            ENDCG
        }
    }
}
