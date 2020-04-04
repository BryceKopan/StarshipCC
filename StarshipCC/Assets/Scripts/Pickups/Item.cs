using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    public bool isInteractable = true;
    public bool displayItemText = true;

    public TextMesh itemTextPrefab = null;
    public int itemTextYOffset = -8;
    private float itemTextVisibleDuration = 1.5f;
    private float itemTextFadeDuration = 0.5f;

    abstract public string Description {get;}

    public float soundVolume = 1f;

    public AudioClip pickupSound;

    protected TextMesh textObj;

    [ReadOnly]
    public PlayerController player;

    public bool isTimed = false;
    public float duration = 0;

    // Use this for initialization
    public virtual void Start () 
    {
    }
	
	// Update is called once per frame
	public virtual void Update () {
		
	}

    public void Equip(PlayerController player)
    {
        this.player = player;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        if (renderer)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        Collider2D collider = gameObject.GetComponent<Collider2D>();
        if(collider)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        
        OnEquip(player);

        if (pickupSound)
        {
            AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position, soundVolume);
        }

        if (isTimed)
        {
            Invoke("Unequip", duration);
        }

        if(displayItemText)
        {
            DisplayItemText();
        }
    }

    public void Unequip()
    {
        OnUnequip(player);

        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        if (renderer)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        Collider2D collider = gameObject.GetComponent<Collider2D>();
        if (collider)
        {
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        this.transform.parent = null;
        this.player = null;
    }

    protected abstract void OnEquip(PlayerController player);

    protected abstract void OnUnequip(PlayerController player);

    public void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if(other.tag == Tags.PLAYER)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player && isInteractable)
            {
                this.Equip(player);
            }
        }
    }

    public void DisplayItemText()
    {
        if(itemTextPrefab != null)
        {
            textObj = Instantiate(itemTextPrefab);
            textObj.text = Description;
            textObj.gameObject.transform.position = transform.position + new Vector3(0, itemTextYOffset, 0);

            Invoke("FadeOutText", itemTextVisibleDuration);
        }
    }

    IEnumerator FadeOutText()
    {
        FadeOutDelete fod = textObj.gameObject.AddComponent<FadeOutDelete>();
        fod.fadeDuration = itemTextFadeDuration;
        fod.Delete();
        return null;
    }
}

