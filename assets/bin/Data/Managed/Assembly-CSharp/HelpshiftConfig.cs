// Decompiled with JetBrains decompiler
// Type: HelpshiftConfig
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
[Serializable]
public class HelpshiftConfig : ScriptableObject
{
  private static HelpshiftConfig instance;
  private const string helpshiftConfigAssetName = "HelpshiftConfig";
  private const string helpshiftConfigPath = "Helpshift/Resources";
  public const string pluginVersion = "5.2.0";

  public static HelpshiftConfig Instance
  {
    get
    {
      HelpshiftConfig.instance = Resources.Load(nameof (HelpshiftConfig)) as HelpshiftConfig;
      if (Object.op_Equality((Object) HelpshiftConfig.instance, (Object) null))
        HelpshiftConfig.instance = ScriptableObject.CreateInstance<HelpshiftConfig>();
      return HelpshiftConfig.instance;
    }
  }
}
