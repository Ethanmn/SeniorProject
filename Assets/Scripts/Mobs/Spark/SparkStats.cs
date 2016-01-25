using UnityEngine;


public class SparkStats : MobStats
{
    // What is the base shift time (to add to range)
    public float baseShiftTime = 3.0f;
    // How long it spends in each form
    public float shiftTime = 3.0f;
    // Timer for timing shift skitter
    public float shiftTimer = 0;
    // Is it skittering or shocking?
    public bool skitter = true;

    // How many times can it shock before going back?
    public int shockCount = 3;
    public int shockCounter = 0;

    // Timer for attack cooldown
    public float zapCooldown = 0.5f;
    public float zapCooldownTimer = 0;
}
