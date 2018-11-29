using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegradingSprite : MonoBehaviour {
    public List<Sprite> healthSprites;

    private float maxHealth;
    private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSprite(float health)
    {
        float healthPercentage = health / maxHealth;
        int numberOfHealthSprites = healthSprites.Count;
        float stepSize = 1f / numberOfHealthSprites;

        for(int i = 0; i < numberOfHealthSprites; i++)
        {
            if(healthPercentage < 1f - (i * stepSize) &&
                    healthPercentage > 1f - ((i + 1) * stepSize))
            {
                sr.sprite = healthSprites[i];
            }
        }
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }
}
