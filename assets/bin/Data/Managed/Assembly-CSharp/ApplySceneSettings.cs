// Decompiled with JetBrains decompiler
// Type: ApplySceneSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class ApplySceneSettings : MonoBehaviour
{
  public bool applyFogParams = true;
  public bool applyEffectColor = true;

  private void Start() => Object.DestroyImmediate((Object) this);
}
