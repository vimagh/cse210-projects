using System;

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string shortName, string description, int points,
        int target, int bonus, int amountCompleted = 0)
        : base(shortName, description, points)
    {
        if (target <= 0)
            throw new ArgumentOutOfRangeException(nameof(target), "The target must be positive.");
        if (bonus < 0 || bonus > int.MaxValue - points)
            throw new ArgumentOutOfRangeException(nameof(bonus), "The bonus is out of range.");
        if (amountCompleted < 0 || amountCompleted > target)
            throw new ArgumentOutOfRangeException(nameof(amountCompleted), "Progress is out of range.");

        _target = target;
        _bonus = bonus;
        _amountCompleted = amountCompleted;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
            return 0;

        _amountCompleted++;
        return GetPoints() + (IsComplete() ? _bonus : 0);
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        return $"{base.GetDetailsString()} -- Completed {_amountCompleted}/{_target} times";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{GetCommonSaveData()},{_target},{_bonus},{_amountCompleted}";
    }
}