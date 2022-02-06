using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	[SerializeField] Button TossButton;
	[SerializeField] ColorEnum _ColorEnum;

	[SerializeField]
	TMPro.TMP_Text playerColor;

	public void SetColor(ColorEnum colorEnum)
	{
		_ColorEnum = colorEnum;
		playerColor.text = "" + colorEnum.ToString();
	}

	private void Start()
	{
		//
		TurnManager.Instance.TurnChanged += ActivePlayer;
		TossButton.onClick.AddListener(TossButtonClicked);
	}




	public void TossButtonClicked() {
		TossButton.interactable = false;
		DiceManagers.Instance.RollAllDices();
	}

	public void ActivePlayer(ColorEnum color) {
		//
		if (color == _ColorEnum) {
			TossButton.interactable = true;
			StopAllCoroutines();
			StartCoroutine(AnimationUp(new Vector3(1.25f, 1.25f, 1)));
			//GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1);
		}
		else
		{
			TossButton.interactable = false;
			StopAllCoroutines();
			StartCoroutine(AnimationDown(new Vector3(1, 1, 1)));
		}
	}


	IEnumerator AnimationDown(Vector3 scale)
	{
		RectTransform rectTransform = GetComponent<RectTransform>();

		float speed = 5;

		while (scale.x - rectTransform.localScale.x < (scale.x - rectTransform.localScale.x) * Time.deltaTime * speed)
		{
			rectTransform.localScale = rectTransform.localScale + Vector3.one * (scale.x - rectTransform.localScale.x) * Time.deltaTime * speed;
			yield return null;
		}
		rectTransform.localScale = scale;

	}

	IEnumerator AnimationUp(Vector3 scale)
	{
		RectTransform rectTransform = GetComponent<RectTransform>();

		float speed = 5;

		while (  scale.x-rectTransform.localScale.x > (scale.x - rectTransform.localScale.x)*Time.deltaTime*speed)
		{
			rectTransform.localScale = rectTransform.localScale + Vector3.one * (scale.x-rectTransform.localScale.x  ) * Time.deltaTime*speed;
			yield return null;
		}
		rectTransform.localScale = scale;

	}


}
