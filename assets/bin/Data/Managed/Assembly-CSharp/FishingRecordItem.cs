// Decompiled with JetBrains decompiler
// Type: FishingRecordItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using UnityEngine;

#nullable disable
public class FishingRecordItem : UIBehaviour
{
  [SerializeField]
  private string[] crownSpriteName;
  [SerializeField]
  private Color[] crownTextColor;
  private GatherItemRecord record;
  private string prefsKey;
  private FishingRecordItem.eValueState valueState;

  public void Setup(Transform t, GatherItemRecord rec)
  {
    this.record = rec;
    this.prefsKey = "gik_" + this.record.listId.ToString();
    this.valueState = FishingRecordItem.eValueState.None;
    if (PlayerPrefs.HasKey(this.prefsKey))
    {
      if (PlayerPrefs.GetInt(this.prefsKey) < this.record.maxValue)
        this.valueState = FishingRecordItem.eValueState.Update;
    }
    else
      this.valueState = FishingRecordItem.eValueState.New;
    this.SetActive(t, (Enum) FishingRecordItem.UI.SPR_STATE_UPDATE, this.valueState == FishingRecordItem.eValueState.Update);
    this.SetActive(t, (Enum) FishingRecordItem.UI.SPR_STATE_NEW, this.valueState == FishingRecordItem.eValueState.New);
    this.SetLabelText(t, (Enum) FishingRecordItem.UI.LBL_LIST_NUMBER, $"#{this.record.listId}");
    this.SetLabelText(t, (Enum) FishingRecordItem.UI.LBL_LIST_NAME, this.record.name);
    this.SetColor(t, (Enum) FishingRecordItem.UI.LBL_RECORD_SIZE_VALUE, this.crownTextColor[this.record.maxCrownType]);
    this.SetLabelText(t, (Enum) FishingRecordItem.UI.LBL_RECORD_SIZE_VALUE, $"{this.record.GetSizeString()}cm");
    this.SetLabelText(t, (Enum) FishingRecordItem.UI.LBL_RECORD_NUM_VALUE, string.Format("{0:#,0}", (object) this.record.num));
    if (this.record.GetCrownType() != GATHER_ITEM_CROWN_TYPE.NONE)
    {
      this.SetSprite(t, (Enum) FishingRecordItem.UI.SPR_CROWN, this.crownSpriteName[this.record.maxCrownType]);
      this.SetActive(t, (Enum) FishingRecordItem.UI.SPR_CROWN, true);
    }
    else
      this.SetActive(t, (Enum) FishingRecordItem.UI.SPR_CROWN, false);
  }

  public bool SaveState()
  {
    if (this.valueState == FishingRecordItem.eValueState.None)
      return false;
    PlayerPrefs.SetInt(this.prefsKey, this.record.maxValue);
    return true;
  }

  private enum UI
  {
    SPR_STATE_UPDATE,
    SPR_STATE_NEW,
    LBL_LIST_NUMBER,
    LBL_LIST_NAME,
    LBL_RECORD_SIZE_VALUE,
    LBL_RECORD_NUM_VALUE,
    SPR_CROWN,
  }

  private enum eValueState
  {
    None,
    New,
    Update,
  }
}
