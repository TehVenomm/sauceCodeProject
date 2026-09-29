// Decompiled with JetBrains decompiler
// Type: ResultExpGaugeCtrl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[RequireComponent(typeof (UISprite))]
public class ResultExpGaugeCtrl : MonoBehaviour
{
  public AnimationCurve lvUpCurve;
  public AnimationCurve lastDirectionCurve;
  private float time = -1f;
  private float totalExp;
  private int startLevel;
  private bool isProgressAnim;
  [HideInInspector]
  public UISprite progress;
  [HideInInspector]
  public float getExp;
  [HideInInspector]
  public float addCountExpValue;
  [HideInInspector]
  public float startExp;
  [HideInInspector]
  public int nowLevel;
  [HideInInspector]
  public int remainLevelUpCnt;
  [HideInInspector]
  public int beforeUpdateLevel;
  [HideInInspector]
  public UserLevelTable.UserLevelData levelTable;
  [HideInInspector]
  public UserLevelTable.UserLevelData nowLevelTable;
  private bool skip;
  public System.Action callBack;
  public Action<bool, int, ResultExpGaugeCtrl> OnUpdate;

  public bool isEnd { get; private set; }

  public void InitDirection(Action<ResultExpGaugeCtrl> initialize_call = null)
  {
    this.progress = ((Component) this).GetComponent<UISprite>();
    if (initialize_call != null)
      initialize_call(this);
    this.totalExp = this.getExp + this.startExp;
    this.startLevel = this.nowLevel;
    if (this.nowLevel < Singleton<UserLevelTable>.I.GetMaxLevel())
    {
      this.time = -1f;
      this.UpdateAddCountExp(this.nowLevel);
      this.SetFillAmount((float) ((int) this.startExp - (int) this.nowLevelTable.needExp) / (float) ((int) this.levelTable.needExp - (int) this.nowLevelTable.needExp));
    }
    else
    {
      this.SetFillAmount(0.0f);
      this.getExp = 0.0f;
    }
  }

  public void SetFillAmount(float amount) => this.progress.fillAmount = amount;

  public void StartAnim() => this.isProgressAnim = true;

  public void Skip() => this.skip = true;

  private void UpdateAddCountExp(int now_level)
  {
    this.levelTable = Singleton<UserLevelTable>.I.GetLevelTable(now_level + 1);
    this.nowLevelTable = Singleton<UserLevelTable>.I.GetLevelTable(now_level);
    if ((double) this.time >= 0.0)
    {
      this.UpdateCurveEvaluate();
    }
    else
    {
      this.time = 0.0f;
      this.addCountExpValue = 0.0f;
    }
  }

  public void UpdateCurveEvaluate()
  {
    int num1 = this.remainLevelUpCnt > 0 ? 1 : 0;
    float num2 = (float) ((int) this.levelTable.needExp - (int) this.nowLevelTable.needExp);
    float num3 = num1 == 0 ? (this.nowLevel != this.startLevel ? this.totalExp - (float) (int) this.nowLevelTable.needExp : this.getExp) : num2;
    float num4 = (float) (int) this.nowLevelTable.needExp;
    if (this.nowLevel == this.startLevel)
      num4 = this.startExp;
    this.time += Time.deltaTime;
    if (num1 != 0)
      this.addCountExpValue = this.lvUpCurve.Evaluate(Mathf.Clamp(this.time, 0.0f, ((Keyframe) ref this.lvUpCurve.keys[this.lvUpCurve.length - 1]).time)) * num3 + num4;
    else
      this.addCountExpValue = this.lastDirectionCurve.Evaluate(Mathf.Clamp(this.time, 0.0f, ((Keyframe) ref this.lastDirectionCurve.keys[this.lastDirectionCurve.length - 1]).time)) * num3 + num4;
  }

  public void Update()
  {
    if (!this.isProgressAnim)
      return;
    if ((double) this.getExp > 0.0)
    {
      bool flag1 = false;
      bool flag2 = false;
      this.beforeUpdateLevel = this.nowLevel;
      this.UpdateCurveEvaluate();
      float num1 = this.addCountExpValue;
      if ((double) num1 >= (double) this.totalExp || this.skip)
      {
        flag1 = this.skip;
        this.skip = false;
        num1 = this.totalExp;
        this.isProgressAnim = false;
        this.isEnd = true;
        if (this.callBack != null)
          this.callBack();
      }
      float num2;
      do
      {
        num2 = (num1 - (float) (int) this.nowLevelTable.needExp) / (float) ((int) this.levelTable.needExp - (int) this.nowLevelTable.needExp);
        if ((double) num2 >= 1.0)
        {
          flag2 = true;
          this.progress.fillAmount = 0.0f;
          ++this.nowLevel;
          --this.remainLevelUpCnt;
          this.time = -1f;
          this.UpdateAddCountExp(this.nowLevel);
          if ((int) this.nowLevelTable.lv >= Singleton<UserLevelTable>.I.GetMaxLevel())
          {
            num2 = 0.0f;
            this.progress.fillAmount = num2;
          }
          if (!flag1)
            num1 = (float) (int) this.nowLevelTable.needExp;
        }
        else
          this.progress.fillAmount = num2;
      }
      while ((double) num2 >= 1.0);
      if (this.OnUpdate == null)
        return;
      this.OnUpdate(flag2, (int) ((double) num1 - (double) this.startExp), this);
    }
    else
    {
      this.isProgressAnim = false;
      this.isEnd = true;
      if (this.callBack == null)
        return;
      this.callBack();
    }
  }
}
