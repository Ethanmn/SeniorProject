/* 
    PublisherBox represnts a "box" or set of static instances of publishers that subscribers can subscribe to.
    This is done so that there is only a single centralized publisher for each event that every subscriber (generally buffs) can subscribe to.
*/

public static class PublisherBox
{
    // Hitting a mob
    public static OnHitPublisher onHitPub = new OnHitPublisher();
    // Hero dashing
    public static OnDashPublisher onDashPub = new OnDashPublisher();
    // Hero is hurt/hit
    public static OnHurtPublisher onHurtPub = new OnHurtPublisher();
    // Hero's health has changed
    public static OnHealthChangePublisher onHealthChangePub = new OnHealthChangePublisher();
    // Hero is attacking
    public static OnAttackPublisher onAttackPub = new OnAttackPublisher();
    // Hero died
    public static OnDeathPublisher onDeathPub = new OnDeathPublisher();
    // Hero collides with a mob
    public static OnCollidePublisher onCollidePub = new OnCollidePublisher();
    // Hero's ammo has changed
    public static OnAmmoChangePublisher onAmmoChangePub = new OnAmmoChangePublisher();
    // Hero's active item is equipped
    public static OnEquipActivePublisher onEquipActivePub = new OnEquipActivePublisher();
    // Hero enters a room
    public static OnRoomEnterPublisher onRoomEnterPub = new OnRoomEnterPublisher();

    // A mob dies
    public static OnKillPublisher onKillPub = new OnKillPublisher();
}
