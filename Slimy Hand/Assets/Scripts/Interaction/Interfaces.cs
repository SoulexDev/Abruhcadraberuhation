using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void Damage(float amount, Vector3 direction = default, Vector3 position = default);
}
public interface IItem
{
    public bool Use();
}