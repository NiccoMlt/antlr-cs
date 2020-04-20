using System;
using System.IO;
using antlr_cs;
using Antlr4.Runtime;
using Xunit;

namespace antlr_cs_test
{
    public class SpreadsheetTests
    {
        private SpreadsheetParser parser;
        private SpreadsheetLexer lexer;
        private SpreadsheetErrorListener errorListener;

        // if you need to output something during tests
        // private readonly ITestOutputHelper output;
        // 
        // public SpreadsheetTests(ITestOutputHelper output)
        // {
        //     this.output = output;
        // }

        private void Setup(string input)
        {
            var inputStream = new AntlrInputStream(input);
            lexer = new SpreadsheetLexer(inputStream);
            var commonTokenStream = new CommonTokenStream(lexer);
            parser = new SpreadsheetParser(commonTokenStream);            

            var writer = new StringWriter();
            errorListener = new SpreadsheetErrorListener(writer);
            lexer.RemoveErrorListeners();
            //markupLexer.addErrorListener(errorListener);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(errorListener);            
        }
        
        /// <summary>
        /// it checks that the correct tokens are selected. 
        /// </summary>
        [Fact]
        public void TestExpressionPow()
        {
            Setup("5^3^2");

            var context = parser.expression() as SpreadsheetParser.PowerExpContext;

            var ts = (CommonTokenStream)parser.InputStream;   

            Assert.Equal(SpreadsheetLexer.NUMBER, ts.Get(0).Type);
            // we didn’t explicitly create one for the ‘^’ symbol so T__2 got automatically created for us
            Assert.Equal(SpreadsheetLexer.T__2, ts.Get(1).Type);
            Assert.Equal(SpreadsheetLexer.NUMBER, ts.Get(2).Type);
            Assert.Equal(SpreadsheetLexer.T__2, ts.Get(3).Type);
            Assert.Equal(SpreadsheetLexer.NUMBER, ts.Get(4).Type); 
        }

        [Fact]
        public void TestVisitPowerExp()
        {
            Setup("4^3^2");

            var context = parser.expression() as SpreadsheetParser.PowerExpContext;

            var visitor = new SpreadsheetVisitor();
            // we visit our test node and get the results
            var result = visitor.VisitPowerExp(context);

            Assert.Equal(double.Parse("262144"), result);
        }
        
        // [...]
        
        [Fact]
        public void TestWrongVisitFunctionExp()
        {
            Setup("logga(100)");

            var context = parser.expression() as SpreadsheetParser.FunctionExpContext;
    
            var visitor = new SpreadsheetVisitor();
            var result = visitor.VisitFunctionExp(context);

            var ts = (CommonTokenStream) parser.InputStream;

            // When we check for the wrong function the parser actually works,
            // because indeed "logga" is syntactically valid as a function name,
            // but it’s not semantically correct.
            Assert.Equal(SpreadsheetLexer.NAME, ts.Get(0).Type);
            Assert.Null(errorListener.Symbol);
            // The function "logga" doesn't exist, so our program doesn't know what to do with it.
            // So when we visit it we get 0 as a result. 
            Assert.Equal(0, result);
        }

        [Fact]
        public void TestCompleteExp()
        {
            Setup("log(5+6*7/8)");

            SpreadsheetParser.ExpressionContext context = parser.expression();

            SpreadsheetVisitor visitor = new SpreadsheetVisitor();
            double result = visitor.Visit(context);

            // we make sure that the correct format is selected, because different countries use different symbols as the decimal mark
            Assert.Equal("1.01072386539177", result.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US").NumberFormat));            
        }
    }
}