using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerStats : NetworkBehaviour {

    [SyncVar(hook = "SetHealth")]
    public int health = 100;

    public Image image;

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (image == null)
            {
                image = GameObject.Find("Health").GetComponent<Image>();
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!isLocalPlayer)
            return;

        if (collision.CompareTag("Bullet"))
        {
            CmdGetDamage();
        }        
    }

    void SetHealth(int health)
    {
        this.health = health;

        if (isLocalPlayer)
        {
            image.fillAmount = (float)health / 100.0f;
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
            if (isLocalPlayer)
            {
                GameObject.FindObjectOfType<UnityStandardAssets.Cameras.FreeLookCam>()
                    .SetTarget(GameObject.FindObjectOfType<PlayerStats>().transform);
            }
        }        
    }

    [Command]
    void CmdGetDamage()
    {
        health -= 10;
    }

}
