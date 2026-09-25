// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.PlayerSelectUIResponse
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using GooglePlayGames.Native.Cwrapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal class PlayerSelectUIResponse : BaseReferenceHolder, IEnumerable<string>, IEnumerable
{
  internal PlayerSelectUIResponse(IntPtr selfPointer)
    : base(selfPointer)
  {
  }

  internal CommonErrorStatus.UIStatus Status()
  {
    return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetStatus(this.SelfPtr());
  }

  private string PlayerIdAtIndex(UIntPtr index)
  {
    return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_string, size) => TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetPlayerIds_GetElement(this.SelfPtr(), index, out_string, size)));
  }

  public IEnumerator<string> GetEnumerator()
  {
    return PInvokeUtilities.ToEnumerator<string>(TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetPlayerIds_Length(this.SelfPtr()), new Func<UIntPtr, string>(this.PlayerIdAtIndex));
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  internal uint MinimumAutomatchingPlayers()
  {
    return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetMinimumAutomatchingPlayers(this.SelfPtr());
  }

  internal uint MaximumAutomatchingPlayers()
  {
    return TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_GetMaximumAutomatchingPlayers(this.SelfPtr());
  }

  protected override void CallDispose(HandleRef selfPointer)
  {
    TurnBasedMultiplayerManager.TurnBasedMultiplayerManager_PlayerSelectUIResponse_Dispose(selfPointer);
  }

  internal static PlayerSelectUIResponse FromPointer(IntPtr pointer)
  {
    return PInvokeUtilities.IsNull(pointer) ? (PlayerSelectUIResponse) null : new PlayerSelectUIResponse(pointer);
  }
}
