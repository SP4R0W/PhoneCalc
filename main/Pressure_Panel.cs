using Godot;
using System;
using Godot.Collections;
using System.Globalization;

public class Pressure_Panel : Panel
{
    public enum types {
        ATMOSPHERE,
        BARS,
        KILOPASCALS,
        MILIMETERS,
        PASCALS,
        POUNDS,
    }

    Dictionary<int,types> ids = new Dictionary<int, types>(){
        {0,types.ATMOSPHERE},
        {1,types.BARS},
        {2,types.KILOPASCALS},
        {3,types.MILIMETERS},
        {4,types.PASCALS},
        {5,types.POUNDS},
    };

    // Hard coded values for converting
    Dictionary<types,Dictionary<types,double>> values = new Dictionary<types, Dictionary<types, double>>(){
        {types.ATMOSPHERE,new Dictionary<types, double>(){
            {types.ATMOSPHERE,1},
            {types.BARS,1.01325},
            {types.KILOPASCALS,101.325},
            {types.MILIMETERS,760.1275},
            {types.PASCALS,101_325},
            {types.POUNDS,14.69595},
        }},
        {types.BARS,new Dictionary<types, double>(){
            {types.ATMOSPHERE,0.986923},
            {types.BARS,1},
            {types.KILOPASCALS,100},
            {types.MILIMETERS,750.1875},
            {types.PASCALS,100_000},
            {types.POUNDS,14.50377},
        }},
        {types.KILOPASCALS,new Dictionary<types, double>(){
            {types.ATMOSPHERE,0.009869},
            {types.BARS,0.01},
            {types.KILOPASCALS,1},
            {types.MILIMETERS,7.501875},
            {types.PASCALS,1_000},
            {types.POUNDS,0.145038},
        }},
        {types.MILIMETERS,new Dictionary<types, double>(){
            {types.ATMOSPHERE,0.001316},
            {types.BARS,0.001333},
            {types.KILOPASCALS,0.1333},
            {types.MILIMETERS,1},
            {types.PASCALS,133.3},
            {types.POUNDS,0.019334},
        }},
        {types.PASCALS,new Dictionary<types, double>(){
            {types.ATMOSPHERE,0.00001},
            {types.BARS,0.00001},
            {types.KILOPASCALS,0.001},
            {types.MILIMETERS,0.007502},
            {types.PASCALS,1},
            {types.POUNDS,0.000145},
        }},
        {types.POUNDS,new Dictionary<types, double>(){
            {types.ATMOSPHERE,0.068046},
            {types.BARS,0.068948},
            {types.KILOPASCALS,6.894757},
            {types.MILIMETERS,51.72361},
            {types.PASCALS,6_894.757},
            {types.POUNDS,1},
        }},
    };

    string textValue = "0";
    double convertedValue = 0;

    types type1 = types.ATMOSPHERE;
    types type2 = types.BARS;

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
