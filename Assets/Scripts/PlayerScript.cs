using UnityEngine;
using System.Collections;

/// <summary>
/// Player controller and behavior
/// </summary>
public class PlayerScript : MonoBehaviour
{
	/// <summary>
	/// 1 - The speed of the ship
	/// </summary>
	public Vector2 speed = new Vector2(50, 50);
	
    public GUIText endTimeText;

	// 2 - Store the movement
	private Vector2 movement;

	private float totalTime;

    private bool isJumping;
	
	void Update()
	{
		// 3 - Retrieve axis information
		float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        
        // 4 - Movement per direction
        movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(JumpCoroutine ());
        }
	}
	
	void FixedUpdate()
	{
		// 5 - Move the game object
		rigidbody2D.velocity = movement;
	}

    IEnumerator JumpCoroutine()
    {
        isJumping = true;
        renderer.material.color = new Color(0, 255, 255);

        yield return new WaitForSeconds(1);

        isJumping = false;
        renderer.material.color = new Color(255, 0, 0);
    }

	void OnCollisionEnter2D(Collision2D hit)
	{
		if (hit.gameObject.tag == "Enemy")
		{
            Destroy();
		}
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Trap")
        {
            if(!isJumping)
            {
                Destroy();
            }
        }
    }

    void Destroy()
    {
        totalTime = TimerScript.Instance.GetTotalTime();
        endTimeText.text = string.Format("TIME {0:f2}", totalTime);
        Destroy(this);
    }
}