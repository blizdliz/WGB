using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoMap;
using GoShared;

public class ObjectSpawnManager : MonoBehaviour 
{
	[SerializeField]
	private Coordinates m_coordinates;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (_UpdatePosition());
	}

	private IEnumerator _UpdatePosition(){
		while(true){
			OnOriginSet (m_coordinates);
			yield return new WaitForSeconds(1.0f);
		}
	}

	void OnOriginSet (Coordinates currentLocation)
	{
		//Position
		Vector3 currentPosition = currentLocation.convertCoordinateToVector ();
		currentPosition.y = transform.position.y;

		transform.position = currentPosition;
	}
}
