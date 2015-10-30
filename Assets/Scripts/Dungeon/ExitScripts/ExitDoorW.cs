public class ExitDoorW : ExitDoor
{
    public override void Start()
    {
        base.Start();
        exitDir = "w";
    }
    public override void SetDirection()
    {
        exitDir = "w";
    }
}