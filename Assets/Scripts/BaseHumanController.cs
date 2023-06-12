using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHumanController : MonoBehaviour
{
    private ObjectManager objectManager;
    private BaseController baseController;
    private Animator anim;
    [SerializeField]
    private Transform bulletPrefab;

    [SerializeField]
    private List<Transform> bulletList = new List<Transform>();

    private int bulletIndex = 0;
    [SerializeField]
    private Transform handTransform;

    protected Transform firstEnemy;
    protected Transform bullet;
    protected float distance;
     

    protected virtual void Start()
    {
        objectManager = ObjectManager.Instance;
        baseController = transform.parent.GetComponent<BaseController>();
        baseController.HumanAnimRun += HumanAnimRun;
        anim = transform.GetComponent<Animator>();

        for (int i = 0; i < 5; i++)
        {
            Transform newBullet = Instantiate(bulletPrefab, transform).transform;
            //newBullet.DOLocalMoveY(newBullet.localPosition.y + 1, 0.1f);
            newBullet.gameObject.SetActive(false);
            bulletList.Add(newBullet);
        }
    }

    protected virtual void Update()
    {

    }


    public virtual void BulletShoot(Transform enemyCar)
    {
        bullet = bulletList[bulletIndex];
        bullet.gameObject.SetActive(true);
        foreach (var item in enemyCar.transform.GetChild(1).GetComponentsInChildren<SphereCollider>())
        {
            firstEnemy = item.transform;
        }
       
        bullet.parent = firstEnemy;
        bullet.position = handTransform.position;
        bullet.LookAt(firstEnemy);
        bullet.eulerAngles = new Vector3(90, bullet.transform.eulerAngles.y, 0);
        //Vector3 bulletDirection = (firstEnemy.position - handTransform.position).normalized;
        //bulletDirection.z += 0.5f;
        //bullet.GetComponent<Rigidbody>().AddForce(bulletDirection * 1000 + Vector3.forward * 500);
        float distance = Vector3.Distance(bullet.position, enemyCar.transform.GetChild(1).position) * 0.05f;
        bullet.DOLocalMove(Vector3.zero, distance).SetEase(Ease.Linear);

        bulletIndex++;
        if (bulletIndex > 4)
        {
            bulletIndex = 0;
        }

        DOVirtual.DelayedCall(0.2f, () =>
        {
            bullet.GetComponent<CapsuleCollider>().enabled = true;
        });


    }


    private void HumanAnimRun(int animNumber)
    {

        if (transform.localPosition.x < 0 && animNumber == 2)
        {
            anim.SetInteger("HumanAnimStatus", 4);

        }
        else if (transform.localPosition.x < 0 && animNumber == 4)
        {
            anim.SetInteger("HumanAnimStatus", 2);
        }
        //else if (transform.localPosition.x > 0 && animNumber == 2)
        //{
        //    anim.SetInteger("HumanAnimStatus", animNumber - 2);
        //}
        else
        {
            anim.SetInteger("HumanAnimStatus", animNumber);
        }
    }
}
