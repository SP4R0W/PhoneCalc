using Godot;
using System;
using Godot.Collections;
using System.Globalization;

public class Power_Panel : Panel
{
    public enum types {
        WATS,
        KILOWATS,
        HP,
        FEET,
        BTU
    }

    Dictionary<int,types> ids = new Dictionary<int, types>(){
        {0,types.WATS},
        {1,types.KILOWATS},
        {2,types.HP},
        {3,types.FEET},
        {4,types.BTU}
    };

    // Hard coded values for converting
    Dictionary<types,Dictionary<types,double>> values = new Dictionary<types, Dictionary<types, double>>(){
        {types.WATS,new Dictionary<types, double>(){
            {types.WATS,1},
            {types.KILOWATS,0.001},
            {types.HP,0.001341},
            {types.FEET,44.25373},
            {types.BTU,0.056869}
        }},
        {types.KILOWATS,new Dictionary<types, double>(){
            {types.WATS,1000},
            {types.KILOWATS,1},
            {types.HP,1.341022},
            {types.FEET,44_253.73},
            {types.BTU,56.86902}
        }},
        {types.HP,new Dictionary<types, double>(){
            {types.WATS,745.6999},
            {types.KILOWATS,0.7457},
            {types.HP,1},
            {types.FEET,33_000},
            {types.BTU,42.40722}
        }},
        {types.FEET,new Dictionary<types, double>(){
            {types.WATS,0.022597},
            {types.KILOWATS,0.000023},
            {types.HP,0.00003},
            {types.FEET,1},
            {types.BTU,0.001285}
        }},
        {types.BTU,new Dictionary<types, double>(){
            {types.WATS,17.58427},
            {types.KILOWATS,0.017584},
            {types.HP,0.023581},
            {types.FEET,778.1694},
            {types.BTU,1}
        }},
    };

    string textValue = "0";
    double convertedValue = 0;

    types type1 = types.WATS;
    types type2 = types.KILOWATS;

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

    void CalculateValue()
    {
        double value = values[type1][type2];
        convertedValue = Convert.ToDouble(textValue) * value;
    }

    void SelectCur1(int index)
    {
        type1 = ids[index];

        CalculateValue();
    }

    void SelectCur2(int index)
    {
        type2 = ids[index];

        CalculateValue();
    }

    void CPressed()
    {
        textValue = "0";

        CalculateValue();
    }

    void DelPressed()
    {
        textValue = StringExtensions.Substr(textValue,0,textValue.Length - 1);
        if (textValue == "")
            textValue = "0";

        CalculateValue();
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
        CalculateValue();
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
