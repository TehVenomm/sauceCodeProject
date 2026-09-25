// Decompiled with JetBrains decompiler
// Type: DebugScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class DebugScene : GameSection
{
  public override void Initialize()
  {
    if (MonoBehaviourSingleton<HomeManager>.IsValid())
      Object.Destroy((Object) ((Component) MonoBehaviourSingleton<HomeManager>.I).gameObject);
    base.Initialize();
  }
}
