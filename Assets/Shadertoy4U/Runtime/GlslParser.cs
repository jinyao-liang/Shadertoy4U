using System;
using UnityEngine;

namespace Shadertoy4U.Glsl
{

public class Parser
{
    Lexer mLexer;

    public Token token { get => mLexer.token; }

    public Parser(Lexer lexer)
    {
        mLexer = lexer;
    }

    public bool Run()
    {
        if (mLexer.token == null)
            mLexer.NextToken();

        Rule.depth = 1024;
        var root = new function_prototype();
        return root.Process(this);
    }

    public Lexer.State Save()
    {
        return mLexer.SaveState();
    }

    public void Restore(Lexer.State s)
    {
        mLexer.RestoreState(s);
    }

    public bool Consume(Token.Type t)
    {
        if (mLexer.token.type != t)
        {
            Debug.LogWarning($"expect {t}, got {mLexer.token.ToString()}");
            return false;
        }
        mLexer.NextToken();
        return true;
    }
}

} // namespace Shadertoy4U.Glsl