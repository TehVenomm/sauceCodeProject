// Decompiled with JetBrains decompiler
// Type: GachaDecoManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GachaDecoManager : MonoBehaviourSingleton<GachaDecoManager>
{
  private bool visible;
  private List<GachaDeco> list;
  private int index;

  public void SetVisible(bool is_visible) => this.visible = is_visible;

  private IEnumerator Start()
  {
    float timer = 0.0f;
    double interval_time = (double) MonoBehaviourSingleton<OutGameSettingsManager>.I.homeScene.gachaDecoIntervalTime;
    GachaDeco info = (GachaDeco) null;
    while (true)
    {
      do
      {
        yield return (object) null;
        if (this.list != MonoBehaviourSingleton<UserInfoManager>.I.gachaDecoList)
        {
          this.list = MonoBehaviourSingleton<UserInfoManager>.I.gachaDecoList;
          this.index = 0;
        }
        if (this.IsVisible() && this.list != null && this.list.Count != 0)
          goto label_6;
      }
      while (info == null);
      this.UpdateGachaDeco((GachaDeco) null);
      info = (GachaDeco) null;
      continue;
label_6:
      if (this.list.Count <= this.index)
        this.index = 0;
      info = this.list[this.index];
      double num = (double) info.remainTime - new TimeSpan(TimeManager.GetNow().Ticks - MonoBehaviourSingleton<UserInfoManager>.I.gachaDecoDateBase).TotalSeconds;
      double wait_time = interval_time;
      if (wait_time > num)
        wait_time = num;
      if (wait_time > 1.0)
      {
        this.UpdateGachaDeco(info);
        timer = (float) wait_time;
        while ((double) timer > 0.0 && this.IsVisible())
        {
          timer -= Time.deltaTime;
          yield return (object) null;
        }
      }
      if (wait_time != interval_time)
        this.list.Remove(info);
      else
        ++this.index;
    }
  }

  protected override void _OnDestroy() => this.UpdateGachaDeco((GachaDeco) null);

  private void UpdateGachaDeco(GachaDeco info)
  {
    if (!MonoBehaviourSingleton<UIManager>.IsValid() || !Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainMenu, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.mainMenu.UpdateGachaDeco(info);
  }

  private bool IsVisible()
  {
    if (!this.visible)
      return false;
    bool flag = false;
    List<GameSectionHierarchy.HierarchyData> hierarchyList = MonoBehaviourSingleton<GameSceneManager>.I.GetHierarchyList();
    int index = 0;
    for (int count = hierarchyList.Count; index < count; ++index)
    {
      GameSectionHierarchy.HierarchyData hierarchyData = hierarchyList[index];
      if (hierarchyData != null && hierarchyData.data != (GameSceneTables.SectionData) null)
      {
        if (hierarchyData.data.sectionName == "HomeTop" || hierarchyData.data.sectionName == "LoungeTop" || hierarchyData.data.sectionName == "ClanTop")
          flag = true;
        else if (flag && !hierarchyData.data.type.IsDialog())
        {
          flag = false;
          break;
        }
      }
    }
    return flag;
  }
}
