using UnityEngine;
using System.Collections;

public class TrapTimer : MonoBehaviour {

    public float TrapLifeTime;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Timer());
	}
	
	IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
