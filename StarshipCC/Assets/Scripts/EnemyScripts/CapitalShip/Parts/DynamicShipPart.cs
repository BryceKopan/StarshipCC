using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicShipPart : MonoBehaviour
{
    [SerializeField] List<GameObject> possibleParts = new List<GameObject>();

    public GameObject Randomize()
    {
        int r = Random.Range(0, possibleParts.Count);

        GameObject newPart = Instantiate(possibleParts[r], transform.position, transform.rotation, transform.parent);
        Destroy(gameObject);

        return newPart;
    }
}
