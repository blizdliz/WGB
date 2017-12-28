using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour
{
	private Animator m_animator;

	[SerializeField]
	private int m_itemValue = 1;

	void Start()
	{
		m_animator = GetComponentInChildren<Animator>();
	}

	/// <summary>
	/// アイテムの価値を返す
	/// </summary>
	/// <returns>The item value.</returns>
	public int GetItemValue(){
		return m_itemValue;
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
