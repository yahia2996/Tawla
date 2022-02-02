using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCell : ACell
{

	internal override void Awake()
	{
		//backwoedId = 23 - transform.GetSiblingIndex();
	}


	internal override bool AddRockToStack(ARock aRock)
	{
		aRock._canMoveClick = false;
		return base.AddRockToStack(aRock);
	}
}
