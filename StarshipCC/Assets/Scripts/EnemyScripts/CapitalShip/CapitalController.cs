using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttachmentSize{Large, Medium, Small};

public class CapitalController : MonoBehaviour {
    public List<GameObject> SmallAttachmentPrefabs;
    public List<GameObject> MediumAttachmentPrefabs;
    public List<GameObject> LargeAttachmentPrefabs;
    public List<GameObject> ItemPrefabs;
    public GameObject baseEdge, smallEdge, mediumEdge, capitalPartShop, capitalPartHiddenRoom;
    public int capitalLength;

    private List<GameObject> turrets;
    private List<GameObject> capitalParts = new List<GameObject>();

	void Start ()
    {
        GenerateCapitalShip();

        GameObject[] turretsArray;
        turretsArray = GameObject.FindGameObjectsWithTag("Enemy");
        turrets = new List<GameObject>(turretsArray);
	}
	
	void Update () 
    {
	}

    //--Capital Ship Generation--
    void GenerateCapitalShip()
    {
        CreateCapitalBase();

        for(int i = 0; i < capitalLength; i++)
        {
            float r = Random.Range(0f, 100f);

            if(i < capitalLength / 2)
            {
                if(r < 5)
                    CreateCapitalPart(capitalPartShop);
                else if(r < 10)
                    CreateCapitalPart(capitalPartHiddenRoom);
                else if(r < 55)
                    CreateCapitalPart(smallEdge);
                else
                    CreateCapitalPart(baseEdge);
            }
            else
            {
                if(r < 5)
                    CreateCapitalPart(capitalPartShop);
                else if(r < 10)
                    CreateCapitalPart(capitalPartHiddenRoom);
                else if(r < 25)
                    CreateCapitalPart(mediumEdge);
                else if (r < 65)
                    CreateCapitalPart(smallEdge);
                else
                    CreateCapitalPart(baseEdge);
            }
        }

        CreateAttachments(capitalParts);
    }

    void CreateCapitalBase()
    {
        GameObject capitalPart = Instantiate(
                baseEdge,
                gameObject.transform.position - new Vector3(100, 0, 0),
                gameObject.transform.rotation);

        capitalPart.transform.SetParent(gameObject.transform); 

        capitalParts.Add(capitalPart);

        for(int i = 0; i < 5; i++)
        {
            if(i == 1)
                CreateCapitalPart(capitalPartHiddenRoom);
            else
                CreateCapitalPart(baseEdge);
        }
    }

    void CreateCapitalPart(GameObject capitalPartPrefab)
    {
        GameObject capitalPartChild;

        float rightEdge = capitalParts[capitalParts.Count - 1].GetComponentInChildren<SpriteRenderer>().bounds.max.x;
        float xOffSet = capitalPartPrefab.GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
        float yOffSet = 0f;

        Transform offSetObject = capitalPartPrefab.transform.Find("YOffSet");

        if(offSetObject)
            yOffSet = offSetObject.localPosition.y * offSetObject.lossyScale.y;

        Vector3 capitalPartPosition = new Vector3(rightEdge + xOffSet, gameObject.transform.position.y - yOffSet, gameObject.transform.position.z);

        GameObject capitalPart = Instantiate(
                capitalPartPrefab,
                capitalPartPosition,
                gameObject.transform.rotation);

        capitalPart.transform.SetParent(gameObject.transform); 

        capitalParts.Add(capitalPart);
    }

    void CreateAttachments(List<GameObject> capitalParts)
    {
        foreach(GameObject capitalPart in capitalParts)
        {
            foreach(Transform child in capitalPart.transform)
            {
                if(child.name == "SmallHardPoint")
                {
                    int r = Random.Range(0, SmallAttachmentPrefabs.Count);

                    GameObject attachment = Instantiate(
                        SmallAttachmentPrefabs[r],
                        child.transform.position,
                        child.transform.rotation);

                    attachment.transform.SetParent(gameObject.transform); 
                }
                else if(child.name == "MediumHardPoint")
                {
                    int r = Random.Range(0, MediumAttachmentPrefabs.Count);

                    GameObject attachment = Instantiate(
                        MediumAttachmentPrefabs[r],
                        child.transform.position,
                        child.transform.rotation);

                    attachment.transform.SetParent(gameObject.transform); 
                }
            }
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