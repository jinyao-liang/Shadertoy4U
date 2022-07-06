
namespace Shadertoy4U
{

public class StringBuffer
{
    string mStr;

    public const char eos = '\0';

    public int pos { get; set; }

    public StringBuffer(string str)
    {
        mStr = str;
        pos = 0;
    }

    public char Next()
    {
        if (pos < mStr.Length)
        {
            return mStr[pos++];
        }
        return eos;
    }
}

} // namespace Shadertoy4U
