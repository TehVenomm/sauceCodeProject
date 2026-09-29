// Decompiled with JetBrains decompiler
// Type: UIChatItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[RequireComponent(typeof (UIButton))]
public class UIChatItem : MonoBehaviour
{
  [SerializeField]
  protected UILabel chatText;
  protected int chatID;
  protected UIChatButtonBase chatButton;

  public void SetChatData(UIChatButtonBase parent, string str, int chat_id)
  {
    this.chatButton = parent;
    this.chatID = chat_id;
    if (Object.op_Inequality((Object) this.chatText, (Object) null))
      this.chatText.text = str;
    ((Component) this).gameObject.SetActive(false);
  }

  private void OnDragOver(GameObject drag)
  {
    if (!Object.op_Inequality((Object) this.chatButton, (Object) null))
      return;
    this.chatButton.ChatSay(this.chatID);
  }

  private void OnDragOut(GameObject drag)
  {
    if (!Object.op_Inequality((Object) this.chatButton, (Object) null))
      return;
    this.chatButton.ChatCancel(this.chatID);
  }
}
