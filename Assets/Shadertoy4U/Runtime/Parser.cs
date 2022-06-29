using System;
using UnityEngine;

namespace Shadertoy4U
{


public class Parser
{
    public class State
    {
    }

    Lexer mLexer;

    string mLastError;

    public Parser(Lexer lexer)
    {
        mLexer = lexer;
    }

    public void Parse()
    {
        var branch = new function_prototype();
        if (!branch.Execute(this))
        {
            Debug.LogError(mLastError);
        }
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
        return null;
    }

    public void Restore(State s)
    {
        //Debug.LogWarning("Restore");
    }

    public void LogError(string str)
    {
        mLastError = str;
    }

}

} // namespace Shadertoy4U
