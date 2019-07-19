using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveConstantly : MonoBehaviour
{
    public float xVelocity;
    public float yVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(xVelocity, yVelocity, 0);
    }
}
