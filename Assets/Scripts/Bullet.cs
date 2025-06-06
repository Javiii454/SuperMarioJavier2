using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    public float bulletSpeed = 10;
    public float bulletDamage = 2;
    
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _rigidBody.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6)
        {
            Enemy enemyScript = collider.gameObject.GetComponent<Enemy>();
            enemyScript.TakeDamage(bulletDamage);
            BulletDeath();
        }

        if(collider.gameObject.CompareTag("Tuberia"))
        {
            BulletDeath();
        }
    }

    void BulletDeath()
    {
        Destroy(gameObject);
    }

}
