using Ludo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : ARock
{
	protected override void RockClicked()
	{

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
	}

	/// <summary>
	/// take list of dice values and updte validCells and return valid cells numbers
	/// </summary>
	/// <param name="diceValues"></param>
	/// <returns></returns>
	internal override int CheckVaildMovmentCellForDiceValues(List<int> diceValues)
	{
		validCellsToMove = new List<ACell>();		

		//check evrey value ability to moove the rock
		for (int i = 0; i < diceValues.Count; i++)
		{
			ACell validCell = CheckMovmentIsValid(diceValues[i]);
			if (validCell!=null) {
				validCellsToMove.Add(validCell);				
			}
		}

		//if we have two dice values and at least one move is valid we check also the summtion
		if (diceValues.Count >= 2&& validCellsToMove.Count > 0) {
			for (int i = 1; i < diceValues.Count; i++)
			{
				int sum = diceValues[0];
				for (int j = 0; j < i; j++)
				{
					sum += diceValues[j+1]; 
				}

				ACell validCell = CheckMovmentIsValid(sum);
				if (validCell != null)
				{
					validCellsToMove.Add(validCell);
				}
				else {
					break;
				}
			}		
		}
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
		int moveAmount = targetCell.GetCellIndex() - _currentIndexOnBord;
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




