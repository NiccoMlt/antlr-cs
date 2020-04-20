using System;
using System.IO;
using Antlr4.Runtime;

namespace antlr_cs_test
{
    public class SpreadsheetErrorListener : BaseErrorListener
    {                
        public string Symbol { get; private set; }
        public StringWriter Writer { get; private set; }

        public SpreadsheetErrorListener(StringWriter writer)
        {
            Writer = writer;
        }

        // if using ANTLR4cs, signature should be:
        // public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)

        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            Writer.WriteLine(msg);

            Symbol = offendingSymbol.Text;
        }
    }
}