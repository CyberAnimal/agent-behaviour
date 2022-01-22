using System;
using UnityEngine;

public class Turning : MonoBehaviour
{
    [Flags]
    public enum Direction
    {
        Right = 1,
        Left = 2
    }

    [SerializeField] private Direction TurnDirection;
    public Direction SelfDirection => TurnDirection;

    [SerializeField] private float _value = 90;
    public float Value => _value;

    public float GetDirection()
    {
        float value;

        if (TurnDirection == Direction.Left)
            value = -_value;

        else value = _value;

        return value;
    }
}
