using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPulse : MonoBehaviour
{
    [SerializeField] private float growthSpeed;
    [SerializeField] private float timeToLive;

    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    void FixedUpdate()
    {
        gameObject.transform.localScale = gameObject.transform.localScale * growthSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;

        if(otherObject.tag == "Player")
        {
            PlayerController pc = otherObject.GetComponent<PlayerController>();
            pc.TakeDamage(1);
        }
    }
}
