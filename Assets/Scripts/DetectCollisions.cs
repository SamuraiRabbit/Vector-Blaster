using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    // OnTriggerEnter auto fills ther rest. Is called when triggered by another collider.
    private void OnTriggerEnter(Collider projectile)
    {
        //Destroys game object, and other object on trigger. (Make sure triggers are set on object colliders.)
        Destroy(gameObject);
        Destroy(this.gameObject);
    }
}
