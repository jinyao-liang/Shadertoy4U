using System.Text;
using UnityEngine;

namespace Shadertoy4U
{

public class Lexer
{
    public class State
    {
        public Token t;
        public char c;
        public int p;
    }

    StringBuffer mInput;

    StringBuilder stringBuilder = new StringBuilder();

    int lineNumber;
    int lastLine;
    char currentChar;
    Token currentToken;
    Token lookAheadToken;

    public Token token { get => currentToken; }

    public Lexer(StringBuffer buf)
    {
        mInput = buf;
        Next();
    }

    public State Backup()
    {
        return new State
        {
            t = currentToken,
            c = currentChar,
            p = mInput.pos,
        };
    }

    public void Restore(State s)
    {
        currentToken = s.t;
        currentChar = s.c;
        mInput.pos = s.p;
    }

    public void NextToken()
    {
        lastLine = lineNumber;
        if (lookAheadToken != null)
        {
            currentToken = lookAheadToken;
            lookAheadToken = null;
        }
        else
        {
            currentToken = NewToken();
        }
    }

    Token NewToken()
    {
        Reset();

        var token = new Token();

        int count = 0;
        while(true)
        {
            if (count++ > 100)
            {
                token.type = Token.Type.EOS;
                return token;
            }
            
            switch(currentChar)
            {
            case '\n': case '\r':
                /* line breaks */
                IncreaseLine();
                break;
            case ' ': case '\f': case '\t': case '\v':
                /* spaces */
                Next();
                break;
            case '/':
                /* comment or operator symbols */
                SaveAndNext();
                if (currentChar == '=')
                {
                    SaveAndNext();
                    token.str = stringBuilder.ToString();
                    token.type = Token.Type.DIV_ASSIGN;
                    return token;
                }
                else if (currentChar == '/')
                {
                    SaveAndNext();
                    do
                    {
                        SaveAndNext();
                    } while (!StringUtil.IsNewLine(currentChar) && currentChar != StringBuffer.eos);
                    token.str = stringBuilder.ToString();
                    token.type = Token.Type.COMMENT;
                    return token;
                }
                else if (currentChar == '*')
                {
                    token.type = Token.Type.EOS;
                    return token;
                }
                else
                {
                    token.str = stringBuilder.ToString();
                    token.type = Token.Type.SLASH;
                    return token;
                }
            case '0': case '1': case '2': case '3': case '4':
            case '5': case '6': case '7': case '8': case '9':
                ReadNumber(token);
                return token;
            case StringBuffer.eos:
                token.type = Token.Type.EOS;
                return token;
            default:
                if (StringUtil.IsAlpha(currentChar))
                {
                    /* identifier or reserved word? */
                    ReadName(token);
                    return token;
                }
                else
                {
                    /* operator tokens ('+', '*', '%', '{', '}', ...) */
                    ReadOperator(token);
                    return token;
                }
            }
        }
    }

    void IncreaseLine()
    {
        var old = currentChar;
        Next();
        if (StringUtil.IsNewLine(currentChar) && currentChar != old)
            Next();
    }

    void SaveAndNext()
    {
        Save();
        Next();
    }

    void Save()
    {
        stringBuilder.Append(currentChar);
    }

    void Next()
    {
        currentChar = mInput.Next();
    }

    void Reset()
    {
        stringBuilder.Clear();
    }

    void ReadName(Token token)
    {
        do
        {
            SaveAndNext();
        } while (StringUtil.IsAlphaNum(currentChar));
        token.str = stringBuilder.ToString();
        token.type = Token.TryGetType(token.str, out var t) ? t : Token.Type.IDENTIFIER;
    }

    void ReadNumber(Token token)
    {
        do
        {
            SaveAndNext();
        } while (StringUtil.IsDigit(currentChar) || currentChar == '.');
        token.str = stringBuilder.ToString();
        token.type = Token.TryGetType(token.str, out var t) ? t : Token.Type.FLOATCONSTANT;
    }

    void ReadOperator(Token token)
    {
        do
        {
            SaveAndNext();
        } while (StringUtil.IsSymbol(currentChar));
        token.str = stringBuilder.ToString();
        token.type = Token.TryGetType(token.str, out var t) ? t : Token.Type.EOS;
    }

}

} // namespace Shadertoy4U
