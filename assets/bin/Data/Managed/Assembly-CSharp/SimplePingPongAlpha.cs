using UnityEngine;

public class SimplePingPongAlpha : MonoBehaviour
{
	private enum eState
	{
		None,
		Idle,
		Forward,
		Back
	}

	[SerializeField]
	private UISprite target;

	[SerializeField]
	private float from;

	[SerializeField]
	private float to;

	[SerializeField]
	private float sec;

	[SerializeField]
	private int endCount;

	[SerializeField]
	private bool endDisable;

	private eState state;

	private int nowCount;

	private float addValue;

	private Color color = default(Color);

	public void Initialize()
	{
		if (state == eState.None)
		{
			from = LimitValue(from);
			to = LimitValue(to);
			addValue = ((sec != 0f) ? ((to - from) / sec) : 0f);
			color = target.color;
			state = eState.Idle;
		}
	}

	public void Play(bool startDefaultValue)
	{
		if (state == eState.None)
		{
			Initialize();
		}
		else if (state == eState.Forward || state == eState.Back)
		{
			return;
		}
		if (startDefaultValue)
		{
			SetValue(from);
		}
		nowCount = 0;
		state = eState.Forward;
		target.gameObject.SetActive(true);
	}

	private void Update()
	{
		if (state != 0 && state != eState.Idle)
		{
			float value = addValue * Time.deltaTime;
			if (state == eState.Forward)
			{
				if (AddValue(value))
				{
					state = eState.Back;
				}
			}
			else if (state == eState.Back && SubValue(value))
			{
				if (++nowCount >= endCount)
				{
					if (endDisable)
					{
						target.gameObject.SetActive(false);
					}
					state = eState.Idle;
				}
				else
				{
					state = eState.Forward;
				}
			}
		}
	}

	private void SetValue(float value)
	{
		color.a = value;
		target.color = color;
	}

	private bool AddValue(float value)
	{
		bool result = false;
		color.a += value;
		if (color.a >= to)
		{
			color.a = to;
			result = true;
		}
		target.color = color;
		return result;
	}

	private bool SubValue(float value)
	{
		bool result = false;
		color.a -= value;
		if (color.a <= from)
		{
			color.a = from;
			result = true;
		}
		target.color = color;
		return result;
	}

	private float LimitValue(float value)
	{
		if (value < 0f)
		{
			value = 0f;
		}
		if (value > 1f)
		{
			value = 1f;
		}
		return value;
	}
}
