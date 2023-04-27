using Godot;
using System;
using Godot.Collections;
using System.Globalization;

public class Time_Panel : Panel
{
    public enum types {
        MICRO,
        MILI,
        SECONDS,
        MINUTES,
        HOURS,
        DAYS,
        WEEKS,
        YEARS
    }

    Dictionary<int,types> length_ids = new Dictionary<int, types>(){
        {0,types.MICRO},
        {1,types.MILI},
        {2,types.SECONDS},
        {3,types.MINUTES},
        {4,types.HOURS},
        {5,types.DAYS},
        {6,types.WEEKS},
        {7,types.YEARS}
    };

    string textValue = "0";
    double convertedValue = 0;

    types type1 = types.MICRO;
    types type2 = types.MILI;

    Label cur1Label;
    Label cur2Label;

    // Hard coded values for converting
    Dictionary<types,Dictionary<types,double>> length_values = new Dictionary<types, Dictionary<types, double>>(){
        {types.MICRO,new Dictionary<types, double>(){
            {types.MICRO,1},
            {types.MILI,0.001},
            {types.SECONDS,0.000001},
            {types.MINUTES,0.000000016666667},
            {types.HOURS,0.000000000277778},
            {types.DAYS,0.000000000011574},
            {types.WEEKS,0.000000000001653},
            {types.YEARS,0.000000000000032},
        }},
        {types.MILI,new Dictionary<types, double>(){
            {types.MICRO,1000},
            {types.MILI,1},
            {types.SECONDS,0.001},
            {types.MINUTES,0.000017},
            {types.HOURS,0.000000277777778},
            {types.DAYS,0.000000011574074},
            {types.WEEKS,0.000000001653439},
            {types.YEARS,0.000000000031688},
        }},
        {types.SECONDS,new Dictionary<types, double>(){
            {types.MICRO,1_000_000},
            {types.MILI,1000},
            {types.SECONDS,1},
            {types.MINUTES,0.016667},
            {types.HOURS,0.000278},
            {types.DAYS,0.000012},
            {types.WEEKS,0.000002},
            {types.YEARS,0.000000031688088},
        }},
        {types.MINUTES,new Dictionary<types, double>(){
            {types.MICRO,60_000_000},
            {types.MILI,60_000},
            {types.SECONDS,60},
            {types.MINUTES,1},
            {types.HOURS,0.016667},
            {types.DAYS,0.000694},
            {types.WEEKS,0.000099},
            {types.YEARS,0.000002},
        }},
        {types.HOURS,new Dictionary<types, double>(){
            {types.MICRO,3_600_000_000},
            {types.MILI,3_600_000},
            {types.SECONDS,3600},
            {types.MINUTES,60},
            {types.HOURS,1},
            {types.DAYS,0.041667},
            {types.WEEKS,0.005952},
            {types.YEARS,0.000114},
        }},
        {types.DAYS,new Dictionary<types, double>(){
            {types.MICRO,86_400_000_000},
            {types.MILI,86_400_000},
            {types.SECONDS,86_400},
            {types.MINUTES,1440},
            {types.HOURS,24},
            {types.DAYS,1},
            {types.WEEKS,0.142857},
            {types.YEARS,0.002738},
        }},
        {types.WEEKS,new Dictionary<types, double>(){
            {types.MICRO,604_800_000_000},
            {types.MILI,604_800_000},
            {types.SECONDS,604_800},
            {types.MINUTES,10_080},
            {types.HOURS,168},
            {types.DAYS,7},
            {types.WEEKS,1},
            {types.YEARS,0.019165},
        }},
        {types.YEARS,new Dictionary<types, double>(){
            {types.MICRO,31_557_600_000_000},
            {types.MILI,31_557_600_000},
            {types.SECONDS,31_557_600},
            {types.MINUTES,525_960},
            {types.HOURS,8766},
            {types.DAYS,365.25},
            {types.WEEKS,52.17857},
            {types.YEARS,1},
        }},
    };

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
        double value = length_values[type1][type2];
        convertedValue = Convert.ToDouble(textValue) * value;
    }

    void SelectCur1(int index)
    {
        type1 = length_ids[index];

        CalculateValue();
    }

    void SelectCur2(int index)
    {
        type2 = length_ids[index];

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