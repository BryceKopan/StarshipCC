using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaledParticles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParticleSystem[] systems = GetComponents<ParticleSystem>();
        foreach(ParticleSystem s in systems)
        {
            var main = s.main;
            main.scalingMode = ParticleSystemScalingMode.Hierarchy;
        }
    }
}
