// Decompiled with JetBrains decompiler
// Type: EquipSetDetailAbilityData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class EquipSetDetailAbilityData : GameSection
{
  private EquipItemAbility ability;
  private int centeringIndex = -1;

  public override void Initialize()
  {
    this.ability = GameSection.GetEventData() as EquipItemAbility;
    base.Initialize();
  }

  public override void UpdateUI()
  {
    this.SetFullScreenButton((Enum) EquipSetDetailAbilityData.UI.BTN_OK);
    this.SetFontStyle((Enum) EquipSetDetailAbilityData.UI.STR_TITLE, (FontStyle) 2);
    this.SetFontStyle((Enum) EquipSetDetailAbilityData.UI.STR_NEED_POINT, (FontStyle) 2);
    AbilityDataTable.AbilityData table = Singleton<AbilityDataTable>.I.GetAbilityData(this.ability.id, this.ability.ap);
    AbilityDataTable.AbilityData minimumAbilityData = Singleton<AbilityDataTable>.I.GetMinimumAbilityData(this.ability.id);
    if (this.ability.ap == -1)
    {
      this.SetLabelText((Enum) EquipSetDetailAbilityData.UI.LBL_DATA_NAME, this.ability.GetName());
      this.SetActive((Enum) EquipSetDetailAbilityData.UI.LBL_DESCRIPTION, minimumAbilityData != null);
      this.SetLabelText((Enum) EquipSetDetailAbilityData.UI.LBL_DESCRIPTION, minimumAbilityData.description);
    }
    else if (table != null)
    {
      this.SetLabelText((Enum) EquipSetDetailAbilityData.UI.LBL_DATA_NAME, table.name);
      this.SetActive((Enum) EquipSetDetailAbilityData.UI.LBL_DESCRIPTION, true);
      this.SetLabelText((Enum) EquipSetDetailAbilityData.UI.LBL_DESCRIPTION, table.description);
    }
    else
    {
      this.SetLabelText((Enum) EquipSetDetailAbilityData.UI.LBL_DATA_NAME, this.sectionData.GetText("NON_DATA"));
      this.SetActive((Enum) EquipSetDetailAbilityData.UI.LBL_DESCRIPTION, minimumAbilityData != null);
      this.SetLabelText((Enum) EquipSetDetailAbilityData.UI.LBL_DESCRIPTION, minimumAbilityData.description);
    }
    AbilityDataTable.AbilityData[] data = Singleton<AbilityDataTable>.I.GetAbilityDataArray(this.ability.id);
    this.SetActive((Enum) EquipSetDetailAbilityData.UI.GRD_DATA_LIST, data != null);
    if (data == null)
      return;
    this.SetGrid((Enum) EquipSetDetailAbilityData.UI.GRD_DATA_LIST, "EquipSetDetailAbilityDataItem", data.Length, false, (Action<int, Transform, bool>) ((i, t, is_recycle) =>
    {
      AbilityDataTable.AbilityData abilityData = data[i];
      string text = string.Format((int) abilityData.needAP >= 0 ? "+{0}" : "{0}", (object) abilityData.needAP);
      this.SetLabelText(t, (Enum) EquipSetDetailAbilityData.UI.LBL_ABILITY_DATA_NAME, abilityData.name);
      this.SetLabelText(t, (Enum) EquipSetDetailAbilityData.UI.LBL_AP, text);
      bool is_visible = table != null && table.needAP == abilityData.needAP;
      this.SetActive(t, (Enum) EquipSetDetailAbilityData.UI.SPR_ABILITY_ON, is_visible);
      Color color = this.ability.ap == -1 ? Color.white : Color.gray;
      if (is_visible)
      {
        this.centeringIndex = i;
        color = Color.white;
      }
      this.SetColor(t, (Enum) EquipSetDetailAbilityData.UI.LBL_ABILITY_DATA_NAME, color);
      this.SetColor(t, (Enum) EquipSetDetailAbilityData.UI.LBL_AP, color);
    }));
    UIScrollView component1 = this.GetComponent<UIScrollView>((Enum) EquipSetDetailAbilityData.UI.SCR_DATA_LIST);
    if (!Object.op_Inequality((Object) component1, (Object) null) || !((Behaviour) component1).enabled)
      return;
    UIGrid component2 = this.GetComponent<UIGrid>((Enum) EquipSetDetailAbilityData.UI.GRD_DATA_LIST);
    double y = (double) component1.panel.GetViewSize().y;
    float cellHeight = component2.cellHeight;
    double num1 = (double) cellHeight;
    int num2 = Mathf.RoundToInt((float) (y / num1)) / 2;
    int num3 = -1;
    if (this.centeringIndex > num2)
      num3 = this.centeringIndex - num2;
    Transform transform = ((Component) component1).transform.Find("_DRAG_SCROLL_");
    Vector3 position = ((Component) component2).transform.position;
    component1.ResetPosition();
    component1.MoveRelative(new Vector3(0.0f, cellHeight * (float) num3));
    ((Component) transform).transform.position = Vector3.op_Subtraction(((Component) transform).transform.position, Vector3.op_Subtraction(((Component) component2).transform.position, position));
  }

  private enum UI
  {
    LBL_DATA_NAME,
    LBL_DESCRIPTION,
    BTN_OK,
    GRD_DATA_LIST,
    SCR_DATA_LIST,
    STR_TITLE,
    STR_NEED_POINT,
    LBL_ABILITY_DATA_NAME,
    LBL_AP,
    SPR_ABILITY_ON,
  }
}
