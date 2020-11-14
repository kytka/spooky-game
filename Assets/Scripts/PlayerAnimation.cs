using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerAnimation : NetworkBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (hasAuthority)
        {
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
