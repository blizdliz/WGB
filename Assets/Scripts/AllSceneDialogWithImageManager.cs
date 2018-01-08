using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AllSceneDialogWithImageManager : MonoBehaviour
{
	[SerializeField]
	private TMP_Text m_titleText;
	[SerializeField]
	private Image m_image;
	[SerializeField]
	private TMP_Text m_text;

	private Animator m_animator;

	public void Init()
	{
		m_titleText.text = "";
		m_image.sprite = null;
		m_text.text = "";
		m_animator = this.gameObject.GetComponent<Animator>();
		this.gameObject.SetActive(false);
	}

	public void Display(string title, string text, Sprite image)
	{
		this.gameObject.SetActive(true);
		StartCoroutine(_Display(title, text, image));
	}

	private IEnumerator _Display(string title, string text, Sprite image)
	{
		m_titleText.text = title;
		m_image.sprite = image;
		m_text.text = text;
		m_animator.SetTrigger("Display");
		yield return null; 
	}

	public void Hide()
	{
		StartCoroutine(_Hide());
	}

	private IEnumerator _Hide()
	{
		m_animator.SetTrigger("Hide");
		yield return new WaitForSeconds(0.3f);
		this.gameObject.SetActive(false);
	}
}
