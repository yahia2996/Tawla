using Ludo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoSinglton<TurnManager>
{
	public ColorEnum curretTurnColor;
	public ColorEnum CurretTurnType { get => curretTurnColor; set => curretTurnColor = value; }

	public Action<ColorEnum> TurnChanged;	
	
	private void Start()
	{
		TurnChanged?.Invoke(GameIntilizer.Instance.mainPlayerColor);
	}

	internal void Turn()
	{
		DiceManagers.Instance.RestDices();
		curretTurnColor = (ColorEnum) (1 + ((int)curretTurnColor * -1));
		//fire event
		TurnChanged?.Invoke(curretTurnColor);
	}
}

