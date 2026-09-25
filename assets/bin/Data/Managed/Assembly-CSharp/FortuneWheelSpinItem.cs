// Decompiled with JetBrains decompiler
// Type: FortuneWheelSpinItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class FortuneWheelSpinItem : MonoBehaviour
{
  [HideInInspector]
  public int id;
  [HideInInspector]
  public int indexPos;
  [HideInInspector]
  public Transform _trans;
  public UITexture iconTex;
  public string itemName;
  public REWARD_TYPE type;

  public string imgId { get; private set; }

  public void CreateItemIcon(int id, REWARD_TYPE type, uint rewardId, int indexPos)
  {
    this.id = id;
    this.indexPos = indexPos;
    this._trans = ((Component) this).transform;
    if (type == REWARD_TYPE.JACKPOT)
      return;
    ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(type, rewardId, this._trans);
    if (!Object.op_Inequality((Object) rewardItemIcon, (Object) null))
      return;
    if (type == REWARD_TYPE.SKILL_ITEM)
      rewardItemIcon.SetSpinMachineSkillItem();
    else
      rewardItemIcon.SetSpinMachineItem();
  }

  public void CreateItemIcon(FortuneWheelItem data, int indexPos)
  {
    this.id = data.id;
    this.indexPos = indexPos;
    this._trans = ((Component) this).transform;
    this.type = (REWARD_TYPE) data.rewardType;
    if (string.IsNullOrEmpty(data.imgId))
    {
      if (this.type == REWARD_TYPE.JACKPOT)
        return;
      ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(this.type, (uint) data.rewardId, this._trans);
      if (Object.op_Inequality((Object) rewardItemIcon, (Object) null))
      {
        if (this.type == REWARD_TYPE.SKILL_ITEM)
          rewardItemIcon.SetSpinMachineSkillItem();
        else
          rewardItemIcon.SetSpinMachineItem();
      }
      ((Component) this.iconTex).gameObject.SetActive(false);
    }
    else
    {
      this.imgId = this.imgId;
      ResourceLoad.LoadFortuneWheelIconTexture(this.iconTex, data.imgId, (Action<Texture>) (tex =>
      {
        if (!Object.op_Inequality((Object) this.iconTex, (Object) null))
          return;
        this.iconTex.mainTexture = tex;
      }));
    }
  }

  public void CreateItemIcon(int indexPos, string imgId)
  {
    this.indexPos = indexPos;
    this._trans = ((Component) this).transform;
    this.imgId = imgId;
    ResourceLoad.LoadFortuneWheelIconTexture(this.iconTex, imgId, (Action<Texture>) (tex =>
    {
      if (!Object.op_Inequality((Object) this.iconTex, (Object) null))
        return;
      this.iconTex.mainTexture = tex;
    }));
  }

  public void UpdateIcon(int itemId, REWARD_TYPE type, uint rewardId)
  {
    Object.Destroy((Object) ((Component) this._trans.Find("ItemIcon")).gameObject);
    if (type == REWARD_TYPE.JACKPOT)
      return;
    ItemIcon rewardItemIcon = ItemIcon.CreateRewardItemIcon(type, rewardId, this._trans);
    if (!Object.op_Inequality((Object) rewardItemIcon, (Object) null))
      return;
    if (type == REWARD_TYPE.SKILL_ITEM)
      rewardItemIcon.SetSpinMachineSkillItem();
    else
      rewardItemIcon.SetSpinMachineItem();
  }

  public void SetRotate(float degree)
  {
    this._trans.localEulerAngles = new Vector3(0.0f, 0.0f, degree);
  }

  public void SetScale(float scale)
  {
    if ((double) this._trans.localScale.x == (double) scale || (double) this._trans.localScale.y == (double) scale)
      return;
    this._trans.localScale = new Vector3(scale, scale, 1f);
  }
}
