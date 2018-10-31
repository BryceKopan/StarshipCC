using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : EnemyController
{
    public float bulletMoveSpeed, pauseAfterShooting;
    public GameObject attackPrefab;

    private GameObject loadedMissile;
    
    protected override void OnStart()
    {
        Load();
    }

    protected override void OnDeath()
    {
        Destroy(loadedMissile);
    }

    protected override SimpleTransform GetTransformToAttack(Vector3 attackPosition)
    {
        SimpleTransform deltaTransform = new SimpleTransform();

        Vector3 rotationVector = transform.position - attackPosition;
        float angle = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        deltaTransform.rotation = q;

        return deltaTransform;
    }

    protected override bool IsInRange(Vector3 attackPosition)
    {
        Vector3 rotationVector = transform.position - attackPosition;
        float angle = Mathf.Atan2(rotationVector.y, rotationVector.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        if(Quaternion.Angle(q, transform.rotation) < 5)
            return true;

        return false;
    }

    protected override void Attack()
    {
        Fire();
        Invoke("Load", pauseAfterShooting/2);
        Invoke("DoneAttacking", pauseAfterShooting);
    }

    void Load()
    {
        var missile = (GameObject) Instantiate(
                attackPrefab,
                transform.position,
                transform.rotation);

        missile.tag = Tags.ENEMY_BULLET;
        missile.transform.parent = transform;

        loadedMissile = missile;
    }

    void Fire()
    {
        loadedMissile.transform.parent = null;

        Bullet missileScript = loadedMissile.GetComponent<Bullet>();
        missileScript.moveVector = -transform.up * bulletMoveSpeed;
    }
}
