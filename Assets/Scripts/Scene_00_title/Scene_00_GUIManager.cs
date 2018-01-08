using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_00_GUIManager : MonoBehaviour
{
	private AllSceneManagementBehavior m_allSceneManager;

	[SerializeField]
	private GameObject m_titleImage;
	[SerializeField]
	private GameObject m_buttonsObj;
	[SerializeField]
	private Button m_PlaySoloButton;
	[SerializeField]
	private Button m_howToPlayButton;

	[SerializeField]
	private Sprite m_HowToPlayImage;

	public void Init(AllSceneManagementBehavior allSceneManager)
	{
		m_allSceneManager = allSceneManager;
		// タイトル画面表示処理
		StartCoroutine(_StartGUISequence());
		// ボタン押下時のイベントを登録
		m_PlaySoloButton.onClick.AddListener(OnClick_PlaySoloButton);
		//m_howToPlayButton.onClick.AddListener(OnClick_HowToPlayButton(null));
		m_howToPlayButton.onClick.AddListener( () => OnClick_HowToPlayButton(m_HowToPlayImage));
	}

	/// <summary>
	/// 画面表示時のGUIのアニメーションとか
	/// </summary>
	/// <returns></returns>
	private IEnumerator _StartGUISequence()
	{
		CanvasGroup titleCanvasGroup = m_titleImage.GetComponent<CanvasGroup>();
		RectTransform titleTransform = m_titleImage.GetComponent<RectTransform>();

		CanvasGroup buttonsCanvasGroup = m_buttonsObj.GetComponent<CanvasGroup>();
		RectTransform buttonsTransform = m_buttonsObj.GetComponent<RectTransform>();
		
		titleCanvasGroup.alpha = 0.0f;
		titleTransform.localScale = new Vector3(1.5f, 1.5f, 1f);

		buttonsCanvasGroup.alpha = 0.0f;
		buttonsTransform.localScale = new Vector3(1.5f, 1.5f, 1f);

		while (titleCanvasGroup.alpha < 1.0f)
		{
			titleCanvasGroup.alpha += 0.1f;
			titleTransform.localScale += new Vector3(-0.05f, -0.05f, 0f);
			yield return new WaitForSeconds(0.01f);
		}

		while (buttonsCanvasGroup.alpha < 1.0f)
		{
			buttonsCanvasGroup.alpha += 0.1f;
			buttonsTransform.localScale += new Vector3(-0.05f, -0.05f, 0f);
			yield return new WaitForSeconds(0.01f);
		}

		yield return null;
	}

	/// <summary>
	/// 一人で遊ぶボタン押下時の処理
	/// </summary>
	public void OnClick_PlaySoloButton()
	{
		m_allSceneManager.SceneChange_Scene01_main();
	}

	/// <summary>
	/// 遊び方ボタン押下時の処理
	/// </summary>
	public void OnClick_HowToPlayButton(Sprite image)
	{
		m_allSceneManager.Display_DialogWithImage("遊び方", "制限時間以内にコインを全て集めよう。時間切れになるか、敵に触れるとゲームオーバーだ。", image);
	}
}
