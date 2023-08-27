using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public Camera camera;
    public Animator animator;
	public Transform hips;

    public int lane = 0;
    float laneSize = 1.75f;

    bool right;
    bool left;
    bool jump;
    bool slide;

	private void Start()
	{
		laneSize = FindObjectOfType<GameManager>().laneSize;
	}

	void Update()
    {
        if(Input.GetAxis("Horizontal") > 0.25f)
        {
            right = true;
        }
        else if(Input.GetAxis("Horizontal") < -0.25f)
        {
            left = true;
        }
        
		if(Input.GetButton("Jump"))
        {
			jump = true;
		}
		else if (Input.GetButton("Slide"))
		{
			slide = true;
		}
	}

	void FixedUpdate()
	{
		animator.SetBool("right", false);
		animator.SetBool("left", false);
		animator.SetBool("jump", false);
		animator.ResetTrigger("slide");

		if (right)
		{
            if(lane < 1)
            {
				animator.SetBool("right", true);
			}
		}
        else if (left)
        {
			if (lane > -1)
			{
				animator.SetBool("left", true);
			}
        }
		
		if (jump)
        {
            animator.SetBool("jump", true);
        }
        else if(slide)
        {
            animator.SetTrigger("slide");
        }

        right = false;
        left = false;
        jump = false;
        slide = false;
	}

	public void MoveLeft()
	{
		lane--;
	}

	public void MoveRight()
	{
		lane++;
	}

	public void MoveToLane()
	{
		Vector3 newPosition = new Vector3(lane * laneSize, transform.position.y, transform.position.z);

		transform.position = newPosition;
		camera.transform.position = newPosition;
		hips.transform.position = newPosition;
	}
}
