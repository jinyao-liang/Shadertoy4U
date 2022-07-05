using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shadertoy4U.Glsl
{

public abstract class Rule
{
    Rule mNext;

    public static int depth = 0;

    public virtual string name => $"{GetType().Name}";

    public bool Process(Parser parser)
    {            
        if (depth-- <= 0)
        {
            Debug.LogWarning($"max depth exceed!");
            return false;
        }

        DebugDump();

        return ProcessSelf(parser) &&
                ProcessBranches(parser);
    }

    void DebugDump()
    {
        string s = $"ExecuteSequence: ";
        DumpNode(this, ref s);
        Debug.Log(s);
    } 
    
    void DumpNode(Rule node, ref string str)
    {
        while (node != null)
        {
            str +=$" -> {node.name}";
            node = node.mNext;
        }
    }

    bool ProcessBranches(Parser parser)
    {
        var branches = CreateBranches();
        if (branches == null)
            return ProcessNext(parser);

        var state = parser.Save();
        for (var i = 0; i < branches.Length; i++)
        {
            if (i > 0)
                parser.Restore(state);

            var next = i == branches.Length - 1 ? mNext : CloneBranch(mNext);
            var branch = branches[i];
            for(var n = branch.Length - 1; n >= 0; n--)
            {
                branch[n].mNext = next;
                next = branch[n];
            }

            if (branch[0].Process(parser))
                return true;
        }

        return false;
    }

    bool ProcessNext(Parser parser)
    {
        if (mNext != null)
            return (mNext.Process(parser));
        return true;
    }

    Rule CloneBranch(Rule r)
    {
        var head = Clone(r);

        var node = head;
        while(r != null)
        {
            node.mNext = Clone(r.mNext);
            node = node.mNext;
            r = r.mNext;
        }

        return head;
    }

    static Rule Clone(Rule r)
    {
        return r?.Clone();
    }

    protected bool Consume(Parser parser, Token.Type t)
    {
        var last = parser.token;
        var r = parser.Consume(t);
        if (r)
        {
            Debug.Log($"token {last.ToString()} consume by {name}");
        }

        return r;
    }

    protected virtual bool ProcessSelf(Parser parser) => true;
    protected virtual Rule[][] CreateBranches() => null;
    protected virtual Rule Clone() => (Rule)Activator.CreateInstance(GetType());
}

} // namespace Shadertoy4U.Glsl
