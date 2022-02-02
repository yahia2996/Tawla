using Ludo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : ARock
{
	internal override void RockClicked()
	{
		if (movmentAnimation!=null||Dice.Instance._diceOneValue==-1) return;

		_selectedRock = this;


		bool positionOne = CheckMovment(Dice.Instance._diceOneValue);


		if (positionOne) {
			ACell cell = CellsManager.Instance.getCell(_indexOnBord + Dice.Instance._diceOneValue, _rockType);
			cell.Higlight();
		}

	    //bool positiontwo = CheckMovment(Dice.Instance._diceTwoValue);

		//



		//to remove
	
		/*Cell cell = CellsManager.Instance.getCell(_indexOnBord + Dice.Instance._diceOneValue, _rockType);
		if (cell)
		{
			//cell.AddToStack(this);
			MoveToCell(cell);
		}	*/
		//		
	}

	internal override bool CheckMovment(int movment) {
		ACell cell = CellsManager.Instance.getCell(_indexOnBord+movment,_rockType);

		if (cell == null) return false;

		if (cell.GetRocksCount() > 1)
		{
			if (cell.GetLastRockInStack()._rockType == _rockType)
			{
				return true;
			}
			else {
				return false;
			}
		}
		else
		{
			return true;
		}
	}

	internal override void MoveToCell(ACell targetCell) {			

		//update stacks
		ACell currentCell = CellsManager.Instance.getCell(_indexOnBord, _rockType);
		currentCell.RemoveFromStack();
		targetCell.AddRockToStack(this);

		//update rock index
		_indexOnBord = _indexOnBord + Dice.Instance._diceOneValue;

		//start movment lerp
		if (movmentAnimation != null)
			StopCoroutine(movmentAnimation);
		movmentAnimation = StartCoroutine(MoveAnimation(targetCell.GetNextRockPosition())); 

	}

	IEnumerator MoveAnimation(Vector2 target) {
		float lerpSpeed = 12;
		while (Vector2.Distance(transform.position, target) > 0.2f)
		{
			transform.position = Vector2.LerpUnclamped(transform.position, target, lerpSpeed * Time.deltaTime);
			yield return null;
		}

		transform.position = target;
		movmentAnimation = null;
	}
}




