// Decompiled with JetBrains decompiler
// Type: Com.Google.Android.Gms.Common.Api.ResultCallbackProxy`1
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Google.Developers;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;

#nullable disable
namespace Com.Google.Android.Gms.Common.Api;

public abstract class ResultCallbackProxy<R> : JavaInterfaceProxy, ResultCallback<R> where R : Result
{
  private const string CLASS_NAME = "com/google/android/gms/common/api/ResultCallback";

  public ResultCallbackProxy()
    : base("com/google/android/gms/common/api/ResultCallback")
  {
  }

  public abstract void OnResult(R arg_Result_1);

  public void onResult(R arg_Result_1) => this.OnResult(arg_Result_1);

  public void onResult(AndroidJavaObject arg_Result_1)
  {
    IntPtr rawObject = arg_Result_1.GetRawObject();
    ConstructorInfo constructor = typeof (R).GetConstructor(new Type[1]
    {
      rawObject.GetType()
    });
    R r;
    if (constructor != (ConstructorInfo) null)
    {
      r = (R) constructor.Invoke(new object[1]
      {
        (object) rawObject
      });
    }
    else
    {
      r = (R) typeof (R).GetConstructor(new Type[0]).Invoke(new object[0]);
      Marshal.PtrToStructure<R>(rawObject, r);
    }
    this.OnResult(r);
  }
}
