using System.Collections;
using UnityEngine;
using UnityEditor;

public class MakeItemObject {
	[MenuItem("Assets/Create/Item Object")]
	public static void CreateItemObject(){
		ItemObject asset = ScriptableObject.CreateInstance<ItemObject> ();
		AssetDatabase.CreateAsset (asset ,"Assets/ItemObjects/NewItemObject.asset");
		AssetDatabase.SaveAssets ();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}

}
