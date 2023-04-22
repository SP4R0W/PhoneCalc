using Godot;
using System;
using Godot.Collections;
using System.Globalization;

public class Velocity_Panel : Panel
{
    public enum types {
        CM,
        M,
        KM,
        FEET,
        MILES,
    }

    private Dictionary<int,types> length_ids = new Dictionary<int, types>(){
        {0,types.CM},
        {1,types.M},
        {2,types.KM},
        {3,types.FEET},
        {4,types.MILES},
    };

    private string textValue = "0";
    private double convertedValue = 0;

    private types type1 = types.CM;
    private types type2 = types.M;

    private Label cur1Label;
    private Label cur2Label;

    private Dictionary<types,Dictionary<types,double>> length_values = new Dictionary<types, Dictionary<types, double>>(){
        {types.CM,new Dictionary<types, double>(){
            {types.CM,1},
            {types.M,0.01},
            {types.KM,0.036},
            {types.FEET,0.032808},
            {types.MILES,0.022371},
        }},
        {types.M,new Dictionary<types, double>(){
            {types.CM,100},
            {types.M,1},
            {types.KM,3.6},
            {types.FEET,3.28084},
            {types.MILES,2.237136},
        }},
        {types.KM,new Dictionary<types, double>(){
            {types.CM,27.77778},
            {types.M,0.277778},
            {types.KM,1},
            {types.FEET,0.911344},
            {types.MILES,0.621427},
        }},
        {types.FEET,new Dictionary<types, double>(){
            {types.CM,30.48},
            {types.M,0.3048},
            {types.KM,1.09728},
            {types.FEET,1},
            {types.MILES,0.681879},
        }},
        {types.MILES,new Dictionary<types, double>(){
            {types.CM,44.7},
            {types.M,0.447},
            {types.KM,1.6092},
            {types.FEET,1.466535},
            {types.MILES,1},
        }},
    };

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
        double value = length_values[type1][type2];
        convertedValue = Convert.ToDouble(textValue) * value;
    }

    private void SelectCur1(int index)
    {
        type1 = length_ids[index];

        CalculateValue();
    }

    private void SelectCur2(int index)
    {
        type2 = length_ids[index];

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
