using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoShared;

public class Scene_01_DebugGUIManager : MonoBehaviour
{
	[SerializeField]
	private LocationManager m_locationManager;

	/// <summary>
	/// 初期化する
	/// </summary>
	/// <param name="isDebug"></param>
	public void InitGUI (bool isDebug)
	{
		m_locationManager.m_isDebug = isDebug;
		this.gameObject.SetActive(isDebug);
	}

	/// <summary>
	/// 北方向へ進むボタンを押下している間呼ばれ続ける
	/// </summary>
	public void OnPointerDown_NorthButton()
	{
		m_locationManager.m_isNorthMove = true;
	}

	/// <summary>
	/// 北方向へ進むボタンを離した際に呼ばれる
	/// </summary>
	public void OnPointerUp_NorthButton()
	{
		m_locationManager.m_isNorthMove = false;
	}

	/// <summary>
	/// 南方向へ進むボタンを押下している間呼ばれ続ける
	/// </summary>
	public void OnPointerDown_SouthButton()
	{
		m_locationManager.m_isSouthMove = true;
	}

	/// <summary>
	/// 南方向へ進むボタンを離した際に呼ばれる
	/// </summary>
	public void OnPointerUp_SouthButton()
	{
		m_locationManager.m_isSouthMove = false;
	}

	/// <summary>
	/// 西方向へ進むボタンを押下している間呼ばれ続ける
	/// </summary>
	public void OnPointerDown_WestButton()
	{
		m_locationManager.m_isWestMove = true;
	}

	/// <summary>
	/// 西方向へ進むボタンを離した際に呼ばれる
	/// </summary>
	public void OnPointerUp_WestButton()
	{
		m_locationManager.m_isWestMove = false;
	}

	/// <summary>
	/// 東方向へ進むボタンを押下している間呼ばれ続ける
	/// </summary>
	public void OnPointerDown_EastButton()
	{
		m_locationManager.m_isEastMove = true;
	}

	/// <summary>
	/// 東方向へ進むボタンを離した際に呼ばれる
	/// </summary>
	public void OnPointerUp_EastButton()
	{
		m_locationManager.m_isEastMove = false;
	}
}
