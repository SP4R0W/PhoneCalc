using Godot;
using System;
using Godot.Collections;

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

    private Dictionary<int,types> length_ids = new Dictionary<int, types>(){
        {0,types.CARATS},
        {1,types.GRAMS},
        {2,types.KGS},
        {3,types.TONS},
        {4,types.OUNCES},
        {5,types.POUNDS},
    };

    private string textValue = "0";
    private double convertedValue = 0;
    
    private types type1 = types.CARATS;
    private types type2 = types.GRAMS;

    private Label cur1Label;
    private Label cur2Label;

    private Dictionary<types,Dictionary<types,double>> length_values = new Dictionary<types, Dictionary<types, double>>(){
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
