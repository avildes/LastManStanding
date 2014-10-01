using UnityEngine;
using System.Collections;

public class BouncyMob : MonoBehaviour
{
    public float speed = 5f;

    private GameObject target;

    private Vector3 _direction;

    private bool alive;

    private bool _ativo;

    public AudioClip dieSound;

    private Vector2 movement;

	void Start ()
    {
        EventManager.onSetAtivo += onSetAtivo;

        StartCoroutine(Spawn());
	}

    IEnumerator Spawn()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(.75f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;

        alive = true;
        _ativo = true;
        target = GameObject.FindGameObjectWithTag("Player");
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
