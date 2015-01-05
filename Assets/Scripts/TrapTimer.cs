using UnityEngine;
using System.Collections;

public class TrapTimer : BaseClass
{
    public float TrapLifeTime;

    private float timer = 7f;

	private Animator animator;
	private Collider2D collider;

	void Start ()
    {
        EventManager.onSetAtivo += onSetAtivo;

        animator = this.animatorCache;
		collider = this.polygonColliderCache2D;
		collider.enabled = false;

		StartCoroutine(OnTrapAnimationEnterEnded());

        StartCoroutine(Timer());
	}

    void onSetAtivo(bool ativo)
    {
        if (!ativo)
        {
            EventManager.onSetAtivo -= onSetAtivo;
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

        EventManager.onSetAtivo -= onSetAtivo;

		animator.SetTrigger("Destroi");
		collider.enabled = false;

		yield return new WaitForSeconds(.75f);

		Destroy(gameObject);
	}
}
