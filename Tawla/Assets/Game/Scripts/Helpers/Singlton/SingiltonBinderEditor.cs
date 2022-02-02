using Assets.Scripts.Helpers.Singlton;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(SingltonBinder))]
[CanEditMultipleObjects]
public class SingiltonBinderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		SingltonBinder myScript = (SingltonBinder)target;
		if (GUILayout.Button("Bind!!!"))
		{
			myScript.Bind();
		}
	}
}
