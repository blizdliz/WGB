using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllSceneManagementBehavior : MonoBehaviour
{
	[SerializeField]
	private string m_Scene00_title_name = "";
	[SerializeField]
	private string m_Scene01_main_name = "";
	[SerializeField]
	private Image m_bgPanel;

	void Start()
	{
		// 破棄しないように設定する
		DontDestroyOnLoad(this);
		m_bgPanel.GetComponent<CanvasGroup>().alpha = 0.0f;
		m_bgPanel.gameObject.SetActive(false);
	}

	/// <summary>
	/// シーン0に遷移する
	/// </summary>
	public void SceneChange_Scene00_title()
	{
		StartCoroutine(_SceneChangeSequence(m_Scene00_title_name));
	}

	/// <summary>
	/// シーン1に遷移する
	/// </summary>
	public void SceneChange_Scene01_main()
	{
		StartCoroutine(_SceneChangeSequence(m_Scene01_main_name));
	}

	/// <summary>
	/// 順を追ってシーン遷移処理を実行する
	/// </summary>
	/// <returns></returns>
	private IEnumerator _SceneChangeSequence(string sceneName)
	{
		m_bgPanel.gameObject.SetActive(true);
		m_bgPanel.GetComponent<CanvasGroup>().alpha = 1.0f;
		yield return new WaitForSeconds(1.0f);

		// 非同期でシーン遷移を行う
		// シーンの読み込みが終わるまで
		yield return SceneManager.LoadSceneAsync(sceneName);

		m_bgPanel.GetComponent<CanvasGroup>().alpha = 0.0f;
		m_bgPanel.gameObject.SetActive(false);
	}
}
