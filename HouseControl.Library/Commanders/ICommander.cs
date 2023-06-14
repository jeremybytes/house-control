namespace HouseControl.Library;

public interface ICommander
{
    Task SendCommand(string message);
}
