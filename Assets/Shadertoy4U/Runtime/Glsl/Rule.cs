using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shadertoy4U.Glsl
{

public abstract class Rule
{
    Rule mParent;
    Rule mChild;
    Rule mNext;

    public static Parser parser;

    public static int depth = 0;

    public virtual string name => $"{GetType().Name}";

    public bool Process()
    {            
        if (depth-- <= 0)
        {
            Debug.LogWarning($"max depth exceed!");
            return false;
        }

        DebugDump(this, 0);

        return ProcessSelf() &&
                ProcessBranches();
    }

    public static void DebugDump(Rule node, int level)
    {
        string str = $"ExecuteSequence: ";
        while (node != null)
        {
            str +=$" -> {node.name}";
            node = node.mNext;
        }
        Debug.Log(str);
    } 

    bool ProcessBranches()
    {
        var branches = CreateBranches();
        if (branches == null)
            return ProcessNext();

        var state = parser.Save();
        for (var i = 0; i < branches.Length; i++)
        {
            if (i > 0)
                parser.Restore(state);

            var next = i == branches.Length - 1 ? mNext : CloneBranch(mNext);
            var branch = branches[i];
            for(var n = branch.Length - 1; n >= 0; n--)
            {
                branch[n].mParent = this;
                branch[n].mNext = next;
                next = branch[n];
            }

            mChild = branch[0];
            if (mChild.Process())
                return true;
        }

        return false;
    }

    bool ProcessNext()
    {
        if (mNext != null)
            return (mNext.Process());
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

    protected bool Consume(Token.Type t)
    {
        var last = parser.token;
        var r = parser.Consume(t);
        if (r)
        {
            Debug.Log($"token {last.ToString()} consume by {name}");
        }

        return r;
    }

    protected virtual bool ProcessSelf() => true;
    protected virtual Rule[][] CreateBranches() => null;
    protected virtual Rule Clone() => (Rule)Activator.CreateInstance(GetType());
}

} // namespace Shadertoy4U.Glsl
