using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
        public Vector3 moveVector;
        public float bulletMoveSpeed = 20f;
        public float bulletDamage = 1f;
        public float lifeTime = 5f;

        void Start()
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            Destroy(gameObject, lifeTime);
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += moveVector;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            PlayerController ship = other.gameObject.GetComponent<PlayerController>();
            if (ship)
            {
                ship.TakeDamage(bulletDamage);
                Destroy(gameObject);
            }
        }
}
