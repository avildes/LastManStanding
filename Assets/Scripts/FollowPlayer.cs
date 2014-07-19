using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 5f;

    private GameObject target;

    private Vector3 _direction;

    private bool alive;

    private bool _ativo;

    public AudioClip dieSound;
    /*
    //-----EVENT MANAGER-----
    public delegate void MobHandler();
    public static event MobHandler onMobDie;
    //-----------------------
    */
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

    void FixedUpdate()
    {
        if (alive)
        {
            _direction = target.transform.position - transform.position;
            rigidbody2D.velocity = _direction.normalized * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Trap")
        {
            StartCoroutine(Die());
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

    IEnumerator Die()
    {
        EventManager.Instance.onMobDieEvent();

        gameObject.GetComponent<AudioSource>().Play();
        alive = false;

        yield return new WaitForSeconds(.1f);

        EventManager.onSetAtivo -= onSetAtivo;
        Destroy(gameObject);
    }
}
