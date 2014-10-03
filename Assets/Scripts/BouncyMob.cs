using UnityEngine;
using System.Collections;
using System;

public class BouncyMob : MonoBehaviour
{
    public float speed = 5f;

    private GameObject target;

    private Vector3 _direction;

    private bool alive;

    private bool _ativo;

    public AudioClip dieSound;

    private Vector2 movement;

    public static event EventHandler<MobDeathEventArgs> onMobDie;

	void Start ()
    {
        EventManager.onSetAtivo += onSetAtivo;

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
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(.75f);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;

        alive = true;
        _ativo = true;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator Die(GameObject trapGameObject)
    {

        //EventManager.Instance.onMobDieEvent();

        onMobDie(this, new MobDeathEventArgs(trapGameObject));
        gameObject.GetComponent<AudioSource>().Play();
        alive = false;

        //movement = new Vector2(0, 0);
        GetComponent<CircleCollider2D>().enabled = false;
        //GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Animator>().SetTrigger("die");
        GetComponent<ParticleSystem>().Play();
        //yield return new WaitForSeconds(.01f);
        yield return new WaitForSeconds(1f);

        EventManager.onSetAtivo -= onSetAtivo;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Trap")
        {
            StartCoroutine(Die(collider.gameObject));
        }
    }
    /*
    void OnCollisionEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Arena")
        {
            Move();
        }
    }*/
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Arena")
            Move();
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
}
