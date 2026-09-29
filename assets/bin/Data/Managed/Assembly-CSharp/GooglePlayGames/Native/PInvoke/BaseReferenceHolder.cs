// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.BaseReferenceHolder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal abstract class BaseReferenceHolder : IDisposable
{
  private static Dictionary<HandleRef, BaseReferenceHolder> _refs = new Dictionary<HandleRef, BaseReferenceHolder>();
  private HandleRef mSelfPointer;

  protected bool IsDisposed() => PInvokeUtilities.IsNull(this.mSelfPointer);

  protected HandleRef SelfPtr()
  {
    if (this.IsDisposed())
      throw new InvalidOperationException("Attempted to use object after it was cleaned up");
    return this.mSelfPointer;
  }

  public BaseReferenceHolder(IntPtr pointer)
  {
    this.mSelfPointer = PInvokeUtilities.CheckNonNull(new HandleRef((object) this, pointer));
  }

  protected abstract void CallDispose(HandleRef selfPointer);

  ~BaseReferenceHolder() => this.Dispose(true);

  public void Dispose()
  {
    this.Dispose(false);
    GC.SuppressFinalize((object) this);
  }

  internal IntPtr AsPointer() => this.SelfPtr().Handle;

  private void Dispose(bool fromFinalizer)
  {
    if (!fromFinalizer && BaseReferenceHolder._refs.ContainsKey(this.mSelfPointer) || PInvokeUtilities.IsNull(this.mSelfPointer))
      return;
    this.CallDispose(this.mSelfPointer);
    this.mSelfPointer = new HandleRef((object) this, IntPtr.Zero);
  }

  internal void ReferToMe() => BaseReferenceHolder._refs[this.SelfPtr()] = this;

  internal void ForgetMe()
  {
    if (!BaseReferenceHolder._refs.ContainsKey(this.SelfPtr()))
      return;
    BaseReferenceHolder._refs.Remove(this.SelfPtr());
    this.Dispose(false);
  }
}
