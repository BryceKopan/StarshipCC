using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnController : MonoBehaviour
{
    public List<GameObject> commonItems;
    public List<GameObject> rareItems;
    public List<GameObject> epicItems;

    public float epicSpawnChance = 0.1f;
    public float rareSpawnChance = 0.3f;

    public GameObject SpawnCommonItem(Vector3 position)
    {
        int r = Random.Range(0, commonItems.Count);

        return Instantiate(
                commonItems[r],
                position,
                Quaternion.identity);
    }

    public GameObject SpawnRareItem(Vector3 position)
    {
        int r = Random.Range(0, rareItems.Count);

        return Instantiate(
                rareItems[r],
                position,
                Quaternion.identity);
    }

    public GameObject SpawnEpicItem(Vector3 position)
    {
        int r = Random.Range(0, epicItems.Count);

        return Instantiate(
                epicItems[r],
                position,
                Quaternion.identity);
    }

    public GameObject SpawnRandomItem(Vector3 position)
    {
        float r = Random.value;

        if(epicItems.Count > 0 && r < epicSpawnChance)
        {
            return SpawnEpicItem(position);
        }

        if (rareItems.Count > 0 && r < rareSpawnChance)
        {
            return SpawnRareItem(position);
        }

        if(commonItems.Count > 0)
        {
            return SpawnCommonItem(position);
        }
        else
        {
            Debug.LogWarning("Warning: No common items assigned to ItemSpawnController");
            return null;
        }
    }
}
