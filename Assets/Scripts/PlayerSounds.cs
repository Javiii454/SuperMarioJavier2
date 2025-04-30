using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip footStepsAudio;
    private GrowndSensor growndSensor;
    private bool alreadyPlaying = false;
    private ParticleSystem particleSystem;
    private Transform particlesTransform;
    private Vector3 particlesPosition;
    // Start is called before the first frame update

    void Awake()
    {
        growndSensor = GetComponentInChildren<GrowndSensor>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particlesTransform = particleSystem.transform;
        particlesPosition = particlesTransform.localPosition;

    }
    void Start()
    {
        audioSource.loop = true;
        audioSource.clip = footStepsAudio;
        
    }

    // Update is called once per frame
    void Update()
    {
        FootStepSouns();
    }

    void FootStepSouns()
    {
        if(growndSensor.isGrounded && Input.GetAxisRaw("Horizontal") != 0 && !alreadyPlaying)
        {   
            particlesTransform.SetParent(gameObject.transform);
            particlesTransform.localPosition = particlesPosition;
            particlesTransform.rotation = transform.rotation;
            audioSource.Play();
            particleSystem.Play();
           
            alreadyPlaying = true;
        }
        else if (!growndSensor.isGrounded || Input.GetAxisRaw("Horizontal") == 0)
        {
            particlesTransform.SetParent(null);
            audioSource.Stop();
            particleSystem.Stop();
            alreadyPlaying = false; 
        }
    }
}
