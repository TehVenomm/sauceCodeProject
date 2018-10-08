using System;
using UnityEngine;

public class SlimeAnimBase<T> where T : new()
{
	protected AnimationCurve animCurve;

	protected AnimationCurve blendCurve;

	protected float playTime;

	protected float nowTime;

	protected T blendParam;

	protected float blendEndTime;

	protected bool isBlend;

	private Action callback;

	public bool isPlaying
	{
		get;
		protected set;
	}

	public virtual void InitAnim(AnimationCurve curve, float time, bool is_blend, T now_param, Action cb)
	{
		animCurve = curve;
		playTime = time;
		callback = cb;
		nowTime = 0f;
		isBlend = is_blend;
		blendParam = now_param;
		isPlaying = true;
	}

	public T Update()
	{
		T result = UpdateAnim();
		updatePlayTime();
		return result;
	}

	public virtual T UpdateAnim()
	{
		return new T();
	}

	public void SetBlendParam(AnimationCurve blend_curve, float end_time)
	{
		blendCurve = blend_curve;
		blendEndTime = end_time;
	}

	private void updatePlayTime()
	{
		nowTime += Time.get_deltaTime();
		if (playTime <= nowTime)
		{
			AnimFinish();
		}
	}

	protected void AnimFinish()
	{
		if (isPlaying)
		{
			isPlaying = false;
			if (callback != null)
			{
				callback.Invoke();
			}
		}
	}

	public void Terminate()
	{
		isPlaying = false;
	}
}
