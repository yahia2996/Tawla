using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoSinglton<Path>
{


	public Vector3 []points = new Vector3[24];

	[SerializeField] float distance;
	[SerializeField]int numOfPoints;
	[SerializeField] float verticalGap;
	[SerializeField] float horizontalGap;
	     
	public void CreatePath() {
		for (int i = 0; i < numOfPoints; i++) {
			points[i] = transform.position + Vector3.left * distance * (i);
		}

		for (int i = 0; i < numOfPoints; i++)
		{
			points[i+6] = transform.position + Vector3.left * distance * (i+6) + Vector3.left * horizontalGap;
		}

		for (int i = 0; i < numOfPoints; i++)
		{
			points[i + 12] = transform.position + Vector3.left * distance * (i + 6) + Vector3.left * horizontalGap+Vector3.down*verticalGap;
		}

		for (int i = 0; i < numOfPoints; i++)
		{
			points[i + 18] = transform.position + Vector3.left * distance * (i) + Vector3.down * verticalGap;
		}
	}

	private void OnDrawGizmos()
	{
		//points = new Vector3[numOfPoints*4];

		//CreatePath();
		for (int i = 0; i < points.Length; i++) {
		   Gizmos.DrawSphere(points[i], 5); 
		}
	}

}
