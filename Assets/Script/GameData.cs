using UnityEngine;

public static class GameData
{
    public static int xfieldSize = 15;
    public static int yfieldSize = 9;
    public enum GameState
    {
        START,
        PLANNING,
        STANDBY,
        END
    }

    public static GameState gameState = GameState.STANDBY;
    public static int turn = 0;
    public static int totalCost = 0;
}
