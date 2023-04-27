using Godot;
using System;
using Godot.Collections;
using System.Globalization;

public class Area_Panel : Panel
{
    public enum types {
        MM,
        CM,
        M,
        KM,
        MILES,
        ACRES,
        HECTARES
    }

    Dictionary<int,types> length_ids = new Dictionary<int, types>(){
        {0,types.MM},
        {1,types.CM},
        {2,types.M},
        {3,types.KM},
        {4,types.MILES},
        {5,types.ACRES},
        {6,types.HECTARES},
    };

    string textValue = "0";
    double convertedValue = 0;

    types type1 = types.MM;
    types type2 = types.CM;

    Label cur1Label;
    Label cur2Label;

    // Hard coded values for converting.
    Dictionary<types,Dictionary<types,double>> length_values = new Dictionary<types, Dictionary<types, double>>(){
        {types.MM,new Dictionary<types, double>(){
            {types.MM,1},
            {types.CM,0.01},
            {types.M,0.000001},
            {types.KM,0.000000000001},
            {types.MILES,0.000000000000386},
            {types.ACRES,0.000000000247105},
            {types.HECTARES,0.0000000001},
        }},
        {types.CM,new Dictionary<types, double>(){
            {types.MM,100},
            {types.CM,1},
            {types.M,0.0001},
            {types.KM,0.0000000001},
            {types.MILES,0.00000000003861},
            {types.ACRES,0.000000024710538},
            {types.HECTARES,0.00000001},
        }},
        {types.M,new Dictionary<types, double>(){
            {types.MM,1_000_000},
            {types.CM,10_000},
            {types.M,1},
            {types.KM,0.000001},
            {types.MILES,0.000000386102159},
            {types.ACRES,0.000247},
            {types.HECTARES,0.0001},
        }},
        {types.KM,new Dictionary<types, double>(){
            {types.MM,1_000_000_000_000},
            {types.CM,10_000_000_000},
            {types.M,1_000_000},
            {types.KM,1},
            {types.MILES,0.386102},
            {types.ACRES,247.1054},
            {types.HECTARES,100},
        }},
        {types.MILES,new Dictionary<types, double>(){
            {types.MM,2_589_988_110_336},
            {types.CM,25_899_881_103},
            {types.M,2_589_988},
            {types.KM,2.589988},
            {types.MILES,1},
            {types.ACRES,640},
            {types.HECTARES,258.9988},
        }},
        {types.ACRES,new Dictionary<types, double>(){
            {types.MM,4_046_856_422},
            {types.CM,40_468_564},
            {types.M,4_046.856},
            {types.KM,0.004047},
            {types.MILES,0.001563},
            {types.ACRES,1},
            {types.HECTARES,0.404686},
        }},
        {types.HECTARES,new Dictionary<types, double>(){
            {types.MM,10_000_000_000},
            {types.CM,100_000_000},
            {types.M,10_000},
            {types.KM,0.01},
            {types.MILES,0.003861},
            {types.ACRES,2.471054},
            {types.HECTARES,1},
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
