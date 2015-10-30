public class ExitDoorN : ExitDoor
{
    public override void Start()
    {
        base.Start();
        exitDir = "n";
    }
    public override void SetDirection()
    {
        exitDir = "n";
    }
}