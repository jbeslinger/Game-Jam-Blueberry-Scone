using System;
using UnityEngine;

public class BitwiseExercise : MonoBehaviour
{
    [Flags]
    public enum Exits { NONE = 0, NORTH = 1, WEST = 2, SOUTH = 4, EAST = 8 }

    public Exits myExits = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            myExits = TurnBitOn(myExits, 1);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            myExits = TurnBitOn(myExits, 2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            myExits = TurnBitOn(myExits, 4);
        if (Input.GetKeyDown(KeyCode.Alpha8))
            myExits = TurnBitOn(myExits, 8);
    }

    private Exits FlipBit(Exits value, int bitToFlip)
    {
        return (Exits)((int)value ^ bitToFlip);
    }

    private Exits TurnBitOn(Exits value, int bitToTurnOn)
    {
        return (Exits)((int)value | bitToTurnOn);
    }
}
