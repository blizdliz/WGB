using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_01_userActManager : MonoBehaviour
{
	private Ray m_ray;
	private RaycastHit m_raycastHit;

	private AvaterStateManager m_avaterStateManager;
	private const string m_itemTagName = "Item";

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
					ItemManager itemManager = m_raycastHit.collider.gameObject.GetComponent<ItemManager>();
					// アイテムが表示状態でかつ未取得のとき
					if (itemManager.GetIsAppered() && !itemManager.GetIsAcquired() )
					{
						// アイテムに対してクリックアクション処理を呼ぶ
						itemManager.OnClickAction();
						// プレイヤーに対してアイテム取得処理を呼ぶ
						m_avaterStateManager.GetItem(itemManager.GetItemValue());
					}
				}
			}
		}
	}
}
