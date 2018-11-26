using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttachmentSize{Large, Medium, Small};

public class CapitalController : MonoBehaviour {
    public List<GameObject> SmallAttachmentPrefabs;
    public List<GameObject> MediumAttachmentPrefabs;
    public List<GameObject> LargeAttachmentPrefabs;
    public List<GameObject> ItemPrefabs;
    public GameObject baseEdge, smallEdge, mediumEdge;

    private List<GameObject> turrets;
    private UnityEngine.UI.Text coinCounter;
    private float coins;
    private List<GameObject> capitalParts = new List<GameObject>();

	void Start ()
    {
        GenerateCapitalShip();

        GameObject[] turretsArray;
        turretsArray = GameObject.FindGameObjectsWithTag("Enemy");
        turrets = new List<GameObject>(turretsArray);

        coinCounter = GameObject.Find("CoinCounter").GetComponent<UnityEngine.UI.Text>();
	}
	
	void Update () 
    {
	}

    //--Capital Ship Generation--
    void GenerateCapitalShip()
    {
        CreateCapitalBase();

        for(int i = 0; i < 50; i++)
        {
            float r = Random.Range(0f, 100f);
            if(r < 25)
                CreateCapitalPart(smallEdge);
            else
                CreateCapitalPart(baseEdge);
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
            CreateCapitalPart(baseEdge);
        }
    }

    void CreateCapitalPart(GameObject capitalPartPrefab)
    {
        Vector3 rightEdge = capitalParts[capitalParts.Count - 1].GetComponent<SpriteRenderer>().bounds.max;
        Vector3 offSet = capitalPartPrefab.GetComponent<SpriteRenderer>().bounds.extents;

        Vector3 capitalPartPosition = rightEdge + offSet;
        capitalPartPosition.y = gameObject.transform.position.y;
        capitalPartPosition.z = gameObject.transform.position.z;

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
            }
        }
    }

    //--Game General Use--
    public void AddCoins(int coin)
    {
        coins += coin;
        coinCounter.text = "Coin: " + coins;
    }

    public void SpawnItem(Vector3 postion)
    {
        int r = Random.Range(0, ItemPrefabs.Count);
        
        Instantiate(
                ItemPrefabs[r],
                postion,
                Quaternion.identity);
    }
}