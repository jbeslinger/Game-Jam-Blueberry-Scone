using System;

class Cell
{
    #region Enums
    [Flags] public enum Exits { NONE = 0, NORTH = 1, EAST = 2, SOUTH = 4, WEST = 8 }
    #endregion

    #region Fields
    public bool isStart, isGoal;
    public Exits exits = Exits.NONE;
    #endregion

    #region Properties
    public int NumberOfExits { get; private set; } = 0;
    #endregion

    #region Methods
    public void AddExit(Exits exit)
    {
        if (((int)exits & (int)exit) == (int)exit)
            return;

        exits = TurnBitOn(exits, (int)exit);
        NumberOfExits++;
    }

    public bool HasTheExit(Exits exit)
    {
        return ((int)exits & (int)exit) == (int)exit;
    }

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
