using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scene_01_GameOverGUIManager : MonoBehaviour
{
	[SerializeField]
	private GameObject m_gameOverText;
	[SerializeField]
	private GameObject m_bgImage;
	[SerializeField]
	private GameObject m_okButton;

	/// <summary>
	/// GUIを初期化する
	/// </summary>
	public void InitGUI()
	{
		this.gameObject.SetActive(false);
	}

	/// <summary>
	/// GUIを表示するときの処理
	/// </summary>
	public void DisplayGUI(string text)
	{
		this.gameObject.SetActive(true);
		StartCoroutine(_DisplayGUISequence(text));
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	private IEnumerator _DisplayGUISequence(string text)
	{
		CanvasGroup bgCanvasGroup = m_bgImage.GetComponent<CanvasGroup>();

		RectTransform textTransform = m_gameOverText.GetComponent<RectTransform>();
		TMP_Text gameOverText = m_gameOverText.GetComponent<TMP_Text>();
		gameOverText.text = text;

		CanvasGroup buttonCanvasGroup = m_okButton.GetComponent<CanvasGroup>();

		bgCanvasGroup.alpha = 0.0f;

		textTransform.localPosition = new Vector3(0f, 720f, 0f);

		buttonCanvasGroup.alpha = 0.0f;

		while (bgCanvasGroup.alpha < 1.0f)
		{
			bgCanvasGroup.alpha += 0.1f;
			buttonCanvasGroup.alpha += 0.1f;
			yield return new WaitForSeconds(0.01f);
		}

		while (textTransform.localPosition.y > 0)
		{
			textTransform.localPosition += new Vector3(0f, -30f, 0f);
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
