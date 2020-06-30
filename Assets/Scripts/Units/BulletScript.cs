using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BulletScript : MonoBehaviour
{
    public Bullet bulletProperties;
    [SerializeField] private int damage;
    [SerializeField] private float speed;

    [SerializeField] private Vector3 bulletDirection;

    private Rigidbody rigidbody;

    private void Awake()
    {
        InitItem();
        Debug.Log(Vector3.forward);
    }

    private void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    private void InitItem()
    {
        damage = bulletProperties.damage;
        speed = bulletProperties.speed;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mages"))
        {
            var enemy = other.gameObject.GetComponent<IEnemy>();
            DamageInEnemy(enemy);
        }

        if (other.CompareTag("Crystal"))
        {
            var target = other.gameObject.GetComponent<IUnit>();
            target.TakeDamage(damage);
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Rune"))
        {
            var target = other.gameObject.GetComponent<RuneScript>();
            target.HeIsBeenHit();
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Unit") || other.CompareTag("Attack")) return;
        Destroy(this.gameObject);
    }

    private void DamageInEnemy(IEnemy enemy)
    {
        enemy.TakeDamage(damage);
        Destroy(this.gameObject);
    }
}