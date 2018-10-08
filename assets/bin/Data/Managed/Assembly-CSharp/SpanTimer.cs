using UnityEngine;

public class SpanTimer
{
	private float span;

	private float nextTime;

	private bool pause;

	public SpanTimer(float span)
	{
		this.span = span;
	}

	public bool IsReady()
	{
		if (span < 0f || pause)
		{
			return false;
		}
		if (span == 0f)
		{
			return true;
		}
		if (nextTime > Time.get_time())
		{
			return false;
		}
		ResetNextTime();
		return true;
	}

	public void ResetNextTime()
	{
		nextTime = Time.get_time() + span;
	}

	public void SetTempSpan(float temp_span)
	{
		nextTime = Time.get_time() + temp_span;
	}

	public void PauseOn()
	{
		pause = true;
	}

	public void PauseOff()
	{
		pause = false;
	}
}
