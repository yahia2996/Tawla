using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : ACell
{
	internal virtual void Awake()
	{
		Id = transform.GetSiblingIndex();
		//CellIndx = Id;
	}

	private void Start()
	{
		//rigister to Action
		DiceManagers.Instance.DicesValuesUpdtatedAction += DiceValuesUpdated;
	}

	
}
