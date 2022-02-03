using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsManager : MonoSinglton<CellsManager>
{
	[SerializeField]   ACell []_Bordcells;
	[SerializeField]   HiglighterRock _HigligtPrefab;

	[SerializeField] ACell outCell1;
	[SerializeField] ACell outCell2;


	public bool _PLayerHaveValidMoves = true;


	public Stack<HiglighterRock> savedrocksStack = new Stack<HiglighterRock>();
	public Stack<HiglighterRock> usedRocksStack = new Stack<HiglighterRock>();


	public static int targetCellIndex;

	public HiglighterRock _Higligtrock
	{
		get {
			if (savedrocksStack.Count > 0)
			{
				HiglighterRock higlighterRock  = savedrocksStack.Pop();
				usedRocksStack.Push(higlighterRock);
				return higlighterRock;
			}
			else {
				HiglighterRock higlighterRock = Instantiate(_HigligtPrefab, _HigligtPrefab.transform.parent);
				usedRocksStack.Push(higlighterRock);
				return higlighterRock;
			}	
		}		
	}

	internal void ClearHiglight()
	{
		while (usedRocksStack.Count > 0) {
			HiglighterRock higlighterRock = usedRocksStack.Pop();
			higlighterRock.RestParent();
			savedrocksStack.Push(higlighterRock);
		}
	}

	internal ACell GetCell(int cellIndex)
	{
		if(TurnManager.Instance.curretTurnColor == GameIntilizer.Instance.mainPlayerColor) 
		return getCellForword(cellIndex);
		else
		return getCellBackword(cellIndex);
	}

	public ACell getCellForword(int cellIndex)
	{
		if (cellIndex > _Bordcells.Length-1) return outCell1;
		targetCellIndex = cellIndex;
		return _Bordcells[cellIndex];
		
	}

	public ACell getCellBackword(int cellIndex)
	{
		if (cellIndex > _Bordcells.Length-1) return outCell2;
		targetCellIndex = 23 - cellIndex;
		return _Bordcells[23-cellIndex];
	}


	internal void ShowHiglightRock(ACell cell) {
		Vector2 pos = cell.GetNextRockPositionBeforeAddingToStack(cell.GetRocksCountInCell());
		var higlight = _Higligtrock;
		higlight.cell = cell;
		higlight.transform.parent = cell.transform;
		higlight.transform.SetAsFirstSibling();
		higlight.transform.position = pos;
	}


}
