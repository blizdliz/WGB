using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_01_CompassGUIManager : MonoBehaviour
{
	[SerializeField]
	private RectTransform m_compassImageTransform;
	
	// Update is called once per frame
	void Update ()
	{
		// カメラの向きを取得し方位磁針に反映する
		Vector3 camRotation = Camera.main.transform.localRotation.eulerAngles;
		m_compassImageTransform.localRotation = Quaternion.Euler(0,0, camRotation.y);
	}
}
