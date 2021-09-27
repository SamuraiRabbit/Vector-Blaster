using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.5f, _xRange = 25, _zRange = 14;
    public int pointValue = 10;
    private Rigidbody _enemyRb;
    private GameObject _player;
    private GameManager _gameManager;
    private AudioManager _audioManager;
    [SerializeField]
    private ParticleSystem _enemyExplosion;

    // Start is called before the first frame update
    void Start()
    {   // On game start, sets the enemy rigid body and the player.
        _enemyRb = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
        // References the game manager and the game manager script, allowing for script communication.
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();

    }

    // FixedUpdate is called before the Update method
    void FixedUpdate()
    {
        if (_player != null)
        {
            // Will move towards a target when it is active, subtract current postion from target position (and then * speed!)
            //.normalized prevents a greater magnitude of force, the further away the target is.
            Vector3 lookDirection = (_player.transform.position - transform.position).normalized;

            _enemyRb.AddForce(lookDirection * _speed);
        }
        else _speed = 0;// Stops if target is not active.

        // Prevents enemy from moving out of bounds. Should replace with invisible walls to clean up code?
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
    private void OnTriggerEnter(Collider projectile)
    {
        // Plays sound effect
        _audioManager.EnemyExplosionAudio();
        // Plays particle effect
        Instantiate(_enemyExplosion, transform.position, _enemyExplosion.transform.rotation);
        // Destroys this game object when it collides with a projectile.
        Destroy(gameObject);
        Destroy(this.gameObject);
        


        if (_gameManager.isGameActive)
        {
            _gameManager.UpdateScore(pointValue);
        }
        
    }


}
