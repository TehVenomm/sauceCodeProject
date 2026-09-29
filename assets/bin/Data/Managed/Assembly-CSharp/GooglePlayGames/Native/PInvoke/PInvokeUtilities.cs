// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.PInvokeUtilities
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

#nullable disable
namespace GooglePlayGames.Native.PInvoke;

internal static class PInvokeUtilities
{
  private static readonly DateTime UnixEpoch = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);

  internal static HandleRef CheckNonNull(HandleRef reference)
  {
    return !PInvokeUtilities.IsNull(reference) ? reference : throw new InvalidOperationException();
  }

  internal static bool IsNull(HandleRef reference)
  {
    return PInvokeUtilities.IsNull(HandleRef.ToIntPtr(reference));
  }

  internal static bool IsNull(IntPtr pointer) => pointer.Equals((object) IntPtr.Zero);

  internal static DateTime FromMillisSinceUnixEpoch(long millisSinceEpoch)
  {
    return PInvokeUtilities.UnixEpoch.Add(TimeSpan.FromMilliseconds((double) millisSinceEpoch));
  }

  internal static string OutParamsToString(PInvokeUtilities.OutStringMethod outStringMethod)
  {
    UIntPtr out_size = outStringMethod((byte[]) null, UIntPtr.Zero);
    if (out_size.Equals((object) UIntPtr.Zero))
      return (string) null;
    try
    {
      byte[] numArray = new byte[(int) out_size.ToUInt32()];
      IntPtr num = (IntPtr) outStringMethod(numArray, out_size);
      return Encoding.UTF8.GetString(numArray, 0, (int) out_size.ToUInt32() - 1);
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ("Exception creating string from char array: " + (object) ex));
      return string.Empty;
    }
  }

  internal static T[] OutParamsToArray<T>(PInvokeUtilities.OutMethod<T> outMethod)
  {
    UIntPtr out_size = outMethod((T[]) null, UIntPtr.Zero);
    if (out_size.Equals((object) UIntPtr.Zero))
      return new T[0];
    T[] out_bytes = new T[out_size.ToUInt64()];
    IntPtr num = (IntPtr) outMethod(out_bytes, out_size);
    return out_bytes;
  }

  internal static IEnumerable<T> ToEnumerable<T>(UIntPtr size, Func<UIntPtr, T> getElement)
  {
    for (ulong i = 0; i < size.ToUInt64(); ++i)
      yield return getElement(new UIntPtr(i));
  }

  internal static IEnumerator<T> ToEnumerator<T>(UIntPtr size, Func<UIntPtr, T> getElement)
  {
    return PInvokeUtilities.ToEnumerable<T>(size, getElement).GetEnumerator();
  }

  internal static UIntPtr ArrayToSizeT<T>(T[] array)
  {
    return array == null ? UIntPtr.Zero : new UIntPtr((ulong) array.Length);
  }

  internal static long ToMilliseconds(TimeSpan span)
  {
    double totalMilliseconds = span.TotalMilliseconds;
    if (totalMilliseconds > (double) long.MaxValue)
      return long.MaxValue;
    return totalMilliseconds < (double) long.MinValue ? long.MinValue : Convert.ToInt64(totalMilliseconds);
  }

  internal delegate UIntPtr OutStringMethod([In, Out] byte[] out_bytes, UIntPtr out_size);

  internal delegate UIntPtr OutMethod<T>([In, Out] T[] out_bytes, UIntPtr out_size);
}
