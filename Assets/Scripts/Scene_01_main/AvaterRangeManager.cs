using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvaterRangeManager : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			// 敵に接触
			Debug.Log("敵が範囲内に入った");
			other.gameObject.GetComponent<EnemyManager>().Appear();
		}
		else if (other.gameObject.tag == "Item")
		{
			// アイテムに接触
			Debug.Log("アイテムが範囲内に入った");
			other.gameObject.GetComponent<ItemManager>().Appear();
		}
	}

	/// <summary>
	/// Raises the trigger exit event.
	/// </summary>
	/// <param name="other"></param>
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			// 敵に接触
			Debug.Log("敵が範囲外になった");
			other.gameObject.GetComponent<EnemyManager>().Disappear();
		}
		else if (other.gameObject.tag == "Item")
		{
			// アイテムに接触
			Debug.Log("アイテムが範囲外になった");
			other.gameObject.GetComponent<ItemManager>().Disappear();
		}
	}
}
