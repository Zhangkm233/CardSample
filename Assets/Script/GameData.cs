using UnityEngine;

public static class GameData
{
    public static int xfieldSize = 15;
    public static int yfieldSize = 9;
    public static int xOffset = 6;//偏移值 用于计算棋盘坐标
    public static int yOffset = 4;
    public enum GameState
    {
        START,
        PLANNING,
        STANDBY,
        PLAYING,
        END
    }

    public static GameState gameState = GameState.STANDBY;
    public static int turn = 0;
    public static int totalCost = 0;
}
