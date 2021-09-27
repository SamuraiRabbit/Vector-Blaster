using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    [SerializeField]
    private float _speed = 12, _turnSpeed = 5, _verticalInput, _horizontalInput, _xRange = 23, _zRange = 12;
    [SerializeField]
    private GameObject _projectilePrefab;
    private AudioManager _audioManager;
    private GameManager _gameManager;
    [SerializeField]
    private Transform _firePoint;
    private Rigidbody _playerRb;
    [SerializeField]
    private ParticleSystem _playerExplosion;

    // Start is called before the first frame update
    void Start()
    {   //
        _playerRb = GetComponent<Rigidbody>();
        _audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        //References the game manager and the game manager script, allowing for script communication.
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // FixedUpdate is called before the Update method
    void FixedUpdate()
    {
        if (_gameManager.isGameActive)
        {
            // Get player input
            _verticalInput = Input.GetAxis("Vertical");
            _horizontalInput = Input.GetAxis("Horizontal");

            // Apply force to player character
            _playerRb.AddForce(transform.forward * Time.fixedDeltaTime * _speed * _verticalInput);
            // Rotates player character
            transform.Rotate(Vector3.up * Time.fixedDeltaTime * _turnSpeed * _horizontalInput);


            // Prevents player from moving out of bounds. Should replace with invisible walls to clean up code?
            if (transform.position.x < -_xRange)
            {
                transform.position = new Vector3(-_xRange, transform.position.y, transform.position.z);
            }

            if (transform.position.x > _xRange)
            {
                transform.position = new Vector3(_xRange, transform.position.y, transform.position.z);
            }

            if (transform.position.z < -_zRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -_zRange);
            }

            if (transform.position.z > _zRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, _zRange);
            }
        }   

      

    }

    private void Update()
    {
        // Shoot when Fire1 button pressed
        if (Input.GetButtonDown("Fire1") && _gameManager.isGameActive)
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        // Fires gameObject in the _projectilePrefab variable from the _firepoint
        Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
        //plays sound effect
        _audioManager.PlayerShootAudio();
    }

    private void OnTriggerEnter(Collider enemy)
    {
        //plays sound effect
        _audioManager.PlayerExplosionAudio();
        // Runs particle effect
        Instantiate(_playerExplosion, transform.position, _playerExplosion.transform.rotation);
        // Destroys this game object and triggers game over when it collides with an enemy.
        Destroy(gameObject);
        Destroy(this.gameObject);
        
        _gameManager.GameOver();
    }
}
