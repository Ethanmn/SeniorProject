/* 
    PublisherBox represnts a "box" or set of static instances of publishers that subscribers can subscribe to.
    This is done so that there is only a single centralized publisher for each event that every subscriber (generally buffs) can subscribe to.
*/

using UnityEngine;
using System;
using System.Collections.Generic;

public static class PublisherBox
{
    public static OnHitPublisher onHitPub = new OnHitPublisher();
    public static OnDashPublisher onDashPub = new OnDashPublisher();
    public static OnHurtPublisher onHurtPub = new OnHurtPublisher();
    public static OnHealthChangePublisher onHealthChangePub = new OnHealthChangePublisher();
    public static OnAttackPublisher onAttackPub = new OnAttackPublisher();
    public static OnDeathPublisher onDeathPub = new OnDeathPublisher();
    public static OnCollidePublisher onCollidePub = new OnCollidePublisher();
}
