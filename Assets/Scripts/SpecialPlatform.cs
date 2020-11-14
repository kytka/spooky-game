using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpecialPlatform : NetworkBehaviour
{
    [SyncVar]
    public int players;
    public int playersRequired;

    public bool Active { get { return players == playersRequired; } }

    private void OnCollisionEnter(Collision collision)
    {
        if (isServer)
        {
            if(collision.gameObject.tag == "Player")
                players++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (isServer)
        {
            if (collision.gameObject.tag == "Player")
                players--;
        }
    }
}
