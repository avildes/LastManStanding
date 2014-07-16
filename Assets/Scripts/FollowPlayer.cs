using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 5f;

    private GameObject target;

    private Vector3 _direction;

    private bool alive;

    private bool _ativo;

    //-----EVENT MANAGER-----
    public delegate void MobHandler();
    public static event MobHandler onMobDie;
    //-----------------------

    void Start()
    {
        GameController.onSetAtivo += onSetAtivo;

        alive = true;
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
        this._ativo = ativo;
        //gameObject.SetActive(ativo);
    }

    IEnumerator Die()
    {
        onMobDie();

        gameObject.GetComponent<AudioSource>().Play();
        alive = false;

        yield return new WaitForSeconds(.1f);

        Destroy(gameObject);
    }
}
