using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSprite : MonoBehaviour {

	public List<Sprite> sprites;

	void Start () 
	{
		int r = Random.Range(0, sprites.Count);
		gameObject.GetComponent<SpriteRenderer>().sprite = sprites[r];
	}
}