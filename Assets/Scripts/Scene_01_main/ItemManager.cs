using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour
{
	private Animator m_animator;

	// アイテムの表示状態
	private bool m_isAppear;
	// アイテムの獲得状態
	private bool m_isAcquired;

	[SerializeField]
	// アイテムの価値
	private int m_itemValue = 1;

	void Start()
	{
		InitItem();
	}

	/// <summary>
	/// アイテムを初期化する
	/// </summary>
	private void InitItem()
	{
		m_animator = GetComponentInChildren<Animator>();
		m_isAppear = false;
		m_isAcquired = false;
		Disappear();
	}

	/// <summary>
	/// アイテムの表示状態を返す
	/// </summary>
	/// <returns></returns>
	public bool GetIsAppered()
	{
		return m_isAppear;
	}

	/// <summary>
	/// アイテムの獲得状態を返す
	/// </summary>
	/// <returns></returns>
	public bool GetIsAcquired()
	{
		return m_isAcquired;
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
		m_isAcquired = true;
		StartCoroutine(StartItemAcquiredSequence());
	}

	/// <summary>
	/// アイテム獲得から消滅までの一連の流れ
	/// </summary>
	/// <returns></returns>
	private IEnumerator StartItemAcquiredSequence()
	{
		m_animator.SetTrigger("ItemGet");
		yield return new WaitForSeconds(1.0f);
		Destroy(this.gameObject);
	}

	/// <summary>
	/// アイテム出現処理
	/// </summary>
	public void Appear()
	{
		// アイテムの表示状態を非表示にセットする
		m_isAppear = true;
		// 子供のオブジェクトをアクティブにする
		foreach (Transform child in transform)
		{
			//child is your child transform
			child.gameObject.SetActive(true);
		}
	}

	/// <summary>
	/// アイテム非表示処理
	/// </summary>
	public void Disappear()
	{
		// アイテムの表示状態を非表示にセットする
		m_isAppear = false;
		// 子供のオブジェクトを非アクティブにする
		foreach (Transform child in transform)
		{
			//child is your child transform
			child.gameObject.SetActive(false);
			// TODO:とりあえず影だけ表示させておくためにreturnする
			return;
		}
	}
}
