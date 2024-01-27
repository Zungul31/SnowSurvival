using System;

public enum EDirection : byte
{
    None,
    Up,
    Right,
    Down,
    Left
}

public enum EPlayerStatus : byte
{
    Idle,
    Moves,
    Mines
}

public enum EResources : byte
{
    Wood,       // Древесина
    Plank,      // Доска
    Cobble,     // Булыжник
    Rock,       // Камень
}