///-----------------------------------------------------------------
///   Class:          PlayerMovement
///   Description:    This script handles user input and character movement
///   Author/Revision History: Handled by Github
///-----------------------------------------------------------------
#region using directives
using UnityEngine;
using UnityEngine.InputSystem;
#endregion

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    private Vector2 wasdInput;
    private Vector3 movementVelocity;

    public float movementSpeed = 3;

    private string p1;

    private bool freezePlayer;
    public bool bonusActive;

    private float time;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        freezePlayer = false;
    }

    private void Start()
    {
        p1 = gameObject.name.Substring(6, 1);

        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer1Ended");
        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer2Ended");

        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer1Start");
        NotificationCenter.DefaultCenter.AddObserver(this, "ChopTimer2Start");
    }

    private void OnMovement(InputValue value)
    {
        wasdInput = value.Get<Vector2>();
        ProcessInput();
    }

    private void ProcessInput()
    {
        movementVelocity = Vector3.zero;

        float vertVal = 0f;
        float horzVal = 0f;

        //Set input values
        if(wasdInput.y > 0f)
        {
            vertVal += 1f;
        }
        else if(wasdInput.y < 0f)
        {
            vertVal -= 1f;
        }

        if(wasdInput.x > 0f)
        {
            horzVal += 1f;
        }
        else if(wasdInput.x < 0f)
        {
            horzVal -= 1f;
        }

        //Set player move velocity
        if(vertVal != 0)
        {
            movementVelocity += Vector3.forward * vertVal * movementSpeed;
        }
        if(horzVal != 0)
        {
            movementVelocity += Vector3.right * horzVal * movementSpeed;
        }
    }

    private void Update()
    {
        if (bonusActive)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                movementSpeed = 3;
                bonusActive = false;
            }
        }
    }

    //Move player
    void LateUpdate()
    {
        if (!freezePlayer)
        {
            if (wasdInput == Vector2.zero)
            {
                movementVelocity = Vector3.zero;
            }
            rb.velocity = new Vector3(movementVelocity.x, rb.velocity.y, movementVelocity.z);
        }
    }

    //--Methods below are all called via Notification system--//

    private void ChopTimer1Start()
    {
        if(p1 == "1")
        {
            freezePlayer = true;
        }
    }

    private void ChopTimer2Start()
    {
        if (p1 == "2")
        {
            freezePlayer = true;
        }
    }

    void ChopTimer1Ended()
    {
        if (p1 == "1")
        {
            freezePlayer = false;
        }
    }
    void ChopTimer2Ended()
    {
        if (p1 == "2")
        {
            freezePlayer = false;
        }
    }


}