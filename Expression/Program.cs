namespace ExpressionProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GlobalSettings settings = GlobalSettings.getSettings();
            // GlobalSettings settings2 = new GlobalSettings();

            settings.programName = "this is my program";

            GlobalSettings settings2 = GlobalSettings.getSettings();

            Expression[] expressions = new Expression[10];

            expressions[0] = new Literal(5);
            expressions[1] = new ArithmeticExpresion(new Literal(2.5), '/', new Literal(0.5));
            expressions[2] = new ArithmeticExpresion(new Literal(3.5), '*', expressions[1]);
            expressions[3] = new ArithmeticExpresion(expressions[1], '-', expressions[2]);

            for (int i = 0; i < expressions.Length; i++) 
            {
                if (expressions[i] != null)
                {
                    Console.WriteLine($"{expressions[i].ToString()}={expressions[i].getValue()}");
                }
            }
        }
    }

    interface Expression
    {
        public abstract double getValue();
        public abstract string ToString(bool parenthesis);
    }

    class Literal : Expression
    {
        private double value;

        public Literal(double _value)
        {
            value = _value;
        }

        public double getValue()
        { return value; }

        public override string ToString()
        {
            return value.ToString();
        }

        public string ToString(bool parenthesis)
        {
            return ToString();
        }
    }

    class ArithmeticExpresion : Expression
    {
        Expression left, right;
        char op;

        public ArithmeticExpresion(Expression _left, char _op, Expression _right)
        { 
            left = _left;
            right = _right;
            op = _op;
        }

        public double getValue()
        { 
            double leftValue = left.getValue();
            double rightValue = right.getValue();

            switch (op)
            {
                case '+':
                    return leftValue + rightValue;
                case '-':
                    return leftValue - rightValue;
                case '*':
                    return leftValue * rightValue;
                case '/':
                    return leftValue / rightValue;
            }
            return 0;
        }

        public override string ToString()
        {
            return $"{left.ToString(true)}{op}{right.ToString(true)}";
        }

        public string ToString(bool parenthesis)
        {
            string ret="";
            if (parenthesis)
            {
                ret += "(";
            }
            ret += ToString();
            if (parenthesis)
            {
                ret += ")";
            }
            return ret;
        }
    }

    class GlobalSettings
    {
        private static GlobalSettings? globalSettings = null;

        public string programName {  get; set; }
        public static GlobalSettings getSettings()
        {
            if (globalSettings == null)
            {
                globalSettings = new GlobalSettings();
            }
            return globalSettings;
        }

        private GlobalSettings() 
        {
            programName = "program";
        }
    }
}
