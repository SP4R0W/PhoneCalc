using Godot;
using System;
using Godot.Collections;

public class Volume_Panel : Panel
{

    public enum types {
        MILILITERS,
        CM,
        LITERS,
        METERS,
        SPOON_US,
        TABLESPOON_US,
        CUP_US,
        GALON_US,
        SPOON_UK,
        TABLESPOON_UK,
        GALON_UK,
        OUNCES_US,
        OUNCES_UK,
    }

    private Dictionary<int,types> length_ids = new Dictionary<int, types>(){
        {0,types.MILILITERS},
        {1,types.CM},
        {2,types.LITERS},
        {3,types.METERS},
        {4,types.SPOON_US},
        {5,types.TABLESPOON_US},
        {6,types.CUP_US},
        {7,types.GALON_US},
        {8,types.SPOON_UK},
        {9,types.TABLESPOON_UK},
        {10,types.GALON_UK},
        {11,types.OUNCES_US},
        {12,types.OUNCES_UK},
    };

    private string textValue = "0";
    private double convertedValue = 0;
    
    private types type1 = types.MILILITERS;
    private types type2 = types.CM;

    private Label cur1Label;
    private Label cur2Label;

    private Dictionary<types,Dictionary<types,double>> length_values = new Dictionary<types, Dictionary<types, double>>(){
        {types.MILILITERS,new Dictionary<types, double>(){
            {types.MILILITERS,1},
            {types.CM,1},
            {types.LITERS,0.001},
            {types.METERS,0.000001},
            {types.SPOON_US,0.202884},
            {types.TABLESPOON_US,0.067628},
            {types.CUP_US,0.004227},
            {types.GALON_US,0.000264},
            {types.SPOON_UK,0.168936},
            {types.TABLESPOON_UK,0.056312},
            {types.GALON_UK,0.00022},
            {types.OUNCES_US,0.033814},
            {types.OUNCES_UK,0.035195},
        }},
        {types.CM,new Dictionary<types, double>(){
            {types.MILILITERS,1},
            {types.CM,1},
            {types.LITERS,0.001},
            {types.METERS,0.000001},
            {types.SPOON_US,0.202884},
            {types.TABLESPOON_US,0.067628},
            {types.CUP_US,0.004227},
            {types.GALON_US,0.000264},
            {types.SPOON_UK,0.168936},
            {types.TABLESPOON_UK,0.056312},
            {types.GALON_UK,0.00022},
            {types.OUNCES_US,0.033814},
            {types.OUNCES_UK,0.035195},
        }},
        {types.LITERS,new Dictionary<types, double>(){
            {types.MILILITERS,1_000},
            {types.CM,1_000},
            {types.LITERS,1},
            {types.METERS,0.001},
            {types.SPOON_US,202.8841},
            {types.TABLESPOON_US,67.62805},
            {types.CUP_US,4.226753},
            {types.GALON_US,0.264172},
            {types.SPOON_UK,168.9364},
            {types.TABLESPOON_UK,56.31213},
            {types.GALON_UK,0.219969},
            {types.OUNCES_US,33.81402},
            {types.OUNCES_UK,35.19508},
        }},
        {types.METERS,new Dictionary<types, double>(){
            {types.MILILITERS,1_000_000},
            {types.CM,1_000_000},
            {types.LITERS,1_000},
            {types.METERS,1},
            {types.SPOON_US,202_884.1},
            {types.TABLESPOON_US,67_628.05},
            {types.CUP_US,4_226.753},
            {types.GALON_US,264.1721},
            {types.SPOON_UK,168_936.4},
            {types.TABLESPOON_UK,56_312.13},
            {types.GALON_UK,219.9692},
            {types.OUNCES_US,33_814.02},
            {types.OUNCES_UK,35_195.08},
        }},
        {types.SPOON_US,new Dictionary<types, double>(){
            {types.MILILITERS,4.928922},
            {types.CM,4.928922},
            {types.LITERS,0.004929},
            {types.METERS,0.000005},
            {types.SPOON_US,1},
            {types.TABLESPOON_US,0.333333},
            {types.CUP_US,0.020833},
            {types.GALON_US,0.001302},
            {types.SPOON_UK,0.832674},
            {types.TABLESPOON_UK,0.277558},
            {types.GALON_UK,0.001084},
            {types.OUNCES_US,0.166667},
            {types.OUNCES_UK,0.173474},
        }},
        {types.TABLESPOON_US,new Dictionary<types, double>(){
            {types.MILILITERS,14.78676},
            {types.CM,14.78676},
            {types.LITERS,0.014787},
            {types.METERS,0.000015},
            {types.SPOON_US,3},
            {types.TABLESPOON_US,1},
            {types.CUP_US,0.0625},
            {types.GALON_US,0.003906},
            {types.SPOON_UK,2.498023},
            {types.TABLESPOON_UK,0.832674},
            {types.GALON_UK,0.003253},
            {types.OUNCES_US,0.5},
            {types.OUNCES_UK,0.520421},
        }},
        {types.CUP_US,new Dictionary<types, double>(){
            {types.MILILITERS,236.5882},
            {types.CM,236.5882},
            {types.LITERS,0.236588},
            {types.METERS,0.000237},
            {types.SPOON_US,48},
            {types.TABLESPOON_US,16},
            {types.CUP_US,1},
            {types.GALON_US,0.0625},
            {types.SPOON_UK,39.96836},
            {types.TABLESPOON_UK,13.32279},
            {types.GALON_UK,0.052042},
            {types.OUNCES_US,8},
            {types.OUNCES_UK,8.326742},
        }},
        {types.GALON_US,new Dictionary<types, double>(){
            {types.MILILITERS,3_785.412},
            {types.CM,3_785.412},
            {types.LITERS,3.785412},
            {types.METERS,0.003785},
            {types.SPOON_US,768},
            {types.TABLESPOON_US,256},
            {types.CUP_US,16},
            {types.GALON_US,1},
            {types.SPOON_UK,639.4938},
            {types.TABLESPOON_UK,213.1646},
            {types.GALON_UK,0.832674},
            {types.OUNCES_US,128},
            {types.OUNCES_UK,133.2279},
        }},
        {types.SPOON_UK,new Dictionary<types, double>(){
            {types.MILILITERS,5.919388},
            {types.CM,5.919388},
            {types.LITERS,0.005919},
            {types.METERS,0.000006},
            {types.SPOON_US,1.20095},
            {types.TABLESPOON_US,0.400317},
            {types.CUP_US,0.02502},
            {types.GALON_US,0.001564},
            {types.SPOON_UK,1},
            {types.TABLESPOON_UK,0.333333},
            {types.GALON_UK,0.001302},
            {types.OUNCES_US,0.200158},
            {types.OUNCES_UK,0.208333},
        }},
        {types.TABLESPOON_UK,new Dictionary<types, double>(){
            {types.MILILITERS,17.75816},
            {types.CM,17.75816},
            {types.LITERS,0.017758},
            {types.METERS,0.000018},
            {types.SPOON_US,3.60285},
            {types.TABLESPOON_US,1.20095},
            {types.CUP_US,0.075059},
            {types.GALON_US,0.004691},
            {types.SPOON_UK,3},
            {types.TABLESPOON_UK,1},
            {types.GALON_UK,0.003906},
            {types.OUNCES_US,0.600475},
            {types.OUNCES_UK,0.625},
        }},
        {types.GALON_UK,new Dictionary<types, double>(){
            {types.MILILITERS,4_546.09},
            {types.CM,4_546.09},
            {types.LITERS,4.54609},
            {types.METERS,0.004546},
            {types.SPOON_US,922.3295},
            {types.TABLESPOON_US,307.4432},
            {types.CUP_US,19.2152},
            {types.GALON_US,1.20095},
            {types.SPOON_UK,768},
            {types.TABLESPOON_UK,256},
            {types.GALON_UK,1},
            {types.OUNCES_US,153.7216},
            {types.OUNCES_UK,160},
        }},
        {types.OUNCES_US,new Dictionary<types, double>(){
            {types.MILILITERS,29.57353},
            {types.CM,29.57353},
            {types.LITERS,0.029574},
            {types.METERS,0.00003},
            {types.SPOON_US,6},
            {types.TABLESPOON_US,2},
            {types.CUP_US,0.125},
            {types.GALON_US,0.007812},
            {types.SPOON_UK,4.996045},
            {types.TABLESPOON_UK,1.665348},
            {types.GALON_UK,0.006505},
            {types.OUNCES_US,1},
            {types.OUNCES_UK,1.040843},
        }},
        {types.OUNCES_UK,new Dictionary<types, double>(){
            {types.MILILITERS,28.41306},
            {types.CM,28.41306},
            {types.LITERS,0.028413},
            {types.METERS,0.000028},
            {types.SPOON_US,5.76456},
            {types.TABLESPOON_US,1.92152},
            {types.CUP_US,0.120095},
            {types.GALON_US,0.007506},
            {types.SPOON_UK,4.8},
            {types.TABLESPOON_UK,1.6},
            {types.GALON_UK,0.00625},
            {types.OUNCES_US,0.96076},
            {types.OUNCES_UK,1},
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
