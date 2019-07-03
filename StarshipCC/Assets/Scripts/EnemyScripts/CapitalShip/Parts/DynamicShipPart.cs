using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicShipPart : MonoBehaviour
{
    [SerializeField] List<GameObject> possibleParts = new List<GameObject>();
    List<GameObject> SmallAttachmentPrefabs = new List<GameObject>();

    private Transform enemies;

    void Start()
    {
        GameObject capitalShip = GameObject.Find("CapitalShip");
        enemies = capitalShip.transform.Find("Enemies");
        SmallAttachmentPrefabs = capitalShip.GetComponent<ConstructedCapitalController>().SmallAttachmentPrefabs;

        GameObject newPart = Randomize();

        Transform hardPoint = newPart.transform.Find("SmallHardPoint");
        if(hardPoint != null)
            CreateAttachment(hardPoint);
    }

    public GameObject Randomize()
    {
        int r = Random.Range(0, possibleParts.Count);

        GameObject newPart = Instantiate(possibleParts[r], transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);

        return newPart;
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
