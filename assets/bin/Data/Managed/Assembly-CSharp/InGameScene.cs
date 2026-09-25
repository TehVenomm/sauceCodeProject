// Decompiled with JetBrains decompiler
// Type: InGameScene
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class InGameScene : GameSection
{
  public override void Initialize()
  {
    base.Initialize();
    if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
      Object.DestroyImmediate((Object) MonoBehaviourSingleton<InGameRecorder>.I);
    if (!Object.op_Inequality((Object) MonoBehaviourSingleton<UIManager>.I.mainChat, (Object) null))
      return;
    MonoBehaviourSingleton<UIManager>.I.mainChat.Open();
  }

  protected override void OnDestroy()
  {
    base.OnDestroy();
    if (MonoBehaviourSingleton<GameSceneManager>.IsValid())
      MonoBehaviourSingleton<GameSceneManager>.I.SetExternalStageName((string) null);
    if (MonoBehaviourSingleton<InGameRecorder>.IsValid())
      Object.Destroy((Object) MonoBehaviourSingleton<InGameRecorder>.I);
    if (MonoBehaviourSingleton<QuestManager>.IsValid())
      MonoBehaviourSingleton<QuestManager>.I.ClearPlayData();
    if (MonoBehaviourSingleton<FieldManager>.IsValid())
      MonoBehaviourSingleton<FieldManager>.I.ClearCurrentFieldData();
    if (MonoBehaviourSingleton<InGameManager>.IsValid())
      MonoBehaviourSingleton<InGameManager>.I.OnEndInGameScene();
    if (!MonoBehaviourSingleton<ChatManager>.IsValid())
      return;
    MonoBehaviourSingleton<ChatManager>.I.DestroyRoomChat();
  }
}
