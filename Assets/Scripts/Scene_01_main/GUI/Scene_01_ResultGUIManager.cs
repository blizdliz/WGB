using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_01_ResultGUIManager : MonoBehaviour
{
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
	public void DisplayGUI()
	{
		this.gameObject.SetActive(true);
	}

	/// <summary>
	/// GUIを非表示にする際の処理
	/// </summary>
	public void HideGUI()
	{
		this.gameObject.SetActive(false);
	}
}
