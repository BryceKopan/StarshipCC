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

        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.radius = Radius;
        collider.isTrigger = true;

        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();

        Invoke("Death", Duration);

        if (effect)
        {
            effect = Instantiate(effect);
            effect.transform.position = transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        CapitalHull hull = collider.gameObject.GetComponent<CapitalHull>();

        if (hull != null)
        {
            hull.TakeDamage(Damage);
        }
    }

    public IEnumerator Death()
    {
        Destroy(gameObject);
        return null;
    }
}
