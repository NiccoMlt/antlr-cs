using System;
using Antlr4.Runtime;

namespace antlr_cs
{
    class Program
    {
        public static void Main()
        {
            const string input = "log(10 + A1 * 35 + (5.4 - 7.4))";

            // C# uses Antlr4.Runtime.AntlrInputStream instead of Java's org.antlr.v4.runtime.CharStream
            var inputStream = new AntlrInputStream(input);
            var spreadsheetLexer = new SpreadsheetLexer(inputStream);            
            var commonTokenStream = new CommonTokenStream(spreadsheetLexer);
            var spreadsheetParser = new SpreadsheetParser(commonTokenStream);

            var expressionContext = spreadsheetParser.expression();
            var visitor = new SpreadsheetVisitor();

            // we output on the screen the result of our visitor
            Console.WriteLine(visitor.Visit(expressionContext));
        }
    }    
}