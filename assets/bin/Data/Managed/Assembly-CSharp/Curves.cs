using UnityEngine;

public static class Curves
{
	public static readonly AnimationCurve easeLinear = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	public static readonly AnimationCurve easeInOut = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	public static readonly AnimationCurve easeIn = CreateEaseInCurve();

	public static readonly AnimationCurve sinCurve = CreateSinCurve();

	public static readonly AnimationCurve sinHalfCurve = CreateSinHalfCurve();

	public static readonly AnimationCurve arcHalfCurve = CreateArcHalfCurve();

	public static AnimationCurve CreateSinCurve()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Expected O, but got Unknown
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		AnimationCurve val = new AnimationCurve();
		val.AddKey(new Keyframe(0.25f, 1f, 0f, 0f));
		val.AddKey(new Keyframe(0.75f, -1f, 0f, 0f));
		val.set_preWrapMode(4);
		val.set_postWrapMode(4);
		return val;
	}

	public static AnimationCurve CreateSinHalfCurve()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Expected O, but got Unknown
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		AnimationCurve val = new AnimationCurve();
		val.AddKey(new Keyframe(0f, 0f, 0f, 0f));
		val.AddKey(new Keyframe(0.5f, 1f, 0f, 0f));
		val.set_preWrapMode(4);
		val.set_postWrapMode(4);
		return val;
	}

	public static AnimationCurve CreateEaseInCurve()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Expected O, but got Unknown
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		AnimationCurve val = new AnimationCurve();
		val.AddKey(new Keyframe(0f, 0f, 0f, 0f));
		val.AddKey(new Keyframe(1f, 1f, 2f, 1f));
		return val;
	}

	public static AnimationCurve CreateArcHalfCurve()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Expected O, but got Unknown
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		AnimationCurve val = new AnimationCurve();
		val.AddKey(new Keyframe(0f, 0f, 0f, 4f));
		val.AddKey(new Keyframe(0.5f, 1f, 0f, 0f));
		val.AddKey(new Keyframe(1f, 0f, -4f, 0f));
		return val;
	}
}
