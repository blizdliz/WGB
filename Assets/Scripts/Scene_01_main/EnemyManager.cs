using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	private Animator m_animator;
	private EnemyMoveManager m_enemyMoveManager;
	// 敵の表示状態
	private bool m_isAppear;
	// 敵の生存状態
	private bool m_isDefeated;

	[SerializeField]
	// アイテムの価値
	private int m_itemValue = 100;

	void Start()
	{
		m_animator = GetComponentInChildren<Animator>();
		m_enemyMoveManager = this.gameObject.GetComponent<EnemyMoveManager>();
		m_enemyMoveManager.Init();
		InitEnemy();
	}

	/// <summary>
	/// アイテムを初期化する
	/// </summary>
	private void InitEnemy()
	{
		m_isAppear = false;
		m_isDefeated = false;
		Disappear();
	}

	/// <summary>
	/// 敵の表示状態を返す
	/// </summary>
	/// <returns></returns>
	public bool GetIsAppered()
	{
		return m_isAppear;
	}

	/// <summary>
	/// 敵の獲得状態を返す
	/// </summary>
	/// <returns></returns>
	public bool GetIsDefeated()
	{
		return m_isDefeated;
	}

	/// <summary>
	/// 敵のアイテムとしての価値を返す
	/// </summary>
	/// <returns>The item value.</returns>
	public int GetItemValue()
	{
		return m_itemValue;
	}

	/// <summary>
	/// 敵クリック時の操作
	/// </summary>
	public void OnClickAction()
	{
		Debug.Log("敵をクリック");
		m_isDefeated = true;
		m_enemyMoveManager.SetIsDefeated(m_isDefeated);
		StartCoroutine(StartEnemyDefeatedSequence());
	}

	/// <summary>
	/// 敵がやられてから消滅までの一連の流れ
	/// </summary>
	/// <returns></returns>
	private IEnumerator StartEnemyDefeatedSequence()
	{
		m_animator.SetTrigger("Idle");
		yield return new WaitForSeconds(1.0f);
		Destroy(this.gameObject);
	}

	/// <summary>
	/// 敵表示処理
	/// </summary>
	public void Appear()
	{
		// 敵の表示状態を非表示にセットする
		m_isAppear = true;
		// 子供のオブジェクトをアクティブにする
		foreach (Transform child in transform)
		{
			//child is your child transform
			child.gameObject.SetActive(true);
		}
	}

	/// <summary>
	/// 敵非表示処理
	/// </summary>
	public void Disappear()
	{
		// 敵の表示状態を非表示にセットする
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
