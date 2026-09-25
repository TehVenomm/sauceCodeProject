// Decompiled with JetBrains decompiler
// Type: HomeTopBonusTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

#nullable disable
public class HomeTopBonusTime : UIBehaviour
{
  private bool isClosedBonusTime = true;
  private UITweenCtrl tweenCtrl;
  private Transform myTrans;
  private bool isFirst = true;
  private bool isActiveTap = true;
  private static readonly HomeTopBonusTime.UI[] PlateUI = new HomeTopBonusTime.UI[4]
  {
    HomeTopBonusTime.UI.SPR_ALL,
    HomeTopBonusTime.UI.SPR_MORNING,
    HomeTopBonusTime.UI.SPR_AFTERNOON,
    HomeTopBonusTime.UI.SPR_NIGHT
  };
  private List<TimeSlotEvent> timeSlotEvents = new List<TimeSlotEvent>();
  private int currentIndex;
  private int previousIndex = -1;
  private string currentStartDate = "";
  private string currentEndDate = "";
  private bool isVisible;
  private float time;
  private static readonly float ChangeIndexTime = 2f;

  public void SetUp()
  {
    this.myTrans = ((Component) this).transform;
    this.tweenCtrl = ((Component) this.myTrans).GetComponent<UITweenCtrl>();
    this.isFirst = true;
    this.timeSlotEvents = MonoBehaviourSingleton<StatusManager>.I.timeSlotEvents;
    this.SetActive(this.myTrans, (Enum) HomeTopBonusTime.UI.OBJ_BONUS_TIME_VISIBLE, false);
  }

  public void UpdateBonusTime()
  {
    bool is_visible = this.IsBonusTime();
    if (this.isVisible != is_visible)
    {
      this.isVisible = is_visible;
      this.SetActive(this.myTrans, (Enum) HomeTopBonusTime.UI.OBJ_BONUS_TIME_VISIBLE, is_visible);
    }
    if (!is_visible)
      return;
    if (this.timeSlotEvents.Count >= 2)
      this.UpdateVisualIndex();
    if (this.previousIndex != this.currentIndex)
    {
      this.previousIndex = this.currentIndex;
      this.UpdateName(this.UpdatePlate());
    }
    if (!this.IsUpdateDate())
      return;
    this.UpdateDate();
    if (!this.isFirst)
      return;
    this.isActiveTap = true;
    this.TweenFirst();
    this.isFirst = false;
    this.isClosedBonusTime = false;
    this.time = 0.0f;
  }

  private void UpdateVisualIndex()
  {
    this.time += Time.deltaTime;
    if ((double) this.time < (double) HomeTopBonusTime.ChangeIndexTime)
      return;
    ++this.currentIndex;
    if (this.currentIndex >= this.timeSlotEvents.Count)
      this.currentIndex = 0;
    this.time = 0.0f;
  }

  public void OnTap()
  {
    if (!this.isActiveTap)
      return;
    this.TweenBonusTime(this.isClosedBonusTime);
    this.isClosedBonusTime = !this.isClosedBonusTime;
  }

  private void TweenFirst()
  {
    TweenPosition tween = ((Component) this.FindCtrl(this.myTrans, (Enum) HomeTopBonusTime.UI.OBJ_TWEEN_BONUS_TIME)).GetComponent<TweenPosition>();
    Vector3 defaultFrom = new Vector3();
    if (Object.op_Inequality((Object) tween, (Object) null))
    {
      defaultFrom = tween.from;
      Vector3 vector3 = defaultFrom;
      vector3.x = 350f;
      tween.from = vector3;
    }
    if (Object.op_Equality((Object) this.tweenCtrl, (Object) null))
      return;
    this.isActiveTap = false;
    this.tweenCtrl.Play(onFinished: (EventDelegate.Callback) (() =>
    {
      tween.from = defaultFrom;
      this.isActiveTap = true;
    }));
  }

  private void TweenBonusTime(bool isCloseBonusTime)
  {
    if (Object.op_Equality((Object) this.tweenCtrl, (Object) null))
      return;
    this.isActiveTap = false;
    this.tweenCtrl.Play(isCloseBonusTime, (EventDelegate.Callback) (() => this.isActiveTap = true));
  }

  private void UpdateName(Transform plate)
  {
    TimeSlotEvent timeSlotEvent = this.timeSlotEvents[this.currentIndex];
    this.SetLabelText(plate, (Enum) HomeTopBonusTime.UI.LBL_BONUS_TIME_NAME, timeSlotEvent.description);
  }

  private void UpdateDate()
  {
    string startData = this.timeSlotEvents[this.currentIndex].startData;
    string endDate = this.timeSlotEvents[this.currentIndex].endDate;
    this.currentStartDate = startData;
    this.currentEndDate = endDate;
    StringBuilder stringBuilder = new StringBuilder(13);
    stringBuilder.Append(this.GetTime(startData));
    stringBuilder.Append("　〜　");
    stringBuilder.Append(this.GetTime(endDate));
    this.SetLabelText(this.myTrans, (Enum) HomeTopBonusTime.UI.LBL_BONUS_TIME_DATE, stringBuilder.ToString());
  }

  private string GetTime(string date)
  {
    string[] strArray = date.Split(' ')[1].Split(':');
    int result1;
    int result2;
    return int.TryParse(strArray[0], out result1) && int.TryParse(strArray[1], out result2) ? $"{result1:d1}:{result2:d2}" : $"{strArray[0]:d1}:{strArray[1]:d2}";
  }

  private Transform UpdatePlate()
  {
    int index1 = 0;
    for (int length = HomeTopBonusTime.PlateUI.Length; index1 < length; ++index1)
      this.SetActive(this.myTrans, (Enum) HomeTopBonusTime.PlateUI[index1], false);
    int index2 = this.timeSlotEvents[this.currentIndex].timeSlotType;
    if (index2 >= HomeTopBonusTime.PlateUI.Length)
      index2 = 1;
    this.SetActive(this.myTrans, (Enum) HomeTopBonusTime.PlateUI[index2], true);
    return this.FindCtrl(this.myTrans, (Enum) HomeTopBonusTime.PlateUI[index2]);
  }

  private bool IsBonusTime()
  {
    if (this.timeSlotEvents == null || this.timeSlotEvents.Count <= 0)
      return false;
    DateTime now = TimeManager.GetNow();
    DateTime dateTime1 = DateTime.Parse(this.timeSlotEvents[this.currentIndex].endDate);
    DateTime dateTime2 = DateTime.Parse(this.timeSlotEvents[this.currentIndex].startData);
    if (dateTime1.CompareTo(now) > 0 && dateTime2.CompareTo(now) < 0)
      return true;
    this.timeSlotEvents.RemoveAt(this.currentIndex);
    this.currentIndex = 0;
    return false;
  }

  private bool IsUpdateDate()
  {
    string startData = this.timeSlotEvents[this.currentIndex].startData;
    string endDate = this.timeSlotEvents[this.currentIndex].endDate;
    if (this.currentStartDate == startData && this.currentEndDate == endDate)
      return false;
    this.currentStartDate = startData;
    this.currentEndDate = endDate;
    return true;
  }

  private enum UI
  {
    OBJ_BONUS_TIME_VISIBLE,
    LBL_BONUS_TIME_NAME,
    LBL_BONUS_TIME_DATE,
    BTN_BONUS_TIME,
    OBJ_TWEEN_BONUS_TIME,
    SPR_ALL,
    SPR_MORNING,
    SPR_AFTERNOON,
    SPR_NIGHT,
  }
}
