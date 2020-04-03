using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerItem : Item
{
    LineRenderer lineRenderer = null;
    Color lineColor = Color.green;
    int length = 200;

    public override string Description
    {
        get
        {
            return "Laser Pointer";
        }
    }

    public override void Update()
    {
        base.Update();

        if(lineRenderer)
        {
            Vector3 startPos = new Vector3(0, 0, 6);

            // Cast a ray out from the player
            RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.up, length, LayerMask.NameToLayer("LaserPointer"));

            // If it hits something...
            if (hit.collider != null)
            {
                //Draw the line accordingly
                lineRenderer.SetPosition(0, startPos);
                lineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, 6));
            }
            else
            {
                Vector2 target = Vector2.up * length;
                lineRenderer.SetPosition(0, startPos);
                lineRenderer.SetPosition(1, new Vector3(target.x, target.y, 6));
            }
        }
    }

    protected override void OnEquip(PlayerController player)
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.5f;
        lineRenderer.endWidth = 0.5f;
        lineRenderer.useWorldSpace = false;

        Material lineMaterial = new Material(Shader.Find("Standard"));
        lineMaterial.color = lineColor;
        lineRenderer.material = lineMaterial;
    }

    protected override void OnUnequip(PlayerController player)
    {
        Destroy(lineRenderer);
        lineRenderer = null;
    }
}
