using Godot;
using System;
using Godot.Collections;
using System.Globalization;

public class Temperature_Panel : Panel
{
    public enum types {
        CELSIUS,
        FAHRENHEIT,
        KELVIN
    }

    private Dictionary<int,types> temperature_ids = new Dictionary<int, types>(){
        {0,types.CELSIUS},
        {1,types.FAHRENHEIT},
        {2,types.KELVIN},
    };

    private string textValue = "0";
    private double convertedValue = 0;

    private types type1 = types.CELSIUS;
    private types type2 = types.FAHRENHEIT;

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
        if ((type1 == types.CELSIUS && type2 == types.CELSIUS) ||
        (type1 == types.FAHRENHEIT && type2 == types.FAHRENHEIT) ||
        (type1 == types.KELVIN && type2 == types.KELVIN))
        {
            convertedValue = Convert.ToDouble(textValue);
        }
        else if (type1 == types.CELSIUS && type2 == types.FAHRENHEIT)
        {
            convertedValue = (Convert.ToDouble(textValue) * 1.8) + 32;
        }
        else if (type1 == types.FAHRENHEIT && type2 == types.CELSIUS)
        {
            convertedValue = (Convert.ToDouble(textValue) - 32) / 1.8;
        }
        else if (type1 == types.CELSIUS && type2 == types.KELVIN)
        {
            convertedValue = Convert.ToDouble(textValue) + 273.15;
        }
        else if (type1 == types.KELVIN && type2 == types.CELSIUS)
        {
            convertedValue = Convert.ToDouble(textValue) - 273.15;
        }
        else if (type1 == types.FAHRENHEIT && type2 == types.KELVIN)
        {
            convertedValue = (Convert.ToDouble(textValue) + 459.67) * ((double) 5/9);
        }
        else if (type1 == types.KELVIN && type2 == types.FAHRENHEIT)
        {
            convertedValue = (Convert.ToDouble(textValue) * ((double) 9/5)) - 459.67;
        }
    }

    private void SelectCur1(int index)
    {
        type1 = temperature_ids[index];

        CalculateValue();
    }

    private void SelectCur2(int index)
    {
        type2 = temperature_ids[index];

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
