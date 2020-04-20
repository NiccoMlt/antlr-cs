using System;

namespace antlr_cs
{
    // Because official ANTLR 4 C# target generates ISpreadsheetVisitor in SpreadsheetVisitor.cs file
    // we add our SpreadsheetVisitor class in a file named differently
    
    public class SpreadsheetVisitor : SpreadsheetBaseVisitor<double>
    {
        private readonly DataRepository _data = new DataRepository();

        public override double VisitNumericAtomExp(SpreadsheetParser.NumericAtomExpContext context)
        {            
            return double.Parse(context.NUMBER().GetText(), System.Globalization.CultureInfo.InvariantCulture);
        }

        public override double VisitIdAtomExp(SpreadsheetParser.IdAtomExpContext context)
        {
            var id = context.ID().GetText();

            return _data[id];
        }

        public override double VisitParenthesisExp(SpreadsheetParser.ParenthesisExpContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitMulDivExp(SpreadsheetParser.MulDivExpContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            double result = 0;

            if (context.ASTERISK() != null)
            {
                result = left * right;
            }
            if (context.SLASH() != null)
            {
                result = left / right;
            }

            return result;
        }
        
        public override double VisitAddSubExp(SpreadsheetParser.AddSubExpContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            double result = 0;

            if (context.PLUS() != null)
            {
                result = left + right;
            }
            if (context.MINUS() != null)
            {
                result = left - right;
            }

            return result;
        }

        public override double VisitPowerExp(SpreadsheetParser.PowerExpContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));

            var result = Math.Pow(left,right);            

            return result;
        }

        public override double VisitFunctionExp(SpreadsheetParser.FunctionExpContext context)
        {
            var name = context.NAME().GetText();

            var result = name switch
            {
                "sqrt" => Math.Sqrt(Visit(context.expression())),
                "log" => Math.Log10(Visit(context.expression())),
                _ => 0
            };

            return result;
        }
    }
}