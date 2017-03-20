public struct Telegram
{
    public int sender;
    public int receiver;
    public int message;
    public float dispatchTime;
    public object extraInfo;

    public void Clear()
    {
        sender = 0;
        receiver = 0;
        message = 0;
        dispatchTime = 0;
        extraInfo = 0;
    }
}