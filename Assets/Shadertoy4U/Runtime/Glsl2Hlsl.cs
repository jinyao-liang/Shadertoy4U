using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shadertoy4U
{

public static class Glsl2Hlsl
{
    public static string Convert(string glsl)
    {
        Parser parser = new Parser();
        var hlsl = parser.Parse(glsl);
        return hlsl;
    }
}

} // namespace Shadertoy4U
