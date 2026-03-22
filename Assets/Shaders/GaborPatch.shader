Shader "Unlit/GaborPatch"
{
    Properties
    {
        _Frequency ("Frequency", Float) = 20.0
        _Sigma ("Sigma (Spread)", Float) = 0.2
        _Contrast ("Contrast", Range(0, 1)) = 1.0
        _Angle ("Angle", Float) = 0.0
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

            float _Frequency, _Sigma, _Contrast, _Angle;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 pos = i.uv - 0.5;
                float rad = radians(_Angle);
                float x_rot = pos.x * cos(rad) - pos.y * sin(rad);
                
                float grating = sin(2.0 * 3.14159 * _Frequency * x_rot);
                float envelope = exp(-dot(pos, pos) / (2.0 * _Sigma * _Sigma));
                float gray = 0.5 + 0.5 * grating * envelope * _Contrast;

                return fixed4(gray, gray, gray, envelope);
            }
            ENDCG
        }
    }
}
