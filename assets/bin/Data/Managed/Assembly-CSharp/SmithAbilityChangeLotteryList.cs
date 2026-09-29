// Decompiled with JetBrains decompiler
// Type: SmithAbilityChangeLotteryList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class SmithAbilityChangeLotteryList : GameSection
{
  private List<Transform> touchAndReleaseButtons = new List<Transform>();
  private List<EquipItemAbility> abilities;
  private List<SmithAbilityChangeLotteryList.MinMaxAp> minMaxAps;

  public override void Initialize() => this.StartCoroutine(this.DoInitialize());

  protected virtual IEnumerator DoInitialize()
  {
    object[] eventData = GameSection.GetEventData() as object[];
    EquipItemInfo equipItemInfo = eventData[0] as EquipItemInfo;
    SmithEquipBase.SmithType smithType = (SmithEquipBase.SmithType) eventData[1];
    bool wait = true;
    switch (smithType)
    {
      case SmithEquipBase.SmithType.GENERATE:
        MonoBehaviourSingleton<SmithManager>.I.SendGetAbilityListPreGenerate(MonoBehaviourSingleton<SmithManager>.I.GetSmithData<SmithManager.SmithCreateData>().createEquipItemTable.id, (Action<Error, List<SmithGetAbilityListForCreateModel.Param>>) ((error, list) =>
        {
          wait = false;
          this.SetAbilities(list);
        }));
        break;
      case SmithEquipBase.SmithType.ABILITY_CHANGE:
        MonoBehaviourSingleton<SmithManager>.I.SendGetAbilityList(equipItemInfo.uniqueID, (Action<Error, List<SmithGetAbilityList.Param>>) ((error, list) =>
        {
          wait = false;
          this.SetAbilities(list);
        }));
        break;
    }
    while (wait)
      yield return (object) null;
    base.Initialize();
  }

  protected void InitializeBase() => base.Initialize();

  protected void SetAbilities(List<SmithGetAbilityListForCreateModel.Param> list)
  {
    this.ClearAbilities();
    if (list == null)
      return;
    foreach (SmithGetAbilityListForCreateModel.Param obj in list)
    {
      this.abilities.Add(new EquipItemAbility((uint) obj.aid, 0));
      this.minMaxAps.Add(new SmithAbilityChangeLotteryList.MinMaxAp(obj.minap, obj.maxap));
    }
  }

  private void SetAbilities(List<SmithGetAbilityList.Param> list)
  {
    this.ClearAbilities();
    if (list == null)
      return;
    foreach (SmithGetAbilityList.Param obj in list)
    {
      this.abilities.Add(new EquipItemAbility((uint) obj.aid, 0));
      this.minMaxAps.Add(new SmithAbilityChangeLotteryList.MinMaxAp(obj.minap, obj.maxap));
    }
  }

  private void ClearAbilities()
  {
    this.abilities = new List<EquipItemAbility>();
    this.minMaxAps = new List<SmithAbilityChangeLotteryList.MinMaxAp>();
  }

  public override void UpdateUI()
  {
    this.SetLabelText((Enum) SmithAbilityChangeLotteryList.UI.STR_TITLE_REFLECT, this.sectionData.GetText("STR_TITLE"));
    this.SetDynamicList((Enum) SmithAbilityChangeLotteryList.UI.GRD_ABILITY, "SmithAbilityChangeLotteryListItem", this.abilities.Count, false, (Func<int, bool>) null, (Func<int, Transform, Transform>) null, (Action<int, Transform, bool>) ((index, t, reset) =>
    {
      EquipItemAbility ability = this.abilities[index];
      SmithAbilityChangeLotteryList.MinMaxAp minMaxAp = this.minMaxAps[index];
      string ap;
      string description;
      this.GetAbilityDetail(ability, minMaxAp.minAp, minMaxAp.maxAp, out ap, out description);
      this.SetLabelText(t, (Enum) SmithAbilityChangeLotteryList.UI.LBL_ABILITY_DETAIL_NAME, ability.GetName());
      this.SetLabelText(t, (Enum) SmithAbilityChangeLotteryList.UI.LBL_ABILITY_DETAIL_POINT, ap);
      this.SetLabelText(t, (Enum) SmithAbilityChangeLotteryList.UI.LBL_ABILITY_DETAIL_DESC, description);
    }));
  }

  public override void OnNotify(GameSection.NOTIFY_FLAG flags)
  {
    base.OnNotify(flags);
    if ((flags & GameSection.NOTIFY_FLAG.PRETREAT_SCENE) == (GameSection.NOTIFY_FLAG) 0)
      return;
    this.NoEventReleaseTouchAndReleases(this.touchAndReleaseButtons);
  }

  private void GetAbilityDetail(
    EquipItemAbility ability,
    int minAp,
    int maxAp,
    out string ap,
    out string description)
  {
    ap = "";
    description = "";
    if (minAp == maxAp)
    {
      ap = "+" + minAp.ToString();
      description = Singleton<AbilityDataTable>.I.GetAbilityData(ability.id, minAp).description;
    }
    else
    {
      ap = $"+{minAp.ToString()}〜{maxAp.ToString()}";
      description = Singleton<AbilityDataTable>.I.GenerateAbilityDescriptionPreGrant(ability.id, minAp, maxAp);
    }
  }

  private enum UI
  {
    STR_TITLE_REFLECT,
    GRD_ABILITY,
    LBL_ABILITY_DETAIL_NAME,
    LBL_ABILITY_DETAIL_DESC,
    LBL_ABILITY_DETAIL_POINT,
  }

  private struct MinMaxAp(int minAp, int maxAp)
  {
    public int minAp = minAp;
    public int maxAp = maxAp;
  }
}
