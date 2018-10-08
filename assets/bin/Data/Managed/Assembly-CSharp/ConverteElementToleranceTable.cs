using System;
using UnityEngine;

[Serializable]
public class ConverteElementToleranceTable
{
	public ELEMENT_TYPE fire;

	public ELEMENT_TYPE water = ELEMENT_TYPE.WATER;

	public ELEMENT_TYPE thunder = ELEMENT_TYPE.THUNDER;

	public ELEMENT_TYPE soil = ELEMENT_TYPE.SOIL;

	public ELEMENT_TYPE light = ELEMENT_TYPE.LIGHT;

	public ELEMENT_TYPE dark = ELEMENT_TYPE.DARK;

	public int GetChangeElement(int no)
	{
		int num = -1;
		switch (no)
		{
		case 0:
			num = (int)fire;
			break;
		case 1:
			num = (int)water;
			break;
		case 2:
			num = (int)thunder;
			break;
		case 3:
			num = (int)soil;
			break;
		case 4:
			num = (int)light;
			break;
		case 5:
			num = (int)dark;
			break;
		}
		if (num < 0)
		{
			Debug.LogError((object)"GetElement Not No");
		}
		return num;
	}
}
