using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class XmlParser : MonoBehaviour
{
	void Start(){
		string fullpath = Application.streamingAssetsPath + "/DummyData/DummyData.xml";
		string content = File.ReadAllText( fullpath );
		ParseXml (content);
	}

	public void ParseXml(string content){
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.Load (new StringReader(content));

		XmlNode root = xmlDoc.FirstChild;
		XmlNodeList areaList = xmlDoc.GetElementsByTagName("Area");
		Debug.Log("FirstChild " + root.InnerText);   //子ノードをふくむ、すべてのタグのテキストが表示される

		XmlNodeList header = root.FirstChild.ChildNodes; //最初のノード＝'Header'タグの、子ノードのリスト
		foreach (XmlNode node in areaList){
			Debug.Log(node.Name + ", " + node.InnerText);   //タグ名と、テキストを表示
		}
	}
}
