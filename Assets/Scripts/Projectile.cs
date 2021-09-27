using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 40f, _topBound = 30f, _lowerBound = -30, _leftBound = -30f, _rightBound = 30f;
    // Boundaries of projectile
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Projectile aoutomatically moves forward
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);

        // If an object goes past the players view, remove that object. Could refactor with a switch statment? Or invisible walls?
        if (transform.position.z > _topBound)
        {
            Destroy(gameObject);
        }

        else if (transform.position.z < _lowerBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > _rightBound)
        {
            Destroy(gameObject);
        }

        else if (transform.position.x < _leftBound)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider enemy)
    {
        // Destroys this game object when it collides with another.
        Destroy(gameObject);
        Destroy(this.gameObject);
    }
}
