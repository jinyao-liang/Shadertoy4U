using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shadertoy4U
{

public static class Glsl2Hlsl
{
    public static string Convert(string glsl)
    {
        var buf = new StringBuffer(glsl);
        var lexer = new Lexer(buf);
        lexer.NextToken();

        // while(true)
        // {
        //     lexer.NextToken();
        //     if (!lexer.token.valid)
        //         break;
        //     Debug.Log($"{lexer.token.type}: {lexer.token.str}");
        // }
        // Debug.Log($"done.");

        Parser parser = new Parser(lexer);
        parser.Parse();
        Debug.Log($"done.");

        return glsl;
    }
}

} // namespace Shadertoy4U
