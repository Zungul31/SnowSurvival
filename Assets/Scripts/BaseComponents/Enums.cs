using System;

public enum EManagerStatus : byte
{
    Shutdown,
    Initializing,
    Started,
}

public enum EPlayerStatus : byte
{
    Idle,
    Moves,
    Mines
}

public enum EItemType
{
    None = -2,
    All,
    Wood,       // Древесина
    Plank,      // Доска
    Cobble,     // Булыжник
    Rock,       // Камень
    
    Gold,       // Золото
}

public enum ETypeInteractiveObj : byte
{
    Spawner,
    Taker,
    Giver,
    Store,
}

public enum ETypeTool : byte
{
    None,
    Axe,        // Топор
    Shovel,     // Лопата
    Pickaxe,    // Кирка
}