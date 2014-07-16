using UnityEngine;
using System.Collections;

public class TrapTimer : MonoBehaviour
{
    public float TrapLifeTime;

	private float timer = 8f;

	private Animator animator;
	private Collider2D collider;

	void Start ()
    {
        GameController.onSetAtivo += onSetAtivo;

		animator = gameObject.GetComponent<Animator>();
		collider = gameObject.GetComponent<PolygonCollider2D>();
		collider.enabled = false;

		StartCoroutine(OnTrapAnimationEnterEnded());

        StartCoroutine(Timer());
	}

    void onSetAtivo(bool ativo)
    {
        if (!ativo)
        {
			GameController.onSetAtivo -= onSetAtivo;
			Destroy(gameObject);
        }
    }

	IEnumerator OnTrapAnimationEnterEnded()
	{
		yield return new WaitForSeconds(.75f);
		collider.enabled = true;
	}
	
	IEnumerator Timer()
	{
		yield return new WaitForSeconds(timer);
		
		GameController.onSetAtivo -= onSetAtivo;

		animator.SetTrigger("Destroi");
		collider.enabled = false;

		yield return new WaitForSeconds(.75f);

		Destroy(gameObject);
	}
}
