using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全てのGUIのマネージャークラス
/// </summary>
public class Scene_01_GUIManager : MonoBehaviour
{
	[SerializeField]
	private Scene_01_NormalGUIManager m_normalGUIManager;
	[SerializeField]
	private Scene_01_GameOverGUIManager m_gameOverGUIManager;
	[SerializeField]
	private Scene_01_ResultGUIManager m_resultGUIManager;

	// Use this for initialization
	void Start ()
	{
		m_normalGUIManager.InitGUI();
		m_gameOverGUIManager.InitGUI();
		m_resultGUIManager.InitGUI();
	}

	/// <summary>
	/// 現在の残り時間をセットする
	/// </summary>
	/// <param name="time"></param>
	public void SetCurrentTime(double time)
	{
		m_normalGUIManager.SetCurrentTime(time);
	}

	/// <summary>
	/// 所持アイテム数をセットする
	/// </summary>
	/// <param name="num"></param>
	public void SetAcquiredItemNum(int num)
	{
		m_normalGUIManager.SetAcquiredItemNum(num);
	}

	/// <summary>
	/// フィールド上のアイテム数をセットする
	/// </summary>
	/// <param name="num"></param>
	public void SetCurrentItemNum(int num)
	{
		m_normalGUIManager.SetCurrentItemNum(num);
	}

	/// <summary>
	/// フィールド上の敵の数をセットする
	/// </summary>
	/// <param name="num"></param>
	public void SetCurrentEnemyNum(int num)
	{
		m_normalGUIManager.SetCurrentEnemyNum(num);
	}

	/// <summary>
	/// ゲームオーバーのGUIを表示する
	/// </summary>
	public void DisplayGameOverGUI()
	{
		m_gameOverGUIManager.DisplayGUI();
	}

	/// <summary>
	/// リザルトのGUIを表示する
	/// </summary>
	public void DisplayResultGUI()
	{
		m_gameOverGUIManager.HideGUI();
		m_resultGUIManager.DisplayGUI();
	}
}
