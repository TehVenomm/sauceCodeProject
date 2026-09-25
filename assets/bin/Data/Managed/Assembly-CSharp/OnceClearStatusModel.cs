// Decompiled with JetBrains decompiler
// Type: OnceClearStatusModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;
using System.Collections.Generic;

#nullable disable
public class OnceClearStatusModel : BaseModel
{
  public static string URL = "ajax/once/clearstatus";
  public OnceClearStatusModel.Param result = new OnceClearStatusModel.Param();

  [Serializable]
  public class Param
  {
    public List<ClearStatusQuest> clearStatusQuest = new List<ClearStatusQuest>();
    public List<ClearStatusQuestEnemySpecies> clearStatusQuestEnemySpecies = new List<ClearStatusQuestEnemySpecies>();
  }
}
