using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject mob;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(SpawnMob());
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	IEnumerator SpawnMob()
	{
		Instantiate(mob, transform.position, Quaternion.identity);

		yield return new WaitForSeconds(2);

		StartCoroutine(SpawnMob());
	}
}
