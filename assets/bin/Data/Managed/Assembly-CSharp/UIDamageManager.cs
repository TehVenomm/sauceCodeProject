// Decompiled with JetBrains decompiler
// Type: UIDamageManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class UIDamageManager : MonoBehaviourSingleton<UIDamageManager>
{
  private static readonly Vector3 OFFSET_RECOVER_NUM = new Vector3(0.0f, 0.7f, 0.0f);
  protected List<UIDamageNum> damageNumList = new List<UIDamageNum>();
  protected List<UIAdditionalDamageNum> additionalDamageNumList = new List<UIAdditionalDamageNum>();
  protected List<UIPlayerDamageNum> playerDamageNumList = new List<UIPlayerDamageNum>();
  protected List<UIPlayerDamageNum> playerRecoverNumList = new List<UIPlayerDamageNum>();
  protected List<UIPlayerDamageNum> enemyRecoverNumList = new List<UIPlayerDamageNum>();
  private UIDamageNum originalDamage;
  private UIPlayerDamageNum currentRecoverNum;
  private Object m_playerDamageNumObj;
  private Object m_damageNumObj;
  private Object m_additionalDamageNumObj;

  public void RegisterDamageNumResources(
    Object damageNumObj,
    Object playerDamageNumObj,
    Object additionalDamageNumObj)
  {
    this.m_damageNumObj = damageNumObj;
    this.m_playerDamageNumObj = playerDamageNumObj;
    this.m_additionalDamageNumObj = additionalDamageNumObj;
  }

  public void Create(
    Vector3 pos,
    int damage,
    UIDamageNum.DAMAGE_COLOR color,
    int groupOffset = 0,
    int effective = 0,
    bool isRegionOnly = false)
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return;
    float pixelHeight = MonoBehaviourSingleton<InGameCameraManager>.I.GetPixelHeight();
    Vector3 pos1;
    // ISSUE: explicit constructor call
    ((Vector3) ref pos1).\u002Ector(pos.x, pos.y + MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.screenSaftyOffset, pos.z);
    Vector3 screenPoint = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(pos1);
    if ((double) screenPoint.y > (double) pixelHeight)
    {
      screenPoint.y = pixelHeight;
      pos = MonoBehaviourSingleton<InGameCameraManager>.I.ScreenToWorldPoint(screenPoint);
      pos.y -= MonoBehaviourSingleton<InGameSettingsManager>.I.selfController.screenSaftyOffset;
    }
    bool flag = UIDamageNum.DAMAGE_COLOR.BUFF != color && color != 0;
    if (isRegionOnly)
      color = !flag ? (UIDamageNum.DAMAGE_COLOR.BUFF != color ? UIDamageNum.DAMAGE_COLOR.REGION_ONLY_NORMAL : UIDamageNum.DAMAGE_COLOR.REGION_ONLY_BUFF) : UIDamageNum.DAMAGE_COLOR.REGION_ONLY_ELEMENT;
    if (groupOffset > 0 | flag)
    {
      this.CreateAdditionalDamage(pos, damage, color, groupOffset, this.originalDamage, effective);
    }
    else
    {
      UIDamageNum damage_num = (UIDamageNum) null;
      int index = 0;
      for (int count = this.damageNumList.Count; index < count; ++index)
      {
        if (!this.damageNumList[index].enable)
        {
          damage_num = this.damageNumList[index];
          break;
        }
      }
      if (Object.op_Equality((Object) damage_num, (Object) null))
      {
        GameObject gameObject = (GameObject) Object.Instantiate(this.m_damageNumObj);
        if (Object.op_Equality((Object) gameObject, (Object) null))
          return;
        Utility.Attach(this._transform, gameObject.transform);
        damage_num = gameObject.GetComponent<UIDamageNum>();
        if (Object.op_Equality((Object) damage_num, (Object) null))
        {
          Object.Destroy((Object) gameObject);
          return;
        }
        this.damageNumList.Add(damage_num);
        int count = this.damageNumList.Count;
      }
      groupOffset = this.CalcOffsetPosition(pos, damage_num, groupOffset);
      this.originalDamage = damage_num;
      damage_num.Initialize(pos, damage, color, groupOffset);
    }
  }

  private void CreateAdditionalDamage(
    Vector3 pos,
    int damage,
    UIDamageNum.DAMAGE_COLOR color,
    int groupOffset,
    UIDamageNum originalDamage,
    int effective)
  {
    UIAdditionalDamageNum damage_num = (UIAdditionalDamageNum) null;
    int index = 0;
    for (int count = this.additionalDamageNumList.Count; index < count; ++index)
    {
      if (!this.additionalDamageNumList[index].enable)
      {
        damage_num = this.additionalDamageNumList[index];
        break;
      }
    }
    if (Object.op_Equality((Object) damage_num, (Object) null))
    {
      GameObject gameObject = (GameObject) Object.Instantiate(this.m_additionalDamageNumObj);
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return;
      Utility.Attach(this._transform, gameObject.transform);
      damage_num = gameObject.GetComponent<UIAdditionalDamageNum>();
      if (Object.op_Equality((Object) damage_num, (Object) null))
      {
        Object.Destroy((Object) gameObject);
        return;
      }
      this.additionalDamageNumList.Add(damage_num);
    }
    groupOffset = this.CalcOffsetPosition(pos, (UIDamageNum) damage_num, groupOffset);
    damage_num.Initialize(pos, damage, color, groupOffset, originalDamage, effective);
  }

  private int CalcOffsetPosition(Vector3 pos, UIDamageNum damage_num, int orgGroupOffet)
  {
    for (int index = 0; index < 10; ++index)
    {
      if (!this.isLabelOverlap(pos, damage_num, orgGroupOffet + index))
        return orgGroupOffet + index;
    }
    return orgGroupOffet;
  }

  private bool isLabelOverlap(Vector3 pos, UIDamageNum damage_num, int groupOffset)
  {
    ((Component) damage_num).transform.position = damage_num.GetUIPosFromWorld(pos, groupOffset);
    Vector3 localPosition = ((Component) damage_num).transform.localPosition;
    for (int index = 0; index < this.damageNumList.Count; ++index)
    {
      if (this._isLabelOverlap(localPosition, damage_num, this.damageNumList[index]))
        return true;
    }
    for (int index = 0; index < this.additionalDamageNumList.Count; ++index)
    {
      if (this._isLabelOverlap(localPosition, damage_num, (UIDamageNum) this.additionalDamageNumList[index]))
        return true;
    }
    return false;
  }

  private bool _isLabelOverlap(
    Vector3 nextPosition,
    UIDamageNum damage_num,
    UIDamageNum damageNumLists)
  {
    if (Object.op_Equality((Object) damageNumLists, (Object) null) || Object.op_Equality((Object) damage_num, (Object) null) || !damageNumLists.enable || Object.op_Equality((Object) damage_num, (Object) damageNumLists))
      return false;
    Vector3 localPosition = ((Component) damageNumLists).transform.localPosition;
    float num1 = Mathf.Abs(localPosition.x - nextPosition.x);
    float num2 = Mathf.Abs(localPosition.y - nextPosition.y);
    float num3 = 16f * (float) damageNumLists.DamageLength;
    return (double) num1 < (double) num3 && (double) num2 < 15.0;
  }

  public UIPlayerDamageNum CreatePlayerDamage(
    Character chara,
    int damage,
    UIPlayerDamageNum.DAMAGE_COLOR color)
  {
    UIPlayerDamageNum playerDamageNum = this.CreatePlayerDamageNum();
    if (Object.op_Equality((Object) playerDamageNum, (Object) null))
      return (UIPlayerDamageNum) null;
    return !playerDamageNum.Initialize(chara, damage, color) ? (UIPlayerDamageNum) null : playerDamageNum;
  }

  public UIPlayerDamageNum CreatePlayerDamage(Character chara, AttackedHitStatus status)
  {
    UIPlayerDamageNum playerDamageNum = this.CreatePlayerDamageNum();
    if (Object.op_Equality((Object) playerDamageNum, (Object) null))
      return (UIPlayerDamageNum) null;
    return !playerDamageNum.Initialize(chara, status) ? (UIPlayerDamageNum) null : playerDamageNum;
  }

  public UIPlayerDamageNum CreatePlayerRecoverHp(
    Character chara,
    int damage,
    UIPlayerDamageNum.DAMAGE_COLOR color)
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return (UIPlayerDamageNum) null;
    UIPlayerDamageNum uiPlayerDamageNum = (UIPlayerDamageNum) null;
    for (int index = 0; index < this.playerRecoverNumList.Count; ++index)
    {
      if (!this.playerRecoverNumList[index].enable)
      {
        uiPlayerDamageNum = this.playerRecoverNumList[index];
        break;
      }
    }
    if (Object.op_Equality((Object) uiPlayerDamageNum, (Object) null))
    {
      GameObject gameObject = (GameObject) Object.Instantiate(this.m_playerDamageNumObj);
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return (UIPlayerDamageNum) null;
      Utility.Attach(this._transform, gameObject.transform);
      uiPlayerDamageNum = gameObject.GetComponent<UIPlayerDamageNum>();
      if (Object.op_Equality((Object) uiPlayerDamageNum, (Object) null))
      {
        Object.Destroy((Object) gameObject);
        return (UIPlayerDamageNum) null;
      }
      uiPlayerDamageNum.offset = UIDamageManager.OFFSET_RECOVER_NUM;
      this.playerRecoverNumList.Add(uiPlayerDamageNum);
    }
    return !uiPlayerDamageNum.Initialize(chara, damage, color) ? (UIPlayerDamageNum) null : uiPlayerDamageNum;
  }

  private UIPlayerDamageNum CreatePlayerDamageNum()
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return (UIPlayerDamageNum) null;
    UIPlayerDamageNum playerDamageNum = (UIPlayerDamageNum) null;
    int index = 0;
    for (int count = this.playerDamageNumList.Count; index < count; ++index)
    {
      if (!this.playerDamageNumList[index].enable)
      {
        playerDamageNum = this.playerDamageNumList[index];
        break;
      }
    }
    if (Object.op_Equality((Object) playerDamageNum, (Object) null))
    {
      GameObject gameObject = (GameObject) Object.Instantiate(this.m_playerDamageNumObj);
      if (Object.op_Equality((Object) gameObject, (Object) null))
        return (UIPlayerDamageNum) null;
      Utility.Attach(this._transform, gameObject.transform);
      playerDamageNum = gameObject.GetComponent<UIPlayerDamageNum>();
      if (Object.op_Equality((Object) playerDamageNum, (Object) null))
      {
        Object.Destroy((Object) gameObject);
        return (UIPlayerDamageNum) null;
      }
      this.playerDamageNumList.Add(playerDamageNum);
    }
    return playerDamageNum;
  }

  public UIPlayerDamageNum CreateEnemyRecoverHp(
    Character chara,
    int damage,
    UIPlayerDamageNum.DAMAGE_COLOR color)
  {
    if (!((Component) this).gameObject.activeInHierarchy)
      return (UIPlayerDamageNum) null;
    GameObject gameObject = (GameObject) Object.Instantiate(this.m_playerDamageNumObj);
    if (Object.op_Equality((Object) gameObject, (Object) null))
      return (UIPlayerDamageNum) null;
    Utility.Attach(this._transform, gameObject.transform);
    UIPlayerDamageNum component = gameObject.GetComponent<UIPlayerDamageNum>();
    if (Object.op_Equality((Object) component, (Object) null))
    {
      Object.Destroy((Object) gameObject);
      return (UIPlayerDamageNum) null;
    }
    this.enemyRecoverNumList.Add(component);
    if (!component.Initialize(chara, damage, color, false))
      return (UIPlayerDamageNum) null;
    component.EnableAutoDelete();
    return component;
  }

  private void LateUpdate() => this.EnemyRecoverHpUIProc();

  private void EnemyRecoverHpUIProc()
  {
    if (Object.op_Inequality((Object) this.currentRecoverNum, (Object) null))
    {
      if ((double) this.currentRecoverNum.AlphaRate > 0.800000011920929 && this.currentRecoverNum.enable)
        return;
      this.currentRecoverNum = (UIPlayerDamageNum) null;
    }
    else
    {
      if (this.enemyRecoverNumList.Count <= 0)
        return;
      this.currentRecoverNum = this.enemyRecoverNumList[0];
      this.currentRecoverNum.Play();
      this.enemyRecoverNumList.RemoveAt(0);
    }
  }
}
