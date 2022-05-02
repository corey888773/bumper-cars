Shader "Unlit/BackgroundShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float3 _Object_Scale;/* = float3(length(float3(UNITY_MATRIX_M[0].x, UNITY_MATRIX_M[1].x, UNITY_MATRIX_M[2].x)),
                             length(float3(UNITY_MATRIX_M[0].y, UNITY_MATRIX_M[1].y, UNITY_MATRIX_M[2].y)),
                             length(float3(UNITY_MATRIX_M[0].z, UNITY_MATRIX_M[1].z, UNITY_MATRIX_M[2].z)));*/

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = mul(unity_ObjectToWorld, v.uv);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            float f(float3 pol)
            {
                return (sin(pol.x + 1.2 * pol.y + 3.0 * pol.z + 4.0 * sin(2.0 * pol.z - 3.0 * pol.x + 3.0 * pol.y) + 3.7 * sin(1.0 * pol.z + 3.0 * pol.x - 3.0 * pol.y)) * 0.9 + sin(-4.0 * pol.z) + cos(9.0 * pol.x - 2.5 * pol.z) * sin(19.0 * pol.x + 0.1221 + 0.65 * pol.y + 2.6 * pol.z)) * 0.5 + 0.5;
            }

            float gwiazda(float2 pos, float fi, float R, float r, float epsR, float epsr)
            {
                float odl = length(pos);
                
                float3 skal = float3(normalize(pos), fi);
                
                float wartosc = exp(-f(skal) * 2.0);
                
                return max(smoothstep(r, r - epsr, odl) * wartosc, smoothstep(R + epsR, R, odl));
            }

            float pole_fragment(float2 pos, float alfa, float beta, float R, float r, float w)
            {
                float odl = length(pos), cos_beta = cos(beta);
                
                float2 jedn_pos = pos / odl, na_okr = float2(cos(alfa), sin(alfa));
                
                float wart, wart1 = odl - (R + r) * 0.5, wart2 = dot(jedn_pos, na_okr) - cos_beta;
                wart1 = w - wart1 * wart1;
                
                wart = min(smoothstep(0.0, w, wart1), smoothstep(0.0, cos_beta * 0.1, wart2));
                
                return wart;
            }

            float3 fragment_odb(float2 pos, float fi, float R, int n)
            {
                float3 color;
                float znak = 1.0;
                
                for(int i = 0; i < 100; i++)
                {    
                    if(i >= n)
                        break;
                    
                    float nowy_R = R * exp(-float(i) * 0.1);
                    
                    float nowy_kolor = pole_fragment(pos, znak * fi * nowy_R * 3.0, 1.0, nowy_R, nowy_R - 0.001, 0.00001);
                    
                    color = max(color, float3(nowy_kolor * R / nowy_R, nowy_kolor, nowy_kolor));
                    
                    znak *= -1.0;
                }
                
                return color;
            }

            float4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                float wsp = unity_ObjectToWorld[0].x / unity_ObjectToWorld[1].y;

                float2 pos = i.uv;

                pos.x *= wsp;

                pos.y -= 0.5;
                pos.x -= 0.5 * wsp;

                float czas = _Time.y * 1.0;

                float gwiazda_kol = gwiazda(pos, czas, 0.05, 0.5, 0.01, 0.5);
                
                col = min(col, float4(max(float3(gwiazda_kol, gwiazda_kol, gwiazda_kol), fragment_odb(pos, czas, 0.4, 10)), 1.0));

                return col;
            }
            ENDCG
        }
    }
}
