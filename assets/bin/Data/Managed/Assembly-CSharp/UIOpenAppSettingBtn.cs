// Decompiled with JetBrains decompiler
// Type: UIOpenAppSettingBtn
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIOpenAppSettingBtn : MonoBehaviour
{
  private BootProcess currentBootProcess;

  public void SetBootProcess(BootProcess pro) => this.currentBootProcess = pro;

  public void OpenAppSetting() => AndroidPermissionsManager.OpenAppSetting();

  public void QuitApp() => Application.Quit();

  public void AskPermission()
  {
    this.currentBootProcess.OnGrantButtonPress();
    MonoBehaviourSingleton<UIManager>.I.loading.ShowEmptyFirstLoad(true);
    MonoBehaviourSingleton<UIManager>.I.loading.HideAllTextMsg();
  }

  private void OnEnable()
  {
    this.currentBootProcess = ((Component) MonoBehaviourSingleton<AppMain>.I).gameObject.GetComponent<BootProcess>();
  }

  private void OnDisable() => this.currentBootProcess = (BootProcess) null;
}
