using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Shadertoy4U;

public class LexerTest
{
    [Test]
    public void TestReserved()
    {
        var src = "     void <<= >> > vec3    ";
        var lexer = new Lexer(new StringBuffer(src));
        
        lexer.NextToken();
        Assert.True(lexer.token.type == Token.Type.VOID);

        lexer.NextToken();
        Assert.True(lexer.token.type == Token.Type.LEFT_ASSIGN);

        lexer.NextToken();
        Assert.True(lexer.token.type == Token.Type.RIGHT_OP);

        lexer.NextToken();
        Assert.True(lexer.token.type == Token.Type.RIGHT_ANGLE);

        lexer.NextToken();
        Assert.True(lexer.token.type == Token.Type.VEC3);
    }
}
