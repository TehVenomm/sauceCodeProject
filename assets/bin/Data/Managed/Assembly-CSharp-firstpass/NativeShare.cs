// Decompiled with JetBrains decompiler
// Type: NativeShare
// Assembly: Assembly-CSharp-firstpass, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BB1BE8DD-31E2-441F-A619-55A0A9A5488F
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp-firstpass.dll

using System.Collections.Generic;
using System.IO;
using UnityEngine;

#nullable disable
public class NativeShare
{
  private static AndroidJavaClass m_ajc;
  private static AndroidJavaObject m_context;
  private string subject;
  private string text;
  private string title;
  private string targetPackage;
  private string targetClass;
  private List<string> files;
  private List<string> mimes;

  private static AndroidJavaClass AJC
  {
    get
    {
      if (NativeShare.m_ajc == null)
        NativeShare.m_ajc = new AndroidJavaClass("com.yasirkula.unity.NativeShare");
      return NativeShare.m_ajc;
    }
  }

  private static AndroidJavaObject Context
  {
    get
    {
      if (NativeShare.m_context == null)
      {
        using (AndroidJavaObject androidJavaObject = (AndroidJavaObject) new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
          NativeShare.m_context = androidJavaObject.GetStatic<AndroidJavaObject>("currentActivity");
      }
      return NativeShare.m_context;
    }
  }

  public NativeShare()
  {
    this.subject = string.Empty;
    this.text = string.Empty;
    this.title = string.Empty;
    this.targetPackage = string.Empty;
    this.targetClass = string.Empty;
    this.files = new List<string>(0);
    this.mimes = new List<string>(0);
  }

  public NativeShare SetSubject(string subject)
  {
    if (subject != null)
      this.subject = subject;
    return this;
  }

  public NativeShare SetText(string text)
  {
    if (text != null)
      this.text = text;
    return this;
  }

  public NativeShare SetTitle(string title)
  {
    if (title != null)
      this.title = title;
    return this;
  }

  public NativeShare SetTarget(string androidPackageName, string androidClassName = null)
  {
    if (!string.IsNullOrEmpty(androidPackageName))
    {
      this.targetPackage = androidPackageName;
      if (androidClassName != null)
        this.targetClass = androidClassName;
    }
    return this;
  }

  public NativeShare AddFile(string filePath, string mime = null)
  {
    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
    {
      this.files.Add(filePath);
      this.mimes.Add(mime ?? string.Empty);
    }
    else
      Debug.LogError((object) ("File does not exist at path or permission denied: " + filePath));
    return this;
  }

  public void Share()
  {
    if (this.files.Count == 0 && this.subject.Length == 0 && this.text.Length == 0)
      Debug.LogWarning((object) "Share Error: attempting to share nothing!");
    else
      ((AndroidJavaObject) NativeShare.AJC).CallStatic(nameof (Share), new object[8]
      {
        (object) NativeShare.Context,
        (object) this.targetPackage,
        (object) this.targetClass,
        (object) this.files.ToArray(),
        (object) this.mimes.ToArray(),
        (object) this.subject,
        (object) this.text,
        (object) this.title
      });
  }

  public static bool TargetExists(string androidPackageName, string androidClassName = null)
  {
    if (string.IsNullOrEmpty(androidPackageName))
      return false;
    if (androidClassName == null)
      androidClassName = string.Empty;
    return ((AndroidJavaObject) NativeShare.AJC).CallStatic<bool>(nameof (TargetExists), new object[3]
    {
      (object) NativeShare.Context,
      (object) androidPackageName,
      (object) androidClassName
    });
  }

  public static bool FindTarget(
    out string androidPackageName,
    out string androidClassName,
    string packageNameRegex,
    string classNameRegex = null)
  {
    androidPackageName = (string) null;
    androidClassName = (string) null;
    if (string.IsNullOrEmpty(packageNameRegex))
      return false;
    if (classNameRegex == null)
      classNameRegex = string.Empty;
    string str = ((AndroidJavaObject) NativeShare.AJC).CallStatic<string>("FindMatchingTarget", new object[3]
    {
      (object) NativeShare.Context,
      (object) packageNameRegex,
      (object) classNameRegex
    });
    if (string.IsNullOrEmpty(str))
      return false;
    int length = str.IndexOf('>');
    if (length <= 0 || length >= str.Length - 1)
      return false;
    androidPackageName = str.Substring(0, length);
    androidClassName = str.Substring(length + 1);
    return true;
  }
}
