using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{

    private GameManager gameManager;
    private float timer;
    private Camera ortho;
    private Vector3 mousePos;

    private Action<int> humanAnimRun;

    private List<BaseController> baseControllers = new List<BaseController>();
    public bool IsInRadar { get => isInRadar; set => isInRadar = value; }


    [SerializeField, ReadOnly]
    private bool humanInCar = true;
    [SerializeField, ReadOnly]
    private bool isInRadar;
    [SerializeField, ReadOnly]
    private bool shootActive = true;

    private Transform enemyTransform;

    public Transform EnemyTransform { get => enemyTransform; set => enemyTransform = value; }
    public Action<int> HumanAnimRun { get => humanAnimRun; set => humanAnimRun = value; }


    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        ortho = ObjectManager.Instance.OrthoCamera;
        shootActive = true;
    }

    protected virtual void Update()
    {

        if (!gameManager.RunGame)
            return;


        if (Input.GetMouseButtonDown(0))
        {

        }

        else if (Input.GetMouseButtonUp(0))
        {
            timer = 0;
            humanInCar = true;
            HumanAnimRun((int)HumanPosStatus.InCar);
        }

        else if (Input.GetMouseButton(0))
        {
            GetMousePos();

            timer += Time.deltaTime;

            if (mousePos.x < 0 && timer > 1 && humanInCar)
            {
                humanInCar = false;
                HumanAnimRun((int)HumanPosStatus.TurnLeft);
            }
            if (mousePos.x > 0 && timer > 1 && humanInCar)
            {
                humanInCar = false;
                HumanAnimRun((int)HumanPosStatus.TurnRight);
            }
        }

        if(!humanInCar && isInRadar && shootActive)
        {
            shootActive = false;
            foreach (var item in transform.GetComponentsInChildren<HumanPlayerController>())
            {
                item.BulletShoot(enemyTransform);
            }
            DOVirtual.DelayedCall(0.2f, () =>
             {
                 shootActive = true;
             });
        }
    }

    private void GetMousePos()
    {
        mousePos = ortho.ScreenToWorldPoint(Input.mousePosition);
    }
}
