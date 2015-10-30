public class ExitDoorS : ExitDoor
{
    public override void Start()
    {
        base.Start();
        exitDir = "s";
    }
    public override void SetDirection()
    {
        exitDir = "s";
    }
}