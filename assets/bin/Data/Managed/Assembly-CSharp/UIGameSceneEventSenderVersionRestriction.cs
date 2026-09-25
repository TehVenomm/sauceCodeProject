// Decompiled with JetBrains decompiler
// Type: UIGameSceneEventSenderVersionRestriction
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (UIGameSceneEventSender))]
public class UIGameSceneEventSenderVersionRestriction : MonoBehaviour
{
  public uint major;
  public uint minor;
  public uint revision;

  public string GetCheckApplicationVersionText() => $"{this.major}.{this.minor}.{this.revision}";
}
