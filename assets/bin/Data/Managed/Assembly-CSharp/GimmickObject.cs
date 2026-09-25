// Decompiled with JetBrains decompiler
// Type: GimmickObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class GimmickObject : StageObject
{
  public static int GetID(string name)
  {
    string s = "";
    int index = 0;
    for (int length = name.Length; index < length; ++index)
    {
      if (name[index] >= '0' && '9' >= name[index])
        s += name[index].ToString();
    }
    return int.Parse(s) + 200000;
  }

  protected override void Awake()
  {
    base.Awake();
    Utility.SetLayerWithChildren(((Component) this).transform, 18);
    this.id = GimmickObject.GetID(((Object) ((Component) this).gameObject).name);
  }
}
