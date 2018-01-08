using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllSceneDialogManager : MonoBehaviour
{
	[SerializeField]
	private GameObject m_bgImage;
	[SerializeField]
	private AllSceneDialogWithImageManager m_dialogWithImage;

	public void Init ()
	{
		// ダイアログの背景を非表示に設定
		HideBG();
		// 画像付きダイアログを初期化
		m_dialogWithImage.Init();
	}

	/// <summary>
	///  ダイアログの背景を表示
	/// </summary>
	private void DisplayBG()
	{
		m_bgImage.gameObject.SetActive(true);
		StartCoroutine(_DisplayBG());
	}
	private IEnumerator _DisplayBG()
	{
		CanvasGroup bg = m_bgImage.GetComponent<CanvasGroup>();
		bg.alpha = 0;
		// 背景
		while (bg.alpha < 1.0f)
		{
			bg.alpha += 0.1f;
			yield return new WaitForSeconds(0.01f);
		}
	}

	/// <summary>
	///  ダイアログの背景を非表示
	/// </summary>
	private void HideBG()
	{
		StartCoroutine(_HideBG());
		m_bgImage.gameObject.SetActive(false);
	}
	private IEnumerator _HideBG()
	{
		CanvasGroup bg = m_bgImage.GetComponent<CanvasGroup>();
		bg.alpha = 1;
		// 背景
		while (bg.alpha > 0.0f)
		{
			bg.alpha -= 0.1f;
			yield return new WaitForSeconds(0.01f);
		}
	}

	/// <summary>
	/// 画像付きダイアログを表示する
	/// </summary>
	/// <param name="title"></param>
	/// <param name="text"></param>
	/// <param name="image"></param>
	public void Display_DialogWithImage(string title, string text, Sprite image)
	{
		DisplayBG();
		m_dialogWithImage.Display(title, text, image);
	}
	/// <summary>
	/// 画像付きダイアログを非表示にする
	/// </summary>
	public void Hide_DialogWithImage()
	{
		m_dialogWithImage.Hide();
		HideBG();
	}
}
