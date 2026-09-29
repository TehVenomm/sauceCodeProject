// Decompiled with JetBrains decompiler
// Type: QuestAcceptRoom
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class QuestAcceptRoom : QuestRoom
{
  public override void Initialize()
  {
    GC.Collect();
    base.Initialize();
  }

  protected void OnQuery_QuestAcceptRoomInvalid_OK() => this.OnQuery_QuestRoomInvalid_OK();

  protected void OnQuery_INVITE()
  {
    if (!MonoBehaviourSingleton<PartyManager>.I.IsMaxPartyMember())
      return;
    GameSection.ChangeEvent("MAX_MEMBER");
  }
}
