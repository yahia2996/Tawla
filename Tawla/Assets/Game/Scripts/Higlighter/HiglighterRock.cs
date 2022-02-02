using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HiglighterRock : MonoBehaviour, IPointerDownHandler
{
	[SerializeField]
	Transform _deultParent; 

	private ACell cell;

	public ACell Cell { get => cell; set => cell = value; }

	public void OnPointerDown(PointerEventData eventData)
	{
		Rock._selectedRock.MoveToCell(cell);
		transform.parent = _deultParent;

		TurnManager.Instance.Turn();

	}
}
