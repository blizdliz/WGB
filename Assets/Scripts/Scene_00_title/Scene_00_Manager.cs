using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_00_Manager : MonoBehaviour
{
	[SerializeField]
	private GameObject m_allSceneManagementObj;

	[SerializeField]
	private Scene_00_GUIManager m_scene_00_guiManager;

	private AllSceneManagementBehavior m_allSceneManagementBehavior;

	// Use this for initialization
	void Start ()
	{
		GameObject allSceneManagementObj = GameObject.FindGameObjectWithTag(m_allSceneManagementObj.tag);
		if (allSceneManagementObj == null )
		{
			m_allSceneManagementBehavior = GameObject.Instantiate(m_allSceneManagementObj).GetComponent<AllSceneManagementBehavior>();
		}
		else
		{
			m_allSceneManagementBehavior = allSceneManagementObj.GetComponent<AllSceneManagementBehavior>();
		}

		m_scene_00_guiManager.Init(m_allSceneManagementBehavior);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
