﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
        public Vector3 moveVector;
        public float bulletMoveSpeed = 40f;
        public float bulletDamage = 1f;
        public float lifeTime = 5f;
        public Material material;

        void Start()
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.GetComponent<Renderer>().material = material;
            Destroy(gameObject, lifeTime);
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += moveVector;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            string thisTag = gameObject.tag;
            string otherTag = other.gameObject.tag;

            if (thisTag.Equals(Tags.FRIENDLY_BULLET) && otherTag.Equals(Tags.ENEMY))
            {
                //TODO deal damage to enemy here
            }
            else if (thisTag.Equals(Tags.ENEMY_BULLET) && otherTag.Equals(Tags.PLAYER))
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();

                if(player)
                {
                    player.TakeDamage(bulletDamage);
                    Destroy(gameObject);
                }
            }
        }
}
