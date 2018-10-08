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
		AnimationCurve animationCurve = new AnimationCurve();
		animationCurve.AddKey(new Keyframe(0.25f, 1f, 0f, 0f));
		animationCurve.AddKey(new Keyframe(0.75f, -1f, 0f, 0f));
		animationCurve.preWrapMode = WrapMode.PingPong;
		animationCurve.postWrapMode = WrapMode.PingPong;
		return animationCurve;
	}

	public static AnimationCurve CreateSinHalfCurve()
	{
		AnimationCurve animationCurve = new AnimationCurve();
		animationCurve.AddKey(new Keyframe(0f, 0f, 0f, 0f));
		animationCurve.AddKey(new Keyframe(0.5f, 1f, 0f, 0f));
		animationCurve.preWrapMode = WrapMode.PingPong;
		animationCurve.postWrapMode = WrapMode.PingPong;
		return animationCurve;
	}

	public static AnimationCurve CreateEaseInCurve()
	{
		AnimationCurve animationCurve = new AnimationCurve();
		animationCurve.AddKey(new Keyframe(0f, 0f, 0f, 0f));
		animationCurve.AddKey(new Keyframe(1f, 1f, 2f, 1f));
		return animationCurve;
	}

	public static AnimationCurve CreateArcHalfCurve()
	{
		AnimationCurve animationCurve = new AnimationCurve();
		animationCurve.AddKey(new Keyframe(0f, 0f, 0f, 4f));
		animationCurve.AddKey(new Keyframe(0.5f, 1f, 0f, 0f));
		animationCurve.AddKey(new Keyframe(1f, 0f, -4f, 0f));
		return animationCurve;
	}
}
