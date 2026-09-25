// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeEndpointDiscoveryListenerHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class NativeEndpointDiscoveryListenerHelper : BaseReferenceHolder
{
  internal NativeEndpointDiscoveryListenerHelper()
    : base(EndpointDiscoveryListenerHelper.EndpointDiscoveryListenerHelper_Construct())
  {
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    EndpointDiscoveryListenerHelper.EndpointDiscoveryListenerHelper_Dispose(selfPointer);
  }

  internal void SetOnEndpointFound(Action<long, NativeEndpointDetails> callback)
  {
    EndpointDiscoveryListenerHelper.EndpointDiscoveryListenerHelper_SetOnEndpointFoundCallback(this.SelfPtr(), new EndpointDiscoveryListenerHelper.OnEndpointFoundCallback(NativeEndpointDiscoveryListenerHelper.InternalOnEndpointFoundCallback), Callbacks.ToIntPtr<long, NativeEndpointDetails>(callback, new Func<IntPtr, NativeEndpointDetails>(NativeEndpointDetails.FromPointer)));
  }

  [MonoPInvokeCallback(typeof (EndpointDiscoveryListenerHelper.OnEndpointFoundCallback))]
  private static void InternalOnEndpointFoundCallback(long id, IntPtr data, IntPtr userData)
  {
    Callbacks.PerformInternalCallback<long>("NativeEndpointDiscoveryListenerHelper#InternalOnEndpointFoundCallback", Callbacks.Type.Permanent, id, data, userData);
  }

  internal void SetOnEndpointLostCallback(Action<long, string> callback)
  {
    EndpointDiscoveryListenerHelper.EndpointDiscoveryListenerHelper_SetOnEndpointLostCallback(this.SelfPtr(), new EndpointDiscoveryListenerHelper.OnEndpointLostCallback(NativeEndpointDiscoveryListenerHelper.InternalOnEndpointLostCallback), Callbacks.ToIntPtr((Delegate) callback));
  }

  [MonoPInvokeCallback(typeof (EndpointDiscoveryListenerHelper.OnEndpointLostCallback))]
  private static void InternalOnEndpointLostCallback(
    long id,
    string lostEndpointId,
    IntPtr userData)
  {
    Action<long, string> permanentCallback = Callbacks.IntPtrToPermanentCallback<Action<long, string>>(userData);
    if (permanentCallback == null)
      return;
    try
    {
      permanentCallback(id, lostEndpointId);
    }
    catch (Exception ex)
    {
      Logger.e("Error encountered executing NativeEndpointDiscoveryListenerHelper#InternalOnEndpointLostCallback. Smothering to avoid passing exception into Native: " + (object) ex);
    }
  }
}
