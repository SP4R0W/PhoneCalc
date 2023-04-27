using Godot;
using System;
using Godot.Collections;
using System.Globalization;

public class Science_panel : Panel
{
    string num1 = "0";
    string num2 = "0";
    string result = "0";

    enum numbers {
        ONE,
        TWO,
        RESULT
    }

    enum operations {
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

    Dictionary<operations,string> operation_signs = new Dictionary<operations, string>(){
        {operations.ADD,"+"},
        {operations.SUBTRACT,"-"},
        {operations.MULTIPLY,"x"},
        {operations.DIVIDE,"/"},
        {operations.MOD,"remainder"},
	    {operations.ROOT,"root of degree"},
	    {operations.POWER,"^"},
        {operations.LOG,"log base"},
    };

    numbers current_num = numbers.ONE;
    operations current_operation = operations.NONE;

    Label label_action;
    Label label_num;
    Random rnd;

    public override void _Ready()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

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

    void ResetToDefault()
    {
        num1 = "0";
        num2 = "0";
        result = "0";

        current_num = numbers.ONE;
        current_operation = operations.NONE;
    }

    string GetResultAsFirst()
    {
        string res = result;
        ResetToDefault();

        return res;
    }

    string DoEquasion()
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

    void AddNum(string number)
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

    void SetEquasion(operations operation)
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
            num1 = GetResultAsFirst();

        current_operation = operation;
        current_num = numbers.TWO;
    }

    void CEPressed()
    {
        if (current_num == numbers.ONE)
            num1 = "0";
        else if (current_num == numbers.TWO)
            num2 = "0";
        else
            ResetToDefault();
    }

    void CPressed()
    {
        ResetToDefault();
    }

    void DelPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

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

    void RandPressed()
    {
        if (current_num == numbers.RESULT)
            ResetToDefault();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(rnd.NextDouble());
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(rnd.NextDouble());
    }

    void PIPressed()
    {
        if (current_num == numbers.RESULT)
            ResetToDefault();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.PI);
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.PI);
    }

    void EPressed()
    {
        if (current_num == numbers.RESULT)
            ResetToDefault();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.E);
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.E);
    }

    void ZeroPressed()
    {
        AddNum("0");
    }

    void OnePressed()
    {
        AddNum("1");
    }

    void TwoPressed()
    {
        AddNum("2");
    }

    void ThreePressed()
    {
        AddNum("3");
    }

    void FourPressed()
    {
        AddNum("4");
    }

    void FivePressed()
    {
        AddNum("5");
    }

    void SixPressed()
    {
        AddNum("6");
    }

    void SevenPressed()
    {
        AddNum("7");
    }

    void EightPressed()
    {
        AddNum("8");
    }

    void NinePressed()
    {
        AddNum("9");
    }

    void DotPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
        {
            if (StringExtensions.Find(num1,".") == -1)
                num1 += ".";
        }
        else if (current_num == numbers.TWO)
        {
            if (StringExtensions.Find(num2,".") == -1)
                num2 += ".";
        }
    }

    void PlusMinPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Convert.ToDouble(num1) * -1);
        else if (current_num == numbers.TWO)
        {
            num2 = Convert.ToString(Convert.ToDouble(num2) * -1);
        }
    }

    void EqlPressed()
    {
        if (current_num == numbers.ONE)
            return;

        current_num = numbers.RESULT;
        result = DoEquasion();
    }

    void AddPressed()
    {
        SetEquasion(operations.ADD);
    }

    void SubPressed()
    {
        SetEquasion(operations.SUBTRACT);
    }

    void MulPressed()
    {
        SetEquasion(operations.MULTIPLY);
    }

    void DivPressed()
    {
        SetEquasion(operations.DIVIDE);
    }

    void ModPressed()
    {
        SetEquasion(operations.MOD);
    }

    void PowPressed()
    {
        SetEquasion(operations.POWER);
    }

    void RootPressed()
    {
        SetEquasion(operations.ROOT);
    }

    void LogPressed()
    {
        SetEquasion(operations.LOG);
    }

    void AbsPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Abs(Convert.ToDouble(num1)));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Abs(Convert.ToDouble(num2)));
    }

    void NPressed()
    {
        // Factorials

        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
        {
            if (Convert.ToDouble(num1) < 0)
                return;

            double a = 1;
            double fact = Convert.ToDouble(num1);

            for (int i = 1;i<=fact;i++)
                a *= i;

            num1 = Convert.ToString(a);
        }
        else if (current_num == numbers.TWO)
        {
            if (Convert.ToDouble(num2) < 0)
                return;

            double a = 1;
            double fact = Convert.ToDouble(num2);

            for (int i = 1;i<=fact;i++)
                a *= i;

            num2 = Convert.ToString(a);
        }
    }

    void Pow2Pressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(Convert.ToDouble(num1),2));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(Convert.ToDouble(num2),2));
    }

    void Pow3Pressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(Convert.ToDouble(num1),3));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(Convert.ToDouble(num2),3));
    }

    void DivXPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(1/Convert.ToDouble(num1));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(1/Convert.ToDouble(num2));
    }

    void CqrtPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(Convert.ToDouble(num1),(double) 1/3));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(Convert.ToDouble(num2),(double) 1/3));
    }

    void SqrtPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

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

    void TwoPowPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(2,Convert.ToDouble(num1)));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(2,Convert.ToDouble(num2)));
    }

    void TenPowPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(10,Convert.ToDouble(num1)));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(10,Convert.ToDouble(num2)));
    }

    void Log10Pressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

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

    void LogEPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

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

    void EXPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(Math.E,Convert.ToDouble(num1)));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(Math.E,Convert.ToDouble(num2)));
    }

    void RoundPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Round(Convert.ToDouble(num1)));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Round(Convert.ToDouble(num2)));
    }

    void CeilPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Ceiling(Convert.ToDouble(num1)));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Ceiling(Convert.ToDouble(num2)));
    }

    void FloorPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.RESULT)
            num1 = GetResultAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Floor(Convert.ToDouble(num1)));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Floor(Convert.ToDouble(num2)));
    }
}
