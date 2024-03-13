using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodItem : Item
{
    [SerializeField] private float replenishAmount = 15;
    public override bool Use()
    {
        Player.Instance.stats.Eat(replenishAmount);
        return true;
    }
}