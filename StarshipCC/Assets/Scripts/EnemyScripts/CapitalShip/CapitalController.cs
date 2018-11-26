using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttachmentSize{Large, Medium, Small};

public class CapitalController : MonoBehaviour {
    // public float difficultyLevel = .5f;
    // public float smallTurretDifficulty = .25f;
    // public float mediumTurretDifficulty = .5f;
    // public float largeTurretDifficulty = 1f;

    public int level = 0;
    public List<GameObject> SmallAttachmentPrefabs;
    public List<GameObject> MediumAttachmentPrefabs;
    public List<GameObject> LargeAttachmentPrefabs;
    public List<GameObject> ItemPrefabs;
    public Vector3 leftBound, rightBound;
    public GameObject baseEdge, smallEdge, mediumEdge;

    private List<GameObject> turrets;
    private UnityEngine.UI.Text coinCounter;
    private float coins;
    private Transform parentObject;
    private List<GameObject> edgeParts = new List<GameObject>();

    private AttachmentSize[][] levelAttachmentSizes = new AttachmentSize[][] 
    {
        new AttachmentSize[] {AttachmentSize.Small},
        new AttachmentSize[] {AttachmentSize.Small, AttachmentSize.Small},
        new AttachmentSize[] {AttachmentSize.Small, AttachmentSize.Small, AttachmentSize.Small},
        new AttachmentSize[] {AttachmentSize.Medium},
        new AttachmentSize[] {AttachmentSize.Small, AttachmentSize.Medium, AttachmentSize.Small},
        new AttachmentSize[] {AttachmentSize.Medium, AttachmentSize.Medium},
        new AttachmentSize[] {AttachmentSize.Medium , AttachmentSize.Small, AttachmentSize.Medium}
    };

	void Start ()
    {
        parentObject = GameObject.Find("CapitalShip").transform;

        GenerateCapitalShip();

        GameObject[] turretsArray;
        turretsArray = GameObject.FindGameObjectsWithTag("Enemy");
        turrets = new List<GameObject>(turretsArray);

        coinCounter = GameObject.Find("CoinCounter").GetComponent<UnityEngine.UI.Text>();
	}
	
	void Update () 
    {
        if(!AreTurretsAlive())
        {
            Start();
        }
	}

    public void SpawnItem(Vector3 postion)
    {
        int r = Random.Range(0, ItemPrefabs.Count);
        
        Instantiate(
                ItemPrefabs[r],
                postion,
                Quaternion.identity);
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

    void GenerateCapitalShip()
    {
        Vector3 startingPosition = new Vector3(0,0,0);
        for(int i = 0; i < levelAttachmentSizes.GetLength(0) * 2; i++)
        {
            Debug.Log(startingPosition);
            var attachmentSizes = GenerateAttachmentSizes();
            var attachmentPositions = GenerateAttachmentPositions(attachmentSizes, startingPosition);
            CreateTurrets(attachmentSizes, attachmentPositions, startingPosition);
            startingPosition = CreateCapitalEdge(attachmentSizes, attachmentPositions, startingPosition);

            level++;
        }
    }

    List<AttachmentSize> GenerateAttachmentSizes()
    {
        return new List<AttachmentSize>(levelAttachmentSizes[level/2]);
    }

    List<Vector3> GenerateAttachmentPositions(List<AttachmentSize> attachmentSizes, Vector3 startingPosition)
    {
        float distance = rightBound.x - leftBound.x;
        float stepDistance = distance / (attachmentSizes.Count * 2);
        List<Vector3> attachmentPositions = new List<Vector3>();

        for(int i = 1; i < attachmentSizes.Count * 2; i+=2)
        {
            attachmentPositions.Add(new Vector3(leftBound.x + (i * stepDistance), transform.position.y, transform.position.z) + startingPosition);
        }

        return attachmentPositions;
    }

    void CreateTurrets(List<AttachmentSize> attachmentSizes, List<Vector3> attachmentPositions, Vector3 startingPosition)
    {
        for(int i = 0; i < attachmentSizes.Count; i++)
        {
            List<GameObject> attachmentPrefabs = new List<GameObject>();

            if(attachmentSizes[i] == AttachmentSize.Small)
            {
                attachmentPrefabs = SmallAttachmentPrefabs;
            }
            else if(attachmentSizes[i] == AttachmentSize.Medium)
            {
                attachmentPrefabs = MediumAttachmentPrefabs;
            }
            else if(attachmentSizes[i] == AttachmentSize.Large)
            {
                attachmentPrefabs = LargeAttachmentPrefabs;
            }

            int r = Random.Range(0, attachmentPrefabs.Count);
            //int r = 8;
            InstantiateAttachment(attachmentPrefabs[r], attachmentPositions[i] + startingPosition);
        }
    }

    Vector3 CreateCapitalEdge(List<AttachmentSize> attachmentSizes, List<Vector3> attachmentPositions, Vector3 startingPosition)
    {
        // for(int i = 0; i < edgeParts.Count; i++)
        // {
        //     Destroy(edgeParts[i]);
        // }
        edgeParts.Clear();

        for(int i = 0; i < attachmentSizes.Count; i++)
        {
            if(attachmentSizes[i] == AttachmentSize.Small)
            {
                edgeParts.Add(InstantiateEdge(smallEdge, attachmentPositions[i] + startingPosition));
            }
            else if(attachmentSizes[i] == AttachmentSize.Medium)
            {
                edgeParts.Add(InstantiateEdge(mediumEdge, attachmentPositions[i] + startingPosition));
            }
            else if(attachmentSizes[i] == AttachmentSize.Large)
            {
            }
        }

        Vector3 leftEdge, rightEdge = new Vector3(0, 0, 0);
        Debug.Log("Count" + edgeParts.Count);

        for(int i = 0; i < edgeParts.Count; i++)
        {
            Debug.Log("I:" + i);

            if(i == 0)
            {
                leftEdge = leftBound + startingPosition;
                rightEdge = edgeParts[i].GetComponent<SpriteRenderer>().bounds.min;
            }
            else if(i == edgeParts.Count - 1)
            {
                leftEdge = edgeParts[i].GetComponent<SpriteRenderer>().bounds.max;
                rightEdge = rightBound + startingPosition;
            }
            else
            {
                leftEdge = edgeParts[i].GetComponent<SpriteRenderer>().bounds.max;
                rightEdge = edgeParts[i + 1].GetComponent<SpriteRenderer>().bounds.min;
            }

            leftEdge += startingPosition;
            rightEdge += startingPosition;

            Debug.Log(leftEdge + ":" + rightEdge);

            if(leftEdge.x < rightEdge.x)
            {
                Vector3 offSet = baseEdge.GetComponent<SpriteRenderer>().bounds.extents;
                edgeParts.Insert(i + 1, InstantiateEdge(baseEdge, new Vector3(leftEdge.x, transform.position.y, 0) + new Vector3(offSet.x, 0, 0)));
            }
        }

        return startingPosition + (rightBound - leftBound) / 2;
    }

    void InstantiateAttachment(GameObject attachmentPrefab, Vector3 attachmentPosition)
    {
        GameObject attachment = Instantiate(
                attachmentPrefab,
                attachmentPosition,
                gameObject.transform.rotation);

        attachment.transform.SetParent(parentObject); 
    }

    GameObject InstantiateEdge(GameObject edgePrefab, Vector3 edgePosition)
    {
        GameObject edge = Instantiate(
                edgePrefab,
                edgePosition,
                gameObject.transform.rotation);

        edge.transform.SetParent(parentObject); 

        return edge;
    }

    public void AddCoins(int coin)
    {
        coins += coin;
        coinCounter.text = "Coin: " + coins;
    }
}