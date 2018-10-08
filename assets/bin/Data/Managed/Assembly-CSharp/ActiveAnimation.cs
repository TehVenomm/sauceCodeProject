using AnimationOrTween;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("NGUI/Internal/Active Animation")]
public class ActiveAnimation
{
	public static ActiveAnimation current;

	public List<EventDelegate> onFinished = new List<EventDelegate>();

	[HideInInspector]
	public GameObject eventReceiver;

	[HideInInspector]
	public string callWhenFinished;

	private Animation mAnim;

	private Direction mLastDirection;

	private Direction mDisableDirection;

	private bool mNotify;

	private Animator mAnimator;

	private string mClip = string.Empty;

	private float playbackTime
	{
		get
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			AnimatorStateInfo currentAnimatorStateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
			return Mathf.Clamp01(currentAnimatorStateInfo.get_normalizedTime());
		}
	}

	public bool isPlaying
	{
		get
		{
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0077: Expected O, but got Unknown
			if (mAnim == null)
			{
				if (mAnimator != null)
				{
					if (mLastDirection == Direction.Reverse)
					{
						if (playbackTime == 0f)
						{
							return false;
						}
					}
					else if (playbackTime == 1f)
					{
						return false;
					}
					return true;
				}
				return false;
			}
			foreach (AnimationState item in mAnim)
			{
				AnimationState val = item;
				if (mAnim.IsPlaying(val.get_name()))
				{
					if (mLastDirection == Direction.Forward)
					{
						if (val.get_time() < val.get_length())
						{
							return true;
						}
					}
					else
					{
						if (mLastDirection != Direction.Reverse)
						{
							return true;
						}
						if (val.get_time() > 0f)
						{
							return true;
						}
					}
				}
			}
			return false;
		}
	}

	public ActiveAnimation()
		: this()
	{
	}

	public void Finish()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		if (mAnim != null)
		{
			foreach (AnimationState item in mAnim)
			{
				AnimationState val = item;
				if (mLastDirection == Direction.Forward)
				{
					val.set_time(val.get_length());
				}
				else if (mLastDirection == Direction.Reverse)
				{
					val.set_time(0f);
				}
			}
			mAnim.Sample();
		}
		else if (mAnimator != null)
		{
			mAnimator.Play(mClip, 0, (mLastDirection != Direction.Forward) ? 0f : 1f);
		}
	}

	public void Reset()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		if (mAnim != null)
		{
			foreach (AnimationState item in mAnim)
			{
				AnimationState val = item;
				if (mLastDirection == Direction.Reverse)
				{
					val.set_time(val.get_length());
				}
				else if (mLastDirection == Direction.Forward)
				{
					val.set_time(0f);
				}
			}
		}
		else if (mAnimator != null)
		{
			mAnimator.Play(mClip, 0, (mLastDirection != Direction.Reverse) ? 0f : 1f);
		}
	}

	private void Start()
	{
		if (eventReceiver != null && EventDelegate.IsValid(onFinished))
		{
			eventReceiver = null;
			callWhenFinished = null;
		}
	}

	private void Update()
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		float deltaTime = RealTime.deltaTime;
		if (deltaTime != 0f)
		{
			if (mAnimator != null)
			{
				mAnimator.Update((mLastDirection != Direction.Reverse) ? deltaTime : (0f - deltaTime));
				if (isPlaying)
				{
					return;
				}
				mAnimator.set_enabled(false);
				this.set_enabled(false);
			}
			else
			{
				if (!(mAnim != null))
				{
					this.set_enabled(false);
					return;
				}
				bool flag = false;
				foreach (AnimationState item in mAnim)
				{
					AnimationState val = item;
					if (mAnim.IsPlaying(val.get_name()))
					{
						float num = val.get_speed() * deltaTime;
						AnimationState obj = val;
						obj.set_time(obj.get_time() + num);
						if (num < 0f)
						{
							if (val.get_time() > 0f)
							{
								flag = true;
							}
							else
							{
								val.set_time(0f);
							}
						}
						else if (val.get_time() < val.get_length())
						{
							flag = true;
						}
						else
						{
							val.set_time(val.get_length());
						}
					}
				}
				mAnim.Sample();
				if (flag)
				{
					return;
				}
				this.set_enabled(false);
			}
			if (mNotify)
			{
				mNotify = false;
				if (current == null)
				{
					current = this;
					EventDelegate.Execute(onFinished);
					if (eventReceiver != null && !string.IsNullOrEmpty(callWhenFinished))
					{
						eventReceiver.SendMessage(callWhenFinished, 1);
					}
					current = null;
				}
				if (mDisableDirection != 0 && mLastDirection == mDisableDirection)
				{
					NGUITools.SetActive(this.get_gameObject(), false);
				}
			}
		}
	}

	private void Play(string clipName, Direction playDirection)
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		if (playDirection == Direction.Toggle)
		{
			playDirection = ((mLastDirection != Direction.Forward) ? Direction.Forward : Direction.Reverse);
		}
		if (mAnim != null)
		{
			this.set_enabled(true);
			mAnim.set_enabled(false);
			if (string.IsNullOrEmpty(clipName))
			{
				if (!mAnim.get_isPlaying())
				{
					mAnim.Play();
				}
			}
			else if (!mAnim.IsPlaying(clipName))
			{
				mAnim.Play(clipName);
			}
			foreach (AnimationState item in mAnim)
			{
				AnimationState val = item;
				if (string.IsNullOrEmpty(clipName) || val.get_name() == clipName)
				{
					float num = Mathf.Abs(val.get_speed());
					val.set_speed(num * (float)playDirection);
					if (playDirection == Direction.Reverse && val.get_time() == 0f)
					{
						val.set_time(val.get_length());
					}
					else if (playDirection == Direction.Forward && val.get_time() == val.get_length())
					{
						val.set_time(0f);
					}
				}
			}
			mLastDirection = playDirection;
			mNotify = true;
			mAnim.Sample();
		}
		else if (mAnimator != null)
		{
			if (this.get_enabled() && isPlaying && mClip == clipName)
			{
				mLastDirection = playDirection;
			}
			else
			{
				this.set_enabled(true);
				mNotify = true;
				mLastDirection = playDirection;
				mClip = clipName;
				mAnimator.Play(mClip, 0, (playDirection != Direction.Forward) ? 1f : 0f);
			}
		}
	}

	public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection, EnableCondition enableBeforePlay, DisableCondition disableCondition)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if (!NGUITools.GetActive(anim.get_gameObject()))
		{
			if (enableBeforePlay != EnableCondition.EnableThenPlay)
			{
				return null;
			}
			NGUITools.SetActive(anim.get_gameObject(), true);
			UIPanel[] componentsInChildren = anim.get_gameObject().GetComponentsInChildren<UIPanel>();
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				componentsInChildren[i].Refresh();
			}
		}
		ActiveAnimation activeAnimation = anim.GetComponent<ActiveAnimation>();
		if (activeAnimation == null)
		{
			activeAnimation = anim.get_gameObject().AddComponent<ActiveAnimation>();
		}
		activeAnimation.mAnim = anim;
		activeAnimation.mDisableDirection = (Direction)disableCondition;
		activeAnimation.onFinished.Clear();
		activeAnimation.Play(clipName, playDirection);
		if (activeAnimation.mAnim != null)
		{
			activeAnimation.mAnim.Sample();
		}
		else if (activeAnimation.mAnimator != null)
		{
			activeAnimation.mAnimator.Update(0f);
		}
		return activeAnimation;
	}

	public static ActiveAnimation Play(Animation anim, string clipName, Direction playDirection)
	{
		return Play(anim, clipName, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
	}

	public static ActiveAnimation Play(Animation anim, Direction playDirection)
	{
		return Play(anim, null, playDirection, EnableCondition.DoNothing, DisableCondition.DoNotDisable);
	}

	public static ActiveAnimation Play(Animator anim, string clipName, Direction playDirection, EnableCondition enableBeforePlay, DisableCondition disableCondition)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Expected O, but got Unknown
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		if (enableBeforePlay != EnableCondition.IgnoreDisabledState && !NGUITools.GetActive(anim.get_gameObject()))
		{
			if (enableBeforePlay != EnableCondition.EnableThenPlay)
			{
				return null;
			}
			NGUITools.SetActive(anim.get_gameObject(), true);
			UIPanel[] componentsInChildren = anim.get_gameObject().GetComponentsInChildren<UIPanel>();
			int i = 0;
			for (int num = componentsInChildren.Length; i < num; i++)
			{
				componentsInChildren[i].Refresh();
			}
		}
		ActiveAnimation activeAnimation = anim.GetComponent<ActiveAnimation>();
		if (activeAnimation == null)
		{
			activeAnimation = anim.get_gameObject().AddComponent<ActiveAnimation>();
		}
		activeAnimation.mAnimator = anim;
		activeAnimation.mDisableDirection = (Direction)disableCondition;
		activeAnimation.onFinished.Clear();
		activeAnimation.Play(clipName, playDirection);
		if (activeAnimation.mAnim != null)
		{
			activeAnimation.mAnim.Sample();
		}
		else if (activeAnimation.mAnimator != null)
		{
			activeAnimation.mAnimator.Update(0f);
		}
		return activeAnimation;
	}
}
