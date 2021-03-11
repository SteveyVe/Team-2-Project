using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeLogic : MonoBehaviour
{
    public float maxHealth = 5.0f;
    [Range(0.0f, 10.0f)]
    [SerializeField]private float health;

    
    public bool isChoppedDown = false;
    
    public Sprite fullHPPhase;
    public Sprite secondPhase;
    public Sprite thirdPhase;
    public SpriteRenderer treeStage;

    public Image healthBar;


    



    // Start is called before the first frame update
    void Start()
    {
        treeStage.sprite = fullHPPhase;
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        
    }

    private void Update()
    {
        healthBar.fillAmount = health / maxHealth;
        if (health <= 0)
        {
            health = 0.0f;
            isChoppedDown = true;
            Destroy(transform.gameObject);
        }
        else if (health <= maxHealth * 0.33f)
        {
            treeStage.sprite = thirdPhase;
        }
        else if (health <= maxHealth * 0.66f)
        {
            treeStage.sprite = secondPhase;
        }
        else
        {
            treeStage.sprite = fullHPPhase;
        }
    }





}
