using Godot;
using System;
using Godot.Collections;
using System.Globalization;

public class Mass_Panel : Panel
{
    public enum types {
        CARATS,
        GRAMS,
        KGS,
        TONS,
        OUNCES,
        POUNDS
    }

    Dictionary<int,types> length_ids = new Dictionary<int, types>(){
        {0,types.CARATS},
        {1,types.GRAMS},
        {2,types.KGS},
        {3,types.TONS},
        {4,types.OUNCES},
        {5,types.POUNDS},
    };

    string textValue = "0";
    double convertedValue = 0;

    types type1 = types.CARATS;
    types type2 = types.GRAMS;

    Label cur1Label;
    Label cur2Label;

    // Hard coded values for converting
    Dictionary<types,Dictionary<types,double>> length_values = new Dictionary<types, Dictionary<types, double>>(){
        {types.CARATS,new Dictionary<types, double>(){
            {types.CARATS,1},
            {types.GRAMS,0.2},
            {types.KGS,0.0002},
            {types.TONS,0.0000002},
            {types.OUNCES,0.007055},
            {types.POUNDS,0.000441},
        }},
        {types.GRAMS,new Dictionary<types, double>(){
            {types.CARATS,5},
            {types.GRAMS,1},
            {types.KGS,0.001},
            {types.TONS,0.000001},
            {types.OUNCES,0.035274},
            {types.POUNDS,0.002205},
        }},
        {types.KGS,new Dictionary<types, double>(){
            {types.CARATS,5_000},
            {types.GRAMS,1_000},
            {types.KGS,1},
            {types.TONS,0.001},
            {types.OUNCES,35.27396},
            {types.POUNDS,2.204623},
        }},
        {types.TONS,new Dictionary<types, double>(){
            {types.CARATS,5_000_000},
            {types.GRAMS,1_000_000},
            {types.KGS,1000},
            {types.TONS,1},
            {types.OUNCES,35_273.96},
            {types.POUNDS,2_204.623},
        }},
        {types.OUNCES,new Dictionary<types, double>(){
            {types.CARATS,141.7476},
            {types.GRAMS,28.34952},
            {types.KGS,0.02835},
            {types.TONS,0.000028},
            {types.OUNCES,1},
            {types.POUNDS,0.0625},
        }},
        {types.POUNDS,new Dictionary<types, double>(){
            {types.CARATS,2_267.962},
            {types.GRAMS,453.5924},
            {types.KGS,0.453592},
            {types.TONS,0.000454},
            {types.OUNCES,16},
            {types.POUNDS,1},
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
