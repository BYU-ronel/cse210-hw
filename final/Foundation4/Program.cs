using System;
using System.Collections.Generic;

// Base class for all activities
public abstract class Activity
{
    protected DateTime _date;
    protected int _minutes;

    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    public virtual double GetDistance() { return 0; }
    public virtual double GetSpeed() { return 0; }
    public virtual double GetPace() { return 0; }

    public string GetSummary()
    {
        return $"{_date.ToString("dd MMM yyyy")} {_GetType()}({_minutes} min) - Distance {GetDistance():F1} miles, Speed {GetSpeed():F1} mph, Pace: {GetPace():F2} min per mile";
    }

    protected abstract string _GetType();
}

// Derived class for Running
public class Running : Activity
{
    private double _distance;

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance() { return _distance; }
    public override double GetSpeed() { return (_distance / _minutes) * 60; }
    public override double GetPace() { return _minutes / _distance; }

    protected override string _GetType() { return "Running"; }
}

// Derived class for Cycling
public class Cycling : Activity
{
    private double _speed;

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance() { return (_speed / 60) * _minutes; }
    public override double GetSpeed() { return _speed; }
    public override double GetPace() { return 60 / _speed; }

    protected override string _GetType() { return "Cycling"; }
}

// Derived class for Swimming
public class Swimming : Activity
{
    private int _laps;

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance() { return (_laps * 50 / 1000) * 0.62; }
    public override double GetSpeed() { return (GetDistance() / _minutes) * 60; }
    public override double GetPace() { return _minutes / GetDistance(); }

    protected override string _GetType() { return "Swimming"; }
}

class Program
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        activities.Add(new Running(new DateTime(2022, 11, 3), 30, 3.0));
        activities.Add(new Cycling(new DateTime(2022, 11, 3), 30, 6.0));
        activities.Add(new Swimming(new DateTime(2022, 11, 3), 30, 20));

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}