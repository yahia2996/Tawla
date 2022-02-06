using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCell : ACell
{
	internal override bool AddRockToStack(ARock aRock)
	{
		aRock._insideBord = false;
		return base.AddRockToStack(aRock);
	}
	private void Start()
	{
		//rigister to Action
		DiceManagers.Instance.DicesValuesUpdtatedAction += DiceValuesUpdated;
	}


	internal override int GetCellIndex()
	{
		return CellIndx;
		//return TurnManager.Instance.curretTurnColor == GameIntilizer.Instance.mainPlayerColor ? CellIndx : 23 - Id;

	}

}
