using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCharacterController : MonoBehaviour
{

	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;

	private Rigidbody rigidbody;
	private NavMeshAgent agent;
	private bool grounded = false;

	public float ver;
	public float hor;

	private void Start()
	{

		rigidbody = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}

	void FixedUpdate()
	{
		if (grounded)
		{
			Vector3 targetVelocity = CalculateTargetVelocity();

			ApplyVelocity(targetVelocity);

			// Jump
			if (canJump && Input.GetButton("Jump"))
			{
				rigidbody.velocity = new Vector3(rigidbody.velocity.x, CalculateJumpVerticalSpeed(), rigidbody.velocity.z);
			}
		}

		// We apply gravity manually
		rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

		grounded = false;
	}

    private void OnCollisionStay(Collision collision)
    {
		if (collision.collider.tag.Equals("Surface"))
        {
			grounded = true;
        }
    }

    private Vector3 CalculateTargetVelocity()
    {
		Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		hor = Input.GetAxis("Horizontal");
		ver = Input.GetAxis("Vertical");
		targetVelocity = transform.TransformDirection(targetVelocity) * speed;

		return targetVelocity;
	}

	private void ApplyVelocity(Vector3 targetVelocity)
    {
		// Apply a force that attempts to reach our target velocity
		Vector3 velocity = rigidbody.velocity;
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
		velocityChange.y = 0;
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
	}

	float CalculateJumpVerticalSpeed()
	{
		// From the jump height and gravity 5deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}
