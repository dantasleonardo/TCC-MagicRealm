using UnityEngine;

[CreateAssetMenu(fileName = "Mage Character", menuName = "Enemy/Mage/Character")]
public class Mage : ScriptableObject
{
    public new string name;
    public int totalLife;
    public int totalAttacks;
    public Bullet[] attacksPrefabs;
    public float distanceSeek;
    public float distanceAttack;
    public float stopDistance;
    public float Speed;
}