// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.6084906,fgcg:0.9305385,fgcb:1,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:34071,y:32651,varname:node_3138,prsc:2|emission-8067-OUT,olwid-2725-OUT,olcol-8001-RGB;n:type:ShaderForge.SFN_NormalVector,id:9883,x:32369,y:32543,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:1563,x:32369,y:32705,varname:node_1563,prsc:2;n:type:ShaderForge.SFN_Dot,id:4198,x:32560,y:32529,varname:node_4198,prsc:2,dt:0|A-9883-OUT,B-1563-OUT;n:type:ShaderForge.SFN_Multiply,id:145,x:32752,y:32599,varname:node_145,prsc:2|A-4198-OUT,B-9940-OUT;n:type:ShaderForge.SFN_Vector1,id:9940,x:32530,y:32781,varname:node_9940,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Add,id:6804,x:32986,y:32564,varname:node_6804,prsc:2|A-145-OUT,B-4340-OUT;n:type:ShaderForge.SFN_Vector1,id:4340,x:32713,y:32754,varname:node_4340,prsc:2,v1:0.5;n:type:ShaderForge.SFN_ValueProperty,id:2725,x:33639,y:33065,ptovrint:False,ptlb:Outlier Width,ptin:_OutlierWidth,varname:node_2725,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Color,id:8001,x:33639,y:33151,ptovrint:False,ptlb:Outlier Color,ptin:_OutlierColor,varname:node_8001,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Fresnel,id:9339,x:33096,y:32854,varname:node_9339,prsc:2|EXP-5705-OUT;n:type:ShaderForge.SFN_Vector1,id:5705,x:32881,y:32934,varname:node_5705,prsc:2,v1:3;n:type:ShaderForge.SFN_Color,id:3912,x:32881,y:33043,ptovrint:False,ptlb:FreColor,ptin:_FreColor,varname:node_3912,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Blend,id:8067,x:33799,y:32846,varname:node_8067,prsc:2,blmd:6,clmp:True|SRC-9637-OUT,DST-2272-OUT;n:type:ShaderForge.SFN_Multiply,id:2272,x:33252,y:33004,varname:node_2272,prsc:2|A-9339-OUT,B-3912-RGB;n:type:ShaderForge.SFN_Color,id:9598,x:33162,y:32690,ptovrint:False,ptlb:NoteColor,ptin:_NoteColor,varname:node_9598,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3679245,c2:0.03888837,c3:0.0187433,c4:1;n:type:ShaderForge.SFN_ScreenPos,id:3946,x:32685,y:32187,varname:node_3946,prsc:2,sctp:1;n:type:ShaderForge.SFN_Depth,id:8832,x:32688,y:32346,varname:node_8832,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5012,x:32966,y:32307,varname:node_5012,prsc:2|A-3946-UVOUT,B-8832-OUT;n:type:ShaderForge.SFN_Tex2d,id:3092,x:33144,y:32307,ptovrint:False,ptlb:Tex,ptin:_Tex,varname:node_3092,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b3288fac1dbeed3438bbad17cb17ceda,ntxv:0,isnm:False|UVIN-5012-OUT;n:type:ShaderForge.SFN_Step,id:2003,x:33351,y:32401,varname:node_2003,prsc:2|A-6804-OUT,B-3092-RGB;n:type:ShaderForge.SFN_Lerp,id:5512,x:33691,y:32289,varname:node_5512,prsc:2|A-8656-RGB,B-569-RGB,T-2003-OUT;n:type:ShaderForge.SFN_Color,id:569,x:33301,y:32138,ptovrint:False,ptlb:light,ptin:_light,varname:node_569,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:8656,x:33522,y:32048,ptovrint:False,ptlb:dark,ptin:_dark,varname:node_8656,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:2512,x:33475,y:32611,varname:node_2512,prsc:2|A-6804-OUT,B-9598-RGB;n:type:ShaderForge.SFN_Add,id:9637,x:33763,y:32472,varname:node_9637,prsc:2|A-5512-OUT,B-2512-OUT;proporder:2725-8001-3912-9598-3092-569-8656;pass:END;sub:END;*/

Shader "Notes/shader_test01" {
    Properties {
        _OutlierWidth ("Outlier Width", Float ) = 0
        _OutlierColor ("Outlier Color", Color) = (0.5,0.5,0.5,1)
        _FreColor ("FreColor", Color) = (1,1,1,1)
        _NoteColor ("NoteColor", Color) = (0.3679245,0.03888837,0.0187433,1)
        _Tex ("Tex", 2D) = "white" {}
        _light ("light", Color) = (1,1,1,1)
        _dark ("dark", Color) = (1,0,0,1)
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
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma target 3.0
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float, _OutlierWidth)
                UNITY_DEFINE_INSTANCED_PROP( float4, _OutlierColor)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                float _OutlierWidth_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OutlierWidth );
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*_OutlierWidth_var,1) );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                float4 _OutlierColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OutlierColor );
                return fixed4(_OutlierColor_var.rgb,0);
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
            uniform sampler2D _Tex; uniform float4 _Tex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _FreColor)
                UNITY_DEFINE_INSTANCED_PROP( float4, _NoteColor)
                UNITY_DEFINE_INSTANCED_PROP( float4, _light)
                UNITY_DEFINE_INSTANCED_PROP( float4, _dark)
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
                float4 projPos : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                float4 _dark_var = UNITY_ACCESS_INSTANCED_PROP( Props, _dark );
                float4 _light_var = UNITY_ACCESS_INSTANCED_PROP( Props, _light );
                float node_6804 = ((dot(i.normalDir,lightDirection)*0.5)+0.5);
                float2 node_5012 = (float2((sceneUVs.x * 2 - 1)*(_ScreenParams.r/_ScreenParams.g), sceneUVs.y * 2 - 1).rg*partZ);
                float4 _Tex_var = tex2D(_Tex,TRANSFORM_TEX(node_5012, _Tex));
                float4 _NoteColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _NoteColor );
                float4 _FreColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _FreColor );
                float3 emissive = saturate((1.0-(1.0-(lerp(_dark_var.rgb,_light_var.rgb,step(node_6804,_Tex_var.rgb))+(node_6804*_NoteColor_var.rgb)))*(1.0-(pow(1.0-max(0,dot(normalDirection, viewDirection)),3.0)*_FreColor_var.rgb))));
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
            uniform sampler2D _Tex; uniform float4 _Tex_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _FreColor)
                UNITY_DEFINE_INSTANCED_PROP( float4, _NoteColor)
                UNITY_DEFINE_INSTANCED_PROP( float4, _light)
                UNITY_DEFINE_INSTANCED_PROP( float4, _dark)
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
                float4 projPos : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
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
