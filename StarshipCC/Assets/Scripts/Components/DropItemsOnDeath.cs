using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemsOnDeath : MonoBehaviour
{
    private bool isQuitting;

    private ItemSpawnController spawner;

    public float spawnChance = 1f;

    public bool guaranteeEpicItem = false;
    public bool guaranteeRareItem = false;
    public bool guaranteeCommonItem = false;

    public int numItems = 1;

    void OnDestroy()
    {
        if (!isQuitting)
        {
            float rItem = Random.value;
            if (rItem < spawnChance)
            {
                for(int i = 0; i < numItems; i++)
                {
                    SpawnItem();
                } 
            }
        }
    }

    void SpawnItem()
    {
        if(!spawner)
        {
            spawner = GameObject.FindObjectOfType<ItemSpawnController>();
        }

        if(spawner)
        {
            if(guaranteeEpicItem)
            {
                spawner.SpawnEpicItem(transform.position);
            }
            else if(guaranteeRareItem)
            {
                spawner.SpawnRareItem(transform.position);
            }
            else if(guaranteeCommonItem)
            {
                spawner.SpawnCommonItem(transform.position);
            }
            else
            {
                spawner.SpawnRandomItem(transform.position);
            }
        }
        else
        {
            Debug.LogWarning("No ItemSpawnController found in scene");
        }
    }

    void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
