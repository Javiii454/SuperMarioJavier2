using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour

{
    private AudioSource _audioSource;
    public AudioClip bgm;
    public AudioClip gameOver;
    private GameManager _gameManager;

    //Cronometro
    public float delay = 5;
    public float timer;
    private bool _timerFinished = false;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayBGM();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(!_gameManager.isPlaying && !_timerFinished)
        {
            DeathBGM();
        }*/
        
    }

    void PlayBGM()
    {
        _audioSource.clip = bgm;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public void PauseBGM()
    {
        if(_gameManager.isPaused)
        {
            _audioSource.Pause();
        }
        else
        {
            _audioSource.Play();
        }
    }

    /*public void DeathBGM()
    {
        _audioSource.Stop();
        
        timer += Time.deltaTime;

        if(timer >= delay)
        {
            _timerFinished = true;
            _audioSource.PlayOneShot(gameOver);
        }
    }*/

    public IEnumerator DeathBGM()
    {
        _audioSource.Stop();

        yield return new WaitForSeconds(delay);

        _audioSource.PlayOneShot(gameOver);
    }
    public void Win()
    {
        _audioSource.Pause();
    }
}
