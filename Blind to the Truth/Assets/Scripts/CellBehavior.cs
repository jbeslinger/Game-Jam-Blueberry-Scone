using System;
using UnityEngine;

public class CellBehavior : MonoBehaviour
{
    #region Enums
    [Flags] public enum Exits { NONE = 0, NORTH = 1, WEST = 2, SOUTH = 4, EAST = 8 }
    #endregion

    #region Fields
    public Exits myExits = Exits.NONE;
    public int numberOfExits = 0;
    public bool isStart, isGoal;
    public Transform keySpawnPoint;
    #endregion

    #region Methods
    private Exits FlipBit(Exits value, int bitToFlip)
    {
        return (Exits)((int)value ^ bitToFlip);
    }

    private Exits TurnBitOn(Exits value, int bitToTurnOn)
    {
        return (Exits)((int)value | bitToTurnOn);
    }
    #endregion
}
