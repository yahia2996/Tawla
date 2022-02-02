using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsManager : MonoSinglton<CellsManager>
{
	[SerializeField]   ACell []_Bordcells;
	[SerializeField]    HiglighterRock _HigligtPrefab;

	[SerializeField] ACell outCell1;
	[SerializeField] ACell outCell2;

	public ACell getCell(int i, ColorEnum color)
	{
		if(color == GameIntilizer.Instance.mainPlayer) 
		return getCellForword(i);
		else
		return getCellBackword(i);
	}

	public ACell getCellForword(int i)
	{
		if (i > _Bordcells.Length-1) return outCell1;
		return _Bordcells[i];
		
	}

	public ACell getCellBackword(int i)
	{
		if (i > _Bordcells.Length-1) return outCell2;
		return _Bordcells[23-i];
	}


	internal void Higlight(ACell cell) {
		Vector2 pos = cell.GetNextRockPositionBeforeAddingToStack(cell.GetRocksCount());
		_HigligtPrefab.Cell = cell; 
		_HigligtPrefab.transform.parent = cell.transform;
		_HigligtPrefab.transform.SetAsFirstSibling();

		_HigligtPrefab.transform.position = pos;
	}


}
