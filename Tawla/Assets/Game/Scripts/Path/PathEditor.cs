using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Path))]
[CanEditMultipleObjects]
public class PathEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Path myScript = (Path)target;
		if (GUILayout.Button("Generate Path!!!"))
		{
			myScript.CreatePath();
		}
	}
}
