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

    void FixedUpdate()
    {
        if (alive)
        {
            //rigidbody2D.velocity = movement;
            _direction = target.transform.position - transform.position;
            rigidbody2D.velocity = _direction.normalized * speed;
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
