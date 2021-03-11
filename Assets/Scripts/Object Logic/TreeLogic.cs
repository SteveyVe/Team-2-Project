using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeLogic : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float maxHealth = 5.0f;
    [HideInInspector]public float health;

    public Sprite fullHPPhase;
    public Sprite secondPhase;
    public Sprite thirdPhase;
    public SpriteRenderer treeStage;

    public bool isDead = false;

    public Image healthBar;

    public GameObject player;
    public float renderDistance;
    public float hpRenderDistance;
    public float maxSwing;
    public float maxShake;

    private float swingPos;
    private float swingSpeed;
    private float shakeAmp;

    private AudioSource fall;
    private AudioSource chop;

    public AudioClip fallSound;
    public AudioClip chopSound;

    public GameObject stump;

    // Start is called before the first frame update
    void Start()
    {
        treeStage.sprite = fullHPPhase;
        health = maxHealth;

        fall = gameObject.AddComponent<AudioSource>();
        fall.clip = fallSound;
        chop = gameObject.AddComponent<AudioSource>();
        chop.clip = chopSound;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        swingSpeed += maxSwing * damage / maxHealth;
        shakeAmp += maxShake * damage / maxHealth;
        chop.Play();
    }

    private void Update()
    {

        swingSpeed = Mathf.Lerp(swingSpeed, -swingPos * 20.0f, Time.deltaTime * 20);
        swingPos += swingSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, 0, swingPos);

        shakeAmp = Mathf.Lerp(shakeAmp, 0.0f, Time.deltaTime * 30);
        treeStage.transform.localPosition = new Vector3(Random.Range(-1.0f, 1.0f) * shakeAmp, 4.3f, 0);

        healthBar.fillAmount = health / maxHealth;
        if (!isDead)
        {
            if (health <= 0)
            {
                health = 0.0f;
                fall.volume = 0.1f;
                fall.Play();
                isDead = true;
                GameObject go = Instantiate(stump);
                go.transform.localPosition = transform.localPosition;
                go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y - 3.5f, go.transform.position.z);
                Destroy(gameObject, 3);
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
