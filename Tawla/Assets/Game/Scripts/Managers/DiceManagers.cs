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


	void AddDiceValueToValueList(int val)
	{
		dicesValues.Add(val);
		if (dicesValues.Count == _dices.Length)
		{
			GenrateAllPosibilitesOfResultValues();
		}
	}


	/// <summary>
	/// this method is one game logic take 2 dice value and generte all posibilites 
	/// </summary>
	void GenrateAllPosibilitesOfResultValues() {
		_movmentBudget = dicesValues[0] + dicesValues[1];
		/*if (dicesValues[0] == dicesValues[1])
		{
			//dicesValues.RemoveAt(1);
			dicesValues.Add(dicesValues[0]);
			dicesValues.Add(dicesValues[0]);
			dicesValues.Add(dicesValues[0] * 2);
			dicesValues.Add(dicesValues[0] * 2);
			dicesValues.Add(dicesValues[0] * 3);
			_movmentBudget *= 2;
		}*/
		//_movmentBudget = 
		dicesValues.Add(_movmentBudget);
		FireUpdteValuesAction();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			RollDices();
		}
	}

	void RollDices() {

		//make sure no higlight just for test should removed and next steps
		CellsManager.Instance.ClearHiglight();
		Rock._currentClickedRock = null;

		//rest prevuios values
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
		CellsManager.Instance._PLayerHaveValidMoves = false;

		_movmentBudget -= moveAmount;
		bool removeThePropility = false;
		List<int> updtedValues = new List<int>();
		for (int i = 0; i < dicesValues.Count; i++) {
			if (_movmentBudget >= dicesValues[i]) {
				//remove this value
				if (dicesValues[i] == moveAmount && !removeThePropility) {
					removeThePropility = true;
					continue;
				}
				updtedValues.Add(dicesValues[i]);
			}
		}

		dicesValues = updtedValues;

		FireUpdteValuesAction();		

	}


	void FireUpdteValuesAction() {
		DicesValuesUpdtatedAction?.Invoke(dicesValues);
		if (CellsManager.Instance._PLayerHaveValidMoves == false)
		{
			ChanngeTurn();
		}
	}

	void ChanngeTurn() {		
			TurnManager.Instance.Turn();		
	}
}
