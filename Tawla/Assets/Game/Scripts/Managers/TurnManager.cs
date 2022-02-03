using Ludo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoSinglton<TurnManager>
{
	public ColorEnum curretTurnColor;
	public ColorEnum CurretTurnType { get => curretTurnColor; set => curretTurnColor = value; }

	internal void Turn()
	{
		DiceManagers.Instance.RestDices();
		curretTurnColor = (ColorEnum) (1 + ((int)curretTurnColor * -1));
	}
}

