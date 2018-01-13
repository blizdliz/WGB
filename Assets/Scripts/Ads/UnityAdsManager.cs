using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsManager : MonoBehaviour
{
	[SerializeField]
	private string m_adsGameID;

	public void Init()
	{
		if (Advertisement.isSupported)
		{
			Debug.Log("Platform support Ads.");
			// 初期化
			Advertisement.Initialize(m_adsGameID);
		}
		else
		{
			Debug.Log("Platform not supported");
		}
	}

	/// <summary>
	/// 広告を表示する
	/// </summary>
	public void ShowAd()
	{
		Debug.Log("広告を表示");
		// 広告の準備ができているか確認
		if ( Advertisement.IsReady() )
		{
			Debug.Log("広告を表示");
			// 準備ができていたら広告を再生
			Advertisement.Show();
		}
	}
}
