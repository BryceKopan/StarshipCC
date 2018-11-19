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

    private List<GameObject> turrets;
    private UnityEngine.UI.Text coinCounter;
    private float coins;

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
        var attachmentSizes = GenerateAttachmentSizes();
        var attachmentPositions = GenerateAttachmentPositions(attachmentSizes);
        CreateTurrets(attachmentSizes, attachmentPositions);

        level++;

    }

    List<AttachmentSize> GenerateAttachmentSizes()
    {
        return new List<AttachmentSize>(levelAttachmentSizes[level/2]);
    }

    List<Vector3> GenerateAttachmentPositions(List<AttachmentSize> attachmentSizes)
    {
        float distance = rightBound.x - leftBound.x;
        float stepDistance = distance / (attachmentSizes.Count * 2);
        List<Vector3> attachmentPositions = new List<Vector3>();

        for(int i = 1; i < attachmentSizes.Count * 2; i+=2)
        {
            attachmentPositions.Add(new Vector3(leftBound.x + (i * stepDistance), transform.position.y, transform.position.z));
        }

        return attachmentPositions;
    }

    void CreateTurrets(List<AttachmentSize> attachmentSizes, List<Vector3> attachmentPositions)
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
            InstantiateAttachment(attachmentPrefabs[r], attachmentPositions[i]);
        }
    }

    void InstantiateAttachment(GameObject attachmentPrefab, Vector3 attachmentPosition)
    {
        Instantiate(
                attachmentPrefab,
                attachmentPosition,
                gameObject.transform.rotation);
    }

    public void AddCoins(int coin)
    {
        coins += coin;
        coinCounter.text = "Coin: " + coins;
    }
}