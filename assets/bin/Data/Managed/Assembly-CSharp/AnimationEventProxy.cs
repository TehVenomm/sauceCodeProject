// Decompiled with JetBrains decompiler
// Type: AnimationEventProxy
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class AnimationEventProxy : MonoBehaviour
{
  public AnimationEventProxy.IEvent listener;

  private void OnEvent()
  {
    if (this.listener == null)
      return;
    this.listener.OnEvent();
  }

  private void OnEventStr(string str)
  {
    if (this.listener == null)
      return;
    this.listener.OnEventStr(str);
  }

  private void OnEventInt(int i)
  {
    if (this.listener == null)
      return;
    this.listener.OnEventInt(i);
  }

  public interface IEvent
  {
    void OnEvent();

    void OnEventStr(string str);

    void OnEventInt(int i);
  }
}
