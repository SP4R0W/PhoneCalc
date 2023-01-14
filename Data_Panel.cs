using Godot;
using System;
using Godot.Collections;

public class Data_Panel : Panel
{
    public enum types {
        BITS,
        BYTES,
        KILOBITS,
        KIBIBITS,
        KILOBYTES,
        KIBIBYTES,
        MEGABITS,
        MEBIBITS,
        MEGABYTES,
        MEBIBYTES,
        GIGABITS,
        GIBIBITS,
        GIGABYTES,
        GIBIBYES,
        TERABITS,
        TEBIBITS,
        TERABYTES,
        TEBIBYTES
    }

    private Dictionary<int,types> ids = new Dictionary<int, types>(){
        {0,types.BITS},
        {1,types.BYTES},
        {2,types.KILOBITS},
        {3,types.KIBIBITS},
        {4,types.KILOBYTES},
        {5,types.KIBIBYTES},
        {6,types.MEGABITS},
        {7,types.MEBIBITS},
        {8,types.MEGABYTES},
        {9,types.MEBIBYTES},
        {10,types.GIGABITS},
        {11,types.GIBIBITS},
        {12,types.GIGABYTES},
        {13,types.GIBIBYES},
        {14,types.TERABITS},
        {15,types.TEBIBITS},
        {16,types.TERABYTES},
        {17,types.TEBIBYTES},
    };

    private Dictionary<types,Dictionary<types,double>> values = new Dictionary<types, Dictionary<types, double>>(){
        {types.BITS,new Dictionary<types, double>(){
            {types.BITS,1},
            {types.BYTES,0.125},
            {types.KILOBITS,0.001},
            {types.KIBIBITS,0.000977},
            {types.KILOBYTES,0.000125},
            {types.KIBIBYTES,0.000122},
            {types.MEGABITS,0.000001},
            {types.MEBIBITS,0.000000953674316},
            {types.MEGABYTES,0.000000125},
            {types.MEBIBYTES,0.00000011920929},
            {types.GIGABITS,0.000000001},
            {types.GIBIBITS,0.000000000931323},
            {types.GIGABYTES,0.000000000125},
            {types.GIBIBYES,0.000000000116415},
            {types.TERABITS,0.000000000001},
            {types.TEBIBITS,0.000000000000909},
            {types.TERABYTES,0.000000000000125},
            {types.TEBIBYTES,0.000000000000114},
        }},
        {types.BYTES,new Dictionary<types, double>(){
            {types.BITS,8},
            {types.BYTES,1},
            {types.KILOBITS,0.008},
            {types.KIBIBITS,0.007812},
            {types.KILOBYTES,0.001},
            {types.KIBIBYTES,0.000977},
            {types.MEGABITS,0.000008},
            {types.MEBIBITS,0.000008},
            {types.MEGABYTES,0.000001},
            {types.MEBIBYTES,0.000000953674316},
            {types.GIGABITS,0.000000008},
            {types.GIBIBITS,0.000000007450581},
            {types.GIGABYTES,0.000000001},
            {types.GIBIBYES,0.000000000931323},
            {types.TERABITS,0.000000000008},
            {types.TEBIBITS,0.000000000007276},
            {types.TERABYTES,0.000000000001},
            {types.TEBIBYTES,0.000000000000909},
        }},
        {types.KILOBITS,new Dictionary<types, double>(){
            {types.BITS,1_000},
            {types.BYTES,125},
            {types.KILOBITS,1},
            {types.KIBIBITS,0.976563},
            {types.KILOBYTES,0.125},
            {types.KIBIBYTES,0.12207},
            {types.MEGABITS,0.001},
            {types.MEBIBITS,0.000954},
            {types.MEGABYTES,0.000001},
            {types.MEBIBYTES,0.000125},
            {types.GIGABITS,0.000001},
            {types.GIBIBITS,0.000000931322575},
            {types.GIGABYTES,0.000000125},
            {types.GIBIBYES,0.000000116415322},
            {types.TERABITS,0.000000001},
            {types.TEBIBITS,0.000000000909495},
            {types.TERABYTES,0.000000000125},
            {types.TEBIBYTES,0.000000000113687},
        }},
        {types.KIBIBITS,new Dictionary<types, double>(){
            {types.BITS,1_024},
            {types.BYTES,128},
            {types.KILOBITS,1_024},
            {types.KIBIBITS,1},
            {types.KILOBYTES,0.128},
            {types.KIBIBYTES,0.125},
            {types.MEGABITS,0.001024},
            {types.MEBIBITS,0.000977},
            {types.MEGABYTES,0.000128},
            {types.MEBIBYTES,0.000122},
            {types.GIGABITS,0.000001},
            {types.GIBIBITS,0.000000953674316},
            {types.GIGABYTES,0.000000128},
            {types.GIBIBYES,0.00000011920929},
            {types.TERABITS,0.000000001024},
            {types.TEBIBITS,0.000000000931323},
            {types.TERABYTES,0.000000000128},
            {types.TEBIBYTES,0.000000000116415},
        }},
        {types.KILOBYTES,new Dictionary<types, double>(){
            {types.BITS,8_000},
            {types.BYTES,1_000},
            {types.KILOBITS,8},
            {types.KIBIBITS,7.8125},
            {types.KILOBYTES,1},
            {types.KIBIBYTES,0.976563},
            {types.MEGABITS,0.008},
            {types.MEBIBITS,0.007629},
            {types.MEGABYTES,0.001},
            {types.MEBIBYTES,0.000954},
            {types.GIGABITS,0.000008},
            {types.GIBIBITS,0.000007},
            {types.GIGABYTES,0.000001},
            {types.GIBIBYES,0.000000931322575},
            {types.TERABITS,0.000000008},
            {types.TEBIBITS,0.000000007275958},
            {types.TERABYTES,0.000000001},
            {types.TEBIBYTES,0.000000000909495},
        }},
        {types.KIBIBYTES,new Dictionary<types, double>(){
            {types.BITS,8_192},
            {types.BYTES,1_024},
            {types.KILOBITS,8_192},
            {types.KIBIBITS,8},
            {types.KILOBYTES,1_024},
            {types.KIBIBYTES,1},
            {types.MEGABITS,0.008192},
            {types.MEBIBITS,0.007812},
            {types.MEGABYTES,0.001024},
            {types.MEBIBYTES,0.000977},
            {types.GIGABITS,0.000008},
            {types.GIBIBITS,0.000008},
            {types.GIGABYTES,0.000001},
            {types.GIBIBYES,0.000000953674316},
            {types.TERABITS,0.000000008192},
            {types.TEBIBITS,0.000000007450581},
            {types.TERABYTES,0.000000001024},
            {types.TEBIBYTES,0.000000000931323},
        }},
        {types.MEGABITS,new Dictionary<types, double>(){
            {types.BITS,1_000_000},
            {types.BYTES,125_000},
            {types.KILOBITS,1_000},
            {types.KIBIBITS,976.5625},
            {types.KILOBYTES,125},
            {types.KIBIBYTES,122.0703},
            {types.MEGABITS,1},
            {types.MEBIBITS,0.953674},
            {types.MEGABYTES,0.125},
            {types.MEBIBYTES,0.119209},
            {types.GIGABITS,0.001},
            {types.GIBIBITS,0.000931},
            {types.GIGABYTES,0.000125},
            {types.GIBIBYES,0.000116},
            {types.TERABITS,0.000001},
            {types.TEBIBITS,0.000000909494702},
            {types.TERABYTES,0.000000125},
            {types.TEBIBYTES,0.000000113686838},
        }},
        {types.MEBIBITS,new Dictionary<types, double>(){
            {types.BITS,1_048_576},
            {types.BYTES,131_072},
            {types.KILOBITS,1_048.576},
            {types.KIBIBITS,1_024},
            {types.KILOBYTES,131.072},
            {types.KIBIBYTES,128},
            {types.MEGABITS,1.048576},
            {types.MEBIBITS,1},
            {types.MEGABYTES,0.131072},
            {types.MEBIBYTES,0.125},
            {types.GIGABITS,0.001049},
            {types.GIBIBITS,0.000977},
            {types.GIGABYTES,0.000131},
            {types.GIBIBYES,0.000122},
            {types.TERABITS,0.000001},
            {types.TEBIBITS,0.000000953674316},
            {types.TERABYTES,0.000000131072},
            {types.TEBIBYTES,0.00000011920929},
        }},
        {types.MEGABYTES,new Dictionary<types, double>(){
            {types.BITS,8_000_000},
            {types.BYTES,1_000_000},
            {types.KILOBITS,8_000},
            {types.KIBIBITS,7_812.5},
            {types.KILOBYTES,1_000},
            {types.KIBIBYTES,976.5625},
            {types.MEGABITS,8},
            {types.MEBIBITS,7.629395},
            {types.MEGABYTES,1},
            {types.MEBIBYTES,0.953674},
            {types.GIGABITS,0.008},
            {types.GIBIBITS,0.007451},
            {types.GIGABYTES,0.001},
            {types.GIBIBYES,0.000931},
            {types.TERABITS,0.000008},
            {types.TEBIBITS,0.000007},
            {types.TERABYTES,0.000001},
            {types.TEBIBYTES,0.000000909494702},
        }},
        {types.MEBIBYTES,new Dictionary<types, double>(){
            {types.BITS,8_388_608},
            {types.BYTES,1_048_576},
            {types.KILOBITS,8_388.608},
            {types.KIBIBITS,8_192},
            {types.KILOBYTES,1_048.576},
            {types.KIBIBYTES,1_024},
            {types.MEGABITS,8.388608},
            {types.MEBIBITS,8},
            {types.MEGABYTES,1.048576},
            {types.MEBIBYTES,1},
            {types.GIGABITS,0.008389},
            {types.GIBIBITS,0.007812},
            {types.GIGABYTES,0.001049},
            {types.GIBIBYES,0.000977},
            {types.TERABITS,0.000008},
            {types.TEBIBITS,0.000008},
            {types.TERABYTES,0.000001},
            {types.TEBIBYTES,0.000000953674316},
        }},
        {types.GIGABITS,new Dictionary<types, double>(){
            {types.BITS,1_000_000_000},
            {types.BYTES,125_000_000},
            {types.KILOBITS,1_000_000},
            {types.KIBIBITS,976_562.5},
            {types.KILOBYTES,125_000},
            {types.KIBIBYTES,122_070.3},
            {types.MEGABITS,1_000},
            {types.MEBIBITS,953.6743},
            {types.MEGABYTES,125},
            {types.MEBIBYTES,119.2093},
            {types.GIGABITS,1},
            {types.GIBIBITS,0.931323},
            {types.GIGABYTES,0.125},
            {types.GIBIBYES,0.116415},
            {types.TERABITS,0.001},
            {types.TEBIBITS,0.000909},
            {types.TERABYTES,0.000125},
            {types.TEBIBYTES,0.000114},
        }},
        {types.GIBIBITS,new Dictionary<types, double>(){
            {types.BITS,1_073_741_824},
            {types.BYTES,134_217_728},
            {types.KILOBITS,1_073_742},
            {types.KIBIBITS,1_048_576},
            {types.KILOBYTES,134_217.7},
            {types.KIBIBYTES,131_072},
            {types.MEGABITS,1_073.742},
            {types.MEBIBITS,1_024},
            {types.MEGABYTES,134.2177},
            {types.MEBIBYTES,128},
            {types.GIGABITS,1.073742},
            {types.GIBIBITS,1},
            {types.GIGABYTES,0.134218},
            {types.GIBIBYES,0.125},
            {types.TERABITS,0.001074},
            {types.TEBIBITS,0.000977},
            {types.TERABYTES,0.000134},
            {types.TEBIBYTES,0.000122},
        }},
        {types.GIGABYTES,new Dictionary<types, double>(){
            {types.BITS,8_000_000_000},
            {types.BYTES,1_000_000_000},
            {types.KILOBITS,8_000_000},
            {types.KIBIBITS,7_812_500},
            {types.KILOBYTES,1_000_000},
            {types.KIBIBYTES,976_562.5},
            {types.MEGABITS,8_000},
            {types.MEBIBITS,7_629.395},
            {types.MEGABYTES,1_000},
            {types.MEBIBYTES,953.6743},
            {types.GIGABITS,8},
            {types.GIBIBITS,7.450581},
            {types.GIGABYTES,1},
            {types.GIBIBYES,0.931323},
            {types.TERABITS,0.008},
            {types.TEBIBITS,0.007276},
            {types.TERABYTES,0.001},
            {types.TEBIBYTES,0.000909},
        }},
        {types.GIBIBYES,new Dictionary<types, double>(){
            {types.BITS,8_589_934_592},
            {types.BYTES,1_073_741_824},
            {types.KILOBITS,8_589_935},
            {types.KIBIBITS,8_388_608},
            {types.KILOBYTES,1_073_742},
            {types.KIBIBYTES,1_048_576},
            {types.MEGABITS,8_589.935},
            {types.MEBIBITS,8_192},
            {types.MEGABYTES,1_073.742},
            {types.MEBIBYTES,1_024},
            {types.GIGABITS,8.589935},
            {types.GIBIBITS,1.073742},
            {types.GIGABYTES,1},
            {types.GIBIBYES,1},
            {types.TERABITS,0.00859},
            {types.TEBIBITS,0.007812},
            {types.TERABYTES,0.001074},
            {types.TEBIBYTES,0.000977},
        }},
        {types.TERABITS,new Dictionary<types, double>(){
            {types.BITS,1_000_000_000_000},
            {types.BYTES,125_000_000_000},
            {types.KILOBITS,1_000_000_000},
            {types.KIBIBITS,976_562_500},
            {types.KILOBYTES,125_000_000},
            {types.KIBIBYTES,122_070_312},
            {types.MEGABITS,1_000_000},
            {types.MEBIBITS,953_674.3},
            {types.MEGABYTES,125_000},
            {types.MEBIBYTES,119_209.3},
            {types.GIGABITS,1_000},
            {types.GIBIBITS,931.3226},
            {types.GIGABYTES,125},
            {types.GIBIBYES,116.4153},
            {types.TERABITS,1},
            {types.TEBIBITS,0.909495},
            {types.TERABYTES,0.125},
            {types.TEBIBYTES,0.113687},
        }},
        {types.TEBIBITS,new Dictionary<types, double>(){
            {types.BITS,1_099_511_627_776},
            {types.BYTES,137_438_953_472},
            {types.KILOBITS,1_099_511_628},
            {types.KIBIBITS,1_073_741_824},
            {types.KILOBYTES,137_438_953},
            {types.KIBIBYTES,134_217_728},
            {types.MEGABITS,1_099_512},
            {types.MEBIBITS,1_048_576},
            {types.MEGABYTES,137_439},
            {types.MEBIBYTES,131_072},
            {types.GIGABITS,1_099.512},
            {types.GIBIBITS,1_024},
            {types.GIGABYTES,137.439},
            {types.GIBIBYES,128},
            {types.TERABITS,1.099512},
            {types.TEBIBITS,1},
            {types.TERABYTES,0.137439},
            {types.TEBIBYTES,0.125},
        }},
        {types.TERABYTES,new Dictionary<types, double>(){
            {types.BITS,8_000_000_000_000},
            {types.BYTES,1_000_000_000_000},
            {types.KILOBITS,8_000_000_000},
            {types.KIBIBITS,7_812_500_000},
            {types.KILOBYTES,1_000_000_000},
            {types.KIBIBYTES,976_562_500},
            {types.MEGABITS,8_000_000},
            {types.MEBIBITS,7_629_395},
            {types.MEGABYTES,1_000_000},
            {types.MEBIBYTES,953_674.3},
            {types.GIGABITS,8_000},
            {types.GIBIBITS,7_450.581},
            {types.GIGABYTES,1_000},
            {types.GIBIBYES,931.3226},
            {types.TERABITS,8},
            {types.TEBIBITS,7.275958},
            {types.TERABYTES,1},
            {types.TEBIBYTES,0.909495},
        }},
        {types.TEBIBYTES,new Dictionary<types, double>(){
            {types.BITS,8_796_093_022_208},
            {types.BYTES,1_099_511_627_776},
            {types.KILOBITS,8_796_093_022},
            {types.KIBIBITS,8_589_934_592},
            {types.KILOBYTES,1_099_511_628},
            {types.KIBIBYTES,1_073_741_824},
            {types.MEGABITS,8_796_093},
            {types.MEBIBITS,8_388_608},
            {types.MEGABYTES,1_099_512},
            {types.MEBIBYTES,1_048_576},
            {types.GIGABITS,8_796.093},
            {types.GIBIBITS,8_192},
            {types.GIGABYTES,1_099.512},
            {types.GIBIBYES,1_024},
            {types.TERABITS,8.796093},
            {types.TEBIBITS,8},
            {types.TERABYTES,1.099512},
            {types.TEBIBYTES,1},
        }},
    };

    private string textValue = "0";
    private double convertedValue = 0;
    
    private types type1 = types.BITS;
    private types type2 = types.BYTES;

    private Label cur1Label;
    private Label cur2Label;

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
        double value = values[type1][type2];
        convertedValue = Convert.ToDouble(textValue) * value;
    }

    private void SelectCur1(int index)
    {
        GD.Print(index);
        type1 = ids[index];

        CalculateValue();
    }

    private void SelectCur2(int index)
    {
        type2 = ids[index];

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
