using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructedCapitalController : MonoBehaviour
{
    [SerializeField] public List<GameObject> SmallAttachmentPrefabs;
    [SerializeField] List<GameObject> ItemPrefabs;

    public GameObject SpawnItem(Vector3 postion)
    {
        int r = Random.Range(0, ItemPrefabs.Count);
        
        return Instantiate(
                ItemPrefabs[r],
                postion,
                Quaternion.identity);
    }
}
