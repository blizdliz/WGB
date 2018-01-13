using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllSceneManagementBehavior : MonoBehaviour
{
	[SerializeField]
	// 開発者向けビルドかどうか
	private bool m_isDeveloperBuild = false;
	// デバッグ管理用変数
	public bool m_isDebug = false;
	[SerializeField]
	private Toggle m_debugToggle;

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

	// シーンごとのBGMを管理するクラス
	private AllSceneAudioManager m_allSceneAudioManager;
	[SerializeField]
	// ダイアログを管理するクラス
	private AllSceneDialogManager m_allSceneDialogManager;
	// 広告を管理するクラス
	private UnityAdsManager m_adsManager;

	void Start()
	{
		// 破棄しないように設定する
		DontDestroyOnLoad(this);

		// デバッグトグルの初期状態はオフにセット
		m_debugToggle.isOn = false;
		// デバッグトグル表示処理
		m_debugToggle.gameObject.SetActive(m_isDeveloperBuild);

		m_allSceneAudioManager = this.gameObject.GetComponent<AllSceneAudioManager>();
		m_allSceneAudioManager.Init();
		m_allSceneAudioManager.PlayTitleBGM();

		m_allSceneDialogManager.Init();

		m_bgPanel.GetComponent<CanvasGroup>().alpha = 1.0f;
		m_bgMaskTransform = m_bgMask.GetComponent<RectTransform>();
		m_bgMaskTransform.localScale = new Vector3(0f, 0f, 1f);
		// 広告管理クラス
		m_adsManager = this.gameObject.GetComponent<UnityAdsManager>();
		m_adsManager.Init();
	}

	void Update()
	{
	}

	/// <summary>
	/// デバッグトグルの値変更時の処理
	/// </summary>
	public void DebugToggle_OnValueChange()
	{
		m_isDebug = m_debugToggle.isOn;
	}

	/// <summary>
	/// シーン0に遷移する
	/// </summary>
	public void SceneChange_Scene00_title()
	{
		StartCoroutine(_SceneChangeSequence(m_Scene00_title_name));

		// デバッグトグル表示処理
		m_debugToggle.gameObject.SetActive(m_isDeveloperBuild);
	}

	/// <summary>
	/// シーン1に遷移する
	/// </summary>
	public void SceneChange_Scene01_main()
	{
		m_adsManager.ShowAd();
		StartCoroutine(_SceneChangeSequence(m_Scene01_main_name));

		// デバッグトグル表示処理
		m_debugToggle.gameObject.SetActive(false);
	}

	/// <summary>
	/// 順を追ってシーン遷移処理を実行する
	/// </summary>
	/// <returns></returns>
	private IEnumerator _SceneChangeSequence(string sceneName)
	{
		// BGMを止める
		StopBGM();

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

		// BGMを再生
		if (sceneName == m_Scene00_title_name)
		{
			PlayTitleBGM();
		}
		else if (sceneName == m_Scene01_main_name)
		{
			PlayMainBGM();
		}

		// マスク縮小処理
		while (m_bgMaskTransform.localScale.x > 0)
		{
			m_bgMaskTransform.localScale += new Vector3(-m_maskAnimSpeed, -m_maskAnimSpeed, 0f);
			yield return new WaitForSeconds(0.01f);
		}
	}

	public void StopBGM()
	{
		// BGMを止める
		m_allSceneAudioManager.StopBGM();
	}

	public void PlayTitleBGM()
	{
		m_allSceneAudioManager.PlayTitleBGM();
	}

	public void PlayMainBGM()
	{
		m_allSceneAudioManager.PlayMainBGM();
	}

	public void PlayResultBGM()
	{
		m_allSceneAudioManager.PlayResultBGM();
	}

	// ダイアログに関わる処理

	/// <summary>
	/// 画像付きダイアログを表示する
	/// </summary>
	/// <param name="title"></param>
	/// <param name="text"></param>
	/// <param name="image"></param>
	public void Display_DialogWithImage(string title, string text, Sprite image)
	{
		m_allSceneDialogManager.Display_DialogWithImage(title, text, image);
	}
}
