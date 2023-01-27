using Godot;
using System;
using Godot.Collections;
using System.Globalization;

public class Angle_Panel : Panel
{
    public enum types {
        DEGREES,
        RADIANS,
        GRADS
    }

    private Dictionary<int,types> ids = new Dictionary<int, types>(){
        {0,types.DEGREES},
        {1,types.RADIANS},
        {2,types.GRADS},
    };

    private string textValue = "0";
    private double convertedValue = 0;

    private types type1 = types.DEGREES;
    private types type2 = types.RADIANS;

    private Label cur1Label;
    private Label cur2Label;

    public override void _Ready()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

        cur1Label = GetNode<Label>("Display_normal/label_cur");
        cur2Label = GetNode<Label>("Display_normal/label_convert");
    }

    public override void _Process(float delta)
    {
        cur1Label.Text = textValue;
        cur2Label.Text = Convert.ToString(convertedValue);
    }

    private void CalculateValue()
    {
        if ((type1 == types.DEGREES && type2 == types.DEGREES) ||
        (type1 == types.RADIANS && type2 == types.RADIANS) ||
        (type1 == types.GRADS && type2 == types.GRADS))
        {
            convertedValue = Convert.ToDouble(textValue);
        }
        else if (type1 == types.DEGREES && type2 == types.RADIANS)
        {
            convertedValue = (Convert.ToDouble(textValue) * 0.017453);
        }
        else if (type1 == types.RADIANS && type2 == types.DEGREES)
        {
            convertedValue = (Convert.ToDouble(textValue) * 57.29578);
        }
        else if (type1 == types.DEGREES && type2 == types.GRADS)
        {
            convertedValue = (Convert.ToDouble(textValue) * 1.111111);
        }
        else if (type1 == types.GRADS && type2 == types.DEGREES)
        {
            convertedValue = (Convert.ToDouble(textValue) * 0.9);
        }
        else if (type1 == types.RADIANS && type2 == types.GRADS)
        {
            convertedValue = (Convert.ToDouble(textValue) * 63.661977);
        }
        else if (type1 == types.GRADS && type2 == types.RADIANS)
        {
            convertedValue = (Convert.ToDouble(textValue) * 0.015708);
        }
    }

    private void SelectCur1(int index)
    {
        type1 = ids[index];

        CalculateValue();
    }

    private void SelectCur2(int index)
    {
        type2 = ids[index];

        CalculateValue();
    }

    private void CPressed()
    {
        textValue = "0";

        CalculateValue();
    }

    private void DelPressed()
    {
        textValue = StringExtensions.Substr(textValue,0,textValue.Length - 1);
        if (textValue == "")
            textValue = "0";

        CalculateValue();
    }

    private void DotPressed()
    {
        if (StringExtensions.Find(textValue,".") == -1)
        {
            textValue += ".";
        }
    }

    private void PlusMinPressed()
    {
        textValue = Convert.ToString(Convert.ToDouble(textValue) * -1);
    }

    private void AddNum(string number)
    {
        if (textValue == "0")
            textValue = "";

        textValue += number;
        CalculateValue();
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
}
