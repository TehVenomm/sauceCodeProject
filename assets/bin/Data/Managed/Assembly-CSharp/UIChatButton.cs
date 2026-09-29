// Decompiled with JetBrains decompiler
// Type: UIChatButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (UIButton))]
public class UIChatButton : UIChatButtonBase
{
  private string[] chatSayTexts = new string[6]
  {
    "ナイス！",
    "ありがとう！",
    "Skill使います！",
    "はやっ！",
    "ゴメン！",
    "ヤバい！"
  };

  private void Start()
  {
    int length = this.chatItem.Length;
    if (length > this.chatSayTexts.Length)
      length = this.chatSayTexts.Length;
    for (int chat_id = 0; chat_id < length; ++chat_id)
      this.chatItem[chat_id].SetChatData((UIChatButtonBase) this, this.chatSayTexts[chat_id], chat_id);
  }

  public override string GetChatSayText(int chatID)
  {
    return chatID < 0 || chatID >= this.chatSayTexts.Length ? string.Empty : this.chatSayTexts[chatID];
  }

  protected override void chat(int id)
  {
    if (id == -1 || !Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      return;
    MonoBehaviourSingleton<StageObjectManager>.I.self.ChatSay(id);
  }
}
