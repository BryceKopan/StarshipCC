using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {

        public float bulletDamage = 1f;
        public float bulletLife = 5f;
        public GameObject ExplosionPrefab;

        public override void Start()
        {
            damage = bulletDamage;
            lifeTime = bulletLife;

            Destroy(gameObject, lifeTime);
        }

        public override void FixedUpdate()
        {
            transform.position += moveVector * Time.fixedDeltaTime;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            string thisTag = gameObject.tag;
            string otherTag = other.gameObject.tag;
            if(!((thisTag == Tags.ENEMY_BULLET && otherTag == Tags.ENEMY) || (thisTag == Tags.FRIENDLY_BULLET && otherTag == Tags.PLAYER)))
            { 
                Hittable hittable = other.gameObject.GetComponent<Hittable>();

                if (hittable != null)
                {
                    hittable.OnHit(this);
                }
            }
        }

        public override void Death()
        {
            Explode(gameObject.transform.position);
            Destroy(gameObject);
        }

        void Explode(Vector3 point)
        {
            Instantiate(
                    ExplosionPrefab,
                    point,
                    gameObject.transform.rotation);
        }
}
