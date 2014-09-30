﻿using UnityEngine;
using System.Collections;
using System;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 5f;

    private GameObject target;

    private Vector3 _direction;

    private bool alive;

    private bool _ativo;

    public AudioClip dieSound;

	private Vector2 movement;
    /*
    //-----EVENT MANAGER-----
    //-----------------------
    */
    public static event EventHandler<MobDeathEventArgs> onMobDie;
	void Start()
    {
        EventManager.onSetAtivo += onSetAtivo;

		StartCoroutine(Spawn());
    }

	IEnumerator Spawn()
	{
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (.75f);
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;

		alive = true;
		_ativo = true;
		target = GameObject.FindGameObjectWithTag("Player");
	}
	/*
	void Update()
	{
		if (alive)
		{
			_direction = target.transform.position - transform.position;
			movement = _direction.normalized * speed;
		}
	}
	*/
    void FixedUpdate()
    {
        if (alive)
        {
            //rigidbody2D.velocity = movement;
			_direction = target.transform.position - transform.position;
			rigidbody2D.velocity = _direction.normalized * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Trap")
        {
            StartCoroutine(Die(collider.gameObject));
        }
    }

	void onSetAtivo(bool ativo)
	{
		_ativo = ativo;
		if (!ativo)
		{
			alive = false;
            EventManager.onSetAtivo -= onSetAtivo;
			Destroy(gameObject);
		}
	}

    IEnumerator Die(GameObject trapGameObject)
    {

        //EventManager.Instance.onMobDieEvent();

        onMobDie(this, new MobDeathEventArgs(trapGameObject));
        gameObject.GetComponent<AudioSource>().Play();
        alive = false;

		//movement = new Vector2(0, 0);
        GetComponent<BoxCollider2D>().enabled = false;
        //GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().SetTrigger("die");
        GetComponent<ParticleSystem>().Play();
        //yield return new WaitForSeconds(.01f);
        yield return new WaitForSeconds(1f);

        EventManager.onSetAtivo -= onSetAtivo;
        Destroy(gameObject);
    }
}

public class MobDeathEventArgs : EventArgs
{
	/// <summary>
	/// Objeto com que o mob colidiu
	/// </summary>
	public GameObject GameObject { get; private set; }

	public MobDeathEventArgs(GameObject gameObject)
	{
		this.GameObject = gameObject;
	}
}
