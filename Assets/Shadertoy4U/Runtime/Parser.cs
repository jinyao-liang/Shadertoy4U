
namespace Shadertoy4U
{

public class Parser
{
    Lexer mLexer;

    public Parser(Lexer lexer)
    {
        mLexer = lexer;
    }

    public void Parse()
    {
        translation_unit();
    }

    /*
    variable_identifier :
        IDENTIFIER
    */
    bool VariableIdentifier()
    {
        return false;
    }

    /*
    primary_expression :
        variable_identifier
        INTCONSTANT
        UINTCONSTANT
        FLOATCONSTANT
        BOOLCONSTANT
        DOUBLECONSTANT
        LEFT_PAREN expression RIGHT_PAREN
    */
    bool PrimaryExpression()
    {
        return false;
    }

    /*
    postfix_expression :
        primary_expression
        postfix_expression LEFT_BRACKET integer_expression RIGHT_BRACKET
        function_call
        postfix_expression DOT FIELD_SELECTION
        postfix_expression INC_OP
        postfix_expression DEC_OP
    */
    bool PostfixExpression()
    {
        return false;
    }

    /*
    integer_expression :
        expression
    */
    bool IntegerExpression()
    {
        return false;
    }

    /*
    function_call :
        function_call_or_method
    */
    bool FunctionCall()
    {
        return false;
    }

    /*
    function_call_or_method :
        function_call_generic
    */
    bool FunctionCallOrmethod()
    {
        return false;
    }

    /*
    function_call_generic :
        function_call_header_with_parameters RIGHT_PAREN
        function_call_header_no_parameters RIGHT_PAREN
    */
    bool function_call_generic()
    {
        return false;
    }

    /*
    function_call_header_no_parameters :
        function_call_header VOID
        function_call_header
    */
    bool function_call_header_no_parameters()
    {
        return false;
    }

    /*
    function_call_header_with_parameters :
        function_call_header assignment_expression
        function_call_header_with_parameters COMMA assignment_expression
    */
    bool function_call_header_with_parameters()
    {
        return false;
    }

    /*
    function_call_header :
        function_identifier LEFT_PAREN
    */
    bool function_call_header()
    {
        return false;
    }

    /*
    function_identifier :
        type_specifier
        postfix_expression
    */
    bool function_identifier()
    {
        return false;
    }

    /*
    unary_expression :
        postfix_expression
        INC_OP unary_expression
        DEC_OP unary_expression
        unary_operator unary_expression
    */
    bool unary_expression()
    {
        return false;
    }

    /*
    unary_operator :
        PLUS
        DASH
        BANG
        TILDE
    */
    bool unary_operator()
    {
        return false;
    }

    /*
    multiplicative_expression :
        unary_expression
        multiplicative_expression STAR unary_expression
        multiplicative_expression SLASH unary_expression
        multiplicative_expression PERCENT unary_expression
    */
    bool multiplicative_expression()
    {
        return false;
    }

    /*
    additive_expression :
        multiplicative_expression
        additive_expression PLUS multiplicative_expression
        additive_expression DASH multiplicative_expression

    */
    bool additive_expression()
    {
        return false;
    }

    /*
    shift_expression :
        additive_expression
        shift_expression LEFT_OP additive_expression
        shift_expression RIGHT_OP additive_expression
    */
    bool shift_expression()
    {
        return false;
    }

    /*
    relational_expression :
        shift_expression
        relational_expression LEFT_ANGLE shift_expression
        relational_expression RIGHT_ANGLE shift_expression
        relational_expression LE_OP shift_expression
        relational_expression GE_OP shift_expression
    */
    bool relational_expression()
    {
        return false;
    }

    /*
    equality_expression :
        relational_expression
        equality_expression EQ_OP relational_expression
        equality_expression NE_OP relational_expression
    */
    bool equality_expression()
    {
        return false;
    }

    /*
    and_expression :
        equality_expression
        and_expression AMPERSAND equality_expression
    */
    bool and_expression()
    {
        return false;
    }

    /*
    exclusive_or_expression :
        and_expression
        exclusive_or_expression CARET and_expression
    */
    bool exclusive_or_expression()
    {
        return false;
    }

    /*
    inclusive_or_expression :
        exclusive_or_expression
        inclusive_or_expression VERTICAL_BAR exclusive_or_expression
    */
    bool inclusive_or_expression()
    {
        return false;
    }

    /*
    logical_and_expression :
        inclusive_or_expression
        logical_and_expression AND_OP inclusive_or_expression
    */
    bool logical_and_expression()
    {
        return false;
    }

    /*
    logical_xor_expression :
        logical_and_expression
        logical_xor_expression XOR_OP logical_and_expression

    */
    bool logical_xor_expression()
    {
        return false;
    }

    /*
    logical_or_expression :
        logical_xor_expression
        logical_or_expression OR_OP logical_xor_expression
    */
    bool logical_or_expression()
    {
        return false;
    }

    /*
    conditional_expression :
        logical_or_expression
        logical_or_expression QUESTION expression COLON assignment_expression
    */
    bool conditional_expression()
    {
        return false;
    }

    /*
    assignment_expression :
        conditional_expression
        unary_expression assignment_operator assignment_expression
    */
    bool assignment_expression()
    {
        return false;
    }

    /*
    assignment_operator :
        EQUAL
        MUL_ASSIGN
        DIV_ASSIGN
        MOD_ASSIGN
        ADD_ASSIGN
        SUB_ASSIGN
        LEFT_ASSIGN
        RIGHT_ASSIGN
        AND_ASSIGN
        XOR_ASSIGN
        OR_ASSIGN
    */
    bool assignment_operator()
    {
        return false;
    }

    /*
    expression :
        assignment_expression
        expression COMMA assignment_expression
    */
    bool expression()
    {
        return false;
    }

    /*
    constant_expression :
        conditional_expression
    */
    bool constant_expression()
    {
        return false;
    }

    /*
    declaration :
        function_prototype SEMICOLON
        init_declarator_list SEMICOLON
        PRECISION precision_qualifier type_specifier SEMICOLON
        type_qualifier IDENTIFIER LEFT_BRACE struct_declaration_list RIGHT_BRACE SEMICOLON
        type_qualifier IDENTIFIER LEFT_BRACE struct_declaration_list RIGHT_BRACE IDENTIFIER
        SEMICOLON
        type_qualifier IDENTIFIER LEFT_BRACE struct_declaration_list RIGHT_BRACE IDENTIFIER
        array_specifier SEMICOLON
        type_qualifier SEMICOLON
        type_qualifier IDENTIFIER SEMICOLON
        type_qualifier IDENTIFIER identifier_list SEMICOLON
    */
    bool declaration()
    {
        return false;
    }

    /*
    identifier_list :
        COMMA IDENTIFIER
        identifier_list COMMA IDENTIFIER
    */
    bool identifier_list()
    {
        return false;
    }

    /*
    function_prototype :
        function_declarator RIGHT_PAREN
    */
    bool function_prototype()
    {
        return false;
    }

    /*
    function_declarator :
        function_header
        function_header_with_parameters
    */
    bool function_declarator()
    {
        return false;
    }

    /*
    function_header_with_parameters :
        function_header parameter_declaration
        function_header_with_parameters COMMA parameter_declaration
    */
    bool function_header_with_parameters()
    {
        return false;
    }

    /*
    function_header :
        fully_specified_type IDENTIFIER LEFT_PAREN
    */
    bool function_header()
    {
        return false;
    }

    /*
    parameter_declarator :
        type_specifier IDENTIFIER
        type_specifier IDENTIFIER array_specifier
    */
    bool parameter_declarator()
    {
        return false;
    }

    /*
    parameter_declaration :
        type_qualifier parameter_declarator
        parameter_declarator
        type_qualifier parameter_type_specifier
        parameter_type_specifier
    */
    bool parameter_declaration()
    {
        return false;
    }

    /*
    parameter_type_specifier :
        type_specifier
    */
    bool parameter_type_specifier()
    {
        return false;
    }

    /*
    init_declarator_list :
        single_declaration
        init_declarator_list COMMA IDENTIFIER
        init_declarator_list COMMA IDENTIFIER array_specifier
        init_declarator_list COMMA IDENTIFIER array_specifier EQUAL initializer
        init_declarator_list COMMA IDENTIFIER EQUAL initializer
    */
    bool init_declarator_list()
    {
        return false;
    }

    /*
    single_declaration :
        fully_specified_type
        fully_specified_type IDENTIFIER
        fully_specified_type IDENTIFIER array_specifier
        fully_specified_type IDENTIFIER array_specifier EQUAL initializer
        fully_specified_type IDENTIFIER EQUAL initializer
    */
    bool single_declaration()
    {
        return false;
    }

    /*
    fully_specified_type :
        type_specifier
        type_qualifier type_specifier
    */
    bool fully_specified_type()
    {
        return false;
    }

    /*
    invariant_qualifier :
        INVARIANT
    */
    bool invariant_qualifier()
    {
        return false;
    }

    /*
    interpolation_qualifier :
        SMOOTH
        FLAT
        NOPERSPECTIVE
    */
    bool interpolation_qualifier()
    {
        return false;
    }

    /*
    layout_qualifier :
        LAYOUT LEFT_PAREN layout_qualifier_id_list RIGHT_PAREN
    */
    bool layout_qualifier()
    {
        return false;
    }

    /*
    layout_qualifier_id_list :
        layout_qualifier_id
        layout_qualifier_id_list COMMA layout_qualifier_id
    */
    bool layout_qualifier_id_list()
    {
        return false;
    }

    /*
    layout_qualifier_id :
        IDENTIFIER
        IDENTIFIER EQUAL constant_expression
        SHARED
    */
    bool layout_qualifier_id()
    {
        return false;
    }

    /*
    precise_qualifier :
        PRECISE
    */
    bool precise_qualifier()
    {
        return false;
    }

    /*
    type_qualifier :
        single_type_qualifier
        type_qualifier single_type_qualifier
    */
    bool type_qualifier()
    {
        return false;
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
    bool single_type_qualifier()
    {
        return false;
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
    bool storage_qualifier()
    {
        return false;
    }

    /*
    type_name_list :
        TYPE_NAME
        type_name_list COMMA TYPE_NAME
    */
    bool type_name_list()
    {
        return false;
    }

    /*
    type_specifier :
        type_specifier_nonarray
        type_specifier_nonarray array_specifier
    */
    bool type_specifier()
    {
        return false;
    }

    /*
    array_specifier :
        LEFT_BRACKET RIGHT_BRACKET
        LEFT_BRACKET conditional_expression RIGHT_BRACKET
        array_specifier LEFT_BRACKET RIGHT_BRACKET
        array_specifier LEFT_BRACKET conditional_expression RIGHT_BRACKET
    */
    bool array_specifier()
    {
        return false;
    }

    /*
    type_specifier_nonarray :
    */
    bool type_specifier_nonarray()
    {
        return false;
    }

    /*
    precision_qualifier :
        HIGH_PRECISION
        MEDIUM_PRECISION
        LOW_PRECISION
    */
    bool precision_qualifier()
    {
        return false;
    }

    /*
    struct_specifier :
        STRUCT IDENTIFIER LEFT_BRACE struct_declaration_list RIGHT_BRACE
        STRUCT LEFT_BRACE struct_declaration_list RIGHT_BRACE
    */
    bool struct_specifier()
    {
        return false;
    }

    /*
    struct_declaration_list :
        struct_declaration
        struct_declaration_list struct_declaration
    */
    bool struct_declaration_list()
    {
        return false;
    }

    /*
    struct_declaration :
        type_specifier struct_declarator_list SEMICOLON
        type_qualifier type_specifier struct_declarator_list SEMICOLON
    */
    bool struct_declaration()
    {
        return false;
    }

    /*
    struct_declarator_list :
        struct_declarator
        struct_declarator_list COMMA struct_declarator
    */
    bool struct_declarator_list()
    {
        return false;
    }

    /*
    struct_declarator :
        IDENTIFIER
        IDENTIFIER array_specifier
    */
    bool struct_declarator()
    {
        return false;
    }

    /*
    initializer :
        assignment_expression
        LEFT_BRACE initializer_list RIGHT_BRACE
        LEFT_BRACE initializer_list COMMA RIGHT_BRACE
    */
    bool initializer()
    {
        return false;
    }

    /*
    initializer_list :
        initializer
        initializer_list COMMA initializer
    */
    bool initializer_list()
    {
        return false;
    }

    /*
    declaration_statement :
        declaration
    */
    bool declaration_statement()
    {
        return false;
    }

    /*
    statement :
        compound_statement
        simple_statement

    */
    bool statement()
    {
        return false;
    }

    /*
    simple_statement :
        declaration_statement
        expression_statement
        selection_statement
        switch_statement
        case_label
        iteration_statement
        jump_statement
    */
    bool simple_statement()
    {
        return false;
    }

    /*
    compound_statement :
        LEFT_BRACE RIGHT_BRACE
        LEFT_BRACE statement_list RIGHT_BRACE
    */
    bool compound_statement()
    {
        return false;
    }

    /*
    statement_no_new_scope :
        compound_statement_no_new_scope
        simple_statement
    */
    bool statement_no_new_scope()
    {
        return false;
    }

    /*
    compound_statement_no_new_scope :
        LEFT_BRACE RIGHT_BRACE
        LEFT_BRACE statement_list RIGHT_BRACE
    */
    bool compound_statement_no_new_scope()
    {
        return false;
    }

    /*
    statement_list :
        statement
        statement_list statement
    */
    bool statement_list()
    {
        return false;
    }

    /*
    expression_statement :
        SEMICOLON
        expression SEMICOLON
    */
    bool expression_statement()
    {
        return false;
    }

    /*
    selection_statement :
        IF LEFT_PAREN expression RIGHT_PAREN selection_rest_statement
    */
    bool selection_statement()
    {
        return false;
    }

    /*
    selection_rest_statement :
        statement ELSE statement
        statement
    */
    bool selection_rest_statement()
    {
        return false;
    }

    /*
    condition :
        expression
        fully_specified_type IDENTIFIER EQUAL initializer
    */
    bool condition()
    {
        return false;
    }

    /*
    switch_statement :
        SWITCH LEFT_PAREN expression RIGHT_PAREN LEFT_BRACE switch_statement_list
        RIGHT_BRACE
    */
    bool switch_statement()
    {
        return false;
    }

    /*
    switch_statement_list :
        nothing
        statement_list
    */
    bool switch_statement_list()
    {
        return false;
    }

    /*
    case_label :
        CASE expression COLON
        DEFAULT COLON
    */
    bool case_label()
    {
        return false;
    }

    /*
    iteration_statement :
        WHILE LEFT_PAREN condition RIGHT_PAREN statement_no_new_scope
        DO statement WHILE LEFT_PAREN expression RIGHT_PAREN SEMICOLON
        FOR LEFT_PAREN for_init_statement for_rest_statement RIGHT_PAREN statement_no_new_scope
    */
    bool iteration_statement()
    {
        return false;
    }

    /*
    for_init_statement :
        expression_statement
        declaration_statement
    */
    bool for_init_statement()
    {
        return false;
    }

    /*
    conditionopt :
        condition
    */
    bool conditionopt()
    {
        return false;
    }

    /*
    for_rest_statement :
        conditionopt SEMICOLON
        conditionopt SEMICOLON expression
    */
    bool for_rest_statement()
    {
        return false;
    }

    /*
    jump_statement :
        CONTINUE SEMICOLON
        BREAK SEMICOLON
        RETURN SEMICOLON
        RETURN expression SEMICOLON
        DISCARD SEMICOLON // Fragment shader only
    */
    bool jump_statement()
    {
        return false;
    }

    /*
    translation_unit :
        external_declaration
        translation_unit external_declaration
    */
    bool translation_unit()
    {
        return false;
    }

    /*
    external_declaration :
        function_definition
        declaration
        SEMICOLON
    */
    bool external_declaration()
    {
        return false;
    }

    /*
    function_definition :
        function_prototype compound_statement_no_new_scope
    */
    bool function_definition()
    {
        return false;
    }
}

} // namespace Shadertoy4U
