// Decompiled with JetBrains decompiler
// Type: GoGameSettingsManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class GoGameSettingsManager : MonoBehaviourSingleton<GoGameSettingsManager>
{
  public List<string> itemsUseShopUI3 = new List<string>();
  public List<int> weaponsWhichPlayerUseItCanFly = new List<int>();
  public List<int> enemysCanFly = new List<int>();
  public Vector3 colliderOfMapScale = new Vector3(1f, 3.5f, 1f);
  public List<long> weaponLimitedNumAbilities = new List<long>();
  public int numAbilityCheck = 4;
  public int limitAbility = 3;
  public List<string> tradingpostCurrentScene = new List<string>();
  public List<string> tradingpostStartSection = new List<string>();
  public List<string> tradingPostDialogBlockerSection = new List<string>();

  public bool UseShopUI3(string spriteName)
  {
    return !string.IsNullOrEmpty(spriteName) && this.itemsUseShopUI3 != null && this.itemsUseShopUI3.Count > 0 && this.itemsUseShopUI3.Contains(spriteName);
  }

  public bool PreventUpdateDialogBlocker(string section_name)
  {
    return this.tradingPostDialogBlockerSection.Contains(section_name);
  }
}
