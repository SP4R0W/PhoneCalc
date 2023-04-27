using Godot;
using System;
using Godot.Collections;
using System.Globalization;

public sealed class Normal_panel : Panel
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
        NONE
    }

    Dictionary<operations,string> operation_signs = new Dictionary<operations, string>(){
        {operations.ADD,"+"},
        {operations.SUBTRACT,"-"},
        {operations.MULTIPLY,"x"},
        {operations.DIVIDE,"/"}
    };

    numbers current_num = numbers.ONE;
    operations current_operation = operations.NONE;

    Label label_action;
    Label label_num;

    public override void _Ready()
    {
        // This is enforcing . as decimal points. In some regions, the system may expect a comma and thus break our entire app when calculating numbers with decimal points.
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

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

    string GetResAsFirst()
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
            num1 = GetResAsFirst();

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
            num1 = GetResAsFirst();

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
            num1 = GetResAsFirst();

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

    void SqrPressed()
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

    void PowPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(Math.Pow(Convert.ToDouble(num1),2));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Math.Pow(Convert.ToDouble(num2),2));
    }

    void DivXPressed()
    {
        if (current_num == numbers.RESULT)
            num1 = GetResAsFirst();

        if (current_num == numbers.ONE)
            num1 = Convert.ToString(1/Convert.ToDouble(num1));
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(1/Convert.ToDouble(num2));
    }

    void PercentagePressed()
    {
        if (current_num == numbers.ONE)
            num1 = "0";
        else if (current_num == numbers.TWO)
            num2 = Convert.ToString(Convert.ToDouble(num1) * (Convert.ToDouble(num2)/100));
        else if (current_num == numbers.RESULT)
        {
            string res = GetResAsFirst();

            num1 = Convert.ToString(Convert.ToDouble(res) * (Convert.ToDouble(res)/100));
        }
    }
}
