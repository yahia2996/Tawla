using Ludo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManagers : MonoSinglton<DiceManagers>
{
	[SerializeField] Dice[] _dices;
	public List<int> dicesValues = new List<int>();
	public Action<List<int>> DicesValuesUpdtatedAction;
	public int _movmentBudget = 0;



	public bool _cheat = true;


	private void Start()
	{
		RegisterToDiceRollResult();
	}

	internal void RestDices()
	{
		for (int i = 0; i < _dices.Length; i++)
		{
			_dices[i].ResetDice();
		}
	}

	private void RegisterToDiceRollResult()
	{
		for (int i = 0; i < _dices.Length; i++) {
			_dices[i]._diceRollDoneAction = AddDiceValueToValueList;
		}
	}

	/// <summary>
	/// get the value of dice and add it to list
	/// </summary>
	/// <param name="val"></param>
	void AddDiceValueToValueList(int val)
	{
		dicesValues.Add(val);

		//when all dices have a values 
		if (dicesValues.Count == _dices.Length)
		{
			GenrateAllPosibilitesOfResultValues();
		}
	}


	/// <summary>
	/// this method is one game logic take 2 dice value and generte all posibilites 
	/// </summary>
	void GenrateAllPosibilitesOfResultValues() {
		
		//update movemt budget by the count of 
		_movmentBudget = dicesValues[0] + dicesValues[1];

		//handle the double case by duplicate budget and values 
		if (dicesValues[0] == dicesValues[1])
		{
			dicesValues.Add(dicesValues[0]);
			dicesValues.Add(dicesValues[0]);		
			_movmentBudget *= 2;
		}

		//fire the event dice values updated
		FireDicesValuesUpdatedAction();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			RollAllDices();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	internal void RollAllDices() {

		//make sure no higlight just for test should removed and next steps
		CellsManager.Instance.ClearHiglight();
		Rock._currentClickedRock = null;

		//rest prevuios dices values
		dicesValues.Clear();
		dicesValues = new List<int>();

		//roll dices
		for (int i = 0; i < _dices.Length; i++)
		{
			_dices[i].RollDice();
		}
	}

	internal void UpdateMovmentBudget(int moveAmount)
	{

		_movmentBudget -= moveAmount;

		bool _valueRemoved = false;
		float restValue=0;

		List<int> updtedValues = new List<int>();
		for (int i = 0; i < dicesValues.Count; i++) {
			if (_movmentBudget >= dicesValues[i]&&restValue<_movmentBudget) {
				
				//remove this value
				if (dicesValues[i] == moveAmount && !_valueRemoved) {
					_valueRemoved = true;
					continue;
				}
				restValue += dicesValues[i];
				updtedValues.Add(dicesValues[i]);
			}
		}

		dicesValues = updtedValues;

		FireDicesValuesUpdatedAction();		

	}


	void FireDicesValuesUpdatedAction() {

		//rest move abillity
		CellsManager.Instance._PLayerHaveValidMoves = false;


		DicesValuesUpdtatedAction?.Invoke(dicesValues);
		if (CellsManager.Instance._PLayerHaveValidMoves == false)
		{
			ChanngeTurn();
		}
	}

	void ChanngeTurn() {
		//CellsManager.Instance._PLayerHaveValidMoves = false;
		TurnManager.Instance.Turn();		
	}
}
