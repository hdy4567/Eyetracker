Shader "Unlit/DichopticContrastShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LeftContrast ("Left Eye Contrast", Range(0, 1)) = 1.0
        _RightContrast ("Right Eye Contrast", Range(0, 1)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; UNITY_VERTEX_INPUT_INSTANCE_ID };
            struct v2f { float2 uv : TEXCOORD0; float4 vertex : SV_POSITION; UNITY_VERTEX_OUTPUT_STEREO };

            sampler2D _MainTex;
            float _LeftContrast, _RightContrast;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
                fixed4 col = tex2D(_MainTex, i.uv);
                float contrast = (unity_StereoEyeIndex == 0) ? _LeftContrast : _RightContrast;
                col.rgb = lerp(0.5, col.rgb, contrast);
                return col;
            }
            ENDCG
        }
    }
}
