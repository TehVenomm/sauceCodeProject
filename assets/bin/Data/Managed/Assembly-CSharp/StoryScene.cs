// Decompiled with JetBrains decompiler
// Type: StoryScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class StoryScene : GameSection
{
  public override void Initialize()
  {
    Utility.CreateGameObjectAndComponent("StoryDirector", MonoBehaviourSingleton<AppMain>.I._transform);
    base.Initialize();
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    if (!MonoBehaviourSingleton<StoryDirector>.IsValid())
      return;
    Object.Destroy((Object) ((Component) MonoBehaviourSingleton<StoryDirector>.I).gameObject);
  }

  public override void UpdateUI()
  {
  }
}
