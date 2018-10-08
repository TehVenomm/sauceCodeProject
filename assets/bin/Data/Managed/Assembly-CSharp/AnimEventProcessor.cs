using System.Collections.Generic;
using UnityEngine;

public class AnimEventProcessor
{
	private const float FORWARD_TIME_MARGIN = 0.005f;

	private AnimEventData animEventData;

	private Animator animator;

	private IAnimEvent listener;

	private int curHash;

	private float curTime;

	private float curLength;

	private float curTimeScale;

	private float curMargin;

	private float beginTime;

	private float lastTime;

	private float lastNormalizedTime;

	private float lastSpeed;

	private AnimEventData.EventData[] curDatas;

	private int curIndex;

	private AnimationClip curClip;

	private bool ignoreEventFlag;

	private bool changeTransition;

	private bool changeDelay;

	private bool waitChange;

	private List<bool> interruptionCheckFlags = new List<bool>();

	public AnimEventProcessor(AnimEventData anim_event_data, Animator _animator, IAnimEvent _listener)
	{
		animEventData = anim_event_data;
		animator = _animator;
		listener = _listener;
	}

	public void Update()
	{
		if (!((Object)animator == (Object)null))
		{
			bool flag = animator.IsInTransition(0);
			AnimatorStateInfo info = (!flag) ? animator.GetCurrentAnimatorStateInfo(0) : animator.GetNextAnimatorStateInfo(0);
			if ((curHash != info.fullPathHash && !waitChange) || (curHash == info.fullPathHash && waitChange))
			{
				if (waitChange)
				{
					waitChange = false;
				}
				else
				{
					if (curDatas != null && lastSpeed > 0f)
					{
						float num = Time.deltaTime * lastSpeed + lastNormalizedTime;
						Forward(num - beginTime + curMargin);
					}
					OnChangeAnim(info.fullPathHash, false);
				}
				if (curDatas == null)
				{
					return;
				}
				if (!flag)
				{
					changeDelay = true;
					changeTransition = false;
					return;
				}
				AnimatorClipInfo[] nextAnimatorClipInfo = animator.GetNextAnimatorClipInfo(0);
				if (nextAnimatorClipInfo.Length == 0)
				{
					changeDelay = true;
					changeTransition = true;
					return;
				}
				ChangeAnimClip(ref info, nextAnimatorClipInfo[0].clip);
			}
			else if (changeDelay)
			{
				AnimatorClipInfo[] array = (!changeTransition) ? animator.GetCurrentAnimatorClipInfo(0) : animator.GetNextAnimatorClipInfo(0);
				if (array.Length <= 0)
				{
					return;
				}
				ChangeAnimClip(ref info, array[0].clip);
			}
			if (!waitChange && curDatas != null && (!flag || !animator.GetCurrentAnimatorStateInfo(0).Equals(animator.GetNextAnimatorStateInfo(0))))
			{
				float num2 = info.normalizedTime * curTimeScale;
				if (animator.speed >= 0f)
				{
					if (num2 >= lastTime && info.loop)
					{
						Forward(curLength);
						beginTime = lastTime;
						lastTime += curLength;
						curTime = 0f;
						curIndex = 0;
					}
					else if (lastNormalizedTime > num2)
					{
						ExecuteLastEvent(false);
						beginTime = 0f;
						lastTime = curLength;
						curTime = 0f;
						curIndex = 0;
					}
					lastNormalizedTime = num2;
					lastSpeed = animator.speed;
					Forward(num2 - beginTime + curMargin);
				}
			}
		}
	}

	private void OnChangeAnim(int motion_hash, bool cross_fade = false)
	{
		ExecuteLastEvent(true);
		curHash = motion_hash;
		curDatas = animEventData.GetEventDatas(curHash);
		curTime = 0f;
		curIndex = 0;
		if (cross_fade)
		{
			ignoreEventFlag = false;
		}
		if (curDatas != null)
		{
			ExecuteFirstEvent();
			Forward(0f);
		}
	}

	private void ChangeAnimClip(ref AnimatorStateInfo info, AnimationClip clip)
	{
		curLength = clip.length;
		curTimeScale = curLength;
		curMargin = 0.005f * (clip.length / info.length);
		beginTime = 0f;
		lastTime = curLength;
		lastNormalizedTime = 0f;
		lastSpeed = 0f;
		changeDelay = false;
		changeTransition = false;
	}

	private void Forward(float time)
	{
		if (curDatas != null && !ignoreEventFlag && curTime <= time)
		{
			while (curIndex < curDatas.Length && !(time < curDatas[curIndex].time))
			{
				if (curDatas[curIndex].time == -3.40282347E+38f)
				{
					curIndex++;
				}
				else
				{
					StartInterruptionCheck();
					listener.OnAnimEvent(curDatas[curIndex]);
					if (EndInterruptionCheck())
					{
						return;
					}
					curIndex++;
				}
			}
			curTime = time;
		}
	}

	private void ExecuteFirstEvent()
	{
		if (curDatas != null && !ignoreEventFlag)
		{
			int num = curDatas.Length;
			while (curIndex < num && curDatas[curIndex].time == -3.40282347E+38f)
			{
				StartInterruptionCheck();
				listener.OnAnimEvent(curDatas[curIndex]);
				if (EndInterruptionCheck())
				{
					break;
				}
				curIndex++;
			}
		}
	}

	public void ExecuteLastEvent(bool delete_data = true)
	{
		if (curDatas != null && !ignoreEventFlag)
		{
			SetInterruptionCheck();
			AnimEventData.EventData[] array = curDatas;
			if (delete_data)
			{
				curDatas = null;
			}
			int num = array.Length;
			if (num > 0 && array[num - 1].time == 3.402823E+38f)
			{
				int i = num - 1;
				while (i > 0 && array[i - 1].time == 3.402823E+38f)
				{
					i--;
				}
				for (; i < num; i++)
				{
					StartInterruptionCheck();
					listener.OnAnimEvent(array[i]);
					if (EndInterruptionCheck())
					{
						break;
					}
				}
			}
		}
	}

	public void IgnoreEventByNextAnim()
	{
		SetInterruptionCheck();
		ignoreEventFlag = true;
	}

	public int GetWaitMotionHash()
	{
		if (!waitChange)
		{
			return 0;
		}
		return curHash;
	}

	public void CrossFade(int motion_hash, float transition_time)
	{
		SetInterruptionCheck();
		animator.CrossFadeInFixedTime(motion_hash, transition_time, -1, 0f);
		OnChangeAnim(motion_hash, true);
		waitChange = true;
	}

	public void ChangeAnimCtrl(RuntimeAnimatorController anim_ctrl, AnimEventData anim_event)
	{
		if (!((Object)anim_ctrl == (Object)null) && !((Object)anim_event == (Object)null))
		{
			ExecuteLastEvent(true);
			curHash = 0;
			curDatas = null;
			curTime = 0f;
			curIndex = 0;
			ignoreEventFlag = false;
			animator.runtimeAnimatorController = anim_ctrl;
			animator.Rebind();
			animEventData = anim_event;
		}
	}

	private void SetInterruptionCheck()
	{
		int i = 0;
		for (int count = interruptionCheckFlags.Count; i < count; i++)
		{
			interruptionCheckFlags[i] = true;
		}
	}

	private void StartInterruptionCheck()
	{
		interruptionCheckFlags.Add(false);
	}

	private bool EndInterruptionCheck()
	{
		bool result = interruptionCheckFlags[interruptionCheckFlags.Count - 1];
		interruptionCheckFlags.RemoveAt(interruptionCheckFlags.Count - 1);
		return result;
	}

	public List<AnimEventData.EventData> ListUpEventData(AnimEventFormat.ID targetID)
	{
		List<AnimEventData.EventData> list = new List<AnimEventData.EventData>();
		AnimEventData.AnimData[] animations = animEventData.animations;
		for (int i = 0; i < animations.Length; i++)
		{
			AnimEventData.EventData[] events = animations[i].events;
			foreach (AnimEventData.EventData eventData in events)
			{
				if (eventData.id == targetID)
				{
					list.Add(eventData);
				}
			}
		}
		return list;
	}
}
