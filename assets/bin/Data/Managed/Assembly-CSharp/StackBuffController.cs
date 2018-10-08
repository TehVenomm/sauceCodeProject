using UnityEngine;

public class StackBuffController
{
	public enum STACK_TYPE
	{
		NONE,
		SNATCH,
		MAX
	}

	private int[] stackCounts;

	public void Init()
	{
		stackCounts = new int[2];
	}

	public int GetStackCount(STACK_TYPE type)
	{
		return stackCounts[(int)type];
	}

	public void IncrementStackCount(STACK_TYPE type)
	{
		stackCounts[(int)type]++;
	}

	public void DecrementStackCount(STACK_TYPE type)
	{
		stackCounts[(int)type] = Mathf.Max(0, stackCounts[(int)type] - 1);
	}
}
