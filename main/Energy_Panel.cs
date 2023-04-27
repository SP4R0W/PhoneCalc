using Godot;
using System;
using Godot.Collections;
using System.Globalization;

public class Energy_Panel : Panel
{
    public enum types {
        JOULES,
        KILOJOULES,
        CALS,
        KCALS,
        POUNDS,
        BTU
    }

    Dictionary<int,types> length_ids = new Dictionary<int, types>(){
        {0,types.JOULES},
        {1,types.KILOJOULES},
        {2,types.CALS},
        {3,types.KCALS},
        {4,types.POUNDS},
        {5,types.BTU},
    };

    string textValue = "0";
    double convertedValue = 0;

    types type1 = types.JOULES;
    types type2 = types.KILOJOULES;

    Label cur1Label;
    Label cur2Label;

    // Hard coded values for converting
    Dictionary<types,Dictionary<types,double>> length_values = new Dictionary<types, Dictionary<types, double>>(){
        {types.JOULES,new Dictionary<types, double>(){
            {types.JOULES,1},
            {types.KILOJOULES,0.001},
            {types.CALS,0.239006},
            {types.KCALS,0.000239},
            {types.POUNDS,0.737562},
            {types.BTU,0.000948},
        }},
        {types.KILOJOULES,new Dictionary<types, double>(){
            {types.JOULES,1_000},
            {types.KILOJOULES,1},
            {types.CALS,239.0057},
            {types.KCALS,0.239006},
            {types.POUNDS,737.5621},
            {types.BTU,0.947817},
        }},
        {types.CALS,new Dictionary<types, double>(){
            {types.JOULES,4.184},
            {types.KILOJOULES,0.004184},
            {types.CALS,1},
            {types.KCALS,0.001},
            {types.POUNDS,3.08596},
            {types.BTU,0.003966},
        }},
        {types.KCALS,new Dictionary<types, double>(){
            {types.JOULES,4_184},
            {types.KILOJOULES,4.184},
            {types.CALS,1000},
            {types.KCALS,1},
            {types.POUNDS,3_085.96},
            {types.BTU,3.965666},
        }},
        {types.POUNDS,new Dictionary<types, double>(){
            {types.JOULES,1.355818},
            {types.KILOJOULES,0.001356},
            {types.CALS,0.324048},
            {types.KCALS,0.000324},
            {types.POUNDS,1},
            {types.BTU,0.001285},
        }},
        {types.BTU,new Dictionary<types, double>(){
            {types.JOULES,1_055.056},
            {types.KILOJOULES,1.055056},
            {types.CALS,252.1644},
            {types.KCALS,0.252164},
            {types.POUNDS,778.1694},
            {types.BTU,1},
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
