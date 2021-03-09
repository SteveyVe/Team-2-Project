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
