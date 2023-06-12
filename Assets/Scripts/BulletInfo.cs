using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        foreach (var item in other.transform.GetComponentsInParent<HumanEnemyController>())
        {
            gameObject.SetActive(false);
            item.Damage();
        }
       
    }
}
