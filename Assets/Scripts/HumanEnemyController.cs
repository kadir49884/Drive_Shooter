using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HumanEnemyController : BaseHumanController
{

    private float health = 15;


    protected override void Start()
    {
        base.Start();
    }

    public override void BulletShoot(Transform playerCar)
    {
        base.BulletShoot(playerCar);
    }

    public void Damage()
    {
        health -= 1;
        if(health < 1)
        {
            transform.SetAsLastSibling();
            DOVirtual.DelayedCall(0.3f, () =>
            {
                gameObject.SetActive(false);
            });
        }
    }

    
    
}
