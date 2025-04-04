using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour


{
    //Private V
    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    //Public V
    public BoxCollider2D collider1;
    public BoxCollider2D collider2;
    public AudioClip boxSFX;
    

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void DestoryBox()
    {
        _audioSource.clip = boxSFX;
        _audioSource.Play();

        _spriteRenderer.enabled = false;
        collider1.enabled = false;
        collider2.enabled = false;

        Destroy(gameObject, boxSFX.length);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player")) //lo de despues de game object es lo mismo que poner .tag == "Player" :33
        {
            
            DestoryBox(); //Destroy es una funcion para destuir el objeto entero que viene con unity por defecto
        }
    }
}
