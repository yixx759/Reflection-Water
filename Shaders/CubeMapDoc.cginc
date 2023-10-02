
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 norm : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                 float3 wpos: TEXCOORD2;
                float4 vertex : SV_POSITION;
                float3 normzo : TEXCOORD1;
                LIGHTING_COORDS(3,4)
                float3 toDir : TEXCOORD5;
            };

            sampler2D _MainTex;
            sampler2D _Mov;
            samplerCUBE _maper;
            float _Gloss, amount, amount1,_FresnelNormalStrength,_FresnelShininess,_FresnelBias,_FresnelStrength;
            float3 BBoxmin, BBoxmax;
            float3 BBoxcent;
            float2 _Mov_TexelSize;
            float4 _MainTex_ST;
            float4 _Mov_ST, _amb;

            v2f vert (appdata v)
            {
                v2f o;
                o.wpos = mul(unity_ObjectToWorld, v.vertex);

                float3 dir = o.wpos - _WorldSpaceCameraPos;
                o.toDir= dir;
                o.uv = TRANSFORM_TEX(v.uv, _Mov);
                o.normzo = UnityObjectToWorldNormal( v.norm);
                v.vertex.z += (tex2Dlod(_Mov, float4(o.uv,0,0)))/1000;
                o.vertex = UnityObjectToClipPos(v.vertex);
                 TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                

                

                float2 uv = i.uv;
                float3 ts = float3(_Mov_TexelSize.xy,0);
                float2 uv0  = uv + ts.xz;
                float2 uv1  = uv + ts.zy;
                float h = tex2D(_Mov, uv).r;
                float h0 = tex2D(_Mov, uv0).r;
                float h1 = tex2D(_Mov, uv1).r;
                // sample the texture
               float4 col = tex2D(_MainTex, i.uv);
               float4 col1 = tex2Dlod(_Mov, float4(i.uv,0,0));
                float3 p0 = float3 (ts.xz, h0 - h);
                float3 p1 = float3 (ts.zy, h1 - h);

                float4 norm = float4(normalize(cross(p0,p1)).xyz,1);


                //change to world space
                
                float4 nunorm =  lerp(norm, float4(0,0,1,1),0.5);

                float3 dir = normalize(i.toDir);
                float3 n = normalize(i.normzo);
                float3 r = reflect(dir,n);

                float3 wrldpos =  i.wpos;
                float3 intersectmaxpoints = (BBoxmax - wrldpos )/ r;
                float3 intersectminpoints = (BBoxmin - wrldpos )/ r;
                float3 largestparm = max(intersectmaxpoints, intersectminpoints);
                float smalltparm = min(min(largestparm.x, largestparm.y), largestparm.z);
                float3 intersectws = wrldpos + smalltparm * r; 
                float3 reflcor = intersectws - BBoxcent;


                float4 refl = texCUBE( _maper,reflcor);
                
                float3 fresnelNormal = n;
                fresnelNormal.xz *= _FresnelNormalStrength;
                fresnelNormal = normalize(fresnelNormal);
                float base = 1 - dot(dir, fresnelNormal);
                float exponential = pow(base, _FresnelShininess);
                float R = saturate(exponential + _FresnelBias * (1.0f - exponential));
                
                R *= _FresnelStrength;
				
                float3 fresnel = refl * R;


                
                

                
                float3 Normal = normalize(nunorm);
                float3 L =  normalize(UnityWorldSpaceLightDir(i.wpos));
                float Attenuation = LIGHT_ATTENUATION(i);
                float diffuse = saturate(dot(Normal, L));
                float3 C = normalize(_WorldSpaceCameraPos- i.wpos);
                
                float3 halfer = normalize(L+ C);
                float shine= exp2(_Gloss*6)+2;
                float speccy= saturate(dot(halfer, Normal))*(diffuse>0);
                speccy = pow(speccy, shine)*Attenuation;
            

                
                //return float4((nunorm).xyz,1);
                //return float4((norm).xyz,1);
                //return float4((refl*diffuse).xyz,1);
                //return float4((((col+refl))*(diffuse)).xyz,1);
                
                return float4(((((col*amount)+(fresnel*amount1))*(diffuse))+speccy+_amb).xyz,1);
            }