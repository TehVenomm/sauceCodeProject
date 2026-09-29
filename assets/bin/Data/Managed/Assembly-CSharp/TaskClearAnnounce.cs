// Decompiled with JetBrains decompiler
// Type: TaskClearAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class TaskClearAnnounce : UIBehaviour
{
  private const int SE_ID = 40000158;
  private AudioClip m_AudioClip;

  private void StoreAudioClip()
  {
    string se = ResourceName.GetSE(40000158);
    if (string.IsNullOrEmpty(se))
      return;
    Transform child = ((Component) this).transform.GetChild(0);
    if (Object.op_Equality((Object) child, (Object) null))
      return;
    ResourceLink component = ((Component) child).GetComponent<ResourceLink>();
    if (Object.op_Equality((Object) component, (Object) null))
      return;
    this.m_AudioClip = component.Get<AudioClip>(se);
  }

  private void PlayAudio()
  {
    if (!Object.op_Inequality((Object) this.m_AudioClip, (Object) null))
      return;
    SoundManager.PlayOneshotJingle(this.m_AudioClip, 40000158);
  }

  private void Start()
  {
    Transform ctrl = this.GetCtrl((Enum) TaskClearAnnounce.UI.OBJ_EFFECT);
    if (Object.op_Inequality((Object) ctrl, (Object) null))
      ctrl.localScale = Vector3.zero;
    this.StoreAudioClip();
  }

  public void Play(string announce, string reward, System.Action onComplete)
  {
    UIWidget component1 = this.GetComponent<UIWidget>((Enum) TaskClearAnnounce.UI.WGT_ANCHOR_POINT);
    UITweenCtrl component2 = this.GetComponent<UITweenCtrl>((Enum) TaskClearAnnounce.UI.OBJ_TWEENCTRL);
    if (Object.op_Equality((Object) component1, (Object) null) || Object.op_Equality((Object) component2, (Object) null))
    {
      if (onComplete == null)
        return;
      onComplete();
    }
    else
    {
      component1.leftAnchor.Set(1f, 150f);
      component1.rightAnchor.Set(1f, 300f);
      component1.bottomAnchor.Set(1f, -130f);
      component1.topAnchor.Set(1f, -105f);
      component1.UpdateAnchors();
      this.SetLabelText((Enum) TaskClearAnnounce.UI.LBL_ANNOUNCE, announce);
      this.SetLabelText((Enum) TaskClearAnnounce.UI.LBL_REWARD, reward);
      component2.Reset();
      component2.Play(onFinished: (EventDelegate.Callback) (() =>
      {
        if (onComplete == null)
          return;
        onComplete();
      }));
      this.PlayAudio();
    }
  }

  public enum UI
  {
    WGT_ANCHOR_POINT,
    OBJ_TWEENCTRL,
    OBJ_EFFECT,
    LBL_ANNOUNCE,
    LBL_REWARD,
  }
}
