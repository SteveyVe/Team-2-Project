using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTreeScript : MonoBehaviour
{
    public minigame mg;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Tree"))
        {
            other.gameObject.GetComponent<TreeLogic>().TakeDamage(1 + ((mg.currentCombo - 1.0f) / 3.0f));
        }
    }
}
