using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ThirdPersonController : NetworkBehaviour
{

    public float speed;
    public Transform myCamera;
    public Transform canon;
    public Rigidbody bulletPrefab;

    Animator anim;
    Rigidbody rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}

    float inputVertical;
	void Update ()
    {
        if (!isLocalPlayer)
            return; 

        if (myCamera == null)
        {
            myCamera = Camera.main.transform;
            GameObject.FindObjectOfType<UnityStandardAssets.Cameras.FreeLookCam>().SetTarget(transform);
        }

        inputVertical = Input.GetAxis("Vertical");

        anim.SetFloat("Speed", inputVertical);
        anim.SetBool("Aiming", Input.GetKey(KeyCode.JoystickButton5));

        if (inputVertical != 0)
        {
            LookToCamera();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            LookToCamera();           
            CmdFire(myCamera.forward.normalized);
        }
    }
    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        var vel = (transform.forward * inputVertical * speed) + (rb.velocity.y * Vector3.up);
        rb.velocity = vel;
    }

    [Command]
    void CmdFire(Vector3 velDir)
    {
        RpcDoTrigger("Attack");
        var b = Instantiate(bulletPrefab, canon.position, canon.rotation);
        b.velocity = velDir * 10;
        NetworkServer.Spawn(b.gameObject);
        Destroy(b.gameObject, 5.0f);
    }

    [ClientRpc]
    public void RpcDoTrigger(string Trigger)
    {
        anim.SetTrigger(Trigger);
    }
    void LookToCamera()
    {
        var ang = transform.eulerAngles;
        ang.y = myCamera.transform.eulerAngles.y;
        rb.rotation = Quaternion.Euler(ang);
    }
}
