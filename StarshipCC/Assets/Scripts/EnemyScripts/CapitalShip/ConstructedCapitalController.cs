using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructedCapitalController : MonoBehaviour
{
    [SerializeField] List<GameObject> SmallAttachmentPrefabs;
    [SerializeField] List<GameObject> ItemPrefabs;

    void Start()
    {
        RandomizeShipParts();
    }

    void RandomizeShipParts()
    {
        List<GameObject> hardPoints = new List<GameObject>();

        DynamicShipPart[] dynamicShipParts = transform.Find("Parts").GetComponentsInChildren<DynamicShipPart>();

        foreach(DynamicShipPart part in dynamicShipParts)
        {
            GameObject newPart = part.Randomize();

            Transform hardPoint = newPart.transform.Find("SmallHardPoint");
            if(hardPoint != null)
                hardPoints.Add(hardPoint.gameObject);
        }

        CreateAttachments(hardPoints);
    }

    void CreateAttachments(List<GameObject> hardPoints)
    {
        foreach(GameObject hardPoint in hardPoints)
        {
            int r = Random.Range(0, SmallAttachmentPrefabs.Count);

            GameObject attachment = Instantiate(
                SmallAttachmentPrefabs[r],
                hardPoint.transform.position,
                hardPoint.transform.rotation);

            attachment.transform.SetParent(gameObject.transform.Find("Enemies")); 
        }
    }

    public GameObject SpawnItem(Vector3 postion)
    {
        int r = Random.Range(0, ItemPrefabs.Count);
        
        return Instantiate(
                ItemPrefabs[r],
                postion,
                Quaternion.identity);
    }
}
