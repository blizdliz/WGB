using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scene_01_ResultGUIManager : MonoBehaviour
{
	[SerializeField]
	private TMP_Text m_aqcuredItemScoreText;
	[SerializeField]
	private TMP_Text m_defeatedEnemyScoreText;

	// スコア取得のためプレイヤーアバターステート管理クラスをインスタンス化
	private AvaterStateManager m_avaterStatemanager;

	/// <summary>
	/// GUIを初期化する
	/// </summary>
	public void InitGUI()
	{
		m_avaterStatemanager = GameObject.Find("Avatar").GetComponent<AvaterStateManager>();
		this.gameObject.SetActive(false);
	}

	/// <summary>
	/// GUIを表示するときの処理
	/// </summary>
	public void DisplayGUI()
	{
		this.gameObject.SetActive(true);
		StartCoroutine(_DisplayGUI());
	}
	/// <summary>
	/// GUIを表示する
	/// </summary>
	/// <returns></returns>
	private IEnumerator _DisplayGUI()
	{
		yield return null;
		
		int itemScore = m_avaterStatemanager.GetPlayerItemPoint();
		int enemyScore = 0;
		m_aqcuredItemScoreText.text = itemScore.ToString();
		m_defeatedEnemyScoreText.text = enemyScore.ToString();
	}

	/// <summary>
	/// GUIを非表示にする際の処理
	/// </summary>
	public void HideGUI()
	{
		this.gameObject.SetActive(false);
	}
}
