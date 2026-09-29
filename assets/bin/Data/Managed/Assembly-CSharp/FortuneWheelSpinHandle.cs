// Decompiled with JetBrains decompiler
// Type: FortuneWheelSpinHandle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
public class FortuneWheelSpinHandle : MonoBehaviour
{
  public float DEFAULT_ACTIVE_SCALE = 1.3f;
  public float DEFAULT_INACTIVE_SCALE = 0.8f;
  public float DEFAULT_SPIN_TIME_x1 = 5f;
  public float DEFAULT_SPIN_TIME_x10 = 1f;
  public float DEFAULT_SPIN_TIME_MULTI = 0.2f;
  public float DEFINE_REWARD_TIME_x1 = 1f;
  public float DEFINE_REWARD_TIME_x10 = 0.4f;
  public float DEFINE_REWARD_TIME_MULTI;
  public float MIN_AJUST_TIME_x1 = 0.8f;
  public float MIN_AJUST_TIME_x10 = 0.4f;
  public float MIN_AJUST_TIME_MULTI;
  public float SPIN_SPEED_MAX_x1 = 500f;
  public float SPIN_SPEED_MAX_x10 = 800f;
  public float SPIN_SPEED_MAX_MULTI = 1000f;
  public int SPIN_SPEED_MIN_X1 = 200;
  public int SPIN_SPEED_MIN_X10 = 200;
  public int SPIN_SPEED_MIN_MULTI = 1000;
  private float spinTimeDur;
  private float defineRewardTime;
  private float minAjustTime;
  private float spinMinSpeed;
  private float spinSpeed;
  public float DEFAULT_END_ACCELERATION = 100f;
  public int DEFAULT_WHEEL_ITEMS_LENGTH = 10;
  private Vector3[] wayPoints;
  private List<FortuneWheelSpinItem> initialSpinItem;
  private List<FortuneWheelSpinItem> pinItem;
  private List<FortuneWheelSpinItem> hiddenSpinItemList = new List<FortuneWheelSpinItem>();
  private List<FortuneWheelItem> itemList;
  private List<FortuneWheelItem> updatedItemList;
  private FortuneWheelReward currentReward;
  private float itemScaleMax = 1.2f;
  private float currentItemScale = 1f;
  private float currentDegree;
  private float spinTime;
  private float ajustTime;
  private float expectedDegree;
  private bool isStartSpin;
  private Vector3 hidePosition = new Vector3(1000f, 0.0f, 0.0f);
  public Transform _trans;
  private int currentIndexPos;
  private int itemDataLength;
  private bool isAjustSpeed;
  private Action<bool> spinEndAction;
  private GameObject spinPrefab;
  private float defaultDistaceScale;
  private FortuneWheelManager.SPIN_TYPE spinType;
  private int oppositeIndex;

  private void InitPosition()
  {
    this.wayPoints = new Vector3[10];
    Vector3 zero = Vector3.zero;
    float num = 220f;
    Vector2 v;
    // ISSUE: explicit constructor call
    ((Vector2) ref v).\u002Ector(0.0f, 1f);
    for (int index = 0; index < 10; ++index)
    {
      this.wayPoints[index] = zero;
      Vector2 vector2 = this.Rotate(v, (float) index * 36f);
      ((Vector2) ref vector2).Normalize();
      vector2 = Vector2.op_Multiply(vector2, num);
      this.wayPoints[index].x += vector2.x;
      this.wayPoints[index].y += vector2.y;
    }
  }

  public void IniSpin(List<FortuneWheelItem> _itemList, GameObject m_SpinItemPrefab)
  {
    this.defaultDistaceScale = this.DEFAULT_ACTIVE_SCALE - this.DEFAULT_INACTIVE_SCALE;
    this._trans = ((Component) this).transform;
    this.spinPrefab = m_SpinItemPrefab;
    this.InitPosition();
    this.itemList = _itemList;
    this.initialSpinItem = new List<FortuneWheelSpinItem>();
    this.pinItem = new List<FortuneWheelSpinItem>();
    this.itemDataLength = this.itemList.Count;
    for (int indexPos = 0; indexPos < this.DEFAULT_WHEEL_ITEMS_LENGTH; ++indexPos)
    {
      int index = indexPos % this.itemDataLength;
      FortuneWheelSpinItem component = ((Component) ResourceUtility.Realizes((Object) m_SpinItemPrefab, ((Component) this).transform, 5)).GetComponent<FortuneWheelSpinItem>();
      component.CreateItemIcon(this.itemList[index], indexPos);
      component._trans.localPosition = this.wayPoints[indexPos];
      component.SetScale(indexPos == 0 ? this.DEFAULT_ACTIVE_SCALE : this.DEFAULT_INACTIVE_SCALE);
      component.SetRotate((float) indexPos * 36f);
      component.itemName = this.itemList[index].name;
      this.initialSpinItem.Add(component);
    }
    this.pinItem.AddRange((IEnumerable<FortuneWheelSpinItem>) this.initialSpinItem);
  }

  public void Skip() => this.StartCoroutine(this.IESkip());

  private IEnumerator IESkip()
  {
    while (!this.isAjustSpeed)
      yield return (object) null;
    yield return (object) new WaitForEndOfFrame();
    if (this.isStartSpin)
    {
      this._trans.localEulerAngles = new Vector3(0.0f, 0.0f, -this.expectedDegree);
      this.ScaleItemFinal(this.expectedDegree);
      this.isStartSpin = false;
      if (this.spinEndAction != null)
        this.spinEndAction(false);
    }
  }

  public void StartSpin(
    FortuneWheelData reward,
    FortuneWheelManager.SPIN_TYPE spinType,
    Action<bool> onEndAction,
    int rewardIndex = 0)
  {
    if (this.isStartSpin || rewardIndex >= reward.spinRewards.Count)
      return;
    this.spinType = spinType;
    double num1;
    switch (spinType)
    {
      case FortuneWheelManager.SPIN_TYPE.X1:
        num1 = (double) this.DEFAULT_SPIN_TIME_x1;
        break;
      case FortuneWheelManager.SPIN_TYPE.X10:
        num1 = (double) this.DEFAULT_SPIN_TIME_x10;
        break;
      default:
        num1 = (double) this.DEFAULT_SPIN_TIME_MULTI;
        break;
    }
    this.spinTimeDur = (float) num1;
    double num2;
    switch (spinType)
    {
      case FortuneWheelManager.SPIN_TYPE.X1:
        num2 = (double) this.DEFINE_REWARD_TIME_x1;
        break;
      case FortuneWheelManager.SPIN_TYPE.X10:
        num2 = (double) this.DEFINE_REWARD_TIME_x10;
        break;
      default:
        num2 = (double) this.DEFINE_REWARD_TIME_MULTI;
        break;
    }
    this.defineRewardTime = (float) num2;
    double num3;
    switch (spinType)
    {
      case FortuneWheelManager.SPIN_TYPE.X1:
        num3 = (double) this.MIN_AJUST_TIME_x1;
        break;
      case FortuneWheelManager.SPIN_TYPE.X10:
        num3 = (double) this.MIN_AJUST_TIME_x10;
        break;
      default:
        num3 = (double) this.MIN_AJUST_TIME_MULTI;
        break;
    }
    this.minAjustTime = (float) num3;
    double num4;
    switch (spinType)
    {
      case FortuneWheelManager.SPIN_TYPE.X1:
        num4 = (double) this.SPIN_SPEED_MAX_x1;
        break;
      case FortuneWheelManager.SPIN_TYPE.X10:
        num4 = (double) this.SPIN_SPEED_MAX_x10;
        break;
      default:
        num4 = (double) this.SPIN_SPEED_MAX_MULTI;
        break;
    }
    this.spinSpeed = (float) num4;
    int num5;
    switch (spinType)
    {
      case FortuneWheelManager.SPIN_TYPE.X1:
        num5 = this.SPIN_SPEED_MIN_X1;
        break;
      case FortuneWheelManager.SPIN_TYPE.X10:
        num5 = this.SPIN_SPEED_MIN_X10;
        break;
      default:
        num5 = this.SPIN_SPEED_MIN_MULTI;
        break;
    }
    this.spinMinSpeed = (float) num5;
    this.spinEndAction = onEndAction;
    this.spinEndAction(true);
    this.currentReward = reward.spinRewards[rewardIndex];
    this.updatedItemList = reward.vaultInfo.itemList;
    this.spinTime = 0.0f;
    this.ajustTime = 0.0f;
    this.isStartSpin = true;
    this.isAjustSpeed = false;
  }

  private void Update()
  {
    if (!this.isStartSpin)
      return;
    this.currentDegree += Time.deltaTime * this.spinSpeed;
    this._trans.localEulerAngles = new Vector3(0.0f, 0.0f, -this.currentDegree);
    if ((double) this.currentDegree > 360.0)
      this.currentDegree -= 360f;
    if (this.isAjustSpeed)
    {
      this.ajustTime += Time.deltaTime;
      float expectedDegree = this.expectedDegree;
      float num1 = this.expectedDegree + 18f;
      if ((double) this.spinTime < (double) this.spinTimeDur)
      {
        this.spinSpeed -= Time.deltaTime * this.DEFAULT_END_ACCELERATION;
      }
      else
      {
        if (this.spinType != FortuneWheelManager.SPIN_TYPE.X1 || (double) this.currentDegree >= (double) expectedDegree && (double) this.currentDegree <= (double) num1 && (double) this.ajustTime >= (double) this.minAjustTime)
        {
          this._trans.localEulerAngles = new Vector3(0.0f, 0.0f, -this.expectedDegree);
          this.ScaleItemFinal(this.expectedDegree);
          this.isStartSpin = false;
          this.spinEndAction(false);
          return;
        }
        float num2 = this.spinSpeed - Time.deltaTime * this.DEFAULT_END_ACCELERATION;
        if ((double) num2 > 0.0)
        {
          float spinSpeed = this.spinSpeed;
          this.spinSpeed = num2;
          if ((double) this.spinSpeed < (double) this.spinMinSpeed)
            this.spinSpeed = spinSpeed;
        }
      }
    }
    else
      this.spinSpeed -= Time.deltaTime * this.DEFAULT_END_ACCELERATION;
    this.ScaleItem();
    this.spinTime += Time.deltaTime;
    if (this.isAjustSpeed)
      return;
    this.isAjustSpeed = true;
    this.oppositeIndex = (this.currentIndexPos + 5) % 10;
    this.oppositeIndex = this.GetReplaceIndex(this.oppositeIndex);
    this.expectedDegree = (float) this.oppositeIndex * 36f;
    this.HandleResult(this.oppositeIndex);
  }

  private void ScaleItem()
  {
    int currentIndexPos = this.GetCurrentIndexPos();
    if (this.currentIndexPos != currentIndexPos)
    {
      this.pinItem.Where<FortuneWheelSpinItem>((Func<FortuneWheelSpinItem, bool>) (item => item.indexPos == this.currentIndexPos)).First<FortuneWheelSpinItem>().SetScale(this.DEFAULT_INACTIVE_SCALE);
      this.currentIndexPos = currentIndexPos;
    }
    this.pinItem.Where<FortuneWheelSpinItem>((Func<FortuneWheelSpinItem, bool>) (item => item.indexPos == this.currentIndexPos)).First<FortuneWheelSpinItem>().SetScale(this.GetCurrentScale());
  }

  private void ScaleItemFinal(float deg)
  {
    int index = this.GetCurrentIndexFromDeg(deg);
    this.pinItem.Where<FortuneWheelSpinItem>((Func<FortuneWheelSpinItem, bool>) (item => item.indexPos == index)).First<FortuneWheelSpinItem>().SetScale(this.DEFAULT_ACTIVE_SCALE);
    for (int index1 = 0; index1 < this.pinItem.Count; ++index1)
    {
      if (this.pinItem[index1].indexPos != index)
        this.pinItem[index1].SetScale(this.DEFAULT_INACTIVE_SCALE);
    }
  }

  private int GetReplaceIndex(int index)
  {
    index = index >= this.DEFAULT_WHEEL_ITEMS_LENGTH ? index % this.DEFAULT_WHEEL_ITEMS_LENGTH : index;
    return this.pinItem.Where<FortuneWheelSpinItem>((Func<FortuneWheelSpinItem, bool>) (i => i.indexPos == index)).First<FortuneWheelSpinItem>().type == REWARD_TYPE.JACKPOT ? this.GetReplaceIndex(++index) : index;
  }

  private void HandleResult(int replaceIndex)
  {
    IEnumerable<FortuneWheelSpinItem> source = this.pinItem.Where<FortuneWheelSpinItem>((Func<FortuneWheelSpinItem, bool>) (s => s.id == this.currentReward.spinItemId));
    if (source != null && source.Count<FortuneWheelSpinItem>() > 0)
      this.expectedDegree = (float) source.First<FortuneWheelSpinItem>().indexPos * 36f;
    else
      this.ReplaceRewardIcon(replaceIndex);
  }

  private void ReplaceRewardIcon(int replaceIndex)
  {
    if (this.currentReward == null)
      return;
    FortuneWheelSpinItem fortuneWheelSpinItem1 = (FortuneWheelSpinItem) null;
    IEnumerable<FortuneWheelSpinItem> source1 = this.initialSpinItem.Where<FortuneWheelSpinItem>((Func<FortuneWheelSpinItem, bool>) (s => s.id == this.currentReward.spinItemId));
    if (source1 != null && source1.Count<FortuneWheelSpinItem>() > 0 && this.hiddenSpinItemList.IndexOf(source1.First<FortuneWheelSpinItem>()) > -1)
      fortuneWheelSpinItem1 = source1.First<FortuneWheelSpinItem>();
    else if (this.updatedItemList != null && this.updatedItemList.Count > 0)
    {
      IEnumerable<FortuneWheelItem> source2 = this.updatedItemList.Where<FortuneWheelItem>((Func<FortuneWheelItem, bool>) (s => s.id == this.currentReward.spinItemId));
      if (source2 != null && source2.Count<FortuneWheelItem>() > 0)
      {
        FortuneWheelItem data = source2.First<FortuneWheelItem>();
        FortuneWheelSpinItem component = ((Component) ResourceUtility.Realizes((Object) this.spinPrefab, ((Component) this).transform, 5)).GetComponent<FortuneWheelSpinItem>();
        component.CreateItemIcon(data, -1);
        component._trans.localPosition = this.hidePosition;
        fortuneWheelSpinItem1 = component;
        this.hiddenSpinItemList.Add(component);
        this.initialSpinItem.Add(component);
      }
    }
    FortuneWheelSpinItem fortuneWheelSpinItem2 = this.pinItem.First<FortuneWheelSpinItem>((Func<FortuneWheelSpinItem, bool>) (s => s.indexPos == replaceIndex));
    fortuneWheelSpinItem2._trans.localPosition = this.hidePosition;
    fortuneWheelSpinItem2.indexPos = -1;
    fortuneWheelSpinItem1._trans.localPosition = this.wayPoints[replaceIndex];
    fortuneWheelSpinItem1.SetRotate((float) replaceIndex * 36f);
    fortuneWheelSpinItem1.indexPos = replaceIndex;
    this.pinItem.Add(fortuneWheelSpinItem1);
    this.pinItem.Remove(fortuneWheelSpinItem2);
    this.hiddenSpinItemList.Add(fortuneWheelSpinItem2);
    this.hiddenSpinItemList.Remove(fortuneWheelSpinItem1);
  }

  private int GetCurrentIndexPos()
  {
    for (int currentIndexPos = 1; currentIndexPos < 10; ++currentIndexPos)
    {
      float num1 = (float) ((double) currentIndexPos * 36.0 - 18.0);
      float num2 = (float) ((double) currentIndexPos * 36.0 + 18.0);
      if ((double) this.currentDegree > (double) num1 && (double) this.currentDegree < (double) num2)
        return currentIndexPos;
    }
    return 0;
  }

  private int GetCurrentIndexFromDeg(float degree)
  {
    for (int currentIndexFromDeg = 1; currentIndexFromDeg < 10; ++currentIndexFromDeg)
    {
      float num1 = (float) ((double) currentIndexFromDeg * 36.0 - 18.0);
      float num2 = (float) ((double) currentIndexFromDeg * 36.0 + 18.0);
      if ((double) degree > (double) num1 && (double) degree < (double) num2)
        return currentIndexFromDeg;
    }
    return 0;
  }

  private bool CheckLocalScale(int indexPos)
  {
    float num1 = (float) indexPos * 36f;
    if ((double) num1 == 0.0)
    {
      double num2 = (double) Mathf.Abs(this.currentDegree - num1);
      if ((double) this.currentDegree > 340.0)
      {
        double currentDegree = (double) this.currentDegree;
      }
      return (double) this.currentDegree > (double) num1;
    }
    double num3 = (double) Mathf.Abs(this.currentDegree - num1);
    return (double) this.currentDegree > (double) num1;
  }

  public Vector2 Rotate(Vector2 v, float degrees)
  {
    float num1 = Mathf.Sin(degrees * ((float) Math.PI / 180f));
    float num2 = Mathf.Cos(degrees * ((float) Math.PI / 180f));
    float x = v.x;
    float y = v.y;
    v.x = (float) ((double) num2 * (double) x - (double) num1 * (double) y);
    v.y = (float) ((double) num1 * (double) x + (double) num2 * (double) y);
    return v;
  }

  private float GetCurrentScale()
  {
    double num1 = (double) this.currentIndexPos * 36.0;
    FortuneWheelSpinItem fortuneWheelSpinItem = this.pinItem.Where<FortuneWheelSpinItem>((Func<FortuneWheelSpinItem, bool>) (item => item.indexPos == this.currentIndexPos)).First<FortuneWheelSpinItem>();
    double num2 = (double) (this._trans.localEulerAngles.z + ((Component) fortuneWheelSpinItem).transform.localEulerAngles.z);
    double z = (double) this._trans.localEulerAngles.z;
    float num3 = Mathf.Abs((float) (num1 - z)) / 18f * this.defaultDistaceScale + ((Component) fortuneWheelSpinItem).transform.localScale.z;
    return (double) num3 < (double) this.DEFAULT_ACTIVE_SCALE ? num3 : this.DEFAULT_ACTIVE_SCALE;
  }
}
