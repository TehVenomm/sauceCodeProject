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
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		if (animator == null)
		{
			return;
		}
		bool flag = animator.IsInTransition(0);
		AnimatorStateInfo info = (!flag) ? animator.GetCurrentAnimatorStateInfo(0) : animator.GetNextAnimatorStateInfo(0);
		if ((curHash != info.get_fullPathHash() && !waitChange) || (curHash == info.get_fullPathHash() && waitChange))
		{
			if (waitChange)
			{
				waitChange = false;
			}
			else
			{
				if (curDatas != null && lastSpeed > 0f)
				{
					float num = Time.get_deltaTime() * lastSpeed + lastNormalizedTime;
					Forward(num - beginTime + curMargin);
				}
				OnChangeAnim(info.get_fullPathHash());
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
			ChangeAnimClip(ref info, nextAnimatorClipInfo[0].get_clip());
		}
		else if (changeDelay)
		{
			AnimatorClipInfo[] array = (!changeTransition) ? animator.GetCurrentAnimatorClipInfo(0) : animator.GetNextAnimatorClipInfo(0);
			if (array.Length <= 0)
			{
				return;
			}
			ChangeAnimClip(ref info, array[0].get_clip());
		}
		if (waitChange || curDatas == null)
		{
			return;
		}
		if (flag)
		{
			AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
			if (((object)currentAnimatorStateInfo).Equals((object)animator.GetNextAnimatorStateInfo(0)))
			{
				return;
			}
		}
		float num2 = info.get_normalizedTime() * curTimeScale;
		if (animator.get_speed() >= 0f)
		{
			if (num2 >= lastTime && info.get_loop())
			{
				Forward(curLength);
				beginTime = lastTime;
				lastTime += curLength;
				curTime = 0f;
				curIndex = 0;
			}
			else if (lastNormalizedTime > num2)
			{
				ExecuteLastEvent(delete_data: false);
				beginTime = 0f;
				lastTime = curLength;
				curTime = 0f;
				curIndex = 0;
			}
			lastNormalizedTime = num2;
			lastSpeed = animator.get_speed();
			Forward(num2 - beginTime + curMargin);
		}
	}

	private void OnChangeAnim(int motion_hash, bool cross_fade = false)
	{
		ExecuteLastEvent();
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
		curLength = clip.get_length();
		curTimeScale = curLength;
		curMargin = 0.005f * (clip.get_length() / info.get_length());
		beginTime = 0f;
		lastTime = curLength;
		lastNormalizedTime = 0f;
		lastSpeed = 0f;
		changeDelay = false;
		changeTransition = false;
	}

	private void Forward(float time)
	{
		if (curDatas == null || ignoreEventFlag || !(curTime <= time))
		{
			return;
		}
		while (curIndex < curDatas.Length && !(time < curDatas[curIndex].time))
		{
			if (curDatas[curIndex].time == float.MinValue)
			{
				curIndex++;
				continue;
			}
			StartInterruptionCheck();
			listener.OnAnimEvent(curDatas[curIndex]);
			if (EndInterruptionCheck())
			{
				return;
			}
			curIndex++;
		}
		curTime = time;
	}

	private void ExecuteFirstEvent()
	{
		if (curDatas == null || ignoreEventFlag)
		{
			return;
		}
		int num = curDatas.Length;
		while (curIndex < num && curDatas[curIndex].time == float.MinValue)
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

	public void ExecuteLastEvent(bool delete_data = true)
	{
		if (curDatas == null || ignoreEventFlag)
		{
			return;
		}
		SetInterruptionCheck();
		AnimEventData.EventData[] array = curDatas;
		if (delete_data)
		{
			curDatas = null;
		}
		int num = array.Length;
		if (num <= 0 || array[num - 1].time != 3.402823E+38f)
		{
			return;
		}
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
		OnChangeAnim(motion_hash, cross_fade: true);
		waitChange = true;
	}

	public void ChangeAnimCtrl(RuntimeAnimatorController anim_ctrl, AnimEventData anim_event)
	{
		if (!(anim_ctrl == null) && !(anim_event == null))
		{
			ExecuteLastEvent();
			curHash = 0;
			curDatas = null;
			curTime = 0f;
			curIndex = 0;
			ignoreEventFlag = false;
			animator.set_runtimeAnimatorController(anim_ctrl);
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
		interruptionCheckFlags.Add(item: false);
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
