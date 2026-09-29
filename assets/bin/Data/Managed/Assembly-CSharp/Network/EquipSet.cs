// Decompiled with JetBrains decompiler
// Type: Network.EquipSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class EquipSet
{
  public int setNo;
  public EquipItem weapon_0 = new EquipItem();
  public EquipItem weapon_1 = new EquipItem();
  public EquipItem weapon_2 = new EquipItem();
  public EquipItem armor = new EquipItem();
  public EquipItem arm = new EquipItem();
  public EquipItem leg = new EquipItem();
  public EquipItem helm = new EquipItem();
  public string setName = "";
  public int showHelm;
  public AccessoryPlaceInfo acc = new AccessoryPlaceInfo();
}
