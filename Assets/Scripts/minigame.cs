using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class minigame : MonoBehaviour
{
    public GameObject MovingBit;
    public GameObject Background;
    public GameObject HitZone;
    public float width;
    public float height;
    public float start_speed;
    public float start_width;
    public float mult_speed;
    public float mult_width;
    public float hit_jerk;
    public float hit_detection_leniency;
    public float hit_zone_bop;
    public float miss_zone_bop;
    private float current_hitzone_min;
    private float current_hitzone_max;
    private float current_hitzone_width;
    private float mover_pos;
    private float mover_speed;
    private float shake_offset;
    private Vector3 origin;
    private float hitzone_v_scale = 1.0f;
    [HideInInspector]public float currentCombo = 0;
    public GameObject axe;

    public GameObject axeObject;

    public int count;


    // Start is called before the first frame update
    void Start()
    {
        origin = gameObject.transform.localPosition;
        Background.transform.localScale = new Vector3(width, height, 1);
        current_hitzone_width = start_width;
        current_hitzone_min = Random.Range(0.0f, 1.0f-current_hitzone_width);
        current_hitzone_max = current_hitzone_min + current_hitzone_width;
        update_hitzone();
        mover_speed = start_speed;
        currentCombo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (count > 2)
        {
            axeObject.SetActive(false);
        }
        else
            count++;

        move_mover();
        shake_minigame();
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            axe.GetComponent<Animator>().SetTrigger("Chop");
            hit_axe();
        }
        hitzone_v_scale = Mathf.Lerp(hitzone_v_scale, 1.0f, Time.deltaTime * 20.0f);
        HitZone.transform.localScale = new Vector3(2 *width * (current_hitzone_width) * hitzone_v_scale, hitzone_v_scale * height, HitZone.transform.localScale.z);
    }

    void move_mover()
    {
        mover_pos += mover_speed * Time.deltaTime;
        if (mover_pos > 1.5f)
        {
            mover_pos = 1.5f;
            mover_speed *= -1;
        }
        if (mover_pos < -0.5f)
        {
            mover_pos = -0.5f;
            mover_speed *= -1;
        }
        MovingBit.transform.localPosition = Vector3.right * (mover_pos - 0.5f) * width;
    }

    void update_hitzone()
    {
        HitZone.transform.localScale = new Vector3(width * (current_hitzone_width), height, 1);
        HitZone.transform.localPosition = Vector3.right * ((current_hitzone_min + current_hitzone_max) /2 - 0.5f) * width;
    }

    public bool check_hit()
    {
        return ((mover_pos > current_hitzone_min - hit_detection_leniency) && (mover_pos < current_hitzone_max + hit_detection_leniency));
    }

    public void hit_axe()
    {
        
        if (check_hit()){
            mover_speed *= mult_speed;
            current_hitzone_width *= mult_width;
            current_hitzone_min = Random.Range(0.0f, 1.0f - current_hitzone_width);
            current_hitzone_max = current_hitzone_min + current_hitzone_width;
            update_hitzone();
            shake_offset = hit_jerk;
            hitzone_v_scale += hit_zone_bop;
            currentCombo += 1.0f;
            count = 0;
            axeObject.SetActive(true);
        }
        else
        {
            mover_speed = start_speed * Mathf.Sign(mover_speed);
            current_hitzone_width = start_width;
            current_hitzone_min = Random.Range(0.0f, 1.0f - current_hitzone_width);
            current_hitzone_max = current_hitzone_min + current_hitzone_width;
            update_hitzone();
            shake_offset = 0.0f;
            hitzone_v_scale += miss_zone_bop;
            currentCombo = 0;
        }
    }

    void shake_minigame()
    {
        shake_offset = Mathf.Lerp(shake_offset, 0, Time.deltaTime * 10.0f);
        gameObject.transform.localPosition = origin + new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)) * shake_offset;
    }

}
