using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardPoint : MonoBehaviour
{
    public float chanceForTurret = .5f;
    public List<GameObject> SmallAttachmentPrefabs = new List<GameObject>();

    private Transform enemies;
    
    void Start()
    {
        GameObject capitalShip = GameObject.Find("CapitalShip");
        enemies = capitalShip.transform.Find("Enemies");

        float r = Random.Range(0f, 1f);

        if(r < chanceForTurret)
            CreateAttachment(transform);
        else
            Destroy(gameObject.transform.parent.gameObject);
    }

    void CreateAttachment(Transform hardPoint)
    {
        int r = Random.Range(0, SmallAttachmentPrefabs.Count);

        GameObject attachment = Instantiate(
            SmallAttachmentPrefabs[r],
            hardPoint.transform.position,
            hardPoint.transform.rotation);

        attachment.transform.SetParent(enemies); 
    }
}
