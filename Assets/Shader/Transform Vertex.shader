Shader "Example/Transform Vertex" {
    Properties {
        _BendScale("Bend Scale", Range(0.0, 1.0)) = 0.2
        _MainTex("Main Texture", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }
        CGPROGRAM
        #pragma surface surf Lambert alpha vertex:vert
        #define PI 3.14159
        struct Input {
            float2 uv_MainTex;
            float4 color : Color;
        };
        sampler2D _MainTex;
        float _BendScale;
        void vert (inout appdata_full v) {
            v.vertex.x += sin(_Time.x*10);

        }
        void surf (Input IN, inout SurfaceOutput o) {
            o.Albedo = fixed4(1, 1, 1, 1);
            o.Alpha = 1;
        }
        ENDCG
    }
    Fallback "Diffuse"
}