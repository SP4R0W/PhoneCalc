using Godot;
using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

public class Currency_Panel : Panel
{
    private enum types {
        PLN,
        USD,
        EUR,
        GBP,
        CNY,
        JPY,
        RUB,
        UAH,
    }

    private Dictionary<types,Dictionary<types,decimal>> values = new Dictionary<types, Dictionary<types, decimal>>(){

    };

    private Dictionary<types,string> suffixes = new Dictionary<types, string>(){
        {types.PLN,"PLN"},
        {types.USD,"USD"},
        {types.EUR,"EUR"},
        {types.GBP,"GBP"},
        {types.CNY,"CNY"},
        {types.JPY,"JPY"},
        {types.RUB,"RUB"},
        {types.UAH,"UAH"},
    };

    private Dictionary<int,types> ids = new Dictionary<int, types>(){
        {0,types.PLN},
        {1,types.USD},
        {2,types.EUR},
        {3,types.GBP},
        {4,types.CNY},
        {5,types.JPY},
        {6,types.RUB},
        {7,types.UAH},
    };

    private string textValue = "0";
    private string convertedValue = "0";

    private types type1 = types.PLN;
    private types type2 = types.USD;

    private Label cur1Label;
    private Label cur2Label;
    private Label valueLabel;

    private int index = 0;
    private bool canWork = false;

    public override void _Ready()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

        UpdateCurrency();

        cur1Label = GetNode<Label>("Display_normal/label_cur");
        cur2Label = GetNode<Label>("Display_normal/label_convert");
        valueLabel = GetNode<Label>("Display_normal/Value");
    }

    public override void _Process(float delta)
    {
        if (!canWork)
        {
            GetNode<Button>("Update_button").Text = "Aktualizowanie";
            return;
        }
        else
        {
            GetNode<Button>("Update_button").Text = "Zaktualizuj kursy";
        }

        var calculatedValue = CalculateValue();

        cur1Label.Text = String.Format("{0} {1}",textValue,suffixes[type1]);
        cur2Label.Text = String.Format("{0:0.00} {1}",calculatedValue,suffixes[type2]);

        valueLabel.Text = String.Format("1 {0} = {1} {2}",suffixes[type1],values[type1][type2],suffixes[type2]);
    }

    private async void UpdateCurrency()
    {
        index = 0;
        canWork = false;
        values.Clear();

        var http = GetNode<HTTPRequest>("HTTPRequest");
        foreach (types currency in (types[]) Enum.GetValues(typeof(types)))
        {
            var cur = Convert.ToString(currency);
            var site = String.Format("https://api.exchangerate.host/latest?base={0}&symbols=PLN,USD,EUR,GBP,CNY,JPY,RUB,UAH",cur);
            var error = http.Request(site);
            if (error == Error.Ok)
                await ToSignal(http,"request_completed");
            else
                GetNode<Button>("Update_button").Text = "Błąd sieci";
        }
    }

    private void GetCurrencyData(int result, int response_code, string[] headers, byte[] body)
    {
        if (response_code == 200)
        {
            JSONParseResult json = JSON.Parse(Encoding.UTF8.GetString(body));
            var parseResult = json.Result as Godot.Collections.Dictionary;
            var parseRates = parseResult["rates"] as Godot.Collections.Dictionary;
            var rates = new Dictionary<types,decimal>();

            foreach (types currency in (types[]) Enum.GetValues(typeof(types)))
            {
                var cur = Convert.ToString(currency);
                rates.Add(currency,Convert.ToDecimal(parseRates[cur]));
            }

            if (Enum.IsDefined(typeof(types),index))
            {
                var value = (types)index;
                values.Add(value,rates);
            }
            index++;

            if (index == 8)
                canWork = true;
        }
        else
        {
            GetNode<Button>("Update_button").Text = "Błąd sieci";
        }
    }

    private decimal CalculateValue()
    {
        var value = values[type1][type2];

        return Convert.ToDecimal(textValue) * value;
    }

    private void SelectCur1(int index)
    {
        if (!canWork)
            return;

        type1 = ids[index];
    }

    private void SelectCur2(int index)
    {
        if (!canWork)
            return;

        type2 = ids[index];
    }

    private void CPressed()
    {
        if (!canWork)
            return;

        textValue = "0";
    }

    private void DelPressed()
    {
        if (!canWork)
            return;

        textValue = StringExtensions.Substr(textValue,0,textValue.Length - 1);
        if (textValue == "")
            textValue = "0";
    }

    private void AddNum(string number)
    {
        if (!canWork)
            return;

        if (textValue == "0")
            textValue = "";

        if (StringExtensions.Find(textValue,".") != -1)
        {
            var split = textValue.Split(".");
            if (split[1].Length > 1)
                return;
        }

        textValue += number;
    }

    private void DotPressed()
    {
        if (!canWork)
            return;

        if (StringExtensions.Find(Convert.ToString(textValue),".") == -1)
            textValue += ".";
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
