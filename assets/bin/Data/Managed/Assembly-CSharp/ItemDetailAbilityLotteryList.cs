// Decompiled with JetBrains decompiler
// Type: ItemDetailAbilityLotteryList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
public class ItemDetailAbilityLotteryList : SmithAbilityChangeLotteryList
{
  protected override IEnumerator DoInitialize()
  {
    bool wait = true;
    MonoBehaviourSingleton<SmithManager>.I.SendGetAbilityListPreGenerate((GameSection.GetEventData() as CreateEquipItemTable.CreateEquipItemData).id, (Action<Error, List<SmithGetAbilityListForCreateModel.Param>>) ((error, list) =>
    {
      wait = false;
      this.SetAbilities(list);
    }));
    while (wait)
      yield return (object) null;
    this.InitializeBase();
  }
}
