TinyPG Changes for kOS
======================

TinyPG is a tool kOS makes use of to help built its kerboscript compiler.
It had to be slightly modified for use with kOS, and this fork of it on
GitHub exists to hold those slight modifications.

The modifications that have been added for kOS purposes are:

1.  Changed compiled regular expressions to uncompiled ones instead.
    (Sadly, the "*Any*" platform bans their use, so we have to take
    the performance hit of doing so.)
2.  Changed the technique used to read case-insensitive keywords so it
    doesn't depend on regular expressions to do it, which are pretty
    slow at doing it when uncompiled.
3.  We noticed that TinyPG uses your terminal token regular expressions
    in such a way that adding a regex caret '^' to the start of them
    will make the same exact logical rule, but execute it much faster.
    So we insert that caret into all TerminalSymbol regex patterns.

About TinyPG Itself
===================

Originally written by Herre Kuijpers.

It is an LL(1) recursive descent parser generator written in C# which can generate a scanner, parser, and parsetree file in either C# or VB code.

The original code and documentation can be found in the article ['A Tiny Parser Generator v1.2' on CodeProject](http://www.codeproject.com/Articles/28294/a-Tiny-Parser-Generator-v1-2
).
  
The source code is licensed under the [Code Project Open License (CPOL)
](http://www.codeproject.com/info/cpol10.aspx).


### Features & Fixes

These are the new features and fixes we have added to the original code:

 - Support for `[IgnoreCase]` flag on terminal symbols.
 - Syntax highlighting now supports `var` keyword.
 - `ParseError` now has correct line numbers.
 - Regex tool now updates live without flicker.
 - The IDE will now display the error line number in the output.
 - Production rules without a code block will by default evaluate their sub-rules.
 - New `[FileAndLine]` flag for redefining the file and line number reported in errors.
 - IDE now uses C# 3.x compiler when testing the generated parser code.
 - Command line building of parsers.
 - IDE expression evaluator now include line and column numbers in errors.
 - Unexpected token errors now display the offending character.
 - We now always show the list of expected tokens on errors.


### Downloads

The latest source code can be found in [zip form here](https://github.com/SickheadGames/TinyPG/archive/master.zip).

The latest binaries can be found in the [build artifacts](http://teamcity.sickhead.com/viewType.html?buildTypeId=bt15&branch_project5=master&guest=1).
