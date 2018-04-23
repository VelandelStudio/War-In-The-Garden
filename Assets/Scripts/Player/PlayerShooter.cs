﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VelandelPiracyHill
{
    public class PlayerShooter : Photon.PunBehaviour
    {
        [SerializeField] Bullet bulletPrefab;
        [SerializeField] List<Animator> leftCanons;
        [SerializeField] List<Animator> rightCanons;

        int squadPos = 0;

        void Awake()
        {
            enabled = photonView.isMine;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                photonView.RPC("RPC_MoveCrewLeft", PhotonTargets.All);
                squadPos = 1;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                photonView.RPC("RPC_MoveCrewRight", PhotonTargets.All);
                squadPos = 2;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                photonView.RPC("RPC_FireBullet", PhotonTargets.All);
            }
        }

        [PunRPC]
        void RPC_FireBullet(PhotonMessageInfo info)
        {
            if (squadPos == 0)
            {
                Debug.Log("Squad Not Set to Canon side");

                return;
            }

            if (squadPos == 1)
            {
                Debug.Log(squadPos);

                foreach (Animator anim in leftCanons)
                {
                    if (anim.GetBool("CannonLoaded"))
                    {
                        Debug.Log("Left Fire");

                        Transform canon = anim.GetComponent<Transform>();
                        Transform bulletSpawn = TransformExtensions.FindAnyChild<Transform>(canon, "Bullet Spawn");

                        anim.SetTrigger("StartShooting");

                        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                        bullet.SetOwner(info.photonView);
                        bullet.gameObject.SetActive(true);
                    }
                }
            }

            if (squadPos == 2)
            {
                Debug.Log(squadPos);

                foreach (Animator anim in rightCanons)
                {
                    if (anim.GetBool("CannonLoaded"))
                    {
                        Debug.Log("Left Fire");

                        Transform canon = anim.GetComponent<Transform>();
                        Transform bulletSpawn = TransformExtensions.FindAnyChild<Transform>(canon, "Bullet Spawn");

                        anim.SetTrigger("StartShooting");

                        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
                        bullet.SetOwner(info.photonView);
                        bullet.gameObject.SetActive(true);
                    }
                }
            }
        }

        [PunRPC]
        void RPC_MoveCrewLeft(PhotonMessageInfo info)
        {
            // Do something
        }

        [PunRPC]
        void RPC_MoveCrewRight(PhotonMessageInfo info)
        {
            // Do something
        }
    }
}

