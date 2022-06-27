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
        var firstChar = buf.NextChar();
        var lexer = new Lexer(buf, firstChar);
        lexer.NextToken();

        Parser parser = new Parser(lexer);
        parser.Parse();

        return glsl;
    }
}

} // namespace Shadertoy4U
