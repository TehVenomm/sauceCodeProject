// Decompiled with JetBrains decompiler
// Type: Network.EquipItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class EquipItem
{
  public string uniqId;
  public int equipItemId;
  public XorInt level = (XorInt) 0;
  public int exceed;
  public int is_locked;
  public int price;
  public List<EquipItem.Ability> ability = new List<EquipItem.Ability>();
  public AbilityItem abilityItem;

  public class Ability
  {
    public int id;
    public int pt;
    public bool vr;
  }
}
