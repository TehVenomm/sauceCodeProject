// Decompiled with JetBrains decompiler
// Type: RoomEmotion
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class RoomEmotion : UIChatButtonBase
{
  public RoomEmotion.OnEmotion onEmotion;

  public void SetOnEmotion(RoomEmotion.OnEmotion on_emotion)
  {
    if (on_emotion == null)
      return;
    this.onEmotion = on_emotion;
  }

  protected override void chat(int id)
  {
    if (this.chatID == -1 || this.onEmotion == null)
      return;
    this.onEmotion(this.chatID);
  }

  public delegate void OnEmotion(int index);
}
