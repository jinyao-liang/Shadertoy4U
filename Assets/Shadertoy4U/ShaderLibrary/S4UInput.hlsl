#ifndef SHADERTOY_4_U_INPUT_INCLUDE
#define SHADERTOY_4_U_INPUT_INCLUDE

float4x4 unity_ObjectToWorld;
float4x4 unity_MatrixVP;
float4 _Time;
float4 _ScreenParams;

struct S4UAttributes
{
    float4 positionOS   : POSITION;
    float2 uv : TEXCOORD0;
};

struct S4UVaryings
{
    float4 positionHCS  : SV_POSITION;
    float2 uv : TEXCOORD0;
};

#define iResolution _ScreenParams
#define iTime _Time.z
#define vec2 float2
#define vec3 float3
#define vec4 float4
#define mat2 float2x2
#define mix lerp

#endif // #ifndef SHADERTOY_4_U_INPUT_INCLUDE
