using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile {

        public float bulletDamage = 1f;
        public float bulletMoveSpeed = 40f;
        public float bulletLife = 5f;
        public Material material;
        public GameObject ExplosionPrefab;

        public override void Start()
        {
            damage = bulletDamage;
            lifeTime = bulletLife;

            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameObject.GetComponent<Renderer>().material = material;
            Destroy(gameObject, lifeTime);
        }

        // Update is called once per frame
        public override void Update()
        {
            transform.position += moveVector;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
        Debug.Log("Collision");

            string thisTag = gameObject.tag;
            string otherTag = other.gameObject.tag;
            //if(!((thisTag == Tags.ENEMY_BULLET && otherTag == Tags.ENEMY) || (thisTag == Tags.FRIENDLY_BULLET && otherTag == Tags.PLAYER)))
            //{ 
            Hittable hittable = other.gameObject.GetComponent<Hittable>();

                if (hittable != null)
                {
            Debug.Log("Hittable");
                    hittable.OnHit(this);
                }
            //}
        }

        void Explode(Vector3 point)
        {
            Instantiate(
                    ExplosionPrefab,
                    point,
                    gameObject.transform.rotation);
        }
}
