
namespace Shadertoy4U
{

public class StringBuffer
{
    string mStr;
    int currentPosition;

    public const char eos = '\0';

    public StringBuffer(string str)
    {
        mStr = str;
        currentPosition = 0;
    }

    public char NextChar()
    {
        if (currentPosition < mStr.Length)
        {
            return mStr[currentPosition++];
        }
        return eos;
    }
}

} // namespace Shadertoy4U
