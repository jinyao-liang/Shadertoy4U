using System;
using UnityEngine;

namespace Shadertoy4U
{

public abstract class Branch
{
    private Branch next;

    private static int depth = 0;

    protected virtual bool ExecuteSelf(Parser parser)
    {
        return true;
    }

    protected virtual Branch[][] GenerateSubsequence()
    {
        return null;
    }

    public bool Execute(Parser parser)
    {
        Debug.Log($"Executing {ToString()} at {depth}");
        if (depth++ > 16)
            return false;

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

public class SingleToken : Branch
{
    Token.Type type;
    public SingleToken(Token.Type t)
    {
        type = t;
    }

    protected override bool ExecuteSelf(Parser parser)
    {
        return parser.Consume(type);
    }

    public override string ToString()
    {
        return $"Shadertoy4U.SingleToken({type})";
    }
}

public class TypeToken : Branch
{
    static readonly Token.Type[] types = new Token.Type[]
    {
        Token.Type.VOID,
        Token.Type.FLOAT,
        Token.Type.DOUBLE,
        Token.Type.INT,
        Token.Type.UINT,
        Token.Type.BOOL,
        Token.Type.VEC2,
        Token.Type.VEC3,
        Token.Type.VEC4,
        Token.Type.DVEC2,
        Token.Type.DVEC3,
        Token.Type.DVEC4,
        Token.Type.BVEC2,
        Token.Type.BVEC3,
        Token.Type.BVEC4,
        Token.Type.IVEC2,
        Token.Type.IVEC3,
        Token.Type.IVEC4,
        Token.Type.UVEC2,
        Token.Type.UVEC3,
        Token.Type.UVEC4,
        Token.Type.MAT2,
        Token.Type.MAT3,
        Token.Type.MAT4,
        Token.Type.MAT2X2,
        Token.Type.MAT2X3,
        Token.Type.MAT2X4,
        Token.Type.MAT3X2,
        Token.Type.MAT3X3,
        Token.Type.MAT3X4,
        Token.Type.MAT4X2,
        Token.Type.MAT4X3,
        Token.Type.MAT4X4,
        Token.Type.DMAT2,
        Token.Type.DMAT3,
        Token.Type.DMAT4,
        Token.Type.DMAT2X2,
        Token.Type.DMAT2X3,
        Token.Type.DMAT2X4,
        Token.Type.DMAT3X2,
        Token.Type.DMAT3X3,
        Token.Type.DMAT3X4,
        Token.Type.DMAT4X2,
        Token.Type.DMAT4X3,
        Token.Type.DMAT4X4,
        Token.Type.ATOMIC_UINT,
        Token.Type.SAMPLER2D,
        Token.Type.SAMPLER3D,
        Token.Type.SAMPLERCUBE,
        Token.Type.SAMPLER2DSHADOW,
        Token.Type.SAMPLERCUBESHADOW,
        Token.Type.SAMPLER2DARRAY,
        Token.Type.SAMPLER2DARRAYSHADOW,
        Token.Type.SAMPLERCUBEARRAY,
        Token.Type.SAMPLERCUBEARRAYSHADOW,
        Token.Type.ISAMPLER2D,
        Token.Type.ISAMPLER3D,
        Token.Type.ISAMPLERCUBE,
        Token.Type.ISAMPLER2DARRAY,
        Token.Type.ISAMPLERCUBEARRAY,
        Token.Type.USAMPLER2D,
        Token.Type.USAMPLER3D,
        Token.Type.USAMPLERCUBE,
        Token.Type.USAMPLER2DARRAY,
        Token.Type.USAMPLERCUBEARRAY,
        Token.Type.SAMPLER1D,
        Token.Type.SAMPLER1DSHADOW,
        Token.Type.SAMPLER1DARRAY,
        Token.Type.SAMPLER1DARRAYSHADOW,
        Token.Type.ISAMPLER1D,
        Token.Type.ISAMPLER1DARRAY,
        Token.Type.USAMPLER1D,
        Token.Type.USAMPLER1DARRAY,
        Token.Type.SAMPLER2DRECT,
        Token.Type.SAMPLER2DRECTSHADOW,
        Token.Type.ISAMPLER2DRECT,
        Token.Type.USAMPLER2DRECT,
        Token.Type.SAMPLERBUFFER,
        Token.Type.ISAMPLERBUFFER,
        Token.Type.USAMPLERBUFFER,
        Token.Type.SAMPLER2DMS,
        Token.Type.ISAMPLER2DMS,
        Token.Type.USAMPLER2DMS,
        Token.Type.SAMPLER2DMSARRAY,
        Token.Type.ISAMPLER2DMSARRAY,
        Token.Type.USAMPLER2DMSARRAY,
        Token.Type.IMAGE2D,
        Token.Type.IIMAGE2D,
        Token.Type.UIMAGE2D,
        Token.Type.IMAGE3D,
        Token.Type.IIMAGE3D,
        Token.Type.UIMAGE3D,
        Token.Type.IMAGECUBE,
        Token.Type.IIMAGECUBE,
        Token.Type.UIMAGECUBE,
        Token.Type.IMAGEBUFFER,
        Token.Type.IIMAGEBUFFER,
        Token.Type.UIMAGEBUFFER,
        Token.Type.IMAGE1D,
        Token.Type.IIMAGE1D,
        Token.Type.UIMAGE1D,
        Token.Type.IMAGE1DARRAY,
        Token.Type.IIMAGE1DARRAY,
        Token.Type.UIMAGE1DARRAY,
        Token.Type.IMAGE2DRECT,
        Token.Type.IIMAGE2DRECT,
        Token.Type.UIMAGE2DRECT,
        Token.Type.IMAGE2DARRAY,
        Token.Type.IIMAGE2DARRAY,
        Token.Type.UIMAGE2DARRAY,
        Token.Type.IMAGECUBEARRAY,
        Token.Type.IIMAGECUBEARRAY,
        Token.Type.UIMAGECUBEARRAY,
        Token.Type.IMAGE2DMS,
        Token.Type.IIMAGE2DMS,
        Token.Type.UIMAGE2DMS,
        Token.Type.IMAGE2DMSARRAY,
        Token.Type.IIMAGE2DMSARRAY,
        Token.Type.UIMAGE2DMSARRAY,
    };

    protected override bool ExecuteSelf(Parser parser)
    {
        var t = parser.token.type;
        if (Array.IndexOf(types, t) >= 0)
            return parser.Consume(t);
        return false;
    }
}

public class StorageToken : Branch
{
    static readonly Token.Type[] types = new Token.Type[]
    {
        Token.Type.CONST,
        Token.Type.IN,
        Token.Type.OUT,
        Token.Type.INOUT,
        Token.Type.CENTROID,
        Token.Type.PATCH,
        Token.Type.SAMPLE,
        Token.Type.UNIFORM,
        Token.Type.BUFFER,
        Token.Type.SHARED,
        Token.Type.COHERENT,
        Token.Type.VOLATILE,
        Token.Type.RESTRICT,
        Token.Type.READONLY,
        Token.Type.WRITEONLY,
        Token.Type.SUBROUTINE,
    };

    protected override bool ExecuteSelf(Parser parser)
    {
        var t = parser.token.type;
        if (Array.IndexOf(types, t) >= 0)
            return parser.Consume(t);
        return false;
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
                new SingleToken(Token.Type.RIGHT_PAREN),
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
                new SingleToken(Token.Type.IDENTIFIER),
                new SingleToken(Token.Type.LEFT_PAREN),
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
                new TypeToken(),
            },
        };
    }
}

/*
function_header_with_parameters :
    function_header parameter_declaration
    function_header_with_parameters COMMA parameter_declaration
*/
public class function_header_with_parameters : Branch
{
    protected override Branch[][] GenerateSubsequence()
    {
        return new Branch[][]
        {
            new Branch[]
            {
                new function_header(),
                new parameter_declaration(),
            },
            new Branch[]
            {
                new function_header_with_parameters(),
                new SingleToken(Token.Type.COMMA),
                new parameter_declaration(),
            },
        };
    }
}

/*
parameter_declaration :
    type_qualifier parameter_declarator
    parameter_declarator
    type_qualifier parameter_type_specifier
    parameter_type_specifier
*/
public class parameter_declaration : Branch
{
    protected override Branch[][] GenerateSubsequence()
    {
        return new Branch[][]
        {
            new Branch[]
            {
                new type_qualifier(),
                new parameter_declarator(),
            },
        };
    }
}

/*
type_qualifier :
    single_type_qualifier
    type_qualifier single_type_qualifier
*/
public class type_qualifier : Branch
{
    protected override Branch[][] GenerateSubsequence()
    {
        return new Branch[][]
        {
            new Branch[]
            {
                new single_type_qualifier(),
            },
            new Branch[]
            {
                new type_qualifier(),
                new single_type_qualifier(),
            },
        };
    }
}

/*
single_type_qualifier :
    storage_qualifier
    layout_qualifier
    precision_qualifier
    interpolation_qualifier
    invariant_qualifier
    precise_qualifier
*/
public class single_type_qualifier : Branch
{
    protected override Branch[][] GenerateSubsequence()
    {
        return new Branch[][]
        {
            new Branch[]
            {
                new storage_qualifier(),
            },
        };
    }
}

/*
storage_qualifier :
    CONST
    IN
    OUT
    INOUT
    CENTROID
    PATCH
    SAMPLE
    UNIFORM
    BUFFER
    SHARED
    COHERENT
    VOLATILE
    RESTRICT
    READONLY
    WRITEONLY
    SUBROUTINE
    SUBROUTINE LEFT_PAREN type_name_list RIGHT_PAREN
*/
public class storage_qualifier : Branch
{
    protected override Branch[][] GenerateSubsequence()
    {
        return new Branch[][]
        {
            new Branch[]
            {
                new StorageToken(),
            },
        };
    }
}


/*
parameter_declarator :
    type_specifier IDENTIFIER
    type_specifier IDENTIFIER array_specifier
*/
public class parameter_declarator : Branch
{
    protected override Branch[][] GenerateSubsequence()
    {
        return new Branch[][]
        {
            new Branch[]
            {
                new type_specifier(),
                new SingleToken(Token.Type.IDENTIFIER),
            },
        };
    }
}

} // namespace Shadertoy4U
