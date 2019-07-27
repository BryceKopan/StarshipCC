using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullExplosion : MonoBehaviour
{
    public bool isExploding = false;

    public float Damage = 1;
    public float Radius = 3;
    public float Duration = 1f;
    public GameObject effect;

    private Projectile projectile;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = Layers.HULL_DAMAGE;
    }

    private void Update()
    {
        if(!isExploding)
        {
            Explode();
        }
    }

    // Update is called once per frame
    void Explode()
    {
        isExploding = true;

        projectile = gameObject.AddComponent<Projectile>();
        projectile.damage = Damage;
        projectile.moveVector = Vector2.zero;
        projectile.speed = 0;
        projectile.range = float.PositiveInfinity; // Doesn't matter because it's not moving

        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.radius = Radius;
        collider.isTrigger = true;
        Invoke("Death", Duration);

        if (effect)
        {
            effect = Instantiate(effect);
            effect.transform.position = transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Hittable hittable = other.gameObject.GetComponent<Hittable>();

        if(hittable != null)
        {
            hittable.OnHit(projectile);
        }
    }

    public IEnumerator Death()
    {
        Destroy(gameObject);
        return null;
    }
}
