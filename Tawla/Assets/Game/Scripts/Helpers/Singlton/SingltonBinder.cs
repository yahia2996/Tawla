using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Helpers.Singlton
{
	[DefaultExecutionOrder(-100)]
	public class SingltonBinder : MonoBehaviour
	{
	    [SerializeField]
		Singltion[] singlton;		

	  	public void LoadScene(string name) {
		   SceneManager.LoadScene(name);
	    }

		private void Awake()
		{
			for (int i = 0; i < singlton.Length; i++)
			{
				singlton[i].Constractor();
			}
		}

		internal void Bind()
		{
			singlton = FindObjectsOfType<Singltion>();			
		}
	}

	
	

}