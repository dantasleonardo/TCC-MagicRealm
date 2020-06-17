using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEnemy : MonoBehaviour
{
    public Mage properties;
    public abstract void TakeDamage(int damage);

    /// <summary>
    /// Attack Function. You must pass an integer value for what type of attack you want to execute.
    /// </summary>
    /// <param name="typeAttack">makes it possible to have 1 or more types of attack.</param>
    public abstract void Attack(int typeAttack);
}