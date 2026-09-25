// Decompiled with JetBrains decompiler
// Type: UISERequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("ProjectUI/UI SE Request")]
public class UISERequest : MonoBehaviour
{
  public SoundID.UISE SEType = SoundID.UISE.INVALID;
  [Range(0.0f, 1f)]
  public float volume = 1f;
  private const float pitch = 1f;
}
