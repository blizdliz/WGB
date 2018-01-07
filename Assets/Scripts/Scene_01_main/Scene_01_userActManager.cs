using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_01_userActManager : MonoBehaviour
{
	private Ray m_ray;
	private RaycastHit m_raycastHit;

	private AvaterStateManager m_avaterStateManager;
	private const string m_itemTagName = "Item";
	private const string m_enemyTagName = "Enemy";

	[SerializeField]
	private AudioClip m_getItemSound;
	[SerializeField]
	private AudioClip m_defeatEnemySound;
	[SerializeField]
	private AudioSource m_audioSource;

	void Start()
	{
		m_ray = new Ray();
		m_raycastHit = new RaycastHit();

		m_avaterStateManager = this.gameObject.GetComponent<AvaterStateManager>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonUp(0))
		{
			m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			// マウスクリックした場所からrayを飛ばし、オブジェクトに当たればtrue
			if (Physics.Raycast(m_ray.origin, m_ray.direction, out m_raycastHit, Mathf.Infinity))
			{
				if (m_raycastHit.collider.gameObject.CompareTag(m_itemTagName))
				{
					Debug.Log("アイテムをクリック");
					ItemManager itemManager = m_raycastHit.collider.gameObject.GetComponent<ItemManager>();
					// アイテムが表示状態でかつ未取得のとき
					if (itemManager.GetIsAppered() && !itemManager.GetIsAcquired() )
					{
						// アイテムクリック音を鳴らす
						m_audioSource.PlayOneShot(m_getItemSound);
						// アイテムに対してクリックアクション処理を呼ぶ
						itemManager.OnClickAction();
						// プレイヤーに対してアイテム取得処理を呼ぶ
						m_avaterStateManager.GetItem(itemManager.GetItemValue());
					}
				}
				else if (m_raycastHit.collider.gameObject.CompareTag(m_enemyTagName))
				{
					Debug.Log("敵をクリック");
					if (m_avaterStateManager.GetPlayerState() == m_avaterStateManager.PLAYER_STATE_POWERUP)
					{
						// プレイヤーが無敵モードのとき
						EnemyManager enemyManager = m_raycastHit.collider.gameObject.GetComponent<EnemyManager>();
						// 敵が表示状態でかつ未取得のとき
						if (enemyManager.GetIsAppered() && !enemyManager.GetIsDefeated())
						{
							// 敵撃破音を鳴らす
							m_audioSource.PlayOneShot(m_defeatEnemySound);
							// 敵に対してクリックアクション処理を呼ぶ
							enemyManager.OnClickAction();
							// プレイヤーに対して敵討伐処理を呼ぶ
							m_avaterStateManager.DefeatEnemy(enemyManager.GetItemValue());
						}
					}
				}
			}
		}
	}
}
