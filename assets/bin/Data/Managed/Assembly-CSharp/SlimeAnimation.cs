using System;
using UnityEngine;

public class SlimeAnimation
{
	private class SlimeParamAnimator<T, T2> where T : SlimeAnimBase<T2>, new()where T2 : new()
	{
		private T anim;

		public SlimeParamAnimator(float time, float start = 0f, float end = 1f)
		{
			anim = new T();
			anim.SetBlendParam(AnimationCurve.Linear(0f, start, time, end), time);
		}

		public T2 Update()
		{
			return anim.Update();
		}

		public void SetAnimation(AnimationCurve curve, float time, bool is_blend, T2 param, Action cb)
		{
			anim.InitAnim(curve, time, is_blend, param, cb);
		}

		public bool IsPlaying()
		{
			return anim.isPlaying;
		}

		public void Terminate()
		{
			anim.Terminate();
		}

		public T GetAnimData()
		{
			return anim;
		}
	}

	private SlimeParamAnimator<SlimePosAnim, Vector3> posAnimator;

	private SlimeParamAnimator<SlimeScaleAnim, Vector3> scaleAnimator;

	private SlimeParamAnimator<SlimeColorAnim, Color> colorAnimator;

	private bool isFadeOut;

	private SlimeController slime;

	private Transform slimeTransform;

	private Material slimeMaterial;

	private const float INTERPOLATION_TIME_POS = 0.2f;

	private const float INTERPOLATION_TIME_SCALE = 0.1f;

	private const float INTERPOLATION_TIME_COLOR = 0.5f;

	private const float FADEIN_ALPHA_MIN = 0f;

	private const float FADEIN_ALPHA_MAX = 0.5f;

	private const float FADEOUT_ALPHA_MIN = 0f;

	private const float FADEOUT_ALPHA_MAX = 0.5f;

	private const float CRUSH_ALPHA_MIN = 0f;

	private const float CRUSH_ALPHA_MAX = 0.5f;

	private const float NON_ANIM_TIME = 1f;

	private readonly AnimationCurve SLIME_ANIM_CURVE_ZERO = AnimationCurve.Linear(0f, 0f, 0f, 0f);

	private readonly AnimationCurve SLIME_ANIM_CURVE_ONE = AnimationCurve.Linear(0f, 1f, 0f, 1f);

	private readonly AnimationCurve SLIME_ANIM_CURVE_HALF = AnimationCurve.Linear(0f, 0.5f, 0f, 0.5f);

	public SlimeAnimation(SlimeController slime_controller)
	{
		slime = slime_controller;
		slimeTransform = slime.transform;
		slimeMaterial = slime.GetComponent<Renderer>().material;
		posAnimator = new SlimeParamAnimator<SlimePosAnim, Vector3>(0.2f);
		scaleAnimator = new SlimeParamAnimator<SlimeScaleAnim, Vector3>(0.1f);
		colorAnimator = new SlimeParamAnimator<SlimeColorAnim, Color>(0.5f);
	}

	public void Update()
	{
		if (posAnimator.IsPlaying())
		{
			slimeTransform.localPosition = posAnimator.Update();
		}
		if (scaleAnimator.IsPlaying())
		{
			slimeTransform.localScale = scaleAnimator.Update();
		}
		if (colorAnimator.IsPlaying())
		{
			Color color = colorAnimator.Update();
			slimeMaterial.color = color;
			if (color.a <= 0.01f && isFadeOut)
			{
				slime.SetInvisible();
			}
			else
			{
				slime.SetVisible();
			}
		}
		else if (slime.IsVisible() && slimeMaterial.color.a > 0f && isFadeOut)
		{
			slimeMaterial.color = colorAnimator.Update();
			slime.SetInvisible();
		}
	}

	public bool IsPlaying()
	{
		if (!posAnimator.IsPlaying() && !scaleAnimator.IsPlaying() && !colorAnimator.IsPlaying())
		{
			return false;
		}
		return true;
	}

	public void TouchOn(Action CallBackPos = null, Action CallBackScale = null, Action CallBackColor = null)
	{
		posAnimator.SetAnimation(SLIME_ANIM_CURVE_ZERO, 1f, is_blend: true, slimeTransform.localPosition, CallBackPos);
		scaleAnimator.SetAnimation(slime.animFadeIn, slime.fadeInAnimTime, is_blend: true, slimeTransform.localScale, CallBackScale);
		colorAnimator.SetAnimation(AnimationCurve.Linear(0f, 0f, 1f, 0.5f), slime.fadeInColorAnimTime, is_blend: false, slimeMaterial.color, CallBackColor);
		isFadeOut = false;
	}

	public void TouchOff(Action CallBackPos = null, Action CallBackScale = null, Action CallBackColor = null)
	{
		posAnimator.SetAnimation(slime.animFadeOut, slime.fadeOutAnimTime, is_blend: true, slimeTransform.localPosition, CallBackPos);
		scaleAnimator.SetAnimation(SLIME_ANIM_CURVE_ONE, 1f, is_blend: true, slimeTransform.localScale, CallBackScale);
		colorAnimator.SetAnimation(AnimationCurve.Linear(0f, 0.5f, 1f, 0f), slime.fadeOutColorAnimTime, is_blend: true, slimeMaterial.color, CallBackColor);
		isFadeOut = true;
	}

	public void Crush(Action CallBackPos = null, Action CallBackScale = null, Action CallBackColor = null)
	{
		posAnimator.SetAnimation(SLIME_ANIM_CURVE_ZERO, 1f, is_blend: false, slimeTransform.localPosition, CallBackPos);
		scaleAnimator.SetAnimation(slime.animCrush, slime.crushAnimTime, is_blend: true, slimeTransform.localScale, CallBackScale);
		colorAnimator.SetAnimation(AnimationCurve.Linear(0f, 0.5f, 1f, 0f), slime.crushColorAnimTime, is_blend: false, slimeMaterial.color, CallBackColor);
		isFadeOut = true;
	}

	public void ScaleUp(Action CallBackPos = null, Action CallBackScale = null, Action CallBackColor = null)
	{
		posAnimator.SetAnimation(SLIME_ANIM_CURVE_ZERO, 1f, is_blend: true, slimeTransform.localPosition, CallBackPos);
		scaleAnimator.SetAnimation(AnimationCurve.Linear(0f, 1f, slime.scaleupAnimTime, slime.scaleupAnimMaxScale), slime.scaleupAnimTime, is_blend: true, slimeTransform.localScale, CallBackScale);
		colorAnimator.SetAnimation(SLIME_ANIM_CURVE_HALF, 1f, is_blend: false, slimeMaterial.color, CallBackColor);
		isFadeOut = false;
	}

	public void ScaleUpDown(Action CallBackPos = null, Action CallBackScale = null, Action CallBackColor = null)
	{
		posAnimator.SetAnimation(SLIME_ANIM_CURVE_ZERO, 1f, is_blend: true, slimeTransform.localPosition, CallBackPos);
		scaleAnimator.SetAnimation(slime.animScaleUpDown, slime.scaleUpDownAnimTime, is_blend: true, slimeTransform.localScale, CallBackScale);
		colorAnimator.SetAnimation(SLIME_ANIM_CURVE_HALF, 1f, is_blend: false, slimeMaterial.color, CallBackColor);
		isFadeOut = false;
	}
}
