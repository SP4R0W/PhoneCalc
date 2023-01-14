using Godot;
using System;
using Godot.Collections;

public class Length_Panel : Panel
{
    public enum types {
        NANOMETERS,
        MICRONS,
        MILIMETERS,
        CENTIMETERS,
        METERS,
        KILOMETERS,
        INCHES,
        FEET,
        YARDS,
        MILES,
        NAUTICAL_MILES
    }

    private Dictionary<int,types> length_ids = new Dictionary<int, types>(){
        {0,types.NANOMETERS},
        {1,types.MICRONS},
        {2,types.MILIMETERS},
        {3,types.CENTIMETERS},
        {4,types.METERS},
        {5,types.KILOMETERS},
        {6,types.INCHES},
        {7,types.FEET},
        {8,types.YARDS},
        {9,types.MILES},
        {10,types.NAUTICAL_MILES},
    };

    private string textValue = "0";
    private double convertedValue = 0;
    
    private types type1 = types.NANOMETERS;
    private types type2 = types.MICRONS;

    private Label cur1Label;
    private Label cur2Label;

    private Dictionary<types,Dictionary<types,double>> length_values = new Dictionary<types, Dictionary<types, double>>(){
        {types.NANOMETERS,new Dictionary<types, double>(){
            {types.NANOMETERS,1},
            {types.MICRONS,0.001},
            {types.MILIMETERS,0.000001},
            {types.CENTIMETERS,0.0000001},
            {types.METERS,0.000000001},
            {types.KILOMETERS,0.000000000001},
            {types.INCHES,0.000000039370079},
            {types.FEET,0.00000000328084},
            {types.YARDS,0.000000001093613},
            {types.MILES,0.000000000000621},
            {types.NAUTICAL_MILES,0.00000000000054},
        }},
        {types.MICRONS,new Dictionary<types, double>(){
            {types.NANOMETERS,1000},
            {types.MICRONS,1},
            {types.MILIMETERS,0.001},
            {types.CENTIMETERS,0.0001},
            {types.METERS,0.000001},
            {types.KILOMETERS,0.000000001},
            {types.INCHES,0.000039},
            {types.FEET,0.000003},
            {types.YARDS,0.000001},
            {types.MILES,0.000000000621371},
            {types.NAUTICAL_MILES,0.000000000539957},
        }},
        {types.MILIMETERS,new Dictionary<types, double>(){
            {types.NANOMETERS,1_000_000},
            {types.MICRONS,1_000},
            {types.MILIMETERS,1},
            {types.CENTIMETERS,0.1},
            {types.METERS,0.001},
            {types.KILOMETERS,0.000001},
            {types.INCHES,0.03937},
            {types.FEET,0.003281},
            {types.YARDS,0.001094},
            {types.MILES,0.000000621371192},
            {types.NAUTICAL_MILES,0.000000539956803},
        }},
        {types.CENTIMETERS,new Dictionary<types, double>(){
            {types.NANOMETERS,10_000_000},
            {types.MICRONS,10_000},
            {types.MILIMETERS,10},
            {types.CENTIMETERS,1},
            {types.METERS,0.01},
            {types.KILOMETERS,0.00001},
            {types.INCHES,0.393701},
            {types.FEET,0.032808},
            {types.YARDS,0.010936},
            {types.MILES,0.000006},
            {types.NAUTICAL_MILES,0.000005},
        }},
        {types.METERS,new Dictionary<types, double>(){
            {types.NANOMETERS,1_000_000_000},
            {types.MICRONS,1_000_000},
            {types.MILIMETERS,1_000},
            {types.CENTIMETERS,100},
            {types.METERS,1},
            {types.KILOMETERS,0.001},
            {types.INCHES,39.37008},
            {types.FEET,3.28084},
            {types.YARDS,1.093613},
            {types.MILES,0.000621},
            {types.NAUTICAL_MILES,0.00054},
        }},
        {types.KILOMETERS,new Dictionary<types, double>(){
            {types.NANOMETERS,1_000_000_000_000},
            {types.MICRONS,1_000_000_000},
            {types.MILIMETERS,1_000_000},
            {types.CENTIMETERS,100_000},
            {types.METERS,1000},
            {types.KILOMETERS,1},
            {types.INCHES,39_370.08},
            {types.FEET,3_280.84},
            {types.YARDS,1_093.613},
            {types.MILES,0.621371},
            {types.NAUTICAL_MILES,0.539957},
        }},
        {types.INCHES,new Dictionary<types, double>(){
            {types.NANOMETERS,25_400_000},
            {types.MICRONS,25_400},
            {types.MILIMETERS,25.4},
            {types.CENTIMETERS,2.54},
            {types.METERS,0.0254},
            {types.KILOMETERS,0.000025},
            {types.INCHES,1},
            {types.FEET,0.083333},
            {types.YARDS,0.027778},
            {types.MILES,0.000016},
            {types.NAUTICAL_MILES,0.000014},
        }},
        {types.FEET,new Dictionary<types, double>(){
            {types.NANOMETERS,304_800_000},
            {types.MICRONS,304_800},
            {types.MILIMETERS,304.8},
            {types.CENTIMETERS,30.48},
            {types.METERS,0.3048},
            {types.KILOMETERS,0.000305},
            {types.INCHES,12},
            {types.FEET,1},
            {types.YARDS,0.333333},
            {types.MILES,0.000189},
            {types.NAUTICAL_MILES,0.000165},
        }},
        {types.YARDS,new Dictionary<types, double>(){
            {types.NANOMETERS,914_400_000},
            {types.MICRONS,914_400},
            {types.MILIMETERS,914.4},
            {types.CENTIMETERS,91.44},
            {types.METERS,0.9144},
            {types.KILOMETERS,0.000914},
            {types.INCHES,36},
            {types.FEET,3},
            {types.YARDS,1},
            {types.MILES,0.000568},
            {types.NAUTICAL_MILES,0.000494},
        }},
        {types.MILES,new Dictionary<types, double>(){
            {types.NANOMETERS,1_609_344_000_000},
            {types.MICRONS,1_609_344_000},
            {types.MILIMETERS,1_609_344},
            {types.CENTIMETERS,160_934.4},
            {types.METERS,1_609.344},
            {types.KILOMETERS,1.609344},
            {types.INCHES,63_360},
            {types.FEET,5_280},
            {types.YARDS,1_760},
            {types.MILES,1},
            {types.NAUTICAL_MILES,0.868976},
        }},
        {types.NAUTICAL_MILES,new Dictionary<types, double>(){
            {types.NANOMETERS,1_852_000_000_000},
            {types.MICRONS,1_852_000_000},
            {types.MILIMETERS,1_852_000},
            {types.CENTIMETERS,185_200},
            {types.METERS,1_852},
            {types.KILOMETERS,1.852},
            {types.INCHES,72_913.39},
            {types.FEET,6_076.115},
            {types.YARDS,2_025.372},
            {types.MILES,1.1507791},
            {types.NAUTICAL_MILES,1},
        }},
    };

    public override void _Ready()
    {
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
        if (StringExtensions.Find(textValue,",") == -1)
        {
            textValue += ",";
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
