// Decompiled with JetBrains decompiler
// Type: GuildStageManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class GuildStageManager : MonoBehaviourSingleton<GuildStageManager>
{
  public HomePeople HomePeople { get; private set; }

  public HomeCamera HomeCamera { get; private set; }

  public bool IsInitialized { get; private set; }

  private IEnumerator Start()
  {
    while (!MonoBehaviourSingleton<StageManager>.IsValid() || MonoBehaviourSingleton<StageManager>.I.isLoading)
      yield return (object) null;
    this.HomeCamera = ((Component) this).gameObject.AddComponent<HomeCamera>();
    this.HomePeople = ((Component) this).gameObject.AddComponent<HomePeople>();
    while (!this.HomeCamera.isInitialized || !this.HomePeople.isInitialized)
      yield return (object) null;
    this.IsInitialized = true;
  }
}
