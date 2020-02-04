using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int damage);
    void TakeHealth(int amount);
    void AddEffectEmmiter<T>(T item) where T : MonoBehaviour;
}

