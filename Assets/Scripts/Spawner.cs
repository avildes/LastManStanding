using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject mob;
    public GameObject trap;

    public float trapSpawnTime = 10;
    public float mobSpawnTime = 2;

	public float boundsX1 = 19f;
	public float boundsX2 = 39f;
	public float boundsY1 = 9f;
	public float boundsY2 = -7f;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(SpawnMob());
        StartCoroutine(SpawnTrap());
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	IEnumerator SpawnMob()
	{
		Instantiate(mob, GetRandomBoundsPosition(), Quaternion.identity);

        yield return new WaitForSeconds(mobSpawnTime);

		StartCoroutine(SpawnMob());
	}

    IEnumerator SpawnTrap()
    {
        Instantiate(trap, GetRandomPosition(), Quaternion.identity);
        
        yield return new WaitForSeconds(trapSpawnTime);
        
        StartCoroutine(SpawnTrap());
    }

    Vector3 GetRandomPosition()
    {
        float x = 0;
        float y = 0;
        float z = 0;
        
        Vector3 vector = new Vector3(0, 0, 0);
        
        vector.x = Random.Range(boundsX2, boundsX1);
        vector.y = Random.Range(boundsY2, boundsY1);
            
        return vector;
    }

	Vector3 GetRandomBoundsPosition()
	{
		float x = 0;
        float y = 0;
        float z = 0;

        float orientation = Random.Range (-1.0F, 1.0F);

        Vector3 vector = new Vector3(0, 0, 0);

		if (orientation < 0)  //Vertical - left, right
        {
            vector.y = Random.Range(boundsY2, boundsY1);

            orientation = Random.Range (-1.0F, 1.0F); // Check if it is X1 or X2 (leftOrRight)

            if(orientation < 0) // X1 left
            {
                vector.x = boundsX1;
            }
            else // X2 right
            {
                vector.x = boundsX2;
            }
        }
        else  // Horizontal - top, bot
        {
            vector.x = Random.Range(boundsX2, boundsX1);

            orientation = Random.Range (-1.0F, 1.0F); // Check if it is Y1 or Y2 (topOrBot)
            
            if(orientation < 0) // Y1 Top
            {
                vector.y = boundsY1;
            }
            else // Y2 Bot
            {
                vector.y = boundsY2;
            }
        }
        return vector;
	}
}
