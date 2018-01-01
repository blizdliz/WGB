using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoShared;

public class Scene_01_GameManager : MonoBehaviour
{
	public int GAME_STATE_ISNOTINIT = 0;// ゲーム未初期化
	public int GAME_STATE_NORMAL = 1;	// ゲーム実施中
	public int GAME_STATE_PAUSE = 2;	// ゲーム中断中
	public int GAME_STATE_END = 3;		// ゲーム終了
	// ゲームの状態
	private int m_gameState = 0;

	[SerializeField]
	private LocationManager m_locationManager;


	[SerializeField]
	// ゲームの残り時間
	private double m_timer = 60;
	// 現在の残り時間
	private double m_currentTimer = 0;
	// 現在の残りのアイテム数
	private int m_currentItemNum = 0;
	// 現在の残りの敵の数
	private int m_currentEnemyNum = 0;

	[SerializeField]
	// 敵とアイテムを生成するクラス
	private ObjectSpawnManager m_objectSpawnManager;
	[SerializeField]
	// GUIを管理するクラス
	private Scene_01_GUIManager m_guiManager;

	/// <summary>
	/// 最初に呼ばれる処理
	/// </summary>
	void Awake () 
	{
		// 初回の位置取得時の処理
		m_locationManager.onOriginSet.AddListener((Coordinates) => { OnOriginSet(Coordinates); });
		// 位置情報更新時の処理
		//m_locationManager.onLocationChanged.AddListener((Coordinates) => {OnLocationChanged(Coordinates);});
	}

	private void OnOriginSet(Coordinates currentLocation)
	{
		// 敵とアイテムの生成クラスを初期化
		m_objectSpawnManager.Init(m_locationManager.currentLocation);
		// アイテムと敵をスポーンさせた後、InitGameの処理を実行させる
		m_objectSpawnManager.SpawnObjects(m_locationManager.currentLocation, InitGame);
	}

	private void OnLocationChanged(Coordinates currentLocation)
	{
		m_objectSpawnManager.SpawnObjects(currentLocation, InitGame);
	}

	/// <summary>
	/// ゲームを初期化する
	/// </summary>
	private void InitGame(Coordinates currentLocation)
	{
		m_gameState = GAME_STATE_ISNOTINIT;
		// 生成されたアイテムの数を取得しGUIには反映する
		m_currentItemNum = m_objectSpawnManager.GetSpawnItemNum();
		m_guiManager.SetCurrentItemNum(m_currentItemNum);
		// 生成された敵の数を取得しGUIに反映する
		m_currentEnemyNum = m_objectSpawnManager.GetSpawnEnemyNum();
		m_guiManager.SetCurrentEnemyNum(m_currentEnemyNum);
		// タイマーを初期化
		m_currentTimer = m_timer;
		m_gameState = GAME_STATE_NORMAL;
	}

	void Update()
	{
		if (m_gameState == GAME_STATE_NORMAL)
		{
			m_currentTimer -= Time.deltaTime;
			if (m_currentTimer <= 0)
			{
				// 時間切れ
				GameFailure();
			}
			else
			{
				// GUIに残り時間をセット
				m_guiManager.SetCurrentTime(m_currentTimer);
			}
		}
	}

	/// <summary>
	/// プレイヤーが死亡した際の処理
	/// </summary>
	public void PlayerDeath()
	{
		// プレイヤー死亡
		GameFailure();
	}

	/// <summary>
	/// 残りのアイテム数を更新する
	/// </summary>
	public void UpdateCurrentItemNum()
	{
		m_currentItemNum -= 1;
		m_guiManager.SetCurrentItemNum(m_currentItemNum);

		if (m_currentItemNum <= 0)
		{
			// ゲーム終了
			GameSuccess();
		}
	}

	/// <summary>
	/// 残りの敵の数を更新する
	/// </summary>
	public void UpdateCurrentEnemyNum()
	{
		m_currentEnemyNum -= 1;
		m_guiManager.SetCurrentEnemyNum(m_currentEnemyNum);

		if (m_currentEnemyNum <= 0)
		{
			// ゲーム終了
			GameSuccess();
		}
	}

	/// <summary>
	/// 成功時のゲーム終了
	/// </summary>
	public void GameSuccess()
	{
		Debug.Log ("成功!");
		m_gameState = GAME_STATE_END;
		// ゲームオーバーGUIを表示
		m_guiManager.DisplayGameOverGUI();
	}

	/// <summary>
	/// 失敗時のゲーム終了
	/// </summary>
	public void GameFailure()
	{
		Debug.Log ("失敗…");
		m_gameState = GAME_STATE_END;
		// ゲームオーバーGUIを表示
		m_guiManager.DisplayGameOverGUI();
	}

	/// <summary>
	/// ゲームの状況を返す
	/// </summary>
	/// <returns></returns>
	public int GetGameState()
	{
		return m_gameState;
	}
}
