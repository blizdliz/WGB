using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scene_01_PowerUPGUIManager : MonoBehaviour
{
	[SerializeField]
	private Button m_powerUpButton;
	[SerializeField]
	private TMP_Text m_powerUPTimeText;

	public void initGUI()
	{
		this.gameObject.SetActive(false);
		m_powerUpButton.gameObject.SetActive(false);
		m_powerUPTimeText.text = "";
		m_powerUPTimeText.gameObject.SetActive(false);
	}
	
	public void Display()
	{
		this.gameObject.SetActive(true);
		m_powerUpButton.gameObject.SetActive(true);
	}

	public void OnClick_PowerUpButton()
	{
		m_powerUpButton.gameObject.SetActive(false);
		m_powerUPTimeText.gameObject.SetActive(true);
	}

	public void HideAll()
	{
		this.gameObject.SetActive(false);
		m_powerUpButton.gameObject.SetActive(false);
		m_powerUPTimeText.text = "";
		m_powerUPTimeText.gameObject.SetActive(false);
	}

	/// <summary>
	/// 残り無敵時間をセットしてGUI上に反映する
	/// </summary>
	/// <param name="timer"></param>
	public void SetCurrentPowerUpTime(double time)
	{
		// 時間
		string tt = Mathf.FloorToInt((float)(time / 3600)).ToString().PadLeft(2, '0');
		// 分
		string mm = Mathf.FloorToInt((float)(time % 3600 / 60)).ToString().PadLeft(2, '0');
		// 秒
		string ss = Mathf.FloorToInt((float)(time % 360 % 60)).ToString().PadLeft(2, '0');
		StringBuilder ttmmss = new StringBuilder();
		ttmmss.Append(tt).Append(":").Append(mm).Append(":").Append(ss);

		m_powerUPTimeText.text = ttmmss.ToString();
	}
}
