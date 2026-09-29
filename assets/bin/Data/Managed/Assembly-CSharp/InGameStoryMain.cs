// Decompiled with JetBrains decompiler
// Type: InGameStoryMain
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class InGameStoryMain : StoryMain
{
  public override void Initialize()
  {
    Utility.CreateGameObjectAndComponent("StoryDirector", MonoBehaviourSingleton<AppMain>.I._transform);
    if (MonoBehaviourSingleton<StageManager>.IsValid())
    {
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.stageObject, (Object) null))
        ((Component) MonoBehaviourSingleton<StageManager>.I.stageObject).gameObject.SetActive(false);
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.skyObject, (Object) null))
        ((Component) MonoBehaviourSingleton<StageManager>.I.skyObject).gameObject.SetActive(false);
    }
    base.Initialize();
  }

  protected override void OnDestroy()
  {
    if (MonoBehaviourSingleton<StageManager>.IsValid())
    {
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.stageObject, (Object) null))
        ((Component) MonoBehaviourSingleton<StageManager>.I.stageObject).gameObject.SetActive(true);
      if (Object.op_Inequality((Object) MonoBehaviourSingleton<StageManager>.I.skyObject, (Object) null))
        ((Component) MonoBehaviourSingleton<StageManager>.I.skyObject).gameObject.SetActive(true);
    }
    if (MonoBehaviourSingleton<StoryDirector>.IsValid())
      Object.Destroy((Object) ((Component) MonoBehaviourSingleton<StoryDirector>.I).gameObject);
    base.OnDestroy();
  }
}
