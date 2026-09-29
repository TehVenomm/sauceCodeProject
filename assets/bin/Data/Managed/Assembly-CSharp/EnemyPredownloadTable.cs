// Decompiled with JetBrains decompiler
// Type: EnemyPredownloadTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[CreateAssetMenu(fileName = "EnemyPredownloadTable", menuName = "ScriptableObject/EnemyPredownloadTable")]
public class EnemyPredownloadTable : ScriptableObject
{
  public int Version;
  public List<EnemyPredownloadTable.Data> EnemyDatas;

  [Serializable]
  public class Data
  {
    public long Size;
    public string categoryName;
    public string packageName;
  }
}
