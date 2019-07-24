using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpreadItem : Item
{
    public float spreadAngle = 5f;

    List<Weapon> affectedWeapons;

    public override string Description
    {
        get
        {
            return "Extra Attacks";
        }
    }

    protected override void OnEquip(PlayerController player)
    {
        affectedWeapons = new List<Weapon>();

        foreach (Weapon w in player.weapons)
        {
            if(w.attackSpawns.Count > 0)
            {
                affectedWeapons.Add(w);

                // Find the left-most and right-most attack spawns
                Transform leftMost = w.attackSpawns[0];
                Transform rightMost = w.attackSpawns[0];

                for (int i = 1; i < w.attackSpawns.Count; i++)
                {
                    Transform t = w.attackSpawns[i];

                    if(t.localPosition.x > rightMost.localPosition.x)
                    {
                        rightMost = t;
                    }
                    else if(t.localPosition.x == rightMost.localPosition.x)
                    {
                        if(t.localRotation.z < rightMost.localRotation.z)
                        {
                            rightMost = t;
                        }
                    }

                    if (t.localPosition.x < leftMost.localPosition.x)
                    {
                        leftMost = t;
                    }
                    else if (t.localPosition.x == leftMost.localPosition.x)
                    {
                        if (t.localRotation.z > leftMost.localRotation.z)
                        {
                            leftMost = t;
                        }
                    }
                }

                // Duplicate the attack spawns found above, then rotate them outward
                Transform newLeft = Instantiate<Transform>(leftMost);
                newLeft.parent = leftMost.parent;
                newLeft.localPosition = leftMost.localPosition;
                newLeft.localScale = leftMost.localScale;

                newLeft.localRotation = Quaternion.Euler(new Vector3(leftMost.localRotation.x, leftMost.localRotation.y, leftMost.localEulerAngles.z + spreadAngle));

                w.AddAttackSpawn(newLeft);

                Transform newRight = Instantiate<Transform>(rightMost);
                newRight.parent = rightMost.parent;
                newRight.localPosition = rightMost.localPosition;
                newRight.localScale = rightMost.localScale;
                newRight.localRotation = Quaternion.Euler(new Vector3(rightMost.localRotation.x, rightMost.localRotation.y, rightMost.localEulerAngles.z - spreadAngle));
                w.AddAttackSpawn(newRight);
            }
        }
    }

    protected override void OnUnequip(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}
