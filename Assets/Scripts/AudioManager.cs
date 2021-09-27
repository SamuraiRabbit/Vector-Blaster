using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _managerAudio;
    [SerializeField]
    private AudioClip _playerShoot, _playerExplosion, _enemyExplosion;

    // Start is called before the first frame update
    void Start()
    {
        _managerAudio = GetComponent<AudioSource>();
    }

    public void PlayerShootAudio()
    {
        _managerAudio.PlayOneShot(_playerShoot, 1.0f);
    }

    public void PlayerExplosionAudio()
    {
        _managerAudio.PlayOneShot(_playerExplosion, 1.0f);
    }

    public void EnemyExplosionAudio()
    {
        _managerAudio.PlayOneShot(_enemyExplosion, 1.0f);
    }
}
