// Decompiled with JetBrains decompiler
// Type: ClanRequestItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class ClanRequestItem : UIBehaviour
{
  public virtual void Setup(Transform t, ClanDelivery info)
  {
    this.SetNPCIcon(t, info.npcId);
    this.SetDeliveryName(t, info.name);
    this.SetDeliveryInfo(t, info.description);
    if (info.isComplete)
    {
      this.SetActive(t, (Enum) ClanRequestItem.UI.SPR_FRAME_CLEAR_BG, true);
      this.SetActive(t, (Enum) ClanRequestItem.UI.SPR_FRAME_NOT_CLEAR_BG, false);
      this.SetActive(t, (Enum) ClanRequestItem.UI.SPR_CLEAE_ICON, true);
    }
    else
    {
      this.SetActive(t, (Enum) ClanRequestItem.UI.SPR_FRAME_NOT_CLEAR_BG, true);
      this.SetActive(t, (Enum) ClanRequestItem.UI.SPR_FRAME_CLEAR_BG, false);
      this.SetActive(t, (Enum) ClanRequestItem.UI.SPR_CLEAE_ICON, false);
    }
    this.SetDeliveryType(t, info.deliveryType);
    this.SetClearGauge(t, info.count, info.needCount);
    this.SetDeliveryPoint(t, info.exp);
    this.SetTimeLimit(t, info.remainTime);
  }

  protected virtual void SetNPCIcon(Transform t, int npcid)
  {
    NPCTable.NPCData npcData = Singleton<NPCTable>.I.GetNPCData(npcid);
    this.SetNPCIcon(t, (Enum) ClanRequestItem.UI.TEX_NPC, npcData.npcModelID);
  }

  protected virtual void SetDeliveryName(Transform t, string name)
  {
    this.SetLabelText(t, (Enum) ClanRequestItem.UI.LBL_QUEST_NAME, name);
  }

  protected virtual void SetDeliveryInfo(Transform t, string info)
  {
    this.SetLabelText(t, (Enum) ClanRequestItem.UI.LBL_QUEST_INFO, info);
  }

  protected virtual void SetDeliveryType(Transform t, int type)
  {
    this.SetActive(t, (Enum) ClanRequestItem.UI.SPR_TYPE_DAILY_TEXT, type == 1);
  }

  protected virtual void SetClearGauge(Transform t, int count, int needCount)
  {
    Transform ctrl = this.FindCtrl(t, (Enum) ClanRequestItem.UI.SPR_GAUGE);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
      ctrl.localScale = needCount != 0 ? new Vector3(Mathf.Clamp((float) count / (float) needCount, 0.0f, 1f), 1f, 1f) : new Vector3(0.0f, 1f, 1f);
    this.SetLabelText(t, (Enum) ClanRequestItem.UI.LBL_GAUGE, $"{count.ToString()}/{needCount.ToString()}");
  }

  protected virtual void SetDeliveryPoint(Transform t, int point)
  {
    this.SetLabelText(t, (Enum) ClanRequestItem.UI.LBL_GET_PT_NUM, "x" + point.ToString());
  }

  protected virtual void SetTimeLimit(Transform t, int limit)
  {
    TimeSpan span = TimeSpan.FromSeconds((double) limit);
    this.SetLabelText(t, (Enum) ClanRequestItem.UI.LBL_QUEST_TIME, "[Time] left \n" + ClanRequestItem.GetRemainTimeText(span));
  }

  public static string GetRemainTimeText(TimeSpan span)
  {
    string str = "";
    if (span.Seconds > 0)
      span = span.Add(TimeSpan.FromMinutes(1.0));
    if (span.Days > 0)
      return str + string.Format(StringTable.Get(STRING_CATEGORY.TIME, 0U), (object) span.Days);
    if (span.Hours > 0)
      return str + string.Format(StringTable.Get(STRING_CATEGORY.TIME, 1U), (object) span.Hours);
    if (span.Minutes > 0)
      return str + string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2U), (object) span.Minutes);
    return str == "" ? string.Format(StringTable.Get(STRING_CATEGORY.TIME, 2U), (object) 0) : str;
  }

  private enum UI
  {
    LBL_QUEST_INFO,
    LBL_QUEST_NAME,
    SPR_TYPE_EVENT_TEXT,
    SPR_TYPE_WEEKLY_TEXT,
    SPR_TYPE_DAILY_TEXT,
    TEX_NPC,
    SPR_GAUGE,
    LBL_GAUGE,
    SPR_FRAME_CLEAR_BG,
    SPR_FRAME_NOT_CLEAR_BG,
    LBL_GET_PT_NUM,
    SPR_CLEAE_ICON,
    LBL_QUEST_TIME,
  }
}
