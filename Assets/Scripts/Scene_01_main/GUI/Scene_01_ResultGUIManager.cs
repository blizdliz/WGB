using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Scene_01_ResultGUIManager : MonoBehaviour
{
	[SerializeField]
	private TMP_Text m_aqcuredItemScoreText;
	[SerializeField]
	private TMP_Text m_defeatedEnemyScoreText;

	[SerializeField]
	private GameObject m_bg;
	[SerializeField]
	private GameObject m_bgTop;
	[SerializeField]
	private GameObject m_bgBottom;

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
		// 初期化
		// スコア文字
		m_aqcuredItemScoreText.text = "...";
		m_defeatedEnemyScoreText.text = "...";
		// 背景
		CanvasGroup bgCanvasGroup = m_bg.GetComponent<CanvasGroup>();
		bgCanvasGroup.alpha = 0.0f;
		// アニメーション
		// 背景
		while (bgCanvasGroup.alpha < 1.0f)
		{
			bgCanvasGroup.alpha += 0.1f;
			yield return new WaitForSeconds(0.01f);
		}
		yield return new WaitForSeconds(0.1f);
		// スコア表示
		int itemScore = m_avaterStatemanager.GetPlayerItemPoint();
		int enemyScore = m_avaterStatemanager.GetPlayerDefeatEnemyNum();

		int currentScore = 0;
		m_aqcuredItemScoreText.text = currentScore.ToString();
		while (currentScore < itemScore)
		{
			currentScore += 1;
			m_aqcuredItemScoreText.text = currentScore.ToString();
			yield return new WaitForSeconds(0.01f);
		}
		currentScore = 0;
		m_defeatedEnemyScoreText.text = currentScore.ToString();
		while (currentScore < enemyScore)
		{
			currentScore += 1;
			m_defeatedEnemyScoreText.text = currentScore.ToString();
			yield return new WaitForSeconds(0.01f);
		}

		yield return null;
	}

	/// <summary>
	/// GUIを非表示にする際の処理
	/// </summary>
	public void HideGUI()
	{
		this.gameObject.SetActive(false);
	}
}
