using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    //Movimiento
    public int direction = 1;
    public float enemySpeed = 2;
    private Rigidbody2D _rigidBody;
    public BoxCollider2D boxCollider;
    //Animacion
    private Animator _animator;
    private AudioSource _audioSource;
    //Audio
    public AudioClip audioClip;
    public AudioClip hitSFX;
    public AudioClip goombaDeathSFX;
    public float maxHealth = 5;         //La vida es un mierdon historico, ayudame silksong tytyty.
    public float currentHealth;
    private Slider healthBar;
    private GameManager gameManager;


    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider2D>();
        healthBar = GetComponentInChildren<Slider>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Start()
    {
        
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(enemySpeed * direction, _rigidBody.velocity.y);
    }

    public void Death()
    {
        gameManager.Kills();
        _audioSource.PlayOneShot(goombaDeathSFX);
        direction = 0;
        _rigidBody.gravityScale = 0;
        _animator.SetTrigger("IsDead");
        boxCollider.enabled = false;
        Destroy(gameObject, 0.3f);
    }
    public void TakeDamage(float damage)
    {
        
        _audioSource.PlayOneShot(hitSFX);
        Debug.Log("Daño recibido?");
        currentHealth-= damage;
        healthBar.value = currentHealth;
        

        if(currentHealth <=0)
        {
            Death();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //Destroy(collision.gameObject);
            PlayerControl playerScript = collision.gameObject.GetComponent<PlayerControl>(); //variable que accede al playercontrol(script para ejecturar la funcion death que esta alli)
            playerScript.Death();

        }

        if(collision.gameObject.CompareTag("Tuberia") || collision.gameObject.layer == 6)
        {
            direction *= -1;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Tuberia") || collider.gameObject.layer == 6)
        {
            direction *= -1;
        }
    }

    void OnBecameVisible()
    {
        direction = 1;
        gameManager.enemiesInScreen.Add(gameObject);
    }

    void OnBecameInvisible()
    {
        direction = 0;
        gameManager.enemiesInScreen.Remove(gameObject);
    }
}
