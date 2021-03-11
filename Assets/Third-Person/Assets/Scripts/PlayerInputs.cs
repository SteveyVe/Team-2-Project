using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerMotor))]

public class PlayerInputs : MonoBehaviour
{
    PlayerMotor playerMotor;

    private void Awake()
    {
        playerMotor = GetComponent<PlayerMotor>();
    }

    public Vector2 Movement()
    {
        Vector2 moveMent = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (moveMent.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.GetChild(1).transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveMent.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.GetChild(1).transform.localScale = new Vector3(-1, 1, 1);
        }
        return moveMent;
    }

    public void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMotor._jumpInput = true;
        }
    }
}
