// Decompiled with JetBrains decompiler
// Type: UIChatButtonBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (UIButton))]
public class UIChatButtonBase : MonoBehaviourSingleton<UIChatButtonBase>
{
  [SerializeField]
  protected UIChatItem[] chatItem;
  protected int chatID = -1;
  protected int chatCancelID = -1;

  private void Start()
  {
    int length = this.chatItem.Length;
    for (int chat_id = 0; chat_id < length; ++chat_id)
      this.chatItem[chat_id].SetChatData(this, string.Empty, chat_id);
  }

  private void Update()
  {
    if (this.chatID == -1 || this.chatID != this.chatCancelID)
      return;
    this.chatID = -1;
    this.chatCancelID = -1;
  }

  public void ChatSay(int chat_id) => this.chatID = chat_id;

  public void ChatCancel(int chat_id) => this.chatCancelID = chat_id;

  private void OnPress(bool pressed)
  {
    if (!pressed)
      this.chat(this.chatID);
    this.chatID = -1;
    this.chatCancelID = -1;
    int index = 0;
    for (int length = this.chatItem.Length; index < length; ++index)
      ((Component) this.chatItem[index]).gameObject.SetActive(pressed);
  }

  public virtual string GetChatSayText(int chatID) => string.Empty;

  protected virtual void chat(int id)
  {
  }

  public void SetChatItem(UIChatItem[] items) => this.chatItem = items;
}
