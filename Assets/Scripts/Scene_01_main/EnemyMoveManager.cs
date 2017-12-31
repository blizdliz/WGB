using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveManager : MonoBehaviour
{
	// 動き回るタイプ
	private const int TYPE_WALKER = 0;
	// 近辺に留まるタイプ
	private const int TYPE_GUARDER = 1;
	private int m_enemyType;

	private CharacterController m_enemyController;
	private Animator m_animator;
	//　目的地
	private Vector3 m_destination;
	[SerializeField]
	//　歩くスピード
	private float m_walkSpeed = 5.0f;
	
	//　速度
	private Vector3 m_velocity;
	//　移動方向
	private Vector3 m_direction;
	//　到着フラグ
	private bool m_arrived;

	// プレイヤーのアバター配列
	private GameObject[] m_playerAvaters;
	// 標的とするプレイヤーアバター
	private GameObject m_targetPlayerAvater;
	// ゲームマネージャー
	private Scene_01_GameManager m_gameManager;

	// Use this for initialization
	void Start ()
	{
		m_gameManager = GameObject.Find("Scene_01_Manager").GetComponent<Scene_01_GameManager>();
		m_playerAvaters = GameObject.FindGameObjectsWithTag("Player");
		m_targetPlayerAvater = m_playerAvaters[0];

		m_enemyController = GetComponent<CharacterController>();
		m_animator = GetComponentInChildren<Animator>();
		m_destination = SetRandomDestination(m_targetPlayerAvater.transform.position);
		m_velocity = Vector3.zero;

		StartCoroutine(_move());
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	private IEnumerator _move()
	{
		yield return null;

		while (true)
		{
			if (m_gameManager.GetGameState() == m_gameManager.GAME_STATE_NORMAL)
			{
				if (m_enemyController.isGrounded)
				{
					m_velocity = Vector3.zero;
					if (!m_arrived)
					{
						m_animator.SetTrigger("Dash");
					}
					m_direction = (m_destination - transform.position).normalized;
					transform.LookAt(new Vector3(m_destination.x, transform.position.y, m_destination.z));
					m_velocity = m_direction * m_walkSpeed;
					Debug.Log(m_destination);
				}
				m_velocity.y += Physics.gravity.y * Time.deltaTime;
				m_enemyController.Move(m_velocity * Time.deltaTime);

				//　目的地に到着したかどうかの判定
				Vector2 selfPos = new Vector2(transform.position.x, transform.position.z);
				Vector2 destPos = new Vector2(m_destination.x, m_destination.z);
				if (Vector2.Distance(selfPos, destPos) < 10.0f)
				{
					m_arrived = true;
					m_animator.SetTrigger("Idle");
					Debug.Log("Arrived");
				}

				if (m_arrived)
				{
					// 目的地を再設定する
					m_destination = SetRandomDestination(m_targetPlayerAvater.transform.position);
					m_arrived = false;
					Debug.Log("Destination reset");
				}
			}

			yield return new WaitForEndOfFrame();
		}
	}

	/// <summary>
	/// 行き先をランダムに決める
	/// </summary>
	/// <param name="oriDestination"></param>
	/// <returns></returns>
	private Vector3 SetRandomDestination(Vector3 oriDestination)
	{
		Vector2 randomDestination = Random.insideUnitCircle * 100;

		return oriDestination + new Vector3(randomDestination.x, 0, randomDestination.y);
	}
}
