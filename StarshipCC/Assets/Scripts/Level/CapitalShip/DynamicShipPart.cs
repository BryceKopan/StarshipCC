using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicShipPart : MonoBehaviour
{
    [SerializeField] List<GameObject> possibleParts = new List<GameObject>();

    private Transform enemies;

    CapitalShipController capitalShip;

    void Start()
    {
        CapitalShipController capitalShip = GameObject.FindObjectOfType<CapitalShipController>();

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
        int r = Random.Range(0, capitalShip.SmallAttachmentPrefabs.Count);

        GameObject attachment = Instantiate(
            capitalShip.SmallAttachmentPrefabs[r],
            hardPoint.transform.position,
            hardPoint.transform.rotation);

        attachment.transform.SetParent(capitalShip.transform); 
    }
}
