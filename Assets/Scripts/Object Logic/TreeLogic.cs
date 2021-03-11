using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeLogic : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float maxHealth = 5.0f;
    [SerializeField]private float health;

    
    public bool isChoppedDown = false;
    
    public Sprite fullHPPhase;
    public Sprite secondPhase;
    public Sprite thirdPhase;
    public SpriteRenderer treeStage;

    public Image healthBar;

    public GameObject player;
    public float renderDistance;
    public float hpRenderDistance;


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

        float dist = Vector3.Distance(transform.position, player.transform.position);

        if (dist >= hpRenderDistance)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            if (dist >= renderDistance)
            {
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<CapsuleCollider>().enabled = true;
            }
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }

    }





}
