// Copyright 2008 - 2010 Herre Kuijpers - <herre.kuijpers@gmail.com>
//
// This source file(s) may be redistributed, altered and customized
// by any means PROVIDING the authors name and all copyright
// notices remain intact.
// THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED. USE IT AT YOUR OWN RISK. THE AUTHOR ACCEPTS NO
// LIABILITY FOR ANY DATA DAMAGE/LOSS THAT THIS PRODUCT MAY CAUSE.
//-----------------------------------------------------------------------
using System.Text;
using System.Text.RegularExpressions;

namespace TinyPG.Compiler
{
    public class TerminalSymbol : Symbol
    {
        public Regex Expression;

        public TerminalSymbol()
            : this("Terminal_" + ++counter, "")
        { }

        public TerminalSymbol(string name)
            : this(name, "")
        { }

        public TerminalSymbol(string name, string pattern)
        {
            Name = name;

            // The pattern is a lot faster if the Regex starts on a \G anchor,
            // as per PR #5:  Since the scanner throws away any matches
            // that don't start at startpos anyway, there is no logical
            // difference between having the anchor and not having it - just
            // a speed difference.  BUT, be careful to only insert that
            // anchor if the value in the grammar file was actually a string
            // literal rather than a bit of code or a variable identifier
            // containing the pattern:
            int insertIndex = -1; // negative value is a flag indicating don't insert anything.
            string patternWithAnchor = pattern;
            if (pattern.StartsWith("@\"") && pattern.EndsWith("\"")) // string literal starting with @"
                insertIndex = 2;
            else if (pattern.StartsWith("\"") && pattern.EndsWith("\"")) // string literal starting with "
                insertIndex = 1;
            // If we are in a quote string of some sort, and it doesn't already start with an opening
            // anchor, add one and protect it with non-capturing grouping parentheses:
            if (insertIndex >= 0 && pattern.IndexOf("\\G") != insertIndex)
            {
                patternWithAnchor = pattern.Insert(insertIndex, "\\G(?:");
                patternWithAnchor = patternWithAnchor.Insert(patternWithAnchor.Length - 1, ")");
            }
            Expression = new Regex(patternWithAnchor, RegexOptions.Compiled);
        }

        public TerminalSymbol(string name, Regex expression)
        {
            Name = name;
            Expression = expression;
        }

        public override string PrintProduction()
        {
            return Helper.Outline(Name, 0, " -> " + Expression + ";", 4);
        }
    }
}
