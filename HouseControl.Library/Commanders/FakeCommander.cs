namespace HouseControl.Library;

public class FakeCommander : ICommander
{
    public Task SendCommand(int deviceNumber, DeviceCommand command)
    {
#if DEBUG
        Console.WriteLine($"Device {deviceNumber}: {command}");
#endif
        return Task.CompletedTask;
    }
}
