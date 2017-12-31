using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 通常時のGUIのマネージャークラス
/// </summary>
public class Scene_01_NormalGUIManager : MonoBehaviour
{
	[SerializeField]
	// 残り時間テキスト
	private TMP_Text m_currentTimeText;
	[SerializeField]
	// 所持アイテム数テキスト
	private TMP_Text m_aqcuredItemNumText;
	[SerializeField]
	// フィールド上のアイテム数テキスト
	private TMP_Text m_currentItemNumText;
	[SerializeField]
	// フィールド上の敵の数テキスト
	private TMP_Text m_currentEnemyNumText;


	/// <summary>
	/// GUIを初期化する
	/// </summary>
	public void InitGUI()
	{
		m_currentTimeText.text = "Loading";
		m_aqcuredItemNumText.text = "Loading";
		m_currentItemNumText.text = "Loading";
		m_currentEnemyNumText.text = "Loading";
	}

	/// <summary>
	/// 残り時間をセットしてGUI上に反映する
	/// </summary>
	/// <param name="timer"></param>
	public void SetCurrentTime(double time)
	{
		// 時間
		string tt = Mathf.FloorToInt((float)(time / 360)).ToString().PadLeft(2,'0');
		// 分
		string mm = Mathf.FloorToInt((float)(time % 360 / 60)).ToString().PadLeft(2, '0');
		// 秒
		string ss = Mathf.FloorToInt((float)(time % 360 % 60)).ToString().PadLeft(2, '0');
		StringBuilder ttmmss = new StringBuilder();
		ttmmss.Append(tt).Append(":").Append(mm).Append(":").Append(ss);

		m_currentTimeText.text = ttmmss.ToString();
	}

	/// <summary>
	/// 所持アイテム数をセットしてGUI上に反映する
	/// </summary>
	/// <param name="num"></param>
	public void SetAcquiredItemNum(int num)
	{
		m_aqcuredItemNumText.text = "×" + num.ToString();
	}

	/// <summary>
	/// フィールド上のアイテム数を表示する
	/// </summary>
	/// <param name="num"></param>
	public void SetCurrentItemNum(int num)
	{
		m_currentItemNumText.text = "×" + num.ToString();
	}

	/// <summary>
	/// フィールド上の敵の数を表示する
	/// </summary>
	/// <param name="num"></param>
	public void SetCurrentEnemyNum(int num)
	{
		m_currentEnemyNumText.text = "×" + num.ToString();
	}
}
