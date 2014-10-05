using UnityEngine;
using System.Collections;
using System;

public class BouncyMob : Mob
{
    public float speed = 5f;

    private GameObject target;

    private Vector3 _direction;

    public AudioClip dieSound;

    private Vector2 movement;

	protected override Collider2D _Collider
	{ 
		get
		{
			return gameObject.GetComponent<CircleCollider2D>();
		}
	}


	void Start ()
    {
        StartCoroutine(Spawn());
	}

    bool once = true;

    void Move()
    {
		_direction = target.transform.position - transform.position;
        //rigidbody2D.AddForce(_direction.normalized * 1000);
        //Debug.Log("dale");
    }

    void FixedUpdate()
    {
		// Se o player morreu, destroi o objeto
		if(target == null)
		{
			Destroy(gameObject);
			return;
		}

        if (alive && once)
        {
            once = false;
            //rigidbody2D.velocity = movement;
            Move();
            //_direction = target.transform.position - transform.position;
        }
        if (alive)
        {
            /*rigidbody2D.velocity = _direction.normalized * speed;*/
            //System.Random a = new System.Random();
            //_direction = new Vector3(a.Next(-4000, 4000), a.Next(-4000, 4000), 0 );
            //rigidbody2D.velocity = _direction.normalized * 4000;
            rigidbody2D.AddForce(_direction.normalized * 200);
        }
    }

    IEnumerator Spawn()
    {
		target = GameObject.FindGameObjectWithTag("Player");

        _Collider.enabled = false;
        yield return new WaitForSeconds(.75f);
		_Collider.enabled = true;

        alive = true;
		_ativo = true;
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Arena")
            Move();
    }
}
