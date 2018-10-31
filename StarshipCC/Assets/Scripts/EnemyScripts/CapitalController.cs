using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalController : MonoBehaviour {
    public List<Vector3> HardPoints;
    public List<GameObject> HardPointPrefabs;
    public List<GameObject> ItemPrefabs;

    private List<GameObject> turrets;

	void Start ()
    {
        int r;
        for(int i = 0; i < HardPoints.Count; i++)
        {
            r = Random.Range(0, HardPointPrefabs.Count);
            InstantiateHardPoint(HardPoints[i], HardPointPrefabs[r]);
        }

        GameObject[] turretsArray;
        turretsArray = GameObject.FindGameObjectsWithTag("Enemy");
        turrets = new List<GameObject>(turretsArray);
	}
	
	void Update () 
    {
        if(!AreTurretsAlive())
        {
            Start();
            SpawnItem();
        }
	}

    void SpawnItem()
    {
        int r = Random.Range(0, ItemPrefabs.Count);
        
        Instantiate(
                ItemPrefabs[r],
                new Vector3(),
                Quaternion.identity);
    }

    void InstantiateHardPoint(Vector3 hardPoint, GameObject hardPointPrefab)
    {
        Instantiate(
                hardPointPrefab,
                hardPoint,
                gameObject.transform.rotation);
    }

    bool AreTurretsAlive()
    {
        foreach(GameObject turret in turrets)
        {
            if(turret != null)
                return true;
        }
        return false;
    }
}
