using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banderita : MonoBehaviour
{
     private AudioSource audioSource;
     private BoxCollider2D boxCollider;
     public AudioClip banderinSFX;
     private SoundManager soundManager;
    // Start is called before the first frame update
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
       
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
         if(collider.gameObject.CompareTag("Player"));
        {
            soundManager.Win();
            audioSource.PlayOneShot(banderinSFX);
            boxCollider.enabled = false;

            
        }
    }
   
}
