# ANTLR4 Tutorial - CSharp Project

Project set up following [C# Setup section of the ANTLR Mega Tutorial](https://tomassetti.me/antlr-mega-tutorial/#csharp-setup).
Reference to original repo at: <https://github.com/unosviluppatore/antlr-mega-tutorial>.

Differently from what is used in [official repo](https://github.com/unosviluppatore/antlr-mega-tutorial/tree/master/antlr-csharp), this project uses official ANTLR 4 runtime instead of [antlr4cs](https://github.com/tunnelvisionlabs/antlr4cs).
It requires the execution of the Java-based ANTLR tool to generate parser and lexer code:

```bash
antlr4 -Dlanguage=CSharp Spreadsheet.g4
```
