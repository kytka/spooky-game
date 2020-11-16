using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DoorsController : NetworkBehaviour
{
    [SyncVar]
    public float yRotation;

    public float openRotation;
    public float closedRotation;
    public bool open;

    private void Start()
    {
        if (isServer)
        {
            yRotation = closedRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((open && transform.rotation.y != openRotation) || !open && transform.rotation.y != closedRotation)
        {
            Quaternion rot = Quaternion.Euler(0, yRotation, 0);
            Quaternion lerpRot = Quaternion.Lerp(transform.rotation, rot, 0.1f);
            transform.rotation = lerpRot;
        }
    }

    [Server]
    public void SetRotation()
    {
        if (isServer)
        {
            if (!open)
                yRotation = openRotation;
            else
                yRotation = closedRotation;

            open = !open;
        }
    }
}
