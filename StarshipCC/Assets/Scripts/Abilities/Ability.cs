using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected bool isReady;
    protected bool isActive;

    protected float activeLength;
    protected float cooldownLength;

    [HideInInspector]
    public PlayerController player;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isReady = true;
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        if(isReady)
        {
            isReady = false;
            isActive = true;
            Invoke("Deactivate", activeLength);
            OnActivate();
        }
    }

    private void Deactivate()
    {
        if(isActive)
        {
            Invoke("MakeReady", cooldownLength);
            isActive = false;
        }
        OnDeactivate();
    }

    private void MakeReady()
    {
        isReady = true;
        OnReady();
    }

    protected abstract void OnActivate();
    protected abstract void OnDeactivate();
    protected abstract void OnReady();
}
