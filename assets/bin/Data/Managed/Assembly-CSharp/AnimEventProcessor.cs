// Decompiled with JetBrains decompiler
// Type: AnimEventProcessor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
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

  public AnimEventProcessor(
    AnimEventData anim_event_data,
    Animator _animator,
    IAnimEvent _listener)
  {
    this.animEventData = anim_event_data;
    this.animator = _animator;
    this.listener = _listener;
  }

  public void Update()
  {
    if (Object.op_Equality((Object) this.animator, (Object) null))
      return;
    bool flag = this.animator.IsInTransition(0);
    AnimatorStateInfo info = flag ? this.animator.GetNextAnimatorStateInfo(0) : this.animator.GetCurrentAnimatorStateInfo(0);
    if (this.curHash != ((AnimatorStateInfo) ref info).fullPathHash && !this.waitChange || this.curHash == ((AnimatorStateInfo) ref info).fullPathHash && this.waitChange)
    {
      if (this.waitChange)
      {
        this.waitChange = false;
      }
      else
      {
        if (this.curDatas != null && (double) this.lastSpeed > 0.0)
          this.Forward(Time.deltaTime * this.lastSpeed + this.lastNormalizedTime - this.beginTime + this.curMargin);
        this.OnChangeAnim(((AnimatorStateInfo) ref info).fullPathHash);
      }
      if (this.curDatas == null)
        return;
      if (flag)
      {
        AnimatorClipInfo[] animatorClipInfo = this.animator.GetNextAnimatorClipInfo(0);
        if (animatorClipInfo.Length != 0)
        {
          this.ChangeAnimClip(ref info, ((AnimatorClipInfo) ref animatorClipInfo[0]).clip);
        }
        else
        {
          this.changeDelay = true;
          this.changeTransition = true;
          return;
        }
      }
      else
      {
        this.changeDelay = true;
        this.changeTransition = false;
        return;
      }
    }
    else if (this.changeDelay)
    {
      AnimatorClipInfo[] animatorClipInfoArray = !this.changeTransition ? this.animator.GetCurrentAnimatorClipInfo(0) : this.animator.GetNextAnimatorClipInfo(0);
      if (animatorClipInfoArray.Length == 0)
        return;
      this.ChangeAnimClip(ref info, ((AnimatorClipInfo) ref animatorClipInfoArray[0]).clip);
    }
    if (this.waitChange || this.curDatas == null || flag && this.animator.GetCurrentAnimatorStateInfo(0).Equals((object) this.animator.GetNextAnimatorStateInfo(0)))
      return;
    float num = ((AnimatorStateInfo) ref info).normalizedTime * this.curTimeScale;
    if ((double) this.animator.speed < 0.0)
      return;
    if ((double) num >= (double) this.lastTime && ((AnimatorStateInfo) ref info).loop)
    {
      this.Forward(this.curLength);
      this.beginTime = this.lastTime;
      this.lastTime += this.curLength;
      this.curTime = 0.0f;
      this.curIndex = 0;
    }
    else if ((double) this.lastNormalizedTime > (double) num)
    {
      this.ExecuteLastEvent(false);
      this.beginTime = 0.0f;
      this.lastTime = this.curLength;
      this.curTime = 0.0f;
      this.curIndex = 0;
    }
    this.lastNormalizedTime = num;
    this.lastSpeed = this.animator.speed;
    this.Forward(num - this.beginTime + this.curMargin);
  }

  private void OnChangeAnim(int motion_hash, bool cross_fade = false)
  {
    this.ExecuteLastEvent();
    this.curHash = motion_hash;
    this.curDatas = this.animEventData.GetEventDatas(this.curHash);
    this.curTime = 0.0f;
    this.curIndex = 0;
    if (cross_fade)
      this.ignoreEventFlag = false;
    if (this.curDatas == null)
      return;
    this.ExecuteFirstEvent();
    this.Forward(0.0f);
  }

  private void ChangeAnimClip(ref AnimatorStateInfo info, AnimationClip clip)
  {
    this.curLength = clip.length;
    this.curTimeScale = this.curLength;
    this.curMargin = (float) (0.004999999888241291 * ((double) clip.length / (double) ((AnimatorStateInfo) ref info).length));
    this.beginTime = 0.0f;
    this.lastTime = this.curLength;
    this.lastNormalizedTime = 0.0f;
    this.lastSpeed = 0.0f;
    this.changeDelay = false;
    this.changeTransition = false;
  }

  private void Forward(float time)
  {
    if (this.curDatas == null || this.ignoreEventFlag || (double) this.curTime > (double) time)
      return;
    while (this.curIndex < this.curDatas.Length && (double) time >= (double) this.curDatas[this.curIndex].time)
    {
      if ((double) this.curDatas[this.curIndex].time == -3.4028234663852886E+38)
      {
        ++this.curIndex;
      }
      else
      {
        this.StartInterruptionCheck();
        this.listener.OnAnimEvent(this.curDatas[this.curIndex]);
        if (this.EndInterruptionCheck())
          return;
        ++this.curIndex;
      }
    }
    this.curTime = time;
  }

  private void ExecuteFirstEvent()
  {
    if (this.curDatas == null || this.ignoreEventFlag)
      return;
    for (int length = this.curDatas.Length; this.curIndex < length && (double) this.curDatas[this.curIndex].time == -3.4028234663852886E+38; ++this.curIndex)
    {
      this.StartInterruptionCheck();
      this.listener.OnAnimEvent(this.curDatas[this.curIndex]);
      if (this.EndInterruptionCheck())
        break;
    }
  }

  public void ExecuteLastEvent(bool delete_data = true)
  {
    if (this.curDatas == null || this.ignoreEventFlag)
      return;
    this.SetInterruptionCheck();
    AnimEventData.EventData[] curDatas = this.curDatas;
    if (delete_data)
      this.curDatas = (AnimEventData.EventData[]) null;
    int length = curDatas.Length;
    if (length <= 0 || (double) curDatas[length - 1].time != 3.4028230607370965E+38)
      return;
    int index = length - 1;
    while (index > 0 && (double) curDatas[index - 1].time == 3.4028230607370965E+38)
      --index;
    for (; index < length; ++index)
    {
      this.StartInterruptionCheck();
      this.listener.OnAnimEvent(curDatas[index]);
      if (this.EndInterruptionCheck())
        break;
    }
  }

  public void IgnoreEventByNextAnim()
  {
    this.SetInterruptionCheck();
    this.ignoreEventFlag = true;
  }

  public int GetWaitMotionHash() => !this.waitChange ? 0 : this.curHash;

  public void CrossFade(int motion_hash, float transition_time)
  {
    this.SetInterruptionCheck();
    this.animator.CrossFadeInFixedTime(motion_hash, transition_time, -1, 0.0f);
    this.OnChangeAnim(motion_hash, true);
    this.waitChange = true;
  }

  public void ChangeAnimCtrl(RuntimeAnimatorController anim_ctrl, AnimEventData anim_event)
  {
    if (Object.op_Equality((Object) anim_ctrl, (Object) null) || Object.op_Equality((Object) anim_event, (Object) null))
      return;
    this.ExecuteLastEvent();
    this.curHash = 0;
    this.curDatas = (AnimEventData.EventData[]) null;
    this.curTime = 0.0f;
    this.curIndex = 0;
    this.ignoreEventFlag = false;
    this.animator.runtimeAnimatorController = anim_ctrl;
    this.animator.Rebind();
    this.animEventData = anim_event;
  }

  private void SetInterruptionCheck()
  {
    int index = 0;
    for (int count = this.interruptionCheckFlags.Count; index < count; ++index)
      this.interruptionCheckFlags[index] = true;
  }

  private void StartInterruptionCheck() => this.interruptionCheckFlags.Add(false);

  private bool EndInterruptionCheck()
  {
    int num = this.interruptionCheckFlags[this.interruptionCheckFlags.Count - 1] ? 1 : 0;
    this.interruptionCheckFlags.RemoveAt(this.interruptionCheckFlags.Count - 1);
    return num != 0;
  }

  public List<AnimEventData.EventData> ListUpEventData(AnimEventFormat.ID targetID)
  {
    List<AnimEventData.EventData> eventDataList = new List<AnimEventData.EventData>();
    foreach (AnimEventData.AnimData animation in this.animEventData.animations)
    {
      foreach (AnimEventData.EventData eventData in animation.events)
      {
        if (eventData.id == targetID)
          eventDataList.Add(eventData);
      }
    }
    return eventDataList;
  }
}
