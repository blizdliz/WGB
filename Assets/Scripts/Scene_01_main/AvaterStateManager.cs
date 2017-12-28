﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvaterStateManager : MonoBehaviour
{
	// プレイヤーの状態定数
	public const int PLAYER_STATE_NORMAL  = 0;
	public const int PLAYER_STATE_DEATH   = 1;
	public const int PLAYER_STATE_POWERUP = 2;

	[HeaderAttribute("DO NOT Edit the following parameters.")]
	[SerializeField]
	// プレイヤーの状態
	private int m_playerState;
	[SerializeField]
	// プレイヤーのアイテム獲得ポイント
	private int m_playerItemPoint;

	// Use this for initialization
	void Start ()
	{
		InitPlayerState ();
	}

	/// <summary>
	/// プレイヤーの状態を初期化する
	/// </summary>
	private void InitPlayerState()
	{
		m_playerState = PLAYER_STATE_NORMAL;
		m_playerItemPoint = 0;
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy"){
			// 敵に接触
			Debug.Log("敵に接触");
			m_playerState = PLAYER_STATE_DEATH;
		}
		else if (other.gameObject.tag == "Item"){
			// アイテムに接触
			Debug.Log("アイテムに接触");
			ItemManager itemManager = other.GetComponent<ItemManager> ();
			m_playerItemPoint += itemManager.GetItemValue();
		}
	}
}
