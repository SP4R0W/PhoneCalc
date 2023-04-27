using Godot;
using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

public class Currency_Panel : Panel
{
    enum types {
        PLN,
        USD,
        EUR,
        GBP,
        CNY,
        JPY,
        RUB,
        UAH,
    }

    Dictionary<types,Dictionary<types,decimal>> values = new Dictionary<types, Dictionary<types, decimal>>(){

    };

    Dictionary<types,string> suffixes = new Dictionary<types, string>(){
        {types.PLN,"PLN"},
        {types.USD,"USD"},
        {types.EUR,"EUR"},
        {types.GBP,"GBP"},
        {types.CNY,"CNY"},
        {types.JPY,"JPY"},
        {types.RUB,"RUB"},
        {types.UAH,"UAH"},
    };

    Dictionary<int,types> ids = new Dictionary<int, types>(){
        {0,types.PLN},
        {1,types.USD},
        {2,types.EUR},
        {3,types.GBP},
        {4,types.CNY},
        {5,types.JPY},
        {6,types.RUB},
        {7,types.UAH},
    };

    string textValue = "0";
    string convertedValue = "0";

    types type1 = types.PLN;
    types type2 = types.USD;

    Label cur1Label;
    Label cur2Label;
    Label valueLabel;

    int index = 0;
    bool canWork = false;

    const string EXCHANGE_RATES_SITE = "https://api.exchangerate.host/latest?base={0}&symbols=PLN,USD,EUR,GBP,CNY,JPY,RUB,UAH";
    const int SUCCESS = 200;

    public override void _Ready()
    {
        // This is enforcing . as decimal points. In some regions, the system may expect a comma and thus break our entire app when calculating numbers with decimal points.
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
            GetNode<Button>("Update_button").Text = "Updating...";
            return;
        }

        GetNode<Button>("Update_button").Text = "Update exchange rates";

        var calculatedValue = CalculateValue();

        cur1Label.Text = String.Format("{0} {1}",textValue,suffixes[type1]);
        cur2Label.Text = String.Format("{0:0.00} {1}",calculatedValue,suffixes[type2]);

        valueLabel.Text = String.Format("1 {0} = {1} {2}",suffixes[type1],values[type1][type2],suffixes[type2]);
    }

    async void UpdateCurrency()
    {
        index = 0;
        canWork = false;
        values.Clear();

        var http = GetNode<HTTPRequest>("HTTPRequest");
        foreach (types currency in (types[]) Enum.GetValues(typeof(types)))
        {
            // This is where we try and get the data from the site. This link can be easily modified to support more currencies.
            var cur = Convert.ToString(currency);
            var site = String.Format(EXCHANGE_RATES_SITE,cur);
            var error = http.Request(site);
            if (error == Error.Ok)
                await ToSignal(http,"request_completed");
            else
                GetNode<Button>("Update_button").Text = "Network Error";
        }
    }

    void GetCurrencyData(int result, int response_code, string[] headers, byte[] body)
    {
        if (response_code == SUCCESS)
        {
            // This is where we convert our data from request into more godot and c# friendly format
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
            GetNode<Button>("Update_button").Text = "Network Error";
    }

    decimal CalculateValue()
    {
        var value = values[type1][type2];

        return Convert.ToDecimal(textValue) * value;
    }

    void SelectCur1(int index)
    {
        if (!canWork)
            return;

        type1 = ids[index];
    }

    void SelectCur2(int index)
    {
        if (!canWork)
            return;

        type2 = ids[index];
    }

    void CPressed()
    {
        if (!canWork)
            return;

        textValue = "0";
    }

    void DelPressed()
    {
        if (!canWork)
            return;

        textValue = StringExtensions.Substr(textValue,0,textValue.Length - 1);
        if (textValue == "")
            textValue = "0";
    }

    void AddNum(string number)
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

    void DotPressed()
    {
        if (!canWork)
            return;

        if (StringExtensions.Find(Convert.ToString(textValue),".") == -1)
            textValue += ".";
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
