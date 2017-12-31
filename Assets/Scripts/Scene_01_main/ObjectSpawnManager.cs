using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoShared;

public class ObjectSpawnManager : MonoBehaviour
{
	[HeaderAttribute("Coordinates")]
	[SerializeField]
	private List<Coordinates> m_itemCoordinates;
	[SerializeField]
	private List<Coordinates> m_enemyCoordinates;

	[HeaderAttribute("GameObject List")]
	[SerializeField]
	private List<GameObject> m_items;
	[SerializeField]
	private List<GameObject> m_enemies;

	[HeaderAttribute("Objects num")]
	[SerializeField]
	// アイテムの生成数
	private int m_itemNum = 10;
	[SerializeField]
	// 敵の生成数
	private int m_enemyNum = 5;

	[HeaderAttribute("Current Objects")]
	[SerializeField]
	// すでに生成されたアイテムを格納する
	private List<GameObject> m_currentItems;
	[SerializeField]
	// 既に生成された敵を格納する
	private List<GameObject> m_currentEnemies;

	[HeaderAttribute("for Debug")]
	[SerializeField]
	// xmlを使用しないテスト版の場合はtrueにする
	private bool m_isTestMode = false;
	
	public void Init(Coordinates centerCoordinates)
	{
		if (m_isTestMode)
		{
			// テストモード時はプレイヤーアバターの周囲にアイテムと敵を設置する
			// アイテムの座標リストをランダムに作成
			SetRandomCoordinates(ref m_itemCoordinates, m_itemNum, 0.0020f, centerCoordinates);
			// 敵の座標リストをランダムに作成
			SetRandomCoordinates(ref m_enemyCoordinates, m_enemyNum, 0.0020f, centerCoordinates);
		}

		m_currentItems = new List<GameObject>();
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
			double latitude = centerCoordinates.latitude + UnityEngine.Random.Range(-randomRange, randomRange);
			double longitude = centerCoordinates.longitude + UnityEngine.Random.Range(-randomRange, randomRange);
			Coordinates newCoordinates = new Coordinates(latitude, longitude, centerCoordinates.altitude);
			newCoordinates.latitude = latitude;
			coordinates.Add(newCoordinates);
		}
	}

	/// <summary>
	/// アイテムと敵を生成する
	/// </summary>
	/// <param name="currentLocation"></param>
	public void SpawnObjects(Coordinates currentLocation, Action<Coordinates> action)
	{
		StartCoroutine(_SpawnObjects(currentLocation, action));
	}

	/// <summary>
	/// アイテム、敵の更新を行う
	/// </summary>
	/// <param name="currentLocation"></param>
	/// <returns></returns>
	private IEnumerator _SpawnObjects(Coordinates currentLocation, Action<Coordinates> action)
	{
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

		action(currentLocation);
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

	/// <summary>
	/// 生成したアイテムの数を返す
	/// </summary>
	/// <returns></returns>
	public int GetSpawnItemNum()
	{
		return m_itemNum;
	}

	/// <summary>
	/// 生成した敵の数を返す
	/// </summary>
	/// <returns></returns>
	public int GetSpawnEnemyNum()
	{
		return m_enemyNum;
	}
}
