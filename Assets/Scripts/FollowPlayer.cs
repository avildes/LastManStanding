using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public Transform target;//set target from inspector instead of looking in Update
	public float speed = 5f;

	//public Vector3 speed = new Vector3(3, 3, 0);
	public Vector3 direction = new Vector3();
	private Vector2 movement;

	private Vector3 actualDirection = new Vector3(0,0,0);

	void Start () {
		
	}

	bool check = true;
	public float waitAndCheck = .5f;

	IEnumerator CheckNasParadas(){

		transform.LookAt(target.position);
		//transform.LookAt (Vector3 (0, target.position.y, target.position.z));

		//Q/uaternion neededRotation = Quaternion.LookRotation(target.position - transform.position);
		
		//Quaternion interpolatedRotation = Quaternion.Slerp(new Quaternion(transform.position.x, transform.position.y, transform.position.z,0), neededRotation, (Time.deltaTime * .2f));

		
		direction = target.position - transform.position;

		movement = speed * transform.forward;

		//transform.Rotate(new Vector3(0,-90,0),Space.Self);//correcting the original rotation

		transform.Rotate(new Vector3(0,-90,0),Space.Self);//correcting the original rotation



		yield return new WaitForSeconds(waitAndCheck);
		check = true;
	}

	void Update(){

	
		//rotate to look at the player

		/*
		 * 
		 * transform.LookAt(target.position);
		transform.Rotate(new Vector3(0,-90,0),Space.Self);//correcting the original rotation
		
		direction = target.position - transform.position;
		/*
		if(direction.Equals(actualDirection)){
			*
			**
			**
			**
			**
			*movement = speed * direction;/*
		}
		else
		{
			if(rigidbody2D.velocity.Equals(new Vector2(0,0))){
				actualDirection = direction;
			}


		}*/


		if (check)
		{
			check = false;
			StartCoroutine(CheckNasParadas());
		}



		/*
		//move towards the player
		//if (Vector3.Distance(transform.position,target.position)>1f){//move if distance from target is greater than 1
			movement = new Vector2(
				(speed.x*Time.deltaTime) - target.position.x,
				(speed.y*Time.deltaTime) - target.position.y);
		//}

		*/
	}

	void FixedUpdate()
	{

		//MovePosition ();
		MoveVelocity ();
		//MoveForce ();
		//MoveTranslate (); <-- n presta...


	}

	void MoveForce(){
		rigidbody2D.AddForce( movement);
	}

	void MoveVelocity(){

		// Apply movement to the rigidbody
		rigidbody2D.velocity = movement;
		//rigidbody2D.velocity = new Vector2((transform.forward.x * speed.x),(transform.forward.y * speed.y));
		//rigidbody2D.AddForce (new Vector2((speed.x * transform.forward.x), (speed.y * transform.forward.y)));//speed * (new Vector2(target.position.x, target.position.y)).normalized);
		
		
		//Debug.Log (target.position.x);
		
		//Debug.Log (rigidbody2D.velocity.x);
		//Debug.Log (rigidbody2D.velocity.y);
	}

	void MoveTranslate (){
		Debug.Log ("Translate");
		speed = 10f;
		transform.Translate(new Vector3(speed* Time.deltaTime,0,0) );
	}

	void MovePosition (){
		Debug.Log ("Position");
		speed = 5f;

		Vector3 dir = target.position - transform.position;
		
		//dir.Normalize();
		
		// Move ourselves in that direction
		transform.position += (dir * (speed * Time.deltaTime));
	}

}
