// Decompiled with JetBrains decompiler
// Type: Network.EquipSetSimple
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class EquipSetSimple
{
  public int setNo;
  public string weapon_0 = "";
  public string weapon_1 = "";
  public string weapon_2 = "";
  public string armor = "";
  public string arm = "";
  public string leg = "";
  public string helm = "";
  public string setName = "";
  public int showHelm;
  public AccessoryPlaceInfo acc = new AccessoryPlaceInfo();
  public int order;
}
