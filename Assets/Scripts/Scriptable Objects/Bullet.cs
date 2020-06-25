using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Units/Attack Unit/Bullet")]
public class Bullet : ScriptableObject
{
    public int damage;
    public float speed;
    public GameObject bulletPrefab;
}