using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour
{
    Text combo;
    public minigame game;

    // Start is called before the first frame update
    void Start()
    {
        combo = gameObject.GetComponent<Text>();
        combo.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        combo.text = game.currentCombo.ToString();
    }
}
