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

            // The pattern is a lot faster if the Regex starts on a caret,
            // as per PR #4:  Since the scanner throws away any matches
            // that don't start at index zero anyway, there is no logical
            // difference between having the caret and not having it - just
            // a speed difference.  BUT, be careful to only insert that
            // caret if the value in the grammar file was actually a string
            // literal rather than a bit of code or a variable identifier
            // containing the pattern:
            int insertIndex = 0;
            string patternWithCaret = pattern;
            if (pattern.StartsWith("@\"")) // string literal starting with @"
                insertIndex = 2;
            else if (pattern.StartsWith("\"")) // string literal starting with "
                insertIndex = 1;
            if (pattern[insertIndex] != '^')
                patternWithCaret = pattern.Insert(insertIndex, "^");

            Expression = new Regex(patternWithCaret, RegexOptions.Compiled);
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
