using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public float speed = 5f;

	private GameObject target;
	
	private Vector3 _direction;

	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate()
	{
		_direction = target.transform.position - transform.position;
		rigidbody2D.velocity = _direction.normalized * speed;
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Trap")
        {
            Destroy(this.gameObject);
        }
    }
}
