using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowndSensor : MonoBehaviour
{

    public bool isGrounded;
    private Enemy _enemyScript;
    public Rigidbody2D rigidBody;
    public float jumpDamage = 5;

    void Awake()
    {
        rigidBody = GetComponentInParent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 3)
        {
            isGrounded = true;
            //Debug.Log(collider.gameObject.name); --> esto hace que se vea en la consola todo bloque que pisa
        }
        else if(collider.gameObject.layer == 6)
        {
            rigidBody.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
            _enemyScript = collider.gameObject.GetComponent<Enemy>();
            _enemyScript.TakeDamage(jumpDamage);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 3)
        {
            isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 3)
        {
            isGrounded = false;
        }
    }
}
