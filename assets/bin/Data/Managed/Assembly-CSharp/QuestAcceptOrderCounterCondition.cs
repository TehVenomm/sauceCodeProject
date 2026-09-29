// Decompiled with JetBrains decompiler
// Type: QuestAcceptOrderCounterCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class QuestAcceptOrderCounterCondition : QuestSearchRoomCondition
{
  public override void Initialize()
  {
    base.Initialize();
    this.SetActive((Enum) QuestSearchRoomCondition.UI.PRIORITY_ROOT, false);
    this.SetActive((Enum) QuestSearchRoomCondition.UI.OBJ_SEARCH, false);
    this.SetActive((Enum) QuestSearchRoomCondition.UI.OBJ_MY_SEARCH, true);
    UIWidget component = this.GetComponent<UIWidget>((Enum) QuestSearchRoomCondition.UI.OBJ_FRAME);
    if (!Object.op_Inequality((Object) component, (Object) null))
      return;
    component.height = 640;
    ((Component) this).transform.localPosition = new Vector3(0.0f, -41f, 0.0f);
    component.UpdateAnchors();
  }

  protected override void CopySearchRequestParam()
  {
  }

  protected override void LoadSearchRequestParam() => this.SetSearchRequestFromPrefs();

  public override void UpdateUI() => base.UpdateUI();

  protected override void OnQuery_SEARCH()
  {
    this.FixBit();
    if (this.searchRequest.rarityBit == 0)
      GameSection.ChangeEvent("NOT_RARITY");
    else if (this.searchRequest.elementBit == 0)
    {
      GameSection.ChangeEvent("NOT_ELEMENT");
    }
    else
    {
      this.SaveSettingsMyGachaSearch();
      this.searchRequest.order = 1;
      GameSection.SetEventData((object) this.searchRequest);
    }
  }

  public void SaveSettingsMyGachaSearch()
  {
    PlayerPrefs.SetInt("MY_GACHA_SEARCH_RAIRTY_KEY", this.searchRequest.rarityBit);
    PlayerPrefs.SetInt("MY_GACHA_SEARCH_ELEMENT_KEY", this.searchRequest.elementBit);
    PlayerPrefs.SetInt("MY_GACHA_SEARCH_LEVEL_MIN_KEY", this.searchRequest.enemyLevelMin);
    PlayerPrefs.SetInt("MY_GACHA_SEARCH_LEVEL_MAX_KEY", this.searchRequest.enemyLevelMax);
    if (!string.IsNullOrEmpty(this.searchRequest.targetEnemySpeciesName))
      PlayerPrefs.SetString("MY_GACHA_SEARCH_SPECIES_KEY", this.searchRequest.targetEnemySpeciesName);
    PlayerPrefs.Save();
  }

  public void SetSearchRequestFromPrefs()
  {
    this.searchRequest = new QuestSearchRoomCondition.SearchRequestParam();
    this.searchRequest.rarityBit = PlayerPrefs.GetInt("MY_GACHA_SEARCH_RAIRTY_KEY", 8388607 /*0x7FFFFF*/);
    this.searchRequest.elementBit = PlayerPrefs.GetInt("MY_GACHA_SEARCH_ELEMENT_KEY", 8388607 /*0x7FFFFF*/);
    this.searchRequest.enemyLevelMin = PlayerPrefs.GetInt("MY_GACHA_SEARCH_LEVEL_MIN_KEY", 1);
    int num = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_LEVEL_MAX;
    if (MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_EXTRA_LEVEL_MAX > 0)
      num = MonoBehaviourSingleton<UserInfoManager>.I.userInfo.constDefine.PARTY_SEARCH_QUEST_EXTRA_LEVEL_MAX;
    this.searchRequest.enemyLevelMax = PlayerPrefs.GetInt("MY_GACHA_SEARCH_LEVEL_MAX_KEY", num);
    this.searchRequest.targetEnemySpeciesName = PlayerPrefs.GetString("MY_GACHA_SEARCH_SPECIES_KEY", (string) null);
  }
}
