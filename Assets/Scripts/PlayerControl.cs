using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerControl : MonoBehaviour

{

    public int direction = 1;
    private float imputHorizontal;
    public float playerSpeed = 4.5f;
    public float jumpForce = 10;

    [SerializeField] private float _dashForce = 20f;
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCooldwon = 1;
    [SerializeField] private float _attackDamage = 10;
    [SerializeField] private float _attackRadius = 1;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform hitBoxPosition;
    private bool _canDash = true;
    private bool _isDashing = false;
    [SerializeField] private float _baseChargedAttackDamage = 15;
    [SerializeField] private float _maxChargedAttackDamage = 40; 
    private float _chargedAttackDamage;

    private Rigidbody2D _rigidBody;
    private GrowndSensor _growndSensor; // _ delante significa que es privada :33
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private AudioSource _audioSource;
    public AudioClip jumpSFX;
    public AudioClip deathSFX;
    private BoxCollider2D _boxCollider;
    private GameManager _gameManager;
    private SoundManager _soundManager;
    //disparo
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public AudioClip shootSFX;
    //Dispara si o no?
    public bool canShoot = false;
    //timerpowerup
    public float powerUpDuration = 10;
    public float powerUpTimer;
    public Image powerUpImage;




    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _growndSensor = GetComponentInChildren<GrowndSensor>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
    }

    string numeros = ":33";
    
    void Start() // Start is called before the first frame update
    {
        _chargedAttackDamage = _baseChargedAttackDamage;
        Debug.Log(numeros);
        //transform.position = new Vector3(-93, -3, 0); hace TP a mario :33
    }

    
    void Update() // Update is called once per frame
    {
        if(!_gameManager.isPlaying)
        {
            return;

            
        }
        if(_gameManager.isPaused)
            {
                return;
            }

        if(_isDashing)
        {
            return;
        }

        imputHorizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump"))
        {
            if (_growndSensor.isGrounded || _growndSensor.canDoubleJump )
            {
                 Jump();
            }
           
        }
        /*if(Input.GetButtonDown("Fire2"))
        {
            NormalAttack();
        }*/
        if(Input.GetButtonDown("Fire2"))
        {
            AttackCharge();
        }
        if(Input.GetButtonUp("Fire2"))
        {
            ChargedAttack();
        }

        if(Input.GetButtonDown("Fire1") && canShoot)
        {
            Shoot();
        }

        Movement(); //Llama la funcion que acabamos de crear para el movimiento
        
        _animator.SetBool("IsJumping", !_growndSensor.isGrounded);

        if(canShoot)
        {
            PowerUpTimer();                                                                                         
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && _canDash)
        {
            StartCoroutine(Dash());
        }
        /*if(_growndSensor.isGrounded)
        {
            _animator.SetBool("IsJumping", true);            
        }
        else
        {            
            _animator.SetBool("IsJumping", false);
        }*/

        //transform.position = new Vector3(transform.position.x + direction * playerSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        //transform.Translate(new Vector3(direction * playerSpeed * Time.deltaTime, 0, 0));
        //transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + imputHorizontal, transform.position.y), playerSpeed * Time.deltaTime); //para plataformas moviles
    }

    void FixedUpdate() // se llama automaticamente todo el rato las veces por segundo que tenga unity
    {
        if(_isDashing)
        {
            return;
        }
        _rigidBody.velocity = new Vector2(imputHorizontal * playerSpeed, _rigidBody.velocity.y);
        //_rigidBody.AddForce(new Vector2(imputHorizontal, 0));
        //_rigidBody.MovePosition(new Vector2(100, 0));
    }

    void Movement() //Funcion creada para poner todo lo relacionado con el movimiento (no se llama automaticamente)
    {
        if(imputHorizontal > 0)
         {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _animator.SetBool("IsRunning", true);   //modifica el IsEunning que es el condicionante de que se active la animacion de correr
        }
        else if(imputHorizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _animator.SetBool("IsRunning", true);
        }
        else        //else se ejecuta si ninguna de las de funciones del if o el esle if se cumple
        {
            _animator.SetBool("IsRunning", false);
        }
    }

    void Jump()
    
    {
        if(!_growndSensor.isGrounded)
        {
            _growndSensor.canDoubleJump = false;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x,0);
            // _rigidBody.AddForce(Vector2.up * doublejumpForce, ForceMode2D.Impulse); para cambiar por separado la fuerza de cada salto 
            //Animación de doble salto aqui
        }
        else
        {
            //Animación de salto aqui
        }
        _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _audioSource.PlayOneShot(jumpSFX); //play one shot hace que se tire el sonido aunque algo se reproduzca tipo generalmente para ataques, disparos o daños
    }

    IEnumerator Dash()
    {
        float gravity = _rigidBody.gravityScale; 
        _rigidBody.gravityScale = 0;
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x,0);
        _isDashing = true;
        _canDash = false; 
        _rigidBody.AddForce(transform.right * _dashForce, ForceMode2D.Impulse); 
        yield return new WaitForSeconds(_dashDuration);
        _rigidBody.gravityScale = gravity;
        _isDashing = false;
        yield return new WaitForSeconds(_dashCooldwon);
        _canDash = true;

    }

    void NormalAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(hitBoxPosition.position, _attackRadius, _enemyLayer ); // Como un trigger enter que comprueba si algo de la layer 6 ha entrado dentro 
       
        foreach(Collider2D enemy in enemies)
        {
           Enemy enemyScript = enemy.GetComponent<Enemy>();
           enemyScript.TakeDamage(_attackDamage);
        }
    }
    void AttackCharge()
    {
        if(_chargedAttackDamage < _maxChargedAttackDamage)
       {
         _chargedAttackDamage += Time.deltaTime;
       }
       else
       {
        _chargedAttackDamage = _maxChargedAttackDamage;
       }
    }

    void ChargedAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(hitBoxPosition.position, _attackRadius,_enemyLayer); // Como un trigger enter que comprueba si algo de la layer 6 ha entrado dentro 
       
        foreach(Collider2D enemy in enemies)
        {
           Enemy enemyScript = enemy.GetComponent<Enemy>();
           enemyScript.TakeDamage(_chargedAttackDamage);
        }
        _chargedAttackDamage = _baseChargedAttackDamage;
    }

    public void Death()
    {
        _animator.SetTrigger("IsDead");
        _audioSource.PlayOneShot(deathSFX);
        _boxCollider.enabled = false;
        Destroy(_growndSensor.gameObject);
        imputHorizontal = 0;
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.AddForce(Vector2.up * jumpForce / 1.5f, ForceMode2D.Impulse);

        StartCoroutine(_soundManager.DeathBGM());//opcion 2: _soundManager.StartCoroutine("DeathBGM"); 

        //_soundManager.Invoke("DeathBGM", deathSFX.length); //el invoke te permite llamar a una funcion pero meterle un tiempo de cooldown sabes
        //_soundManager.DeathBGM();

        _gameManager.isPlaying = false;

        Destroy(gameObject, 8);

       


    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        _audioSource.PlayOneShot(shootSFX);
    }

    void PowerUpTimer()
    {
        powerUpTimer += Time.deltaTime;

        powerUpImage.fillAmount = Mathf.InverseLerp(powerUpDuration, 0, powerUpTimer);

        if(powerUpTimer >= powerUpDuration)
        {
            canShoot = false;
            powerUpTimer = 0;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(hitBoxPosition.position, _attackRadius);
    }
}
