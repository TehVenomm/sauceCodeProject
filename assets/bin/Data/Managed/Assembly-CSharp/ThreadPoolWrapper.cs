// Decompiled with JetBrains decompiler
// Type: ThreadPoolWrapper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Threading;

#nullable disable
public class ThreadPoolWrapper
{
  public static void QueueUserWorkItem(WaitCallback callback)
  {
    ThreadPool.QueueUserWorkItem(callback);
  }
}
