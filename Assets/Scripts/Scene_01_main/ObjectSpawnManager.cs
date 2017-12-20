using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoShared;

public class ObjectSpawnManager : MonoBehaviour
{
	[SerializeField]
	private LocationManager m_locationManager;

	[SerializeField]
	private List<Coordinates> m_itemCoordinates;
	[SerializeField]
	private List<Coordinates> m_enemyCoordinates;

	[SerializeField]
	private List<GameObject> m_items;
	[SerializeField]
	private List<GameObject> m_enemies;

	[SerializeField]
	// すでに生成されたアイテムを格納する
	private List<GameObject> m_currentItems;
	[SerializeField]
	// 既に生成された敵を格納する
	private List<GameObject> m_currentEnemies;

	[SerializeField]
	// xmlを使用しないテスト版の場合はtrueにする
	private bool m_isTestMode = false;

	// Use this for initialization
	void Awake()
	{
		if (m_isTestMode)
		{
			Coordinates centerCoordinates = m_locationManager.currentLocation;
			// テストモード時はプレイヤーアバターの周囲にアイテムと敵を設置する
			// アイテムの座標リストをランダムに作成
			SetRandomCoordinates(ref m_itemCoordinates, 10, 0.0005f, centerCoordinates);
			// 敵の座標リストをランダムに作成
			SetRandomCoordinates(ref m_enemyCoordinates, 5, 0.0010f, centerCoordinates);
		}

		m_currentItems = new List<GameObject>();

		// 初回の位置取得時の処理
		m_locationManager.onOriginSet.AddListener((Coordinates) => { OnOriginSet(Coordinates); });
		// 位置情報更新時の処理
		//m_locationManager.onLocationChanged.AddListener((Coordinates) => {OnLocationChanged(Coordinates);});
	}

	/// <summary>
	/// 引数で渡した範囲でのランダムな座標をリストに格納する
	/// </summary>
	/// <param name="coordinates"></param>
	/// <param name="num"></param>
	/// <param name="randomRange"></param>
	/// <param name="centerCoordinates"></param>
	private void SetRandomCoordinates(ref List<Coordinates> coordinates, int num, float randomRange,Coordinates centerCoordinates)
	{
		coordinates.Clear();
		for (int i = 0; i < num; i++)
		{
			double latitude = centerCoordinates.latitude + Random.Range(-randomRange, randomRange);
			double longitude = centerCoordinates.longitude + Random.Range(-randomRange, randomRange);
			Coordinates newCoordinates = new Coordinates(latitude, longitude, centerCoordinates.altitude);
			newCoordinates.latitude = latitude;
			coordinates.Add(newCoordinates);
		}
	}

	private void OnOriginSet(Coordinates currentLocation)
	{
		StartCoroutine(_SpawnObjects(currentLocation));
	}

	private void OnLocationChanged(Coordinates currentLocation)
	{
		StartCoroutine(_SpawnObjects(currentLocation));
	}

	/// <summary>
	/// アイテム、敵の更新を行う
	/// </summary>
	/// <param name="currentLocation"></param>
	/// <returns></returns>
	private IEnumerator _SpawnObjects(Coordinates currentLocation)
	{
		yield return new WaitForSeconds(1.0f);
		// アイテムを削除
		for (int i = 0; i < m_currentItems.Count; i++)
		{
			Destroy(m_currentItems[i]);
		}
		// 敵を削除
		for (int i = 0; i < m_currentEnemies.Count; i++)
		{
			Destroy(m_currentEnemies[i]);
		}
		m_currentItems.Clear();
		m_currentEnemies.Clear();

		yield return new WaitForSeconds(1.0f);
		// アイテムを生成
		for (int i = 0; i < m_itemCoordinates.Count; i++)
		{
			m_currentItems.Add(SpawnObject(m_itemCoordinates[i], m_items[0]));
		}
		// 敵を生成
		for (int i = 0; i < m_enemyCoordinates.Count; i++)
		{
			m_currentEnemies.Add(SpawnObject(m_enemyCoordinates[i], m_enemies[0]));
		}
	}

	/// <summary>
	/// 引数のオブジェクトを生成する
	/// </summary>
	/// <param name="currentLocation"></param>
	/// <param name="obj"></param>
	/// <returns></returns>
	private GameObject SpawnObject(Coordinates currentLocation, GameObject obj)
	{
		//Position
		Vector3 currentPosition = currentLocation.convertCoordinateToVector();
		currentPosition.y = obj.transform.position.y;

		GameObject item = Instantiate(obj, currentPosition, Quaternion.identity);

		return item;
	}
}
