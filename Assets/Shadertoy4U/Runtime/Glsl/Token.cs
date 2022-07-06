using System.Collections.Generic;

namespace Shadertoy4U
{

public class Token
{
    public Type type;
    public string str;

    public bool valid { get => type != Type.EOS; }

    public override string ToString()
    {
        return $"[{type}]{str}";
    }

    public static bool TryGetType(string str, out Type t)
    {
        return keyword2type.TryGetValue(str, out t);
    }

    public enum Type
    {
        /* terminal symbols denoted by reserved words */
        CONST,
        BOOL,
        FLOAT,
        INT,
        UINT,
        DOUBLE,
        BVEC2,
        BVEC3,
        BVEC4,
        IVEC2,
        IVEC3,
        IVEC4,
        UVEC2,
        UVEC3,
        UVEC4,
        VEC2,
        VEC3,
        VEC4,
        MAT2,
        MAT3,
        MAT4,
        MAT2X2,
        MAT2X3,
        MAT2X4,
        MAT3X2,
        MAT3X3,
        MAT3X4,
        MAT4X2,
        MAT4X3,
        MAT4X4,
        DVEC2,
        DVEC3,
        DVEC4,
        DMAT2,
        DMAT3,
        DMAT4,
        DMAT2X2,
        DMAT2X3,
        DMAT2X4,
        DMAT3X2,
        DMAT3X3,
        DMAT3X4,
        DMAT4X2,
        DMAT4X3,
        DMAT4X4,
        CENTROID,
        IN,
        OUT,
        INOUT,
        UNIFORM,
        PATCH,
        SAMPLE,
        BUFFER,
        SHARED,
        COHERENT,
        VOLATILE,
        RESTRICT,
        READONLY,
        WRITEONLY,
        NOPERSPECTIVE,
        FLAT,
        SMOOTH,
        LAYOUT,
        ATOMIC_UINT,
        SAMPLER2D,
        SAMPLER3D,
        SAMPLERCUBE,
        SAMPLER2DSHADOW,
        SAMPLERCUBESHADOW,
        SAMPLER2DARRAY,
        SAMPLER2DARRAYSHADOW,
        ISAMPLER2D,
        ISAMPLER3D,
        ISAMPLERCUBE,
        ISAMPLER2DARRAY,
        USAMPLER2D,
        USAMPLER3D,
        USAMPLERCUBE,
        USAMPLER2DARRAY,
        SAMPLER1D,
        SAMPLER1DSHADOW,
        SAMPLER1DARRAY,
        SAMPLER1DARRAYSHADOW,
        ISAMPLER1D,
        ISAMPLER1DARRAY,
        USAMPLER1D,
        USAMPLER1DARRAY,
        SAMPLER2DRECT,
        SAMPLER2DRECTSHADOW,
        ISAMPLER2DRECT,
        USAMPLER2DRECT,
        SAMPLERBUFFER,
        ISAMPLERBUFFER,
        USAMPLERBUFFER,
        SAMPLERCUBEARRAY,
        SAMPLERCUBEARRAYSHADOW,
        ISAMPLERCUBEARRAY,
        USAMPLERCUBEARRAY,
        SAMPLER2DMS,
        ISAMPLER2DMS,
        USAMPLER2DMS,
        SAMPLER2DMSARRAY,
        ISAMPLER2DMSARRAY,
        USAMPLER2DMSARRAY,
        IMAGE2D,
        IIMAGE2D,
        UIMAGE2D,
        IMAGE3D,
        IIMAGE3D,
        UIMAGE3D,
        IMAGECUBE,
        IIMAGECUBE,
        UIMAGECUBE,
        IMAGEBUFFER,
        IIMAGEBUFFER,
        UIMAGEBUFFER,
        IMAGE2DARRAY,
        IIMAGE2DARRAY,
        UIMAGE2DARRAY,
        IMAGECUBEARRAY,
        IIMAGECUBEARRAY,
        UIMAGECUBEARRAY,
        IMAGE1D,
        IIMAGE1D,
        UIMAGE1D,
        IMAGE1DARRAY,
        IIMAGE1DARRAY,
        UIMAGE1DARRAY,
        IMAGE2DRECT,
        IIMAGE2DRECT,
        UIMAGE2DRECT,
        IMAGE2DMS,
        IIMAGE2DMS,
        UIMAGE2DMS,
        IMAGE2DMSARRAY,
        IIMAGE2DMSARRAY,
        UIMAGE2DMSARRAY,
        STRUCT,
        VOID,
        WHILE,
        BREAK,
        CONTINUE,
        DO,
        ELSE,
        FOR,
        IF,
        DISCARD,
        RETURN,
        SWITCH,
        CASE,
        DEFAULT,
        SUBROUTINE,
        IDENTIFIER,
        TYPE_NAME,
        FLOATCONSTANT,
        INTCONSTANT,
        UINTCONSTANT,
        BOOLCONSTANT,
        DOUBLECONSTANT,
        FIELD_SELECTION,
        LEFT_OP,
        RIGHT_OP,
        INC_OP,
        DEC_OP,
        LE_OP,
        GE_OP,
        EQ_OP,
        NE_OP,
        AND_OP,
        OR_OP,
        XOR_OP,
        MUL_ASSIGN,
        DIV_ASSIGN,
        ADD_ASSIGN,
        MOD_ASSIGN,
        LEFT_ASSIGN,
        RIGHT_ASSIGN,
        AND_ASSIGN,
        XOR_ASSIGN,
        OR_ASSIGN,
        SUB_ASSIGN,
        LEFT_PAREN,
        RIGHT_PAREN,
        LEFT_BRACKET,
        RIGHT_BRACKET,
        LEFT_BRACE,
        RIGHT_BRACE,
        DOT,
        COMMA,
        COLON,
        EQUAL,
        SEMICOLON,
        BANG,
        DASH,
        TILDE,
        PLUS,
        STAR,
        SLASH,
        PERCENT,
        LEFT_ANGLE,
        RIGHT_ANGLE,
        VERTICAL_BAR,
        CARET,
        AMPERSAND,
        QUESTION,
        INVARIANT,
        PRECISE,
        HIGH_PRECISION,
        MEDIUM_PRECISION,
        LOW_PRECISION,
        PRECISION,

        /* other terminal symbols */
        EOS,
        COMMENT,
    }

    static readonly string[] keywords = new string[]
    {
        // Keywords
        "const",
        "bool",
        "float",
        "int",
        "uint",
        "double",
        "bvec2",
        "bvec3",
        "bvec4",
        "ivec2",
        "ivec3",
        "ivec4",
        "uvec2",
        "uvec3",
        "uvec4",
        "vec2",
        "vec3",
        "vec4",
        "mat2",
        "mat3",
        "mat4",
        "mat2x2",
        "mat2x3",
        "mat2x4",
        "mat3x2",
        "mat3x3",
        "mat3x4",
        "mat4x2",
        "mat4x3",
        "mat4x4",
        "dvec2",
        "dvec3",
        "dvec4",
        "dmat2",
        "dmat3",
        "dmat4",
        "dmat2x2",
        "dmat2x3",
        "dmat2x4",
        "dmat3x2",
        "dmat3x3",
        "dmat3x4",
        "dmat4x2",
        "dmat4x3",
        "dmat4x4",
        "centroid",
        "in",
        "out",
        "inout",
        "uniform",
        "patch",
        "sample",
        "buffer",
        "shared",
        "coherent",
        "volatile",
        "restrict",
        "readonly",
        "writeonly",
        "noperspective",
        "flat",
        "smooth",
        "layout",
        "atomic_uint",
        "sampler2D",
        "sampler3D",
        "samplerCube",
        "sampler2DShadow",
        "samplerCubeShadow",
        "sampler2DArray",
        "sampler2DArrayShadow",
        "isampler2D",
        "isampler3D",
        "isamplerCube",
        "isampler2DArray",
        "usampler2D",
        "usampler3D",
        "usamplerCube",
        "usampler2DArray",
        "sampler1D",
        "sampler1DShadow",
        "sampler1DArray",
        "sampler1DArrayShadow",
        "isampler1D",
        "isampler1DArray",
        "usampler1D",
        "usampler1DArray",
        "sampler2DRect",
        "sampler2DRectShadow",
        "isampler2DRect",
        "usampler2DRect",
        "samplerBuffer",
        "isamplerBuffer",
        "usamplerBuffer",
        "samplerCubeArray",
        "samplerCubeArrayShadow",
        "isamplerCubeArray",
        "usamplerCubeArray",
        "sampler2DMS",
        "isampler2DMS",
        "usampler2DMS",
        "sampler2DMSArray",
        "isampler2DMSArray",
        "usampler2DMSArray",
        "image2D",
        "iimage2D",
        "uimage2D",
        "image3D",
        "iimage3D",
        "uimage3D",
        "imageCube",
        "iimageCube",
        "uimageCube",
        "imageBuffer",
        "iimageBuffer",
        "uimageBuffer",
        "image2DArray",
        "iimage2DArray",
        "uimage2DArray",
        "imageCubeArray",
        "iimageCubeArray",
        "uimageCubeArray",
        "image1D",
        "iimage1D",
        "uimage1D",
        "image1DArray",
        "iimage1DArray",
        "uimage1DArray",
        "image2DRect",
        "iimage2DRect",
        "uimage2DRect",
        "image2DMS",
        "iimage2DMS",
        "uimage2DMS",
        "image2DMSArray",
        "iimage2DMSArray",
        "uimage2DMSArray",
        "struct",
        "void",
        "while",
        "break",
        "continue",
        "do",
        "else",
        "for",
        "if",
        "discard",
        "return",
        "switch",
        "case",
        "default",
        "subroutine",
        "IDENTIFIER",
        "TYPE_NAME",
        "FLOATCONSTANT",
        "INTCONSTANT",
        "UINTCONSTANT",
        "BOOLCONSTANT",
        "DOUBLECONSTANT",
        "FIELD_SELECTION",
        "<<",
        ">>",
        "++",
        "--",
        "<=",
        ">=",
        "==",
        "!=",
        "&&",
        "||",
        "^^",
        "*=",
        "/=",
        "+=",
        "%=",
        "<<=",
        ">>=",
        "&=",
        "^=",
        "|=",
        "-=",
        "(",
        ")",
        "[",
        "]",
        "{",
        "}",
        ".",
        ",",
        ":",
        "=",
        ";",
        "!",
        "-",
        "~",
        "+",
        "*",
        "/",
        "%",
        "<",
        ">",
        "|",
        "^",
        "&",
        "?",
        "invariant",
        "precise",
        "highp",
        "mediump",
        "lowp",
        "precision",
    };

    static readonly Dictionary<string, Type> keyword2type = new Dictionary<string, Type>();

    static Token()
    {
        for(int i = 0; i < keywords.Length; i++)
            keyword2type.Add(keywords[i], (Type)i);
    }
}

} // namespace Shadertoy4U