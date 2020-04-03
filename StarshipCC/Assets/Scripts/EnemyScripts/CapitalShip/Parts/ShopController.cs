using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {
	public int minCost, maxCost;

    public GameObject shopUIPrefab;

	public Vector3 costDisplayOffset;

	ItemSpawnController itemSpawner;

	void Start () 
	{
        itemSpawner = GameObject.FindObjectOfType<ItemSpawnController>();

		foreach(Transform child in transform)
		{
			if(child.name == "ItemSpawnPoint")
			{
                bool foundUIPosition = false;
                foreach(Transform grandchild in child.transform)
                {
                    if(grandchild.name == "UIPosition")
                    {
                        foundUIPosition = true;

                        GameObject uiPosition = grandchild.gameObject;

                        GameObject item = itemSpawner.SpawnRandomItem(child.transform.position);

                        Cost cost = item.AddComponent<Cost>();
                        cost.minCost = minCost;
                        cost.maxCost = maxCost;

                        GameObject ui = Instantiate(shopUIPrefab);

                        TextMesh costText = ui.GetComponentInChildren<TextMesh>();
                        if (costText)
                        {
                            costText.text = "" + cost.GetCost();
                        }

                        ui.transform.parent = child.transform;
                        ui.transform.position = uiPosition.transform.position;
                        ui.transform.localScale = new Vector3(1, 1, 1);
                    }
                }

                if(!foundUIPosition)
                {
                    Debug.LogWarning("Shop Error: item spawn object does not contain UIPosition child");
                }
			}
		}
	}
}
