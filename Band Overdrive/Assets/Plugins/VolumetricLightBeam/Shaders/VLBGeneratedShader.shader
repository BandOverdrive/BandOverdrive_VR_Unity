Shader "Hidden/VLB_BuiltIn_MultiPass"
{
    Properties
    {
        _ConeSlopeCosSin("Cone Slope Cos Sin", Vector) = (0,0,0,0)
        _ConeRadius("Cone Radius", Vector) = (0,0,0,0)
        _ConeApexOffsetZ("Cone Apex Offset Z", Float) = 0

        _ColorFlat("Color", Color) = (1,1,1,1)

        _AlphaInside("Alpha Inside", Range(0,1)) = 1
        _AlphaOutside("Alpha Outside", Range(0,1)) = 1

        _DistanceFallOff("Distance Fall Off", Vector) = (0,1,1,0)

        _DistanceCamClipping("Camera Clipping Distance", Float) = 0.5
        _FadeOutFactor("FadeOutFactor", Float) = 1

        _AttenuationLerpLinearQuad("Lerp between attenuation linear and quad", Float) = 0.5
        _DepthBlendDistance("Depth Blend Distance", Float) = 2

        _FresnelPow("Fresnel Pow", Range(0,15)) = 1

        _GlareFrontal("Glare Frontal", Range(0,1)) = 0.5
        _GlareBehind("Glare from Behind", Range(0,1)) = 0.5
        _DrawCap("Draw Cap", Float) = 1

        _NoiseVelocityAndScale("Noise Velocity And Scale", Vector) = (0,0,0,0)
        _NoiseParam("Noise Param", Vector) = (0,0,0,0)

        _CameraParams("Camera Params", Vector) = (0,0,0,0)

        _BlendSrcFactor("BlendSrcFactor", Int) = 1 // One
        _BlendDstFactor("BlendDstFactor", Int) = 1 // One

        _DynamicOcclusionClippingPlaneWS("Dynamic Occlusion Clipping Plane WS", Vector) = (0,0,0,0)
        _DynamicOcclusionClippingPlaneProps("Dynamic Occlusion Clipping Plane Props", Float) = 0.25

        _DynamicOcclusionDepthTexture("DynamicOcclusionDepthTexture", 2D) = "white" {}
        _DynamicOcclusionDepthProps("DynamicOcclusionDepthProps", Float) = 0.25

        _LocalForwardDirection("LocalForwardDirection", Vector) = (0,0,1)
        _TiltVector("TiltVector", Vector) = (0,0,0,0)
		_AdditionalClippingPlaneWS("AdditionalClippingPlaneWS", Vector) = (0,0,0,0)
    }

    Category
    {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
            "DisableBatching" = "True" // disable dynamic batching which doesn't work neither with multiple materials nor material property blocks
        }

        Blend[_BlendSrcFactor][_BlendDstFactor]
        ZWrite Off

        SubShader
        {
            Pass
            {
                Cull Front

                CGPROGRAM
                #if !defined(SHADER_API_METAL) // Removed shader model spec for Metal support https://github.com/keijiro/Cloner/commit/1120493ca2df265d450de3ec1b38a1d388468964
                #pragma target 3.0
                #endif
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                
                #pragma multi_compile_local __ VLB_ALPHA_AS_BLACK
                #pragma multi_compile_local __ VLB_DEPTH_BLEND


                #include "UnityCG.cginc"

                #include "ShaderDefines.cginc"
                #include "ShaderProperties.cginc"
                #include "ShaderSpecificBuiltin.cginc"
                #include "VolumetricLightBeamShared.cginc"

                v2f vert(vlb_appdata v)         { return vertShared(v, 0); }
                half4 frag(v2f i) : SV_Target   { return fragShared(i, 0); }

                ENDCG
            }
            Pass
            {
                Cull Back

                CGPROGRAM
                #if !defined(SHADER_API_METAL) // Removed shader model spec for Metal support https://github.com/keijiro/Cloner/commit/1120493ca2df265d450de3ec1b38a1d388468964
                #pragma target 3.0
                #endif
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fog
                
                #pragma multi_compile_local __ VLB_ALPHA_AS_BLACK
                #pragma multi_compile_local __ VLB_DEPTH_BLEND


                #include "UnityCG.cginc"

                #include "ShaderDefines.cginc"
                #include "ShaderProperties.cginc"
                #include "ShaderSpecificBuiltin.cginc"
                #include "VolumetricLightBeamShared.cginc"

                v2f vert(vlb_appdata v)         { return vertShared(v, 1); }
                half4 frag(v2f i) : SV_Target   { return fragShared(i, 1); }

                ENDCG
            }

        }
    }
}
