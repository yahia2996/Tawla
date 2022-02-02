using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ludo
{
	public class Dice : MonoSinglton<Dice>
	{
		public TMPro.TMP_Text diceValueTxt;
		public UnityAction<int> diceRollResut;
		private Coroutine diceAnimation;

		public int _diceOneValue = -1;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				RollDice();
			}
		}


		private void RollDice()
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
					_diceOneValue = randomValueResult;
					diceValueTxt.text = randomValueResult + "";
				}
				yield return new WaitForSeconds(0.025f);
			}
			diceRollResut?.Invoke(randomValueResult);
			diceAnimation = null;
		}

		internal void Active()
		{
			transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
		}

		internal void DeActive()
		{
			ResetDice();
			transform.localScale = new Vector3(1, 1, 1);
		}

		internal void TurnDone()
		{
			//sumOfvalueSended = 0;
			//numOfAvaterSended = 0;
			//TurnsManager.instance.DoNextPlayerTurn();
		}

		public void ResetDice()
		{
			_diceOneValue = -1;
			diceValueTxt.text = "*";
		}
	}

}