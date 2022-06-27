using System.Text;

namespace Shadertoy4U
{

public class Lexer
{
    StringBuffer mInput;

    StringBuilder stringBuilder = new StringBuilder();

    int lineNumber;
    int lastLine;
    char currentChar;
    Token currentToken;
    Token lookAheadToken;

    public Token token { get => currentToken; }

    public Lexer(StringBuffer buf, char firstChar)
    {
        mInput = buf;
        currentChar = firstChar;
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
        stringBuilder.Clear();

        var token = new Token();
        while(true)
        {
            switch(currentChar)
            {
            case '\n':
            case '\r':
                /* line breaks */
                IncreaseLine();
                break;
            case ' ':
            case '\f':
            case '\t':
            case '\v':
                /* spaces */
                Next();
                break;
            case StringBuffer.eos:
                token.type = TokenType.EOS;
                return token;
            default:
                if (IsAlpha())
                {
                    /* identifier or reserved word? */
                    do
                    {
                        SaveAndNext();
                    } while (IsAlphaNum());
                    //currentToken.semInfo.ts = tokenBuffer;
                    token.str = stringBuilder.ToString();
                    return token;
                }
                else
                {
                    /* single-char tokens ('+', '*', '%', '{', '}', ...) */
                }
                break;
            }
        }
    }

    void IncreaseLine()
    {
        var old = currentChar;
        Next();
        if (CurrentIsNewLine() && currentChar != old)
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
        currentChar = mInput.NextChar();
    }

    bool CurrentIsNewLine()
    {
        return currentChar == '\n' ||  currentChar == '\r';
    }

    bool IsAlpha()
    {
        return char.IsLetter(currentChar) || currentChar == '_';
    }

    bool IsAlphaNum()
    {
        return char.IsLetterOrDigit(currentChar);
    }

    bool IsDigit()
    {
        return char.IsDigit(currentChar);
    }

}

} // namespace Shadertoy4U
