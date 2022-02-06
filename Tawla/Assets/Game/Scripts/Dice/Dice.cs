using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ludo
{
	public class Dice : MonoBehaviour
	{
		public TMPro.TMP_Text diceValueTxt;
		private Coroutine diceAnimation;
		[HideInInspector]public int _diceValue = -1;
		public int defaultDiceValue = 3;

		internal Action<int> _diceRollDoneAction;

		float RollDiceAnimationWaitingSeconds = 0.025f;
		
		internal void RollDice()
		{
			if(diceAnimation==null)
			diceAnimation = StartCoroutine(RollDiceAnimation());
		}

		IEnumerator RollDiceAnimation()
		{
			int randomValueResult = 0;
			for (int i = 0; i < 20; i++)
			{
				if(!DiceManagers.Instance._cheat)
				randomValueResult = UnityEngine.Random.Range(1, 7);
				else
					randomValueResult = defaultDiceValue;

				if (diceValueTxt)
				{
					_diceValue = randomValueResult;
					diceValueTxt.text = randomValueResult + "";					
				}
				yield return new WaitForSeconds(RollDiceAnimationWaitingSeconds);
			}
			_diceRollDoneAction?.Invoke(randomValueResult);
			diceAnimation = null;
		}
		
		internal void ResetDice()
		{
			_diceValue = defaultDiceValue;
			diceValueTxt.text = "*";		
		}
	}

}