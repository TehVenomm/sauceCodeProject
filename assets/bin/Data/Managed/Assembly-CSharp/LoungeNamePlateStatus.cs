// Decompiled with JetBrains decompiler
// Type: LoungeNamePlateStatus
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class LoungeNamePlateStatus : UIBehaviour
{
  private bool isValidNamePlate;
  private LOUNGE_ACTION_TYPE actionType;
  private LoungePlayer player;

  public override void UpdateUI()
  {
    this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_STATUS_AFK, false);
    this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_STATUS_SMITH, false);
    this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_STATUS_SHOP, false);
    this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_STATUS_AFK_CENTER, false);
    this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_STATUS_SMITH_CENTER, false);
    this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_STATUS_SHOP_CENTER, false);
    if (this.isValidNamePlate)
    {
      switch (this.actionType)
      {
        case LOUNGE_ACTION_TYPE.TO_GACHA:
          this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_STATUS_SHOP, true);
          break;
        case LOUNGE_ACTION_TYPE.TO_EQUIP:
          this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_STATUS_SMITH, true);
          break;
        case LOUNGE_ACTION_TYPE.AFK:
          this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_STATUS_AFK, true);
          break;
      }
    }
    else
    {
      switch (this.actionType)
      {
        case LOUNGE_ACTION_TYPE.TO_GACHA:
          this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_STATUS_SHOP_CENTER, true);
          break;
        case LOUNGE_ACTION_TYPE.TO_EQUIP:
          this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_STATUS_SMITH_CENTER, true);
          break;
        case LOUNGE_ACTION_TYPE.AFK:
          this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_STATUS_AFK_CENTER, true);
          break;
      }
    }
    this.ChangePlayerName(this.player.LoungeCharaInfo.name);
  }

  public void SetPlayer(LoungePlayer player)
  {
    this.player = player;
    this.actionType = player.CurrentActionType;
  }

  public void SetActiveNamePlate(bool isActive)
  {
    this.isValidNamePlate = isActive;
    if (isActive)
      this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.OBJ_LOUNGE_NAMEPLATE, true);
    this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.LBL_NAMEPLATE, isActive);
    this.SetActive(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.SPR_LOUNGE_NAMEPLATE, isActive);
    this.RefreshUI();
  }

  public void ChangePlayerName(string name)
  {
    this.SetLabelText(((Component) this).transform, (Enum) LoungeNamePlateStatus.UI.LBL_NAMEPLATE, name);
  }

  private void LateUpdate()
  {
    if (!MonoBehaviourSingleton<LoungeManager>.IsValid() && !MonoBehaviourSingleton<ClanManager>.IsValid() || this.actionType == this.player.CurrentActionType)
      return;
    this.actionType = this.player.CurrentActionType;
    this.RefreshUI();
  }

  private enum UI
  {
    OBJ_LOUNGE_NAMEPLATE,
    SPR_LOUNGE_NAMEPLATE,
    LBL_NAMEPLATE,
    SPR_STATUS_AFK,
    SPR_STATUS_SMITH,
    SPR_STATUS_SHOP,
    SPR_STATUS_AFK_CENTER,
    SPR_STATUS_SMITH_CENTER,
    SPR_STATUS_SHOP_CENTER,
  }
}
