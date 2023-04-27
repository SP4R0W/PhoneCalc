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

    Dictionary<int,types> ids = new Dictionary<int, types>(){
        {0,types.DEGREES},
        {1,types.RADIANS},
        {2,types.GRADS},
    };

    string textValue = "0";
    double convertedValue = 0;

    types type1 = types.DEGREES;
    types type2 = types.RADIANS;

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
        if ((type1 == types.DEGREES && type2 == types.DEGREES) ||
        (type1 == types.RADIANS && type2 == types.RADIANS) ||
        (type1 == types.GRADS && type2 == types.GRADS))
            convertedValue = Convert.ToDouble(textValue);
        else if (type1 == types.DEGREES && type2 == types.RADIANS)
            convertedValue = (Convert.ToDouble(textValue) * 0.017453);
        else if (type1 == types.RADIANS && type2 == types.DEGREES)
            convertedValue = (Convert.ToDouble(textValue) * 57.29578);
        else if (type1 == types.DEGREES && type2 == types.GRADS)
            convertedValue = (Convert.ToDouble(textValue) * 1.111111);
        else if (type1 == types.GRADS && type2 == types.DEGREES)
            convertedValue = (Convert.ToDouble(textValue) * 0.9);
        else if (type1 == types.RADIANS && type2 == types.GRADS)
            convertedValue = (Convert.ToDouble(textValue) * 63.661977);
        else if (type1 == types.GRADS && type2 == types.RADIANS)
            convertedValue = (Convert.ToDouble(textValue) * 0.015708);
    }

    void SelectCur1(int index)
    {
        type1 = ids[index];

        ConvertValue();
    }

    void SelectCur2(int index)
    {
        type2 = ids[index];

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
