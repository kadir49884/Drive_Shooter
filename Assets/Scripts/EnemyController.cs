using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{

    private Transform playerTransform;
    private bool waitForShoot = true;

   

    protected override void Update()
    {
        if (waitForShoot && playerTransform != null)
        {
            waitForShoot = false;
            DOVirtual.DelayedCall(0.2f, () =>
            {
                waitForShoot = true;
            });
            foreach (var item in transform.GetComponentsInChildren<HumanEnemyController>())
            {
                item.BulletShoot(playerTransform);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.TryGetComponent(out PlayerController playerController))
        {
            playerTransform = other.transform.parent;
        }

        if (transform.position.x < 0)
        {
            HumanAnimRun((int)HumanPosStatus.TurnRight);
        }
        else if (transform.position.x > 0)
        {
            HumanAnimRun((int)HumanPosStatus.TurnLeft);
        }

        DOVirtual.DelayedCall(0.5f, () =>
        {
            if (other.transform.parent.TryGetComponent(out PlayerController playerController))
            {
                playerController.GetComponent<PlayerController>().IsInRadar = true;
                playerController.GetComponent<PlayerController>().EnemyTransform = transform;
            }
        });
    }
}
