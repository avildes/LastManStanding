using UnityEngine;
using System.Collections;
using System;

public abstract class Mob : MonoBehaviour
{
	public static event EventHandler<MobDeathEventArgs> onMobDie;
	protected bool alive;
	protected bool _ativo;

	protected abstract Collider2D _Collider { get; }

	void Start()
	{
		EventManager.onSetAtivo += onSetAtivo;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Trap")
		{
			StartCoroutine(Die(collider.gameObject));
		}
	}
	
	IEnumerator Die(GameObject trapGameObject)
	{
		//EventManager.Instance.onMobDieEvent();
		
		onMobDie(this, new MobDeathEventArgs(trapGameObject));
		gameObject.GetComponent<AudioSource>().Play();
		alive = false;
		
		//movement = new Vector2(0, 0);
		_Collider.enabled = false;
		//GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Animator>().SetTrigger("die");
		GetComponent<ParticleSystem>().Play();
		//yield return new WaitForSeconds(.01f);
		yield return new WaitForSeconds(1f);
		
		EventManager.onSetAtivo -= onSetAtivo;
		Destroy(gameObject);
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
