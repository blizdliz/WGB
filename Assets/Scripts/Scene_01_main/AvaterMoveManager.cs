using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WGBScripts
{
	public class AvaterMoveManager : MonoBehaviour
	{
		[SerializeField]
		private GameObject m_avaterObj;

		private Animator m_animator;
		private MoveAvatar m_moveAvatar;

		// Use this for initialization
		void Start()
		{
			m_animator = m_avaterObj.GetComponent<Animator>();
			m_moveAvatar = this.GetComponent<MoveAvatar>();
			StartCoroutine(_AvaterAnimate());
		}
		
		/// <summary>
		/// 毎フレーム、アバターの移動状態を取得し反映する
		/// </summary>
		/// <returns></returns>
		private IEnumerator _AvaterAnimate()
		{
			while (true)
			{
				if (m_moveAvatar.animationState == 0)
				{
					m_animator.SetTrigger("Idle");
				}
				else
				{
					m_animator.SetTrigger("Dash");
				}
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
