// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject _poofParticlePrefab;

    // handle collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {

        // check if bird hit monster
        Bird bird = collision.collider.GetComponent<Bird>();

        if (bird != null)
        {   
            Instantiate(_poofParticlePrefab,transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
        
        // check if hit other monster
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            return;
        }

        // check if got crushed on top by box (check normal vector)
        if (collision.contacts[0].normal.y < -0.5)
        {
            Instantiate(_poofParticlePrefab,transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
    }
}
