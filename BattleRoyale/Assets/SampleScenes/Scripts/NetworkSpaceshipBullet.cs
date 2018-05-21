using UnityEngine;
using System.Collections;

public class NetworkSpaceshipBullet : MonoBehaviour
{
    public Vector3 originalDirection;

    //The spaceship that shoot that bullet, use to attribute point correctly
    public NetworkSpaceship owner;

    void Start()
    {
        Destroy(gameObject, 3.0f);
        GetComponent<Rigidbody>().velocity = originalDirection * 200.0f;
        transform.forward = originalDirection;
    }

    void OnCollisionEnter()
    {
        Destroy(gameObject);
    }

}
