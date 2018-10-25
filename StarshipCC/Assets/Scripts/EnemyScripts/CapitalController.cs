using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalController : MonoBehaviour {
    public List<Vector3> HardPoints;
    public List<GameObject> HardPointPrefabs;

	void Start ()
    {
        int r;
        for(int i = 0; i < HardPoints.Count; i++)
        {
            r = Random.Range(0, HardPointPrefabs.Count);
            InstantiateHardPoint(HardPoints[i], HardPointPrefabs[r]);
        }
	}
	
	void Update () 
    {
	}

    void InstantiateHardPoint(Vector3 hardPoint, GameObject hardPointPrefab)
    {
        Debug.Log("test");
        Instantiate(
                hardPointPrefab,
                hardPoint,
                gameObject.transform.rotation);
    }
}
