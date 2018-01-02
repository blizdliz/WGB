using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene_00_GUIManager : MonoBehaviour
{
	private AllSceneManagementBehavior m_allSceneManager;

	[SerializeField]
	private Button m_PlaySoloButton;

	public void Init(AllSceneManagementBehavior allSceneManager)
	{
		m_allSceneManager = allSceneManager;
		// ボタン押下時のイベントを登録
		m_PlaySoloButton.onClick.AddListener(OnClick_PlaySoloButton);
	}

	/// <summary>
	/// 一人で遊ぶボタン押下時の処理
	/// </summary>
	public void OnClick_PlaySoloButton()
	{
		m_allSceneManager.SceneChange_Scene01_main();
	}
}
