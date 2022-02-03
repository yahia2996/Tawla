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
		public int _diceValue = -1;
		internal Action<int> _diceRollDoneAction;

		float waitingSeconds = 0.025f;
		
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
				randomValueResult = UnityEngine.Random.Range(1, 7);
				if (diceValueTxt)
				{
					//dice one
					_diceValue = randomValueResult;
					diceValueTxt.text = randomValueResult + "";					
				}
				yield return new WaitForSeconds(waitingSeconds);
			}
			_diceRollDoneAction?.Invoke(randomValueResult);
			diceAnimation = null;
		}
		
		internal void ResetDice()
		{
			_diceValue = -1;
			diceValueTxt.text = "*";		
		}
	}

}