using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenNoParticles : MonoBehaviour
{
    private List<ParticleSystem> psList = new List<ParticleSystem>();
    
    void Start()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        if(ps != null)
            psList.Add(ps);

        psList.AddRange(GetComponentsInChildren<ParticleSystem>());
    }

    void Update()
    {
        bool particleSystemAlive = false;

        foreach(ParticleSystem ps in psList)
        {         
            if(ps.IsAlive())
                particleSystemAlive = true;
        }

        if(!particleSystemAlive)
            Destroy(gameObject);
    }
}
