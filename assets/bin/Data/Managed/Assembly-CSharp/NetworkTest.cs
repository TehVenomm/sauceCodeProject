// Decompiled with JetBrains decompiler
// Type: NetworkTest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class NetworkTest : MonoBehaviour
{
  private NetworkRegistTest netRegister;
  public bool isAutoMode = true;
  public bool isLocalhost = true;

  private void Awake()
  {
    ((Component) this).gameObject.AddComponent<NetworkManager>();
    ((Component) this).gameObject.AddComponent<AccountManager>();
    this.netRegister = ((Component) this).gameObject.AddComponent<NetworkRegistTest>();
  }

  private void Start()
  {
  }

  private void Update()
  {
    if (!this.isAutoMode)
      return;
    this.netRegister.SendRequest();
  }

  private void OnGUI()
  {
    GUILayout.BeginArea(new Rect(10f, 30f, 250f, 100f));
    if (this.netRegister.progress < NetworkRegistTest.PROGRESS.REGIST_FAILED)
    {
      if (GUILayout.Button($"Network Request\n[{(object) this.netRegister.progress}] {(this.netRegister.isSending ? (object) "Sending..." : (object) "")}", Array.Empty<GUILayoutOption>()))
        this.netRegister.SendRequest();
    }
    if (GUILayout.Button("ClearSaveData", Array.Empty<GUILayoutOption>()))
      MonoBehaviourSingleton<AccountManager>.I.ClearAccount();
    GUILayout.EndArea();
  }
}
