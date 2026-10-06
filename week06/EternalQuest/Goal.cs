using System;

public abstract class Goal
{
    private string _shortName;
    private string _description;
    private int _points;

    protected Goal(string shortName, string description, int points)
    {
        if (string.IsNullOrWhiteSpace(shortName))
            throw new ArgumentException("A goal must have a name.");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("A goal must have a description.");
        if (points <= 0)
            throw new ArgumentOutOfRangeException(nameof(points), "Points must be positive.");

        _shortName = shortName;
        _description = description;
        _points = points;
    }

    public string GetShortName()
    {
        return _shortName;
    }

    protected int GetPoints()
    {
        return _points;
    }

    // Each goal decides how to record progress and how many points to award.
    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetStringRepresentation();

    public virtual string GetDetailsString()
    {
        string marker = IsComplete() ? "X" : " ";
        return $"[{marker}] {_shortName} ({_description})";
    }

    protected string GetCommonSaveData()
    {
        // Escape punctuation so commas and colons in text do not break loading.
        string name = Uri.EscapeDataString(_shortName);
        string description = Uri.EscapeDataString(_description);
        return $"{name},{description},{_points}";
    }
}