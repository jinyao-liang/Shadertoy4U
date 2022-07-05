using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shadertoy4U
{

public class HistoryGuard : IDisposable
{
    List<GrammarNode> history;

    public HistoryGuard(List<GrammarNode> h, GrammarNode n)
    {
        history = h;
        history.Add(n);
    }

    public void Dispose()
    {
        history.RemoveAt(history.Count - 1);
    }
}

public abstract class GrammarNode
{
    GrammarNode mParent;
    GrammarNode[] mChildren;

    GrammarNode mNext;

    Parser.State mState;
    bool mDirty;

    static List<GrammarNode> sHistory = new List<GrammarNode>();

    public virtual string name => $"{GetType().Name}({mDirty})";

    protected virtual bool ExecuteSelf(Parser parser) => true;
    protected virtual GrammarNode[][] GenerateSubsequence() => null;

    public static int depth = 0;
    public bool Execute(Parser parser)
    {
        if (mParent != null &&
            mParent.GetType() == GetType() && 
            parser.IsSameState(mParent.mState) &&
            !mParent.mDirty)
        {
            Debug.LogWarning($"deadloop found!");
            return false;
        }

        DumpSeq();
        using (new HistoryGuard(sHistory, this))
        {
            //Debug.Log($"[{depth}]Executing {name}, current token: {parser.token.ToString()}");
            if (depth-- <= 0)
            {
                Debug.LogWarning($"max depth exceed!");
                return false;
            }

            return ExecuteSelf(parser) &&
                    ExecuteChildren(parser);
        }
    }

    private bool ExecuteChildren(Parser parser)
    {
        var seqs = GenerateSubsequence();
        if (seqs == null)
            return true;

        mState = parser.Backup();
        for (var i = 0; i < seqs.Length; i++)
        {
            if (i > 0)
                parser.Restore(mState);

            var seq = seqs[i];
            for(int n = 0; n < seq.Length; n++)
            {
                seq[n].mParent = this;
                if (n+1 < seq.Length)
                    seq[n].mNext = seq[n+1];
            }

            if (seq[0].Execute(parser) && 
                    ExecuteNext(parser))
                return true;
        }

        return false;
    }

    private bool ExecuteNext(Parser parser)
    {
        var node = this;
        while (node != null)
        {
            if (node.mNext != null)
                return node.mNext.Execute(parser);
            node = node.mParent;
        }

        return true;
    }

    void DumpSeq()
    {
        var first = true;
        string s = $"ExecuteSequence: [[[";
        foreach(var n in sHistory)
        {
            if (first)
            {
                first = false;
                s += $"{n.name}";
            }
            else
            {
                s += $" -> {n.name}";
            }
        }
        s += $"]]]     ";

        DumpNode(this, ref s);


        s += $"     ===";

        var node = mParent;
        while (node != null)
        {
            DumpNode(node.mNext, ref s);
            node = node.mParent;
        }
        s += $"===";

        Debug.Log(s);
    }

    void DumpNode(GrammarNode node, ref string str)
    {
        while (node != null)
        {
            str +=$" -> {node.name}";
            node = node.mNext;
        }
    }

    protected bool Consume(Parser parser, Token.Type t)
    {
        var last = parser.token;
        var r = parser.Consume(t);
        if (r)
        {
            Debug.Log($"token {last.ToString()} consume by {name}");
            MarkDirty();
        }

        return r;
    }

    void MarkDirty()
    {
        var p = this;
        while (p != null)
        {
            p.mDirty = true;
            p = p.mParent;
        }
    }
}

public class SingleToken : GrammarNode
{
    Token.Type type;

    public override string name => $"{GetType().Name}[{type}]";

    public SingleToken(Token.Type t)
    {
        type = t;
    }

    protected override bool ExecuteSelf(Parser parser)
    {
        return Consume(parser, type);
    }
}

public class TypeToken : GrammarNode
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
            return Consume(parser, t);
        Debug.LogWarning($"expect 'Type', got {parser.token.ToString()}");
        return false;
    }
}

public class StorageToken : GrammarNode
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
            return Consume(parser, t);
        Debug.LogWarning($"expect 'Storage', got {parser.token.ToString()}");
        return false;
    }
}


/*
function_prototype :
    function_declarator RIGHT_PAREN
*/
public class function_prototype : GrammarNode
{
    protected override GrammarNode[][] GenerateSubsequence()
    {
        return new GrammarNode[][]
        {
            new GrammarNode[]
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
public class function_declarator : GrammarNode
{
    protected override GrammarNode[][] GenerateSubsequence()
    {
        return new GrammarNode[][]
        {
            new GrammarNode[]
            {
                new function_header(),
            },
            new GrammarNode[]
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
public class function_header : GrammarNode
{
    protected override GrammarNode[][] GenerateSubsequence()
    {
        return new GrammarNode[][]
        {
            new GrammarNode[]
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
public class fully_specified_type : GrammarNode
{
    protected override GrammarNode[][] GenerateSubsequence()
    {
        return new GrammarNode[][]
        {
            new GrammarNode[]
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
public class type_specifier : GrammarNode
{
    protected override GrammarNode[][] GenerateSubsequence()
    {
        return new GrammarNode[][]
        {
            new GrammarNode[]
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
public class type_specifier_nonarray : GrammarNode
{
    protected override GrammarNode[][] GenerateSubsequence()
    {
        return new GrammarNode[][]
        {
            new GrammarNode[]
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
public class function_header_with_parameters : GrammarNode
{
    protected override GrammarNode[][] GenerateSubsequence()
    {
        return new GrammarNode[][]
        {
            new GrammarNode[]
            {
                new function_header(),
                new parameter_declaration(),
            },
            new GrammarNode[]
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
public class parameter_declaration : GrammarNode
{
    protected override GrammarNode[][] GenerateSubsequence()
    {
        return new GrammarNode[][]
        {
            new GrammarNode[]
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
public class type_qualifier : GrammarNode
{
    protected override GrammarNode[][] GenerateSubsequence()
    {
        return new GrammarNode[][]
        {
            new GrammarNode[]
            {
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
public class single_type_qualifier : GrammarNode
{
    protected override GrammarNode[][] GenerateSubsequence()
    {
        return new GrammarNode[][]
        {
            new GrammarNode[]
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
public class storage_qualifier : GrammarNode
{
    protected override GrammarNode[][] GenerateSubsequence()
    {
        return new GrammarNode[][]
        {
            new GrammarNode[]
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
public class parameter_declarator : GrammarNode
{
    protected override GrammarNode[][] GenerateSubsequence()
    {
        return new GrammarNode[][]
        {
            new GrammarNode[]
            {
                new type_specifier(),
                new SingleToken(Token.Type.IDENTIFIER),
            },
        };
    }
}

} // namespace Shadertoy4U
