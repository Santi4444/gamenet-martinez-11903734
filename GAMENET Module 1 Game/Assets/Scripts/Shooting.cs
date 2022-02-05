﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    Camera fpsCamera;

    [SerializeField]
    public float fireRate = 0.1f;
    private float fireTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer < fireRate)
        {
            fireTimer += Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && fireTimer > fireRate)
        {
            fireTimer = 0.0f; 
            Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
                {
                    //To do it in an online setting you need to broadcast to everyone so you need a RPC (Remote Procedure Calls)
                    //TakeDamage is the name of the RPCpublic void in TakingDamage script
                    // RpcTarget.AllBuffered is anyone who is in the server
                    //10 is how much damage they take
                    hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10);
                }
            }
        }
    }
}
