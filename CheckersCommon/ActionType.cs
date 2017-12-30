namespace CheckersCommon
{
    public enum ActionType
    {
        CreateRoom = 1,
        CloseRoom = 2,
        JoinRoom = 3,
        LeaveRoom = 4,
        StartGame = 5,
        Surrender = 6,
        MakeMove = 7,
        UpdateGameboard = 8,
#warning czy to jest potrzebne?
        KeepAlive = 999
    }
}
