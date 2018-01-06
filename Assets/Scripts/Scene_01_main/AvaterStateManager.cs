using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvaterStateManager : MonoBehaviour
{
	// プレイヤーの状態定数
	public int PLAYER_STATE_NORMAL  = 0;
	public int PLAYER_STATE_DEATH   = 1;
	public int PLAYER_STATE_POWERUP = 2;

	[HeaderAttribute("DO NOT Edit the following parameters.")]
	[SerializeField]
	// プレイヤーの状態
	private int m_playerState;
	[SerializeField]
	// プレイヤーのアイテム獲得ポイント
	private int m_playerItemPoint;
	[SerializeField]
	// プレイヤーの敵を討伐した数
	private int m_playerDefeatEnemyNum;
	[SerializeField]
	// 無敵ゲージ、ポイント獲得ごとにゲージが溜まる。一定値を超えると無敵モード発動可能（格ゲーかな？）
	private double m_powerUpGagePoint;

	[HeaderAttribute("instance")]
	[SerializeField]
	// ゲームマネージャー
	private Scene_01_GameManager m_gameManager;
	[SerializeField]
	// GUIマネージャークラス
	private Scene_01_GUIManager m_guiManager;

	[SerializeField]
	// プレイヤー強化用のGUIマネージャークラス
	private Scene_01_PowerUPGUIManager m_powerUpGuiManager;

	[HeaderAttribute("Defined Param")]
	[SerializeField]
	// 無敵モード発動のために最低限必要なポイント
	private int m_powerUpLimitPoint = 10;
	[SerializeField]
	// 無敵モード時のポイントと時間の倍率（ポイント * レート = 無敵時間(秒)）
	private float m_powerupTimeRate = 5;

	// Use this for initialization
	void Start ()
	{
		InitPlayerState ();
	}

	/// <summary>
	/// 毎フレーム呼ばれる処理
	/// </summary>
	void Update()
	{
		if (m_playerState == PLAYER_STATE_POWERUP && m_gameManager.GetGameState() == m_gameManager.GAME_STATE_NORMAL)
		{
			// 残り無敵時間の計算処理
			if (m_powerUpGagePoint > 0)
			{
				// GUI上に残り無敵時間を反映
				m_powerUpGuiManager.SetCurrentPowerUpTime(CalculatePowerUpTime(m_powerUpGagePoint));
				// 無敵時間から経過時間を引く
				m_powerUpGagePoint -= Time.deltaTime / m_powerupTimeRate;
			}
			else
			{
				// GUIを非表示にする
				m_powerUpGuiManager.HideAll();
				m_playerState = PLAYER_STATE_NORMAL;
				m_powerUpGagePoint = 0;
			}
		}
	}

	/// <summary>
	/// プレイヤーの状態を初期化する
	/// </summary>
	private void InitPlayerState()
	{
		m_playerState = PLAYER_STATE_NORMAL;
		m_playerItemPoint = 0;
		m_powerUpGagePoint = 0;
		// 取得したアイテム数をGUIにセット
		m_guiManager.SetAcquiredItemNum(m_playerItemPoint);
		// パワーアップ用GUIを初期化
		m_powerUpGuiManager.initGUI();
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			if (m_playerState == PLAYER_STATE_NORMAL)
			{
				// 敵に接触
				Debug.Log("敵に接触");
				m_playerState = PLAYER_STATE_DEATH;
				// 敵に触れたことによりプレイヤー死亡
				m_gameManager.PlayerDeath();
			}
		}
	}

	/// <summary>
	/// アイテム取得処理
	/// </summary>
	/// <param name="itemPoint"></param>
	public void GetItem(int itemPoint)
	{
		Debug.Log("アイテム獲得");
		m_playerItemPoint += itemPoint;
		// ゲームマネージャーのアイテム取得処理を呼ぶ
		m_gameManager.UpdateCurrentItemNum();
		// ポイントをGUIにセット
		m_guiManager.SetAcquiredItemNum(m_playerItemPoint);
		// パワーアップゲージにポイントを加算
		m_powerUpGagePoint += itemPoint;
		if (m_playerState == PLAYER_STATE_NORMAL && m_powerUpGagePoint >= m_powerUpLimitPoint)
		{
			// 規定値を越えた場合、パワーアップGUIを表示
			m_powerUpGuiManager.Display();
		}
	}

	/// <summary>
	/// 敵討伐ポイント取得処理
	/// </summary>
	/// <param name="point"></param>
	public void DefeatEnemy(int point)
	{
		Debug.Log("敵を倒した");
		m_playerItemPoint += point;
		m_playerDefeatEnemyNum += 1;
		// ゲームマネージャーの敵討伐処理を呼ぶ
		m_gameManager.UpdateCurrentEnemyNum();
		// ポイントをGUIにセット
		m_guiManager.SetAcquiredItemNum(m_playerItemPoint);
		// パワーアップゲージにポイントを加算
		m_powerUpGagePoint += point;
		if (m_playerState == PLAYER_STATE_NORMAL && m_powerUpGagePoint >= m_powerUpLimitPoint)
		{
			// 規定値を越えた場合、パワーアップGUIを表示
			m_powerUpGuiManager.Display();
		}
	}

	/// <summary>
	/// プレイヤーが取得したポイントを返す
	/// </summary>
	/// <returns></returns>
	public int GetPlayerItemPoint()
	{
		return m_playerItemPoint;
	}

	/// <summary>
	/// プレイヤーが倒した敵の数を返す
	/// </summary>
	/// <returns></returns>
	public int GetPlayerDefeatEnemyNum()
	{
		return m_playerDefeatEnemyNum;
	}

	/// <summary>
	/// playerStateを取得する
	/// </summary>
	/// <returns></returns>
	public int GetPlayerState()
	{
		return m_playerState;
	}

	/// <summary>
	/// プレイヤーを無敵モードにする
	/// </summary>
	public void SetPlayerStatePOWERUP()
	{
		// プレイヤーステートを変更
		m_playerState = PLAYER_STATE_POWERUP;
		// GUI上に反映
		m_powerUpGuiManager.OnClick_PowerUpButton();
	}

	/// <summary>
	/// パワーアップしていられる時間を計算する
	/// </summary>
	/// <param name="powerUpPoint"></param>
	private double CalculatePowerUpTime(double powerUpPoint)
	{
		return powerUpPoint * m_powerupTimeRate;
	}
}
