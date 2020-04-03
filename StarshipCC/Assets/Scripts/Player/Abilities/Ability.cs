using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected bool isReady;
    protected bool isActive;

    [ReadOnly]
    public PlayerController player;

    abstract public float Duration { get; }
    abstract public float Cooldown { get; }

    // Start is called before the first frame update
    public virtual void Start()
    {
        isReady = true;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip(PlayerController player)
    {
        this.player = player;
        OnEquip(player);
    }

    public void Unequip()
    {
        OnUnequip();
        this.player = null;
    }

    public void Activate()
    {
        if(isReady)
        {
            isReady = false;
            isActive = true;
            Invoke("Deactivate", Duration);
            OnActivate();
        }
    }

    private void Deactivate()
    {
        if(isActive)
        {
            Invoke("MakeReady", Cooldown);
            isActive = false;
        }
        OnDeactivate();
    }

    private void MakeReady()
    {
        isReady = true;
        OnReady();
    }

    protected abstract void OnEquip(PlayerController player);
    protected abstract void OnUnequip();
    protected abstract void OnActivate();
    protected abstract void OnDeactivate();
    protected abstract void OnReady();
}
