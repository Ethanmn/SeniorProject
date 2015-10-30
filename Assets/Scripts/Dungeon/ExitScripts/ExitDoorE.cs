public class ExitDoorE : ExitDoor
{
    public override void Start()
    {
        base.Start();
        exitDir = "e";
    }
    public override void SetDirection()
    {
        exitDir = "e";
    }
}