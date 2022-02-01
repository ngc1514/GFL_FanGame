using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private GameObject projectilDecal;

    private float speed = 50f;
    private float timeToDestroy = 3f;

    public Vector3 Target { get; set; }
    public bool Hit { get; set; }


    void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);

        //
        if(!Hit && Vector3.Distance(transform.position, Target) < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    // fake bullet
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        GameObject.Instantiate(projectilDecal, contact.point, Quaternion.LookRotation(contact.normal)); // quaternion: spawn decal facing out the wall when hit, normal - perpendicular
        Destroy(gameObject);
    }
}
