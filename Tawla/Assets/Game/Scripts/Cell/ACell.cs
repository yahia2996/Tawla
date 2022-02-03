using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACell : MonoBehaviour
{

	[SerializeField]protected int Id;
	[SerializeField] internal int CellIndx;

	private Stack<ARock> rocksStack = new Stack<ARock>();
	

	public const int numOfRocks = 15;
	public const float maxDistanceScale = 2.15f;



	internal Vector2 GetNextRockPosition()
	{
		float distance = Mathf.Clamp(GameIntilizer.Instance.distancesBettweenRocks * numOfRocks / rocksStack.Count, GameIntilizer.Instance.distancesBettweenRocks, GameIntilizer.Instance.distancesBettweenRocks * maxDistanceScale);
		
		if (Id < 12)
		{
			return transform.position + Vector3.down * distance * (rocksStack.Count-1);
		}
		else
		{
			return transform.position + Vector3.up * distance * (rocksStack.Count-1);
		}	
	}

	internal Vector2 GetNextRockPositionBeforeAddingToStack(int count)
	{
		float distance = Mathf.Clamp(GameIntilizer.Instance.distancesBettweenRocks * numOfRocks / rocksStack.Count, GameIntilizer.Instance.distancesBettweenRocks, GameIntilizer.Instance.distancesBettweenRocks * maxDistanceScale);

		if (Id < 12)
		{
			return transform.position + Vector3.down * distance * count;
		}
		else
		{
			return transform.position + Vector3.up * distance * count;
		}
	}
	
	private void ReArrangeRocksPositionsAfterRemove() {
		//return;
		ARock[] aRocksArray = rocksStack.ToArray();
		float distance = Mathf.Clamp(GameIntilizer.Instance.distancesBettweenRocks * numOfRocks / aRocksArray.Length, GameIntilizer.Instance.distancesBettweenRocks, GameIntilizer.Instance.distancesBettweenRocks* maxDistanceScale);
		
		for (int i = aRocksArray.Length-1; i >= 0 ; i--)
		{
			if (Id < 12)
			{
				aRocksArray[i].StopMovmentAnimation();
				aRocksArray[i].transform.position = transform.position  + Vector3.down * distance  * (aRocksArray.Length-1 - i);
			}
			else
			{
				aRocksArray[i].StopMovmentAnimation();
				aRocksArray[i].transform.position = transform.position  + Vector3.up   * distance   * (aRocksArray.Length-1 - i);
			}
		}
	}

	private void ReArrangeRocksPositionsAfterAdding()
	{
		
		ARock[] aRocksArray = rocksStack.ToArray();
		float distance = Mathf.Clamp(GameIntilizer.Instance.distancesBettweenRocks * numOfRocks / aRocksArray.Length, GameIntilizer.Instance.distancesBettweenRocks, GameIntilizer.Instance.distancesBettweenRocks * maxDistanceScale);

		for (int i = aRocksArray.Length - 1; i > 0; i--)
		{
			if (Id < 12)
			{
				aRocksArray[i].StopMovmentAnimation();
				aRocksArray[i].transform.position = transform.position + Vector3.down * distance * (aRocksArray.Length -1 - i);
			}
			else
			{
				aRocksArray[i].StopMovmentAnimation();
				aRocksArray[i].transform.position = transform.position + Vector3.up * distance * (aRocksArray.Length - i-1);
			}
		}
	}

	internal virtual int GetIndex()
	{
	
		return TurnManager.Instance.curretTurnColor == GameIntilizer.Instance.mainPlayerColor ? Id : 23 - Id;
	}

	internal virtual bool AddRockToStack(ARock aRock) {
	    
		rocksStack.Push(aRock);
		aRock.transform.parent = transform;

		ReArrangeRocksPositionsAfterAdding();

		return true;
	}

	internal ARock RemoveFromStack()
	{
		ARock rock = rocksStack.Pop();

		ReArrangeRocksPositionsAfterRemove();

		return rock;
	}

	internal ARock GetLastRockInCellStack()
	{
		ARock rock = rocksStack.Peek();
		return rock;
	}

	internal int GetRocksCountInCell() {
		return rocksStack.Count; 
	}	

	internal virtual void ShowHigligtRock()
	{
		CellsManager.Instance.ShowHiglightRock(this);
	}

	protected void DiceValuesUpdated(List<int> diceValues)
	{
		//check valid rocks in cell
		bool cellHaveValidRock = GetRocksCountInCell() != 0 && GetLastRockInCellStack()._rockColor == TurnManager.Instance.curretTurnColor;
		if (!cellHaveValidRock) return;

		//bind the rock with values
		var lastRockInCellRock = GetLastRockInCellStack();
		int validTargetCellsCount = lastRockInCellRock.CheckVaildMovmentCellForDiceValues(diceValues);

		CellsManager.Instance._PLayerHaveValidMoves = CellsManager.Instance._PLayerHaveValidMoves || validTargetCellsCount > 0;
	}

}
