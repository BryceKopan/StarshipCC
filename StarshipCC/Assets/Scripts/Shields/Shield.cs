using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shield : MonoBehaviour
{
    public SpriteRenderer shieldRenderer;

    public float currentCharge = 0;

    [ReadOnly]
    public PlayerController player = null;

    // Start is called before the first frame update
    public void Awake()
    {
        currentCharge = MaxCharge();

        GameObject spriteObj = new GameObject("Shield Sprite");
        spriteObj.transform.localPosition = Vector3.zero;
        spriteObj.transform.localScale = new Vector3(1, 1, 1);
        spriteObj.transform.localRotation = transform.localRotation;
        spriteObj.transform.parent = this.transform;
        shieldRenderer = spriteObj.AddComponent<SpriteRenderer>();
        shieldRenderer.enabled = false;
    }

    public void FixedUpdate()
    {
    }

    public void Equip(PlayerController player)
    {
        this.player = player;
        OnEquip(player);
    }

    public void Unequip()
    {
        OnUnequip(player);
        this.player = null;
    }

    public IEnumerable Recharge()
    {
        shieldRenderer.enabled = true;
        currentCharge = Mathf.Min(MaxCharge(), currentCharge + RechargeAmount());
        if(currentCharge >= MaxCharge())
        {
            Invoke("Recharge", RechargeDelay());
        }

        OnRecharge();
        return null;
    }

    public bool HasCharge()
    {
        return currentCharge > 0;
    }

    public void SetSprite(Sprite sprite)
    {
        if (shieldRenderer)
        {
            shieldRenderer.transform.localScale = new Vector3(1, 1, 1);
            shieldRenderer.sprite = sprite;
            shieldRenderer.enabled = true;
        }
    }

    public void Hit(Projectile p)
    {
        if(ShouldHit(p) && HasCharge())
        {
            TakeDamageFrom(p);

            CancelInvoke("Recharge");
            Invoke("Recharge", RechargeDelay());

            if (!HasCharge())
            {
                Disable();
            }
        }
    }

    public void Disable()
    {
        shieldRenderer.enabled = false;
        OnDisabled();
    }

    public abstract float MaxCharge();
    public abstract float RechargeDelay();
    public abstract float RechargeAmount();
    protected abstract void TakeDamageFrom(Projectile p);
    protected abstract void OnEquip(PlayerController p);
    protected abstract void OnUnequip(PlayerController p);
    protected abstract void OnRecharge();
    protected abstract void OnDisabled();
    protected abstract bool ShouldHit(Projectile p);
}
