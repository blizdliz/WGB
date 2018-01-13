using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class UnityAdsManager : MonoBehaviour
{
	[SerializeField]
	private string m_appID_android;
	[SerializeField]
	private string m_appID_ios;

	private Action m_action;

	public void Init()
	{
		if (Advertisement.isSupported)
		{
			Debug.Log("Platform support Ads.");
			// 初期化
			string appId = "";
#if UNITY_ANDROID
			appId = m_appID_android;
#elif UNITY_IOS
			appId = m_appID_ios;
#endif
			Advertisement.Initialize(appId);
		}
		else
		{
			Debug.Log("Platform not supported");
		}
	}

	/// <summary>
	/// 広告を表示する
	/// </summary>
	public void ShowAd(Action action)
	{
		if (action != null)
		{
			m_action = action;
		}

		// 広告の準備ができているか確認
		if ( Advertisement.IsReady("rewardedVideo") )
		{
			Debug.Log("広告を表示");

			ShowOptions options = new ShowOptions();
			options.resultCallback = HandleShowResult;

			// 準備ができていたら広告を再生
			Advertisement.Show("rewardedVideo", options);
		}
		else
		{
			if (m_action != null)
			{
				// アクションを実行
				m_action();
				m_action = null;
			}
		}
	}

	void HandleShowResult(ShowResult result)
	{
		if (result == ShowResult.Finished)
		{
			Debug.Log("Video completed - Offer a reward to the player");
		}
		else if (result == ShowResult.Skipped)
		{
			Debug.LogWarning("Video was skipped - Do NOT reward the player");
		}
		else if (result == ShowResult.Failed)
		{
			Debug.LogError("Video failed to show");
		}

		if (m_action != null)
		{
			// アクションを実行
			m_action();
			m_action = null;
		}
	}
}
