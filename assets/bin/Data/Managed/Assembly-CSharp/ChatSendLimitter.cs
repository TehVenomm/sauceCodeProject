using UnityEngine;

internal class ChatSendLimitter
{
	private int maxCount;

	private float limitSec;

	private int firstPos;

	private int lastPos;

	private float[] entries;

	public ChatSendLimitter(int maxCount, float limitSec)
	{
		this.maxCount = maxCount + 1;
		this.limitSec = limitSec;
		entries = new float[this.maxCount];
	}

	public void Touch()
	{
		entries[lastPos] = Time.unscaledTime + limitSec;
		lastPos = Next(lastPos);
	}

	public void Update()
	{
		if (firstPos != lastPos && entries[firstPos] < Time.unscaledTime)
		{
			firstPos = Next(firstPos);
		}
	}

	private int Next(int current)
	{
		current++;
		if (current < maxCount)
		{
			return current;
		}
		return 0;
	}

	public bool IsLimit()
	{
		Update();
		if (firstPos <= lastPos)
		{
			return lastPos - firstPos == maxCount - 1;
		}
		return lastPos - firstPos == -1;
	}
}
