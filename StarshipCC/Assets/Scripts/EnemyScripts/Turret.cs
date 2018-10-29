using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : EnemyController 
{
    public float numberOfShots, timeBetweenShots, pauseAfterShooting, 
           shotScale, bulletMoveSpeed;
    public GameObject attackPrefab;

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
        for(int i = 0; i < numberOfShots; i++)
        {
            Invoke("Fire", i * timeBetweenShots);
        }
        Invoke("DoneAttacking", (numberOfShots * timeBetweenShots) + pauseAfterShooting);
    }

    void Fire()
    {
        var bullet = (GameObject) Instantiate(
                attackPrefab,
                transform.position,
                transform.rotation);

        bullet.transform.localScale = new Vector3(1 * shotScale, 1 * shotScale, 1);

        bullet.tag = Tags.ENEMY_BULLET;

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        //Add velocity to the bullet
        bulletScript.moveVector = -transform.up * bulletMoveSpeed * Time.deltaTime;
    }
}
