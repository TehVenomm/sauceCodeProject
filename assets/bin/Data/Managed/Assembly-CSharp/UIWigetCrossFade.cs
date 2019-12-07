using UnityEngine;

public class UIWigetCrossFade : MonoBehaviour
{
	private enum eFadeState
	{
		None,
		In,
		Idle,
		Out
	}

	public UIWidget[] targetWidget;

	private readonly float kIdleSec = 2f;

	private readonly float kFadeSec = 0.5f;

	private bool bUpdate;

	private int targetIndex;

	private eFadeState fadeState;

	private float processSec;

	public void Play()
	{
		if (!bUpdate && !targetWidget.IsNullOrEmpty())
		{
			bUpdate = true;
			targetIndex = 0;
			fadeState = eFadeState.Idle;
			for (int i = 0; i < targetWidget.Length; i++)
			{
				targetWidget[i].alpha = ((i == targetIndex) ? 1f : 0f);
			}
		}
	}

	public void Reset()
	{
		bUpdate = false;
		targetIndex = 0;
		fadeState = eFadeState.None;
		if (!targetWidget.IsNullOrEmpty())
		{
			for (int i = 0; i < targetWidget.Length; i++)
			{
				targetWidget[i].alpha = 1f;
			}
		}
	}

	private void Update()
	{
		if (!bUpdate)
		{
			return;
		}
		switch (fadeState)
		{
		case eFadeState.In:
		{
			processSec += Time.deltaTime;
			float num2 = processSec / kFadeSec;
			if (num2 > 1f)
			{
				num2 = 1f;
			}
			targetWidget[targetIndex].alpha = num2;
			if (processSec >= kFadeSec)
			{
				processSec = 0f;
				fadeState = eFadeState.Idle;
			}
			break;
		}
		case eFadeState.Idle:
			processSec += Time.deltaTime;
			if (processSec >= kIdleSec)
			{
				processSec = 0f;
				fadeState = eFadeState.Out;
			}
			break;
		case eFadeState.Out:
		{
			processSec += Time.deltaTime;
			float num = 1f - processSec / kFadeSec;
			if (num < 0f)
			{
				num = 0f;
			}
			targetWidget[targetIndex].alpha = num;
			if (processSec >= kFadeSec)
			{
				processSec = 0f;
				fadeState = eFadeState.In;
				if (++targetIndex >= targetWidget.Length)
				{
					targetIndex = 0;
				}
			}
			break;
		}
		}
	}
}
