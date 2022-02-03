using Ludo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : ARock
{
	protected override void RockClicked()
	{

		/*bool isRockMoving = movmentAnimation != null;

		if (isRockMoving || Dice.Instance._diceValue==-1) return;

		_currentClickedRock = this;

		CellsManager.Instance.ClearHiglight();


		bool positionOne = CheckMovment(Dice.Instance._diceValue);
		//bool positionTwo = CheckMovment(Dice.Instance._diceTwoValue);


		if (positionOne) {
			ACell cell = CellsManager.Instance.getCell(_indexOnBord + Dice.Instance._diceValue, _rockColor);
			cell.ShowHigligtRock();
		}*/

		CellsManager.Instance.ClearHiglight();
		//if (TurnManager.Instance.curretTurnColor == this._rockColor)
		{

			if (_currentClickedRock != this)
			{
				_currentClickedRock = this;
				for (int i = 0; i < validCellsToMove.Count; i++)
				{
					validCellsToMove[i].ShowHigligtRock();
				}
			}
			else {
				_currentClickedRock = null;
			}
		}



		/*if (positionTwo)
		{
			ACell cell = CellsManager.Instance.getCell(_indexOnBord + Dice.Instance._diceTwoValue, _rockColor);
			cell.ShowHigligtRock();
		}*/

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


	/// <summary>
	/// 
	/// </summary>
	/// <param name="diceValues"></param>
	/// <returns></returns>
	internal override int CheckVaildMovmentCellForDiceValues(List<int> diceValues)
	{
		validCellsToMove = new List<ACell>();
		//int sumOfValues = 0;

		//check evrey value
		for (int i = 0; i < diceValues.Count; i++)
		{
			ACell validCell = CheckMovmentIsValid(diceValues[i]);
			if (validCell!=null) {
				validCellsToMove.Add(validCell);
				//sumOfValues += diceValues[i];
			}
		}

		//if all values are valid check agains sum of values
		/*if (diceValues.Count == validCellsToMove.Count&&validCellsToMove.Count>0) {
			ACell validCell = CheckMovmentIsValid(sumOfValues);
			if (validCell != null)
			{
				validCellsToMove.Add(validCell);
			}
		}*/

		return validCellsToMove.Count;
	}

	protected override ACell CheckMovmentIsValid(int movment) {
		ACell cell = CellsManager.Instance.GetCell(_currentIndexOnBord+movment);

		if (cell == null) return null;

		if (cell.GetRocksCountInCell() > 1)
		{
			if (cell.GetLastRockInCellStack()._rockColor == _rockColor)
			{
				return cell;
			}
			else {
				return null;
			}
		}
		else
		{
			return cell;
		}
	}

	internal override void MoveToCell(ACell targetCell) {
		
		
		//update stacks
		ACell currentCell = CellsManager.Instance.GetCell(_currentIndexOnBord);
		currentCell.RemoveFromStack();
		targetCell.AddRockToStack(this);



		//update rock index	
		//update position
		int moveAmount = targetCell.GetIndex() - _currentIndexOnBord;
		_currentIndexOnBord += moveAmount;


		//update movment Budget
		DiceManagers.Instance.UpdateMovmentBudget(moveAmount);


		//start movment lerp
		if (movmentAnimation != null)
			StopCoroutine(movmentAnimation);
		movmentAnimation = StartCoroutine(MoveAnimation(targetCell.GetNextRockPosition())); 

	}

	IEnumerator MoveAnimation(Vector2 target) {
		float lerpSpeed = 12;
		float minLerpDistance = 0.2f;
		while (Vector2.Distance(transform.position, target) > minLerpDistance)
		{
			transform.position = Vector2.LerpUnclamped(transform.position, target, lerpSpeed * Time.deltaTime);
			yield return null;
		}

		transform.position = target;
		movmentAnimation = null;
	}
}




