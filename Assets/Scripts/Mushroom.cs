using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    //movimiento
    private Rigidbody2D _rigidBody;
    public BoxCollider2D boxCollider;
    public int direction = 1;
    public float mushroomSpeed = 2;
    //audio
    private AudioSource _audioSource;
    public AudioClip deathMushroomSFX;
    //retraso
    public float delay = 2;
    //sprites
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(mushroomSpeed * direction, _rigidBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Tuberia") || collision.gameObject.layer == 7)
        {
            direction *= -1;
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerControl playerScript = collision.gameObject.GetComponent<PlayerControl>();
            playerScript.canShoot = true;
            playerScript.powerUpTimer = 0;
            StartCoroutine(MushroomTaken());
        }
    }

    public IEnumerator MushroomTaken()
    {
        direction = 0;
        _rigidBody.gravityScale = 0;
        boxCollider.enabled = false;
        _spriteRenderer.enabled = false;
        _audioSource.PlayOneShot(deathMushroomSFX);

        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
    
}
