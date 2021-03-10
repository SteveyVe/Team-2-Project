using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTreeScript : MonoBehaviour
{

    public minigame mg;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TreeHitbox")
        {
            other.gameObject.GetComponent<TreeLogic>().TakeDamage(1 + (mg.currentCombo / 5));
            transform.parent.gameObject.SetActive(false);
        }
    }
}
