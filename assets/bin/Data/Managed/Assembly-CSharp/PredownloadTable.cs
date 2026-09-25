// Decompiled with JetBrains decompiler
// Type: PredownloadTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
public class PredownloadTable : ScriptableObject
{
  public List<PredownloadTable.Data> tutorialDatas;
  public List<PredownloadTable.Data> preloadDatas;
  public List<PredownloadTable.Data> autoDatas;
  public List<PredownloadTable.Data> inGameDatas;
  public List<PredownloadTable.Data> manualDatas;

  [Serializable]
  public class Data
  {
    public string categoryName;
    public List<PredownloadTable.Package> packages;
  }

  [Serializable]
  public class Package
  {
    public string packageName;
    public List<string> resourceNames;
  }
}
