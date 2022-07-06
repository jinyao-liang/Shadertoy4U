using System;
using UnityEngine;

namespace Shadertoy4U.Glsl
{

public class Parser
{
    Lexer mLexer;

    public ParseTree tree { get; set; }

    public Token token { get => mLexer.token; }

    public Parser(string source)
    {
        mLexer = new Glsl.Lexer(new StringBuffer(source));
        mLexer.NextToken();
    }

    public bool Run()
    {
        Rule.depth = 1024;
        Rule.parser = this;
        var root = new function_prototype();
        return root.Process();
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