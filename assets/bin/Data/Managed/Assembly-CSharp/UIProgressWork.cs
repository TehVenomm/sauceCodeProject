using UnityEngine;

public class UIProgressWork : MonoBehaviour
{
	private int min;

	private int max = 100;

	private int now;

	private bool enableUpdateValue = true;

	public int minValue
	{
		get
		{
			return min;
		}
		set
		{
			min = value;
			UpdateValue();
		}
	}

	public int maxValue
	{
		get
		{
			return max;
		}
		set
		{
			max = value;
			UpdateValue();
		}
	}

	public int value
	{
		get
		{
			return now;
		}
		set
		{
			now = Mathf.Clamp(value, min, max);
			enableUpdateValue = false;
			if (min == max)
			{
				SetValue(1f);
			}
			else
			{
				SetValue((float)(now - min) / (float)(max - min));
			}
			enableUpdateValue = true;
		}
	}

	public UIProgressBar progress
	{
		get;
		private set;
	}

	public UIProgressWork()
		: this()
	{
	}

	private void Awake()
	{
		progress = this.GetComponent<UIProgressBar>();
		UpdateValue();
		EventDelegate.Add(progress.onChange, OnValueChange);
	}

	private void OnValueChange()
	{
		UpdateValue();
	}

	private void UpdateValue()
	{
		if (enableUpdateValue)
		{
			if (min > max)
			{
				min = max;
			}
			int num = max - min;
			progress.set_enabled(true);
			if (num == 0)
			{
				SetValue(1f);
				progress.numberOfSteps = 0;
				progress.set_enabled(false);
			}
			else
			{
				now = Mathf.RoundToInt((float)num * progress.value) + min;
				progress.numberOfSteps = num + 1;
			}
		}
	}

	private void SetValue(float value)
	{
		bool enabled = progress.get_enabled();
		bool flag = enableUpdateValue;
		progress.set_enabled(true);
		enableUpdateValue = false;
		if (progress.value == value)
		{
			progress.value = 1f - value;
		}
		progress.value = value;
		enableUpdateValue = flag;
		progress.set_enabled(enabled);
	}
}
