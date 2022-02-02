using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public abstract class ARock : MonoBehaviour, IPointerDownHandler
{
	public ColorEnum _rockType;
	internal bool _canMoveClick = true;
	protected int _indexOnBord = 0;
	public static ARock _selectedRock;

	protected Coroutine movmentAnimation;



	public void OnPointerDown(PointerEventData eventData)
	{		
		if (transform.GetSiblingIndex() == transform.parent.childCount-1 && _rockType==TurnManager.Instance.CurretTurn&&_canMoveClick)
		{			
			RockClicked();
		}
	}

	internal abstract void RockClicked();
	internal abstract bool CheckMovment(int movment);
	internal abstract void MoveToCell(ACell target);
	internal void StopAnimation()
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