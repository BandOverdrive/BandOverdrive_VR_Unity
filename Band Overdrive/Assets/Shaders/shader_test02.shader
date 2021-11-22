// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33583,y:32272,varname:node_3138,prsc:2|emission-4284-OUT,olwid-9794-OUT,olcol-4458-OUT;n:type:ShaderForge.SFN_NormalVector,id:9883,x:32254,y:32697,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:1563,x:32254,y:32524,varname:node_1563,prsc:2;n:type:ShaderForge.SFN_Dot,id:4198,x:32586,y:32554,varname:node_4198,prsc:2,dt:0|A-6933-OUT,B-9883-OUT;n:type:ShaderForge.SFN_Vector1,id:9794,x:33191,y:32578,varname:node_9794,prsc:2,v1:0.00025;n:type:ShaderForge.SFN_Vector3,id:4458,x:33205,y:32657,varname:node_4458,prsc:2,v1:1,v2:1,v3:1;n:type:ShaderForge.SFN_Add,id:4145,x:32468,y:32354,varname:node_4145,prsc:2|A-6521-XYZ,B-1563-OUT;n:type:ShaderForge.SFN_Vector4Property,id:6521,x:32254,y:32354,ptovrint:False,ptlb:highLightoffset01,ptin:_highLightoffset01,varname:node_6521,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_Normalize,id:6933,x:32673,y:32331,varname:node_6933,prsc:2|IN-4145-OUT;n:type:ShaderForge.SFN_If,id:4284,x:33250,y:32331,varname:node_4284,prsc:2|A-4198-OUT,B-928-OUT,GT-5414-OUT,EQ-5414-OUT,LT-7768-OUT;n:type:ShaderForge.SFN_Slider,id:928,x:32831,y:32175,ptovrint:False,ptlb:highLightoffest02,ptin:_highLightoffest02,varname:node_928,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.6,cur:0.6,max:1;n:type:ShaderForge.SFN_Vector1,id:5414,x:33004,y:32331,varname:node_5414,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:7768,x:33004,y:32457,varname:node_7768,prsc:2,v1:0;proporder:6521-928;pass:END;sub:END;*/

Shader "Notes/shader_test02" {
    Properties {
        _highLightoffset01 ("highLightoffset01", Vector) = (0,0,0,0)
        _highLightoffest02 ("highLightoffest02", Range(0.6, 1)) = 0.6
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*0.00025,1) );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                return fixed4(float3(1,1,1),0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _highLightoffset01)
                UNITY_DEFINE_INSTANCED_PROP( float, _highLightoffest02)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                float4 _highLightoffset01_var = UNITY_ACCESS_INSTANCED_PROP( Props, _highLightoffset01 );
                float _highLightoffest02_var = UNITY_ACCESS_INSTANCED_PROP( Props, _highLightoffest02 );
                float node_4284_if_leA = step(dot(normalize((_highLightoffset01_var.rgb+lightDirection)),i.normalDir),_highLightoffest02_var);
                float node_4284_if_leB = step(_highLightoffest02_var,dot(normalize((_highLightoffset01_var.rgb+lightDirection)),i.normalDir));
                float node_5414 = 1.0;
                float node_4284 = lerp((node_4284_if_leA*0.0)+(node_4284_if_leB*node_5414),node_5414,node_4284_if_leA*node_4284_if_leB);
                float3 emissive = float3(node_4284,node_4284,node_4284);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _highLightoffset01)
                UNITY_DEFINE_INSTANCED_PROP( float, _highLightoffest02)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float3 finalColor = 0;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
