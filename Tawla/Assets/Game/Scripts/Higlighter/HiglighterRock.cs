using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HiglighterRock : MonoBehaviour, IPointerDownHandler
{
	[SerializeField]
	Transform _deultParent;

	internal ACell cell;
	

	public void OnPointerDown(PointerEventData eventData)
	{
		HighlightClicked();	
	}


	void HighlightClicked() {
		CellsManager.Instance.ClearHiglight();
		Rock._currentClickedRock.MoveToCell(cell);
		//TurnManager.Instance.Turn();
		Rock._currentClickedRock = null;
	}

	internal void RestParent()
	{
		transform.parent = _deultParent;
	}
}
