// Decompiled with JetBrains decompiler
// Type: Com.Google.Android.Gms.Common.Api.PendingResult`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Google.Developers;
using System;

#nullable disable
namespace Com.Google.Android.Gms.Common.Api;

public class PendingResult<R> : JavaObjWrapper where R : Result
{
  private const string CLASS_NAME = "com/google/android/gms/common/api/PendingResult";

  public PendingResult(IntPtr ptr)
    : base(ptr)
  {
  }

  public PendingResult()
    : base("com.google.android.gms.common.api.PendingResult")
  {
  }

  public R await(long arg_long_1, object arg_object_2)
  {
    return this.InvokeCall<R>(nameof (await), "(JLjava/util/concurrent/TimeUnit;)Lcom/google/android/gms/common/api/Result;", (object) arg_long_1, arg_object_2);
  }

  public R await()
  {
    return this.InvokeCall<R>(nameof (await), "()Lcom/google/android/gms/common/api/Result;");
  }

  public bool isCanceled() => this.InvokeCall<bool>(nameof (isCanceled), "()Z");

  public void cancel() => this.InvokeCallVoid(nameof (cancel), "()V");

  public void setResultCallback(ResultCallback<R> arg_ResultCallback_1)
  {
    this.InvokeCallVoid(nameof (setResultCallback), "(Lcom/google/android/gms/common/api/ResultCallback;)V", (object) arg_ResultCallback_1);
  }

  public void setResultCallback(
    ResultCallback<R> arg_ResultCallback_1,
    long arg_long_2,
    object arg_object_3)
  {
    this.InvokeCallVoid(nameof (setResultCallback), "(Lcom/google/android/gms/common/api/ResultCallback;JLjava/util/concurrent/TimeUnit;)V", (object) arg_ResultCallback_1, (object) arg_long_2, arg_object_3);
  }
}
