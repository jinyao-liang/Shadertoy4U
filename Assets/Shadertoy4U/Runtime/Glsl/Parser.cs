using System;
using UnityEngine;

namespace Shadertoy4U
{


public class Parser
{
    public class State
    {
        public Lexer.State lexState;
    }

    Lexer mLexer;

    string mLastError;

    public Token token { get => mLexer.token; }

    public Parser(Lexer lexer)
    {
        mLexer = lexer;
    }

    public void Parse()
    {
        GrammarNode.depth = 1024;
        var root = new function_prototype();
        root.Execute(this);
    }

    public bool Consume(Token.Type t)
    {
        if (mLexer.token.type != t)
        {
            LogError($"expect {t}, got {mLexer.token.ToString()}");
            Debug.LogWarning($"expect {t}, got {mLexer.token.ToString()}");
            return false;
        }
        mLexer.NextToken();
        return true;
    }

    public State Backup()
    {
        return new State {
            lexState = mLexer.Backup(),
        };
    }

    public void Restore(State s)
    {
        mLexer.Restore(s.lexState);
    }

    public bool IsSameState(State s)
    {
        if (s == null)
            return false;
        return mLexer.IsSameState(s.lexState);
    }

    public void LogError(string str)
    {
        mLastError = str;
    }

}

} // namespace Shadertoy4U
