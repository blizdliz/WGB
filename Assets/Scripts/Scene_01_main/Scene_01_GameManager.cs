using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_01_GameManager : MonoBehaviour
{
	public const int GAME_STATE_ISNOTINIT = 0;
	public const int GAME_STATE_NORMAL = 1;
	public const int GAME_STATE_PAUSE = 2;
	public const int GAME_STATE_END = 3;
	// ゲームの状態
	private int m_gameState = GAME_STATE_ISNOTINIT;

	[SerializeField]
	// ゲームの残り時間
	private double m_timer = 60;
	// 現在の残り時間
	private double m_currentTimer = 0;


	// Use this for initialization
	void Start () 
	{
		InitGame ();
	}

	void Update()
	{
		if (m_gameState == GAME_STATE_NORMAL) {
			m_currentTimer -= Time.deltaTime;
			Debug.Log ("残り時間:" + m_currentTimer.ToString());
			if (m_currentTimer <= 0) {
				// 時間切れ
				GameFailure ();
			}
		}
	}

	/// <summary>
	/// ゲームを初期化する
	/// </summary>
	private void InitGame(){
		m_gameState = GAME_STATE_ISNOTINIT;
		m_currentTimer = m_timer;
		m_gameState = GAME_STATE_NORMAL;
	}

	/// <summary>
	/// 成功時のゲーム終了
	/// </summary>
	public void GameSuccess()
	{
		Debug.Log ("成功!");
		m_gameState = GAME_STATE_END;
	}

	/// <summary>
	/// 失敗時のゲーム終了
	/// </summary>
	public void GameFailure()
	{
		Debug.Log ("失敗…");
		m_gameState = GAME_STATE_END;
	}
}
