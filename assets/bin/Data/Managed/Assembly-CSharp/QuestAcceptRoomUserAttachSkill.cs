// Decompiled with JetBrains decompiler
// Type: QuestAcceptRoomUserAttachSkill
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class QuestAcceptRoomUserAttachSkill : QuestRoomUserAttachSkill
{
  public override void Initialize() => base.Initialize();

  protected void OnQuery_QuestAcceptRoomInvalid_UserDetail_OK()
  {
    this.OnQuery_QuestRoomInvalid_UserDetail_OK();
  }
}
