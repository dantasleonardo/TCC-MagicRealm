using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMagics : MonoBehaviour
{
    public Bullet bulletProperties;
    [SerializeField] private int damage;
    [SerializeField] private float speed;

    [SerializeField] private Vector3 bulletDirection;

    private Rigidbody rigidbody;

    private void Awake() {
        InitItem();
    }

    private void Start() {
        Destroy(this.gameObject, 5.0f);
    }

    private void InitItem() {
        damage = bulletProperties.damage;
        speed = bulletProperties.speed;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        rigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Unit") || other.CompareTag("Building")) {
            var unit = other.gameObject.GetComponent<IUnit>();
            DamageInEnemy(unit);
        }
        if (other.CompareTag("Mages") || other.CompareTag("Attack")) return;
        Destroy(this.gameObject);
        
    }

    private void DamageInEnemy(IUnit unit) {
        unit.TakeDamage(damage);
        Destroy(this.gameObject);
    }
}
