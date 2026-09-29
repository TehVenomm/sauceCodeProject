// Decompiled with JetBrains decompiler
// Type: QuestStoryCompleteModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using Network;
using System;

#nullable disable
public class QuestStoryCompleteModel : BaseModel
{
  public static string URL = "ajax/quest/storycomplete";
  public QuestStoryCompleteModel.Param result = new QuestStoryCompleteModel.Param();

  [Serializable]
  public class Param
  {
    public StoryRewardList reward = new StoryRewardList();
    public TaskUpdateInfo actioncount = new TaskUpdateInfo();
  }

  public class RequestSendForm
  {
    public int qid;
  }
}
