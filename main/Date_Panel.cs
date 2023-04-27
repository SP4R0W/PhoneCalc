using Godot;
using System;
using Godot.Collections;

public class Date_Panel : Panel
{
    enum operations {
        DIFF,
        ADD,
        SUB
    }

    Dictionary<double,string> months = new Dictionary<double, string>(){
        {1,"January"},
        {2,"February"},
        {3,"March"},
        {4,"April"},
        {5,"May"},
        {6,"June"},
        {7,"July"},
        {8,"August"},
        {9,"September"},
        {10,"October"},
        {11,"November"},
        {12,"December"},
    };

    Dictionary<double,double> month_days = new Dictionary<double, double>(){
        {1,31},
        {2,28},
        {3,31},
        {4,30},
        {5,31},
        {6,30},
        {7,31},
        {8,31},
        {9,30},
        {10,31},
        {11,30},
        {12,31},
    };

    Label year1;
    Label year2;
    Label month1;
    Label month2;
    Label day1;
    Label day2;

    Label valueLabel;

    SpinBox spinYear1;
    SpinBox spinYear2;
    SpinBox spinMonth1;
    SpinBox spinMonth2;
    SpinBox spinDay1;
    SpinBox spinDay2;
    SpinBox spinYears;
    SpinBox spinMonths;
    SpinBox spinDays;

    CheckButton diff;
    CheckButton add;
    CheckButton sub;

    DateTime date1 = new DateTime();
    DateTime date2 = new DateTime();

    operations operation = operations.DIFF;

    public override void _Ready()
    {
        year1 = GetNode<Label>("Display_normal/Labels1/Year1");
        year2 = GetNode<Label>("Display_normal/Labels2/Year2");
        month1 = GetNode<Label>("Display_normal/Labels1/Month1");
        month2 = GetNode<Label>("Display_normal/Labels2/Month2");
        day1 = GetNode<Label>("Display_normal/Labels1/Day1");
        day2 = GetNode<Label>("Display_normal/Labels2/Day2");

        valueLabel = GetNode<Label>("Display_normal/Value_Panel/Value");

        spinYear1 = GetNode<SpinBox>("Display_normal/SpinBox1/Year1");
        spinYear2 = GetNode<SpinBox>("Display_normal/SpinBox2/Year2");
        spinMonth1 = GetNode<SpinBox>("Display_normal/SpinBox1/Month1");
        spinMonth2 = GetNode<SpinBox>("Display_normal/SpinBox2/Month2");
        spinDay1 = GetNode<SpinBox>("Display_normal/SpinBox1/Day1");
        spinDay2 = GetNode<SpinBox>("Display_normal/SpinBox2/Day2");

        spinYears = GetNode<SpinBox>("Display_normal/SpinBox3/Years");
        spinMonths = GetNode<SpinBox>("Display_normal/SpinBox3/Months");
        spinDays = GetNode<SpinBox>("Display_normal/SpinBox3/Days");

        diff = GetNode<CheckButton>("Display_normal/Options/Difference");
        add = GetNode<CheckButton>("Display_normal/Options/Add");
        sub = GetNode<CheckButton>("Display_normal/Options/Sub");

        date1 = DateTime.Now;
        date2 = DateTime.Now;

        spinDay1.Value = date1.Day;
        spinDay2.Value = date2.Day;
        spinMonth1.Value = date1.Month;
        spinMonth2.Value = date1.Month;
        spinYear1.Value = date1.Year;
        spinYear2.Value = date2.Year;

        GetNode<Panel>("Display_normal/Labels2").Visible = true;
        GetNode<HBoxContainer>("Display_normal/SpinBox2").Visible = true;

        GetNode<Panel>("Display_normal/Labels3").Visible = false;
        GetNode<HBoxContainer>("Display_normal/SpinBox3").Visible = false;

        diff.Pressed = true;
        add.Pressed = false;
        sub.Pressed = false;
    }

    public override void _Process(float delta)
    {
        day1.Text = Convert.ToString(spinDay1.Value);
        day2.Text = Convert.ToString(spinDay2.Value);
        month1.Text = months[spinMonth1.Value];
        month2.Text = months[spinMonth2.Value];
        year1.Text = Convert.ToString(spinYear1.Value);
        year2.Text = Convert.ToString(spinYear2.Value);

        if (spinMonth1.Value == 2 && ((spinYear1.Value % 4 == 0 && spinYear1.Value % 100 != 0) || (spinYear1.Value % 400 == 0)))
            spinDay1.MaxValue = 29;
        else
            spinDay1.MaxValue = month_days[spinMonth1.Value];

        if (spinMonth2.Value == 2 && ((spinYear2.Value % 4 == 0 && spinYear2.Value % 100 != 0) || (spinYear2.Value % 400 == 0)))
            spinDay2.MaxValue = 29;
        else
            spinDay2.MaxValue = month_days[spinMonth2.Value];


        date1 = new DateTime(year:Convert.ToInt32(spinYear1.Value),month:Convert.ToInt32(spinMonth1.Value),day:Convert.ToInt32(spinDay1.Value));
        date2 = new DateTime(year:Convert.ToInt32(spinYear2.Value),month:Convert.ToInt32(spinMonth2.Value),day:Convert.ToInt32(spinDay2.Value));

        CalculateValue();
    }

    void SetCurrentDate1()
    {
        date1 = DateTime.Now;
        spinDay1.Value = date1.Day;
        spinMonth1.Value = date1.Month;
        spinYear1.Value = date1.Year;
    }

    void SetCurrentDate2()
    {
        date2 = DateTime.Now;
        spinDay2.Value = date2.Day;
        spinMonth2.Value = date2.Month;
        spinYear2.Value = date2.Year;
    }

    void CalculateValue()
    {
        switch (operation)
        {
            case operations.DIFF:
                CalculateDifference();
                break;
            case operations.ADD:
                CalculateAdd();
                break;
            case operations.SUB:
                CalculateSub();
                break;
        }
    }

    void CalculateDifference()
    {
        TimeSpan value = date1 - date2;
        valueLabel.Text = String.Format("The difference between dates is: {0} days",Math.Abs(value.Days));
    }

    void CalculateAdd()
    {
        DateTime dateCopy = date1;
        dateCopy = dateCopy.AddYears(Convert.ToInt32(spinYears.Value));
        dateCopy = dateCopy.AddMonths(Convert.ToInt32(spinMonths.Value));
        dateCopy = dateCopy.AddDays(Convert.ToInt32(spinDays.Value));

        valueLabel.Text = String.Format("Date: {0}",dateCopy.ToShortDateString());
    }

    void CalculateSub()
    {
        DateTime dateCopy = date1;
        dateCopy = dateCopy.AddYears(Convert.ToInt32(spinYears.Value) * -1);
        dateCopy = dateCopy.AddMonths(Convert.ToInt32(spinMonths.Value) * -1);
        dateCopy = dateCopy.AddDays(Convert.ToInt32(spinDays.Value) * -1);

        valueLabel.Text = String.Format("Date: {0}",dateCopy.ToShortDateString());
    }

    void Difference()
    {
        diff.Pressed = true;
        add.Pressed = false;
        sub.Pressed = false;
        operation = operations.DIFF;

        GetNode<Panel>("Display_normal/Labels2").Visible = true;
        GetNode<HBoxContainer>("Display_normal/SpinBox2").Visible = true;

        GetNode<Panel>("Display_normal/Labels3").Visible = false;
        GetNode<HBoxContainer>("Display_normal/SpinBox3").Visible = false;
    }

    void Add()
    {
        add.Pressed = true;
        diff.Pressed = false;
        sub.Pressed = false;
        operation = operations.ADD;

        GetNode<Panel>("Display_normal/Labels2").Visible = false;
        GetNode<HBoxContainer>("Display_normal/SpinBox2").Visible = false;

        GetNode<Panel>("Display_normal/Labels3").Visible = true;
        GetNode<HBoxContainer>("Display_normal/SpinBox3").Visible = true;
    }

    void Sub()
    {
        sub.Pressed = true;
        diff.Pressed = false;
        add.Pressed = false;
        operation = operations.SUB;

        GetNode<Panel>("Display_normal/Labels2").Visible = false;
        GetNode<HBoxContainer>("Display_normal/SpinBox2").Visible = false;

        GetNode<Panel>("Display_normal/Labels3").Visible = true;
        GetNode<HBoxContainer>("Display_normal/SpinBox3").Visible = true;
    }
}
