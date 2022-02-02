using Ludo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoSinglton<TurnManager>
{
	public ColorEnum curretTurn;
	public ColorEnum CurretTurn { get => curretTurn; set => curretTurn = value; }

	internal void Turn()
	{
		//Dice.Instance.ResetDice();
		//curretTurn =(ColorEnum) (1 + ((int)curretTurn * -1));
	}
}

