using System;
using UnityEngine;

[Serializable]
public abstract class InterpolatorBase<T> : Interpolator
{
	public bool play = true;

	public LOOP loopType;

	public float time;

	public T beginValue;

	public T endValue;

	public AnimationCurve easeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	public T addValue;

	public AnimationCurve addCurve;

	protected T nowValue;

	protected bool calcAngle;

	protected float nowTime;

	protected bool turn;

	protected bool init;

	public void Set(float _time, T begin_value, T end_value, AnimationCurve ease_curve = null, T add_value = default(T), AnimationCurve add_curve = null)
	{
		time = _time;
		beginValue = begin_value;
		endValue = end_value;
		if (ease_curve != null)
		{
			easeCurve = ease_curve;
		}
		else
		{
			easeCurve = Curves.easeInOut;
		}
		addValue = add_value;
		addCurve = add_curve;
		init = true;
	}

	public void Set(float _time, T end_value, AnimationCurve ease_curve = null, T add_value = default(T), AnimationCurve add_curve = null)
	{
		Set(_time, Get(), end_value, ease_curve, add_value, add_curve);
	}

	public void Set(T value)
	{
		Set(0f, value, value);
	}

	public void Play()
	{
		if (time == 0f)
		{
			Stop();
			return;
		}
		play = true;
		turn = false;
		nowTime = 0f;
	}

	public void Stop()
	{
		play = false;
	}

	public bool IsPlaying()
	{
		if (init)
		{
			return true;
		}
		if (play)
		{
			return time > 0f;
		}
		return false;
	}

	public void Update(float dt)
	{
		init = false;
		if (!IsPlaying())
		{
			nowValue = endValue;
			return;
		}
		switch (loopType)
		{
		case LOOP.NONE:
			nowTime += dt;
			if (nowTime >= time)
			{
				nowValue = endValue;
				Stop();
				return;
			}
			break;
		case LOOP.REPETE:
			nowTime += dt;
			if (nowTime >= time)
			{
				nowTime %= time;
			}
			break;
		case LOOP.PINGPONG:
			if (!turn)
			{
				nowTime += dt;
				if (nowTime >= time)
				{
					nowTime = time - nowTime % time;
					turn = true;
				}
			}
			else
			{
				nowTime -= dt;
				if (nowTime <= 0f)
				{
					nowTime = (0f - nowTime) % time;
					turn = false;
				}
			}
			break;
		}
		float num = nowTime / time;
		float r = (easeCurve == null) ? num : easeCurve.Evaluate(num);
		Calc(num, r);
	}

	public T Update()
	{
		Update(Time.deltaTime);
		return Get();
	}

	public T Get()
	{
		return nowValue;
	}

	protected virtual void Calc(float t, float r)
	{
	}
}
