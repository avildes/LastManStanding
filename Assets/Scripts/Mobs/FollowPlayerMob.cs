using UnityEngine;
using System.Collections;
using System;

public class FollowPlayerMob : Mob
{
    private float speed = 2f;

    private GameObject target;

    private Vector3 _direction;

    public AudioClip dieSound;

	private Vector2 movement;

	protected override Collider2D _Collider
	{ 
		get
		{
			return gameObject.GetComponent<BoxCollider2D>();
		}
	}

	void Start()
    {
		StartCoroutine(Spawn());
    }

	IEnumerator Spawn()
	{
		target = GameObject.FindGameObjectWithTag("Player");
		_Collider.enabled = false;
		yield return new WaitForSeconds (.75f);
		_Collider.enabled = true;

		alive = true;
		_ativo = true;
	}

    void FixedUpdate()
    {
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

        if (alive)
        {
            //rigidbody2D.velocity = movement;
			_direction = target.transform.position - transform.position;
			rigidbody2D.velocity = _direction.normalized * speed;
        }
    }
}
