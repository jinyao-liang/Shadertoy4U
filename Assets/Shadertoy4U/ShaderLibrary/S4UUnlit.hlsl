#ifndef SHADERTOY_4_U_UNLIT_INCLUDE
#define SHADERTOY_4_U_UNLIT_INCLUDE

S4UVaryings S4UUnlitVertex(S4UAttributes IN)
{
    S4UVaryings OUT;
    OUT.positionHCS = mul(unity_MatrixVP, mul(unity_ObjectToWorld, float4(IN.positionOS.xyz, 1.0)));
    OUT.uv = IN.uv;
    return OUT;
}

half4 S4UUnlitFragment(S4UVaryings IN) : SV_Target
{
    float4 fragColor;
    mainImage(fragColor, IN.uv * _ScreenParams.xy);
    return fragColor;
}

#endif // #ifndef SHADERTOY_4_U_UNLIT_INCLUDE