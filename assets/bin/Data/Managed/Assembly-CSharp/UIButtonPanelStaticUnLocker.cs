// Decompiled with JetBrains decompiler
// Type: UIButtonPanelStaticUnLocker
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (UIButton))]
public class UIButtonPanelStaticUnLocker : MonoBehaviour
{
  [SerializeField]
  protected UIStaticPanelChanger panelChange;
  private UIButton btn;
  private bool isLock;
  private float timer;

  private void Awake() => this.btn = ((Component) this).GetComponent<UIButton>();

  private void OnPress(bool pressed)
  {
    if (!this.isLock)
      this.panelChange.UnLock();
    this.timer = this.btn.duration + 0.1f;
    this.isLock = true;
  }

  private void LateUpdate()
  {
    if (!this.isLock)
      return;
    this.timer -= Time.deltaTime;
    if ((double) this.timer > 0.0)
      return;
    this.panelChange.Lock();
    this.isLock = false;
  }
}
