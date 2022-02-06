using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameIntilizer : MonoSinglton<GameIntilizer>
{
	[Header("StartPoint")]
	[SerializeField]	Cell startPoint;
	[SerializeField]	Cell endPoint;
	internal float distancesBettweenRocks = 12;

	[Header("Prefabs")]
	[SerializeField]	ARock BlackRockPrefab;
	[SerializeField]    ARock WhiteRockPrefab;

	[Header("Celle")]
	[SerializeField]	RectTransform cell1;
	[SerializeField]	RectTransform cell2;

	public ColorEnum mainPlayerColor;

	public Player main, second;

	void Start()
    {
		Application.targetFrameRate = 60;
		IntilizeRocks(mainPlayerColor == ColorEnum.white);

		//set players colors
		main.SetColor(mainPlayerColor);
		second.SetColor((ColorEnum)(1 + ((int)mainPlayerColor * -1)));
	}

	private void IntilizeRocks(bool myColor)
	{
		float distance = cell1.transform.position.y - cell2.transform.position.y;

		distancesBettweenRocks = distance;

		if (!myColor)
		{
			ARock temp = WhiteRockPrefab;
		    WhiteRockPrefab = BlackRockPrefab;
			BlackRockPrefab = temp;
		}
		
		for (int i = 0; i < 15; i++)
		{
			ARock Rock = Instantiate(WhiteRockPrefab, startPoint.transform);
			Rock.GetComponent<RectTransform>().position = startPoint.transform.position + Vector3.down * distancesBettweenRocks * i;
			startPoint.AddRockToStack(Rock);
		}


		int number = 15;
		for (int i = 0; i < number; i++)
		{
			ARock Rock = Instantiate(BlackRockPrefab, endPoint.transform);
			Rock.GetComponent<RectTransform>().position = endPoint.transform.position + Vector3.up * distancesBettweenRocks * i;
			endPoint.AddRockToStack(Rock);
		}		


	}
}
