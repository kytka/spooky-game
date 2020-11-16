using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInteractor : NetworkBehaviour
{
    public Transform camHolder;

    void Update()
    {
        if (hasAuthority)
        {
            Debug.DrawRay(camHolder.position, camHolder.transform.forward, Color.blue);
            RaycastHit hit;
            if(Physics.Raycast(camHolder.position, camHolder.transform.forward, out hit, 3))
            {
                Debug.Log(hit.transform.name);
            }
        }   
    }
}
