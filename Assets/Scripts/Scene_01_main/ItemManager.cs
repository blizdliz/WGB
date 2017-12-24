using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour
{
	private Animator m_animator;

	void Start()
	{
		m_animator = GetComponentInChildren<Animator>();
	}

	/// <summary>
	/// アイテムクリック時の操作
	/// </summary>
	public void OnClickAction()
	{
		Debug.Log("Itemをクリック");
		m_animator.SetTrigger("ItemGet");
	}
}
