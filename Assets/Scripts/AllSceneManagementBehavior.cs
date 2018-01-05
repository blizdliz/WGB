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
	private GameObject m_bgMask;
	private RectTransform m_bgMaskTransform;
	[SerializeField]
	private Image m_bgPanel;

	[SerializeField]
	private float m_maxMaskScale = 1.0f;
	[SerializeField]
	// ループ中の1回でマスクを拡縮させる量（マスク拡縮アニメーションのスピード調整）
	private float m_maskAnimSpeed = 0.1f;

	void Start()
	{
		// 破棄しないように設定する
		DontDestroyOnLoad(this);
		m_bgPanel.GetComponent<CanvasGroup>().alpha = 1.0f;
		m_bgMaskTransform = m_bgMask.GetComponent<RectTransform>();
		m_bgMaskTransform.localScale = new Vector3(0f, 0f, 1f);
	}

	void Update()
	{
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
		// マスク拡大処理、全画面を覆う
		while (m_bgMaskTransform.localScale.x <= m_maxMaskScale)
		{
			m_bgMaskTransform.localScale += new Vector3(m_maskAnimSpeed, m_maskAnimSpeed, 0f);
			yield return new WaitForSeconds(0.01f);
		}

		yield return new WaitForSeconds(1.0f);

		// 非同期でシーン遷移を行う
		// シーンの読み込みが終わるまで
		yield return SceneManager.LoadSceneAsync(sceneName);

		// マスク縮小処理
		while (m_bgMaskTransform.localScale.x > 0)
		{
			m_bgMaskTransform.localScale += new Vector3(-m_maskAnimSpeed, -m_maskAnimSpeed, 0f);
			yield return new WaitForSeconds(0.01f);
		}
	}
}
