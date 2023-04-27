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

    Dictionary<int,types> temperature_ids = new Dictionary<int, types>(){
        {0,types.CELSIUS},
        {1,types.FAHRENHEIT},
        {2,types.KELVIN},
    };

    string textValue = "0";
    double convertedValue = 0;

    types type1 = types.CELSIUS;
    types type2 = types.FAHRENHEIT;

    Label cur1Label;
    Label cur2Label;

    public override void _Ready()
    {
        // This is enforcing . as decimal points. In some regions, the system may expect a comma and thus break our entire app when calculating numbers with decimal points.
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

        cur1Label = GetNode<Label>("Display_normal/label_cur");
        cur2Label = GetNode<Label>("Display_normal/label_convert");
    }

    public override void _Process(float delta)
    {
        cur1Label.Text = textValue;
        cur2Label.Text = Convert.ToString(convertedValue);
    }

    void ConvertValue()
    {
        if ((type1 == types.CELSIUS && type2 == types.CELSIUS) ||
        (type1 == types.FAHRENHEIT && type2 == types.FAHRENHEIT) ||
        (type1 == types.KELVIN && type2 == types.KELVIN))
            convertedValue = Convert.ToDouble(textValue);
        else if (type1 == types.CELSIUS && type2 == types.FAHRENHEIT)
            convertedValue = (Convert.ToDouble(textValue) * 1.8) + 32;
        else if (type1 == types.FAHRENHEIT && type2 == types.CELSIUS)
            convertedValue = (Convert.ToDouble(textValue) - 32) / 1.8;
        else if (type1 == types.CELSIUS && type2 == types.KELVIN)
            convertedValue = Convert.ToDouble(textValue) + 273.15;
        else if (type1 == types.KELVIN && type2 == types.CELSIUS)
            convertedValue = Convert.ToDouble(textValue) - 273.15;
        else if (type1 == types.FAHRENHEIT && type2 == types.KELVIN)
            convertedValue = (Convert.ToDouble(textValue) + 459.67) * ((double) 5/9);
        else if (type1 == types.KELVIN && type2 == types.FAHRENHEIT)
            convertedValue = (Convert.ToDouble(textValue) * ((double) 9/5)) - 459.67;
    }

    void SelectCur1(int index)
    {
        type1 = temperature_ids[index];

        ConvertValue();
    }

    void SelectCur2(int index)
    {
        type2 = temperature_ids[index];

        ConvertValue();
    }

    void CPressed()
    {
        textValue = "0";

        ConvertValue();
    }

    void DelPressed()
    {
        textValue = StringExtensions.Substr(textValue,0,textValue.Length - 1);
        if (textValue == "")
            textValue = "0";

        ConvertValue();
    }

    void DotPressed()
    {
        if (StringExtensions.Find(textValue,".") == -1)
            textValue += ".";
    }

    void PlusMinPressed()
    {
        textValue = Convert.ToString(Convert.ToDouble(textValue) * -1);
    }

    void AddNum(string number)
    {
        if (textValue == "0")
            textValue = "";

        textValue += number;
        ConvertValue();
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
}
