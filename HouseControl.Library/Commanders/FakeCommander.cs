namespace HouseControl.Library;

public class FakeCommander : ICommander
{
    public Task SendCommand(string message)
    {
    #if DEBUG
        Console.WriteLine(message);
#endif
        return Task.CompletedTask;
    }
}
