using UnityEngine;

namespace Shadertoy4U
{

public abstract class Branch
{
    private Branch next;

    public bool Execute(Parser parser)
    {
        Debug.Log($"Executing {ToString()}");

        if (!ExecuteSelf(parser))
            return false;

        var seqs = GenerateSubsequence();
        if (seqs != null)
        {
            var state = parser.Backup();
            foreach (var seq in seqs)
            {
                parser.Restore(state);
                ExecuteSequence(parser, seq);
            }
        }

        return true;
    }

    protected virtual bool ExecuteSelf(Parser parser)
    {
        return true;
    }

    protected virtual Branch[][] GenerateSubsequence()
    {
        return null;
    }

    private bool ExecuteSequence(Parser parser, Branch[] branches)
    {
        var newNext = next;
        for(var i = branches.Length - 1; i >= 0; i--)
        {
            branches[i].next = newNext;
            newNext = branches[i];
        }

        var b = newNext;
        var s = $"ExecuteSequence: {b.ToString()}";
        while (b.next != null)
        {
            b = b.next;
            s = s + $" -> {b.ToString()}";
        }
        Debug.Log(s);

        return newNext.Execute(parser);
    }
}  

public class TokenBranch : Branch
{
    Token.Type type;
    public TokenBranch(Token.Type t)
    {
        type = t;
    }

    protected override bool ExecuteSelf(Parser parser)
    {
        return parser.Consume(type);
    }

    public override string ToString()
    {
        return $"Shadertoy4U.TokenBranch({type})";
    }
}

/*
function_prototype :
    function_declarator RIGHT_PAREN
*/
public class function_prototype : Branch
{
    protected override Branch[][] GenerateSubsequence()
    {
        return new Branch[][]
        {
            new Branch[]
            {
                new function_declarator(),
                new TokenBranch(Token.Type.RIGHT_PAREN),
            },
        };
    }
}

/*
function_declarator :
    function_header
    function_header_with_parameters
*/
public class function_declarator : Branch
{
    protected override Branch[][] GenerateSubsequence()
    {
        return new Branch[][]
        {
            new Branch[]
            {
                new function_header(),
            },
            new Branch[]
            {
                new function_header_with_parameters(),
            },
        };
    }
}

/*
function_header :
    fully_specified_type IDENTIFIER LEFT_PAREN
*/
public class function_header : Branch
{
    protected override Branch[][] GenerateSubsequence()
    {
        return new Branch[][]
        {
            new Branch[]
            {
                new fully_specified_type(),
                new TokenBranch(Token.Type.IDENTIFIER),
                new TokenBranch(Token.Type.LEFT_PAREN),
            },
        };
    }
}

/*
fully_specified_type :
    type_specifier
    type_qualifier type_specifier
*/
public class fully_specified_type : Branch
{
    protected override Branch[][] GenerateSubsequence()
    {
        return new Branch[][]
        {
            new Branch[]
            {
                new type_specifier(),
            },
            new Branch[]
            {
                new type_qualifier(),
                new type_specifier(),
            },
        };
    }
}

/*
type_specifier :
    type_specifier_nonarray
    type_specifier_nonarray array_specifier
*/
public class type_specifier : Branch
{
    protected override Branch[][] GenerateSubsequence()
    {
        return new Branch[][]
        {
            new Branch[]
            {
                new type_specifier_nonarray(),
            },
            new Branch[]
            {
                new type_specifier_nonarray(),
                new array_specifier(),
            },
        };
    }
}

/*
type_specifier_nonarray :
    VOID FLOAT DOUBLE INT ...
    struct_specifier 
    TYPE_NAME
*/
public class type_specifier_nonarray : Branch
{
    protected override Branch[][] GenerateSubsequence()
    {
        return new Branch[][]
        {
            new Branch[]
            {
                new TokenBranch(Token.Type.VOID),
            },
        };
    }
}

/*
array_specifier :
    LEFT_BRACKET RIGHT_BRACKET
    LEFT_BRACKET conditional_expression RIGHT_BRACKET
    array_specifier LEFT_BRACKET RIGHT_BRACKET
    array_specifier LEFT_BRACKET conditional_expression RIGHT_BRACKET
*/
public class array_specifier : Branch
{
    protected override bool ExecuteSelf(Parser parser)
    {
        Debug.LogWarning("array_specifier");
        return false;
    }
}

/*
type_qualifier :
    single_type_qualifier
    type_qualifier single_type_qualifier
*/
public class type_qualifier : Branch
{
    protected override bool ExecuteSelf(Parser parser)
    {
        Debug.LogWarning("type_qualifier");
        return false;
    }
}

public class function_header_with_parameters : Branch
{
    protected override bool ExecuteSelf(Parser parser)
    {
        Debug.LogWarning("function_header_with_parameters");
        return false;
    }
}

} // namespace Shadertoy4U
