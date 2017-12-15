using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoShared;

public class ItemManager : MonoBehaviour
{
	[SerializeField]
	private LocationManager m_locationManager;

	[SerializeField]
	private Coordinates[] m_coordinates;

	[SerializeField]
	private GameObject[] m_items;

	[SerializeField]
	// すでに生成されたアイテムを格納する
	private List<GameObject> m_currentItems;

	// Use this for initialization
	void Awake ()
	{
		m_currentItems = new List<GameObject> ();

		// 初回の位置取得時の処理
		m_locationManager.onOriginSet.AddListener((Coordinates) => {OnOriginSet(Coordinates);});
		// 位置情報更新時の処理
		//m_locationManager.onLocationChanged.AddListener((Coordinates) => {OnLocationChanged(Coordinates);});
	}

	private void OnOriginSet(Coordinates currentLocation){
		StartCoroutine (_SpawnItems(currentLocation));
	}

	private void OnLocationChanged(Coordinates currentLocation){
		StartCoroutine (_SpawnItems(currentLocation));
	}

	private IEnumerator _SpawnItems(Coordinates currentLocation){
		yield return new WaitForSeconds(1.0f);
		for( int i = 0; i < m_currentItems.Count; i++ ){
			Destroy (m_currentItems[i]);
		}
		m_currentItems.Clear ();
		yield return new WaitForSeconds(1.0f);

		for(int i = 0; i < m_coordinates.Length; i++){
			m_currentItems.Add (SpawnItems (m_coordinates [i]));
		}
	}

	private GameObject SpawnItems (Coordinates currentLocation)
	{
		//Position
		Vector3 currentPosition = currentLocation.convertCoordinateToVector ();
		currentPosition.y = m_items[0].transform.position.y;

		GameObject item = Instantiate (m_items[0], currentPosition, Quaternion.identity);

		return item;
	}
}
