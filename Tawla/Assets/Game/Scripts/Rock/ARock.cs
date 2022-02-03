using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public abstract class ARock : MonoBehaviour, IPointerDownHandler
{
	public ColorEnum _rockColor;
	internal bool _insideBord = true;
	protected int _currentIndexOnBord = 0;
	protected Coroutine movmentAnimation;
	public static ARock _currentClickedRock;


	[SerializeField]protected List<ACell> validCellsToMove;

	public virtual void OnPointerDown(PointerEventData eventData)
	{
		if(CheckRockCanCliked())		
		RockClicked();		
	}

	protected virtual bool CheckRockCanCliked() {
		//we can only move the top of cell rock 
		bool isLastRock = transform.GetSiblingIndex() == transform.parent.childCount - 1;
		//check if th eplayer have the turn
		bool hasTheTurn = _rockColor == TurnManager.Instance.CurretTurnType; //black and current turn is black so hastheturn == true
		
	    return isLastRock && hasTheTurn && _insideBord;		
	}

	internal abstract int CheckVaildMovmentCellForDiceValues(List<int> diceValues);

	protected abstract void RockClicked();

	protected abstract ACell CheckMovmentIsValid(int movment);

	internal abstract void MoveToCell(ACell target);

	internal void StopMovmentAnimation()
	{
		if (movmentAnimation != null) {
			StopCoroutine(movmentAnimation);
			movmentAnimation = null;
		}
	}
}


public enum ColorEnum
{
	black = 0, white = 1
}