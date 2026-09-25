// Decompiled with JetBrains decompiler
// Type: BestHTTP.HTTPUpdateDelegator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using BestHTTP.Caching;
using UnityEngine;

#nullable disable
namespace BestHTTP;

internal sealed class HTTPUpdateDelegator : MonoBehaviour
{
  private static HTTPUpdateDelegator instance;

  public static void CheckInstance()
  {
    if (Object.op_Implicit((Object) HTTPUpdateDelegator.instance))
      return;
    HTTPUpdateDelegator.instance = Object.FindObjectOfType(typeof (HTTPUpdateDelegator)) as HTTPUpdateDelegator;
    if (Object.op_Implicit((Object) HTTPUpdateDelegator.instance))
      return;
    GameObject gameObject = new GameObject("HTTP Update Delegator");
    ((Object) gameObject).hideFlags = (HideFlags) 3;
    Object.DontDestroyOnLoad((Object) gameObject);
    HTTPUpdateDelegator.instance = gameObject.AddComponent<HTTPUpdateDelegator>();
  }

  private void Awake() => HTTPCacheService.SetupCacheFolder();

  private void LateUpdate() => HTTPManager.OnUpdate();

  private void OnApplicationQuit() => HTTPManager.OnQuit();
}
