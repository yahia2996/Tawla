using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACell : MonoBehaviour
{

	[SerializeField] int Id;
	//[SerializeField] int backwordId;

	private Stack<ARock> rocksStack = new Stack<ARock>();
	

	public const int numOfRocks = 15;
	public const float maxDistanceScale = 2.15f;


	internal virtual void Awake()
	{
		Id = transform.GetSiblingIndex();
	    //	backwordId = 23-transform.GetSiblingIndex();
	}

	internal Vector2 GetNextRockPosition()
	{
		float distance = Mathf.Clamp(GameIntilizer.Instance.distancesBettweenRocks * numOfRocks / rocksStack.Count, GameIntilizer.Instance.distancesBettweenRocks, GameIntilizer.Instance.distancesBettweenRocks * maxDistanceScale);
		
		if (Id < 12)
		{
			return transform.position + Vector3.down * distance * (rocksStack.Count-1);
		}
		else
		{
			return transform.position + Vector3.up * distance * (rocksStack.Count-1);
		}	
	}

	internal Vector2 GetNextRockPositionBeforeAddingToStack(int count)
	{
		float distance = Mathf.Clamp(GameIntilizer.Instance.distancesBettweenRocks * numOfRocks / rocksStack.Count, GameIntilizer.Instance.distancesBettweenRocks, GameIntilizer.Instance.distancesBettweenRocks * maxDistanceScale);

		if (Id < 12)
		{
			return transform.position + Vector3.down * distance * count;
		}
		else
		{
			return transform.position + Vector3.up * distance * count;
		}
	}
	
	private void ReArrangeRocksPositionsAfterRemove() {
		//return;
		ARock[] aRocksArray = rocksStack.ToArray();
		float distance = Mathf.Clamp(GameIntilizer.Instance.distancesBettweenRocks * numOfRocks / aRocksArray.Length, GameIntilizer.Instance.distancesBettweenRocks, GameIntilizer.Instance.distancesBettweenRocks* maxDistanceScale);
		
		for (int i = aRocksArray.Length-1; i >= 0 ; i--)
		{
			if (Id < 12)
			{
				aRocksArray[i].StopAnimation();
				aRocksArray[i].transform.position = transform.position  + Vector3.down * distance  * (aRocksArray.Length-1 - i);
			}
			else
			{
				aRocksArray[i].StopAnimation();
				aRocksArray[i].transform.position = transform.position  + Vector3.up   * distance   * (aRocksArray.Length-1 - i);
			}
		}
	}

	private void ReArrangeRocksPositionsAfterAdding()
	{
		
		ARock[] aRocksArray = rocksStack.ToArray();
		float distance = Mathf.Clamp(GameIntilizer.Instance.distancesBettweenRocks * numOfRocks / aRocksArray.Length, GameIntilizer.Instance.distancesBettweenRocks, GameIntilizer.Instance.distancesBettweenRocks * maxDistanceScale);

		for (int i = aRocksArray.Length - 1; i > 0; i--)
		{
			if (Id < 12)
			{
				aRocksArray[i].StopAnimation();
				aRocksArray[i].transform.position = transform.position + Vector3.down * distance * (aRocksArray.Length -1 - i);
			}
			else
			{
				aRocksArray[i].StopAnimation();
				aRocksArray[i].transform.position = transform.position + Vector3.up * distance * (aRocksArray.Length - i-1);
			}
		}
	}

	internal virtual bool AddRockToStack(ARock aRock) {
	    
		rocksStack.Push(aRock);
		aRock.transform.parent = transform;

		ReArrangeRocksPositionsAfterAdding();

		return true;
	}

	internal ARock RemoveFromStack()
	{
		ARock rock = rocksStack.Pop();

		ReArrangeRocksPositionsAfterRemove();

		return rock;
	}

	internal ARock GetLastRockInStack()
	{
		ARock rock = rocksStack.Peek();
		return rock;
	}

	internal int GetRocksCount() {
		return rocksStack.Count; 
	}	

	internal virtual void Higlight()
	{
		CellsManager.Instance.Higlight(this);
	}

}
