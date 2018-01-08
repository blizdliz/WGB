using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全てのGUIのマネージャークラス
/// </summary>
public class Scene_01_GUIManager : MonoBehaviour
{
	private AllSceneManagementBehavior m_allSceneManager;

	[SerializeField]
	private Scene_01_NormalGUIManager m_normalGUIManager;
	[SerializeField]
	private Scene_01_GameOverGUIManager m_gameOverGUIManager;
	[SerializeField]
	private Scene_01_ResultGUIManager m_resultGUIManager;
	[SerializeField]
	private Scene_01_DebugGUIManager m_debugGUIManager;

	// Use this for initialization
	void Start ()
	{
		GameObject allSceneManager = GameObject.FindGameObjectWithTag("AllSceneManager");
		bool isDebug = false;
		if (allSceneManager != null)
		{
			m_allSceneManager = allSceneManager.GetComponent<AllSceneManagementBehavior>();
			isDebug = m_allSceneManager.m_isDebug;
		}
		m_normalGUIManager.InitGUI();
		m_gameOverGUIManager.InitGUI();
		m_resultGUIManager.InitGUI();
		m_debugGUIManager.InitGUI(isDebug);
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
	public void DisplayGameOverGUI(string text)
	{
		if (m_allSceneManager != null)
		{
			m_allSceneManager.StopBGM();
		}
		m_gameOverGUIManager.DisplayGUI(text);
	}

	/// <summary>
	/// リザルトのGUIを表示する
	/// </summary>
	public void DisplayResultGUI()
	{
		if (m_allSceneManager != null)
		{
			m_allSceneManager.PlayResultBGM();
		}
		m_gameOverGUIManager.HideGUI();
		m_resultGUIManager.DisplayGUI();
	}

	/// <summary>
	/// タイトル画面へ戻る処理
	/// </summary>
	public void SceneChange_Scene00_title()
	{
		if (m_allSceneManager != null)
		{
			m_allSceneManager.SceneChange_Scene00_title();
		}
	}
}
