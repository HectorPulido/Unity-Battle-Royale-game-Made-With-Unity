using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        CmdDestroy();
    }

    [Command]
    void CmdDestroy()
    {
        Destroy(gameObject);
    }
}
