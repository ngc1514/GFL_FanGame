using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class should be attached to each of the bullet prefab
 * Control speed and destroy time. 
 * Spawn a bullet hole decal after hitting object
 */
public class ProjectileController : MonoBehaviour
{
    [SerializeField] private GameObject projectilDecal;

    private float speed = 50f;
    private float timeToDestroy = 1f; // TODO: make an equation for speed and bullet-destroy-midair time

    public Vector3 Target { get; set; }
    public bool Hit { get; set; }


    void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime); 

        // when not hit and distance is greater, destroy bullet in air
        if(!Hit && Vector3.Distance(transform.position, Target) < 0.1f)
        {
            Destroy(gameObject);
        }
    }

    // fake bullet
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collision enter");
        ContactPoint contact = collision.GetContact(0);
        // + contact.normal * 0.0001 so that we spawn the bulletHole decal a bit forward
        GameObject.Instantiate(projectilDecal, contact.point + contact.normal * 0.1f, Quaternion.LookRotation(contact.normal)); // quaternion: spawn decal facing out the wall when hit, normal - perpendicular
        Destroy(gameObject);
    }
}
