// Decompiled with JetBrains decompiler
// Type: Network.AccessoryPlaceInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Network;

[Serializable]
public class AccessoryPlaceInfo
{
  public List<string> ids = new List<string>();
  public List<string> parts = new List<string>();

  public uint GetId(int index)
  {
    if (index >= this.ids.Count)
      return 0;
    uint result = 0;
    return uint.TryParse(this.ids[index], out result) ? result : 0U;
  }

  public int GetPart(int index)
  {
    if (index >= this.parts.Count)
      return 0;
    int result = 0;
    return int.TryParse(this.parts[index], out result) ? result : 0;
  }

  public void Add(string _uuid, string _part)
  {
    this.ids.Add(_uuid);
    this.parts.Add(_part);
  }

  public void Del(string _uuid, string _part)
  {
    int index = 0;
    for (int count = this.ids.Count; index < count; ++index)
    {
      if (this.ids[index] == _uuid && this.parts[index] == _part)
      {
        this.ids.RemoveAt(index);
        this.parts.RemoveAt(index);
        break;
      }
    }
  }

  public void Clear()
  {
    this.ids.Clear();
    this.parts.Clear();
  }

  public void Copy(AccessoryPlaceInfo src)
  {
    this.Clear();
    if (src == null)
      return;
    if (!src.ids.IsNullOrEmpty<string>())
      this.ids.AddRange((IEnumerable<string>) src.ids);
    if (src.parts.IsNullOrEmpty<string>())
      return;
    this.parts.AddRange((IEnumerable<string>) src.parts);
  }

  public bool IsEqual(AccessoryPlaceInfo src)
  {
    if (this.ids.Count != src.ids.Count)
      return false;
    int index = 0;
    for (int count = src.ids.Count; index < count; ++index)
    {
      string id = src.ids[index];
      if (!this.ids.Contains(id))
        return false;
      string part = src.parts[index];
      if (this.parts[this.ids.IndexOf(id)] != part)
        return false;
    }
    return true;
  }

  public List<CharaInfo.UserAccessory> ConvertAccessory()
  {
    List<CharaInfo.UserAccessory> userAccessoryList = new List<CharaInfo.UserAccessory>();
    int index = 0;
    for (int count = this.ids.Count; index < count; ++index)
    {
      CharaInfo.UserAccessory userAccessory = new CharaInfo.UserAccessory()
      {
        uniqId = this.ids[index],
        place = int.Parse(this.parts[index])
      };
      userAccessory.accessoryId = (int) MonoBehaviourSingleton<InventoryManager>.I.accessoryInventory.GetTableID(userAccessory.uniqId);
      userAccessoryList.Add(userAccessory);
    }
    return userAccessoryList;
  }
}
