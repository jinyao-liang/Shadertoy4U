
namespace Shadertoy4U
{

public static class StringUtil
{
    public static bool IsNewLine(char c)
    {
        return c == '\n' ||  c == '\r';
    }

    public static bool IsAlpha(char c)
    {
        return char.IsLetter(c) || c == '_';
    }

    public static bool IsAlphaNum(char c)
    {
        return char.IsLetterOrDigit(c) || c == '_';
    }

    public static bool IsDigit(char c)
    {
        return char.IsDigit(c);
    }

    public static bool IsSymbol(char c)
    {
        return char.IsSymbol(c);
    }
}

} // namespace Shadertoy4U
