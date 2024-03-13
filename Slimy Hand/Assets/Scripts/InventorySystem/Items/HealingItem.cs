using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealingItem : Item
{
    [SerializeField] private float healAmount;
    public override bool Use()
    {
        //Player.Instance.health.Heal(healAmount);
        return true;
    }
}