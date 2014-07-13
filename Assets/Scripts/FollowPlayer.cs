using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 5f;

    private GameObject target;

    private Vector3 _direction;

    private bool alive;

    void Start()
    {
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
            alive = false;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        onMobDie();

        gameObject.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(.1f);

        GameManager.MobKilled();

        Destroy(this.gameObject);
    }

    public delegate void MobHandler();
    public static event MobHandler onMobDie;
}
