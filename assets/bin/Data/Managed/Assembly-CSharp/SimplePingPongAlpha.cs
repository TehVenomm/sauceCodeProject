using UnityEngine;

public class SimplePingPongAlpha
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

	public SimplePingPongAlpha()
		: this()
	{
	}//IL_0003: Unknown result type (might be due to invalid IL or missing references)
	//IL_0009: Unknown result type (might be due to invalid IL or missing references)
	//IL_000a: Unknown result type (might be due to invalid IL or missing references)


	public void Initialize()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
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
		target.get_gameObject().SetActive(true);
	}

	private void Update()
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		if (state != 0 && state != eState.Idle)
		{
			float value = addValue * Time.get_deltaTime();
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
						target.get_gameObject().SetActive(false);
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
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		color.a = value;
		target.color = color;
	}

	private bool AddValue(float value)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
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
