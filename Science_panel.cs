using Godot;
using System;
using Godot.Collections;

public class Science_panel : Panel
{
    private string num1 = "0";
    private string num2 = "0";
    private string result = "0";

    private enum numbers {
        ONE,
        TWO,
        RESULT
    }

    private enum operations {
        ADD,
        SUBTRACT,
        MULTIPLY,
        DIVIDE,
        MOD,
        ROOT,
        POWER,
        LOG,
        NONE
    }

    private Dictionary<operations,string> operation_signs = new Dictionary<operations, string>(){
        {operations.ADD,"+"},
        {operations.SUBTRACT,"-"},
        {operations.MULTIPLY,"x"},
        {operations.DIVIDE,"/"},
        {operations.MOD,"reszta z dzielenia"},
	    {operations.ROOT,"pierwiastek stopnia"},
	    {operations.POWER,"^"},
        {operations.LOG,"logarytm o podstawie"},
    };

    private numbers current_num = numbers.ONE;
    private operations current_operation = operations.NONE;

    private Label label_action;
    private Label label_num;
    private Random rnd;

    public override void _Ready()
    {
        rnd = new Random();

        label_action = GetNode<Label>("Display_normal/label_action");
        label_num = GetNode<Label>("Display_normal/label_num");
    }

    public override void _Process(float delta)
    {
        if (current_num == numbers.ONE)
        {
            label_action.Text = "";
            label_num.Text = num1;
        }
        else if (current_num == numbers.TWO)
        {
            label_action.Text = String.Format("{0} {1}",num1,operation_signs[current_operation]);
            label_num.Text = num2;
        }
        else
        {
            label_action.Text = String.Format("{0} {1} {2} =",num1,operation_signs[current_operation],num2);
            label_num.Text = result;
        }
    }

    private void ResetToDefault()
    {
        num1 = "0";
        num2 = "0";
        result = "0";

        current_num = numbers.ONE;
        current_operation = operations.NONE;
    }

    private string GetResAsFirst()
    {
        string res = result;
        ResetToDefault();

        return res;
    }

    private string DoEquasion()
    {
        if (current_operation == operations.ADD)
            return Convert.ToString(Convert.ToDouble(num1) + Convert.ToDouble(num2));
        else if (current_operation == operations.SUBTRACT)
            return Convert.ToString(Convert.ToDouble(num1) - Convert.ToDouble(num2));
        else if (current_operation == operations.MULTIPLY)
            return Convert.ToString(Convert.ToDouble(num1) * Convert.ToDouble(num2));
        else if (current_operation == operations.DIVIDE)
            return Convert.ToString(Convert.ToDouble(num1) / Convert.ToDouble(num2));
        else if (current_operation == operations.MOD)
            return Convert.ToString(Convert.ToDouble(num1) % Convert.ToDouble(num2));
        else if (current_operation == operations.POWER)
            return Convert.ToString(Math.Pow(Convert.ToDouble(num1),Convert.ToDouble(num2)));
        else if (current_operation == operations.ROOT)
            return Convert.ToString(Math.Pow(Convert.ToDouble(num1),(double) 1/Convert.ToDouble(num2)));
        else if (current_operation == operations.LOG)
            return Convert.ToString(Math.Log(Convert.ToDouble(num1),Convert.ToDouble(num2)));


        return "0";
    }

    private void AddNum(string number)
    {
        if (current_num == numbers.RESULT)
            ResetToDefault();

        if (label_num.Text.Length > 15)
            return;

        if (current_num == numbers.ONE)
        {
            if (num1 == "0")
                num1 = "";

            num1 += number;
        }
        else if (current_num == numbers.TWO)
        {
            if (num2 == "0")
                num2 = "";

            num2 += number;
        }
    }

    private void SetEquasion(operations operation)
    {
        if (current_num == numbers.TWO)
        {
            var res = DoEquasion();
            ResetToDefault();

            num1 = res;
            current_operation = operation;
            current_num = numbers.TWO;
        }
        else if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        current_operation = operation;
        current_num = numbers.TWO;
    }

    private void CEPressed()
    {
        if (current_num == numbers.ONE)
            num1 = "0";
        else if (current_num == numbers.TWO)
            num2 = "0";
        else
            ResetToDefault();
    }

    private void CPressed()
    {
        ResetToDefault();
    }

    private void DelPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
        {
            num1 = StringExtensions.Substr(num1,0,num1.Length - 1);
            if (num1 == "")
                num1 = "0";
        }
        else if (current_num == numbers.TWO)
        {
            num2 = StringExtensions.Substr(num2,0,num2.Length - 1);
            if (num2 == "")
                num2 = "0";
        }
    }

    private void RandPressed()
    {
        if (current_num == numbers.RESULT)
            ResetToDefault();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(rnd.NextDouble());
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(rnd.NextDouble());
    }

    private void PIPressed()
    {
        if (current_num == numbers.RESULT)
            ResetToDefault();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.PI);
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.PI);
    }

    private void EPressed()
    {
        if (current_num == numbers.RESULT)
            ResetToDefault();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.E);
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.E);
    }

    private void ZeroPressed()
    {
        AddNum("0");
    }

    private void OnePressed()
    {
        AddNum("1");
    }

    private void TwoPressed()
    {
        AddNum("2");
    }

    private void ThreePressed()
    {
        AddNum("3");
    }

    private void FourPressed()
    {
        AddNum("4");
    }

    private void FivePressed()
    {
        AddNum("5");
    }

    private void SixPressed()
    {
        AddNum("6");
    }

    private void SevenPressed()
    {
        AddNum("7");
    }

    private void EightPressed()
    {
        AddNum("8");
    }

    private void NinePressed()
    {
        AddNum("9");
    }

    private void DotPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
        {
            if (StringExtensions.Find(num1,",") == -1)
                num1 += ",";
        }
        else if (current_num == numbers.TWO)
        {
            if (StringExtensions.Find(num2,",") == -1)
                num2 += ",";
        }
    }

    private void PlusMinPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Convert.ToDouble(num1) * -1);
        else if (current_num == numbers.TWO)
        {
            num2 = Convert.ToString(Convert.ToDouble(num2) * -1);
        }
    }

    private void EqlPressed()
    {
        if (current_num == numbers.ONE)
            return;

        current_num = numbers.RESULT;
        result = DoEquasion();
    }

    private void AddPressed()
    {
        SetEquasion(operations.ADD);
    }

    private void SubPressed()
    {
        SetEquasion(operations.SUBTRACT);
    }

    private void MulPressed()
    {
        SetEquasion(operations.MULTIPLY);
    }

    private void DivPressed()
    {
        SetEquasion(operations.DIVIDE);
    }

    private void ModPressed()
    {
        SetEquasion(operations.MOD);
    }

    private void PowPressed()
    {
        SetEquasion(operations.POWER);
    }

    private void RootPressed()
    {
        SetEquasion(operations.ROOT);
    }

    private void LogPressed()
    {
        SetEquasion(operations.LOG);
    }

    private void AbsPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Abs(Convert.ToDouble(num1)));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Abs(Convert.ToDouble(num2)));
    }

    private void NPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
        {
            if (Convert.ToDouble(num1) < 0)
                return;

            double a = 1;
            double fact = Convert.ToDouble(num1);

            for (int i = 1;i<=fact;i++)
            {
                a *= i;
            }

            num1 = Convert.ToString(a);
        }
        else if (current_num == numbers.TWO)
        {
            if (Convert.ToDouble(num2) < 0)
                return;

            double a = 1;
            double fact = Convert.ToDouble(num2);

            for (int i = 1;i<=fact;i++)
            {
                a *= i;
            }

            num2 = Convert.ToString(a);
        }
    }

    private void Pow2Pressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(Convert.ToDouble(num1),2));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(Convert.ToDouble(num2),2));
    }

    private void Pow3Pressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(Convert.ToDouble(num1),3));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(Convert.ToDouble(num2),3));
    }

    private void DivXPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(1/Convert.ToDouble(num1));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(1/Convert.ToDouble(num2));
    }

    private void CqrtPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(Convert.ToDouble(num1),(double) 1/3));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(Convert.ToDouble(num2),(double) 1/3));
    }

    private void SqrtPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
        {
            if (Convert.ToDouble(num1) < 0)
                return;

            num1 = Convert.ToString(Math.Sqrt(Convert.ToDouble(num1)));
        }
        else if (current_num == numbers.TWO)
        {
            if (Convert.ToDouble(num1) < 0)
                return;

            num2 = Convert.ToString(Math.Sqrt(Convert.ToDouble(num2)));
        }
    }

    private void TwoPowPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(2,Convert.ToDouble(num1)));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(2,Convert.ToDouble(num2)));
    }

    private void TenPowPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(10,Convert.ToDouble(num1)));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(10,Convert.ToDouble(num2)));
    }

    private void Log10Pressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
        {
            if (Convert.ToDouble(num1) <= 0)
                return;

            num1 = Convert.ToString(Math.Log10(Convert.ToDouble(num1)));
        }
        else if (current_num == numbers.TWO)
        {
            if (Convert.ToDouble(num1) <= 0)
                return;

            num2 = Convert.ToString(Math.Log10(Convert.ToDouble(num2)));
        }
    }

    private void LogEPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
        {
            if (Convert.ToDouble(num1) <= 0)
                return;

            num1 = Convert.ToString(Math.Log(Convert.ToDouble(num1)));
        }
        else if (current_num == numbers.TWO)
        {
            if (Convert.ToDouble(num1) <= 0)
                return;

            num2 = Convert.ToString(Math.Log(Convert.ToDouble(num2)));
        }
    }

    private void EXPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(Math.E,Convert.ToDouble(num1)));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(Math.E,Convert.ToDouble(num2)));
    }

    private void RoundPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Round(Convert.ToDouble(num1)));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Round(Convert.ToDouble(num2)));
    }

    private void CeilPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
        {
            var numDecimal = Convert.ToDouble(String.Concat("0,",num1.Split(",")[1]));
            var add = 1 - numDecimal;
            num1 = Convert.ToString(Convert.ToDouble(num1) + add);
        }
        else if (current_num == numbers.TWO)
        {
            var numDecimal = Convert.ToDouble(String.Concat("0,",num2.Split(",")[1]));
            var add = 1 - numDecimal;
            num2 = Convert.ToString(Convert.ToDouble(num2) + add);
        }
    }

    private void FloorPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
        {
            var numDecimal = Convert.ToDouble(String.Concat("0,",num1.Split(",")[1]));
            num1 = Convert.ToString(Convert.ToDouble(num1) - numDecimal);
        }
        else if (current_num == numbers.TWO)
        {
            var numDecimal = Convert.ToDouble(String.Concat("0,",num2.Split(",")[1]));
            num2 = Convert.ToString(Convert.ToDouble(num2) - numDecimal);
        }
    }
}
