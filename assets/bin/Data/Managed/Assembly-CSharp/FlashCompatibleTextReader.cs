// Decompiled with JetBrains decompiler
// Type: FlashCompatibleTextReader
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class FlashCompatibleTextReader
{
  private string text_;
  private int index_;
  private int size_;

  public FlashCompatibleTextReader(string text)
  {
    this.text_ = text;
    this.size_ = text.Length;
  }

  public int Peek() => this.index_ >= this.size_ ? -1 : (int) this.text_[this.index_];

  public int Read() => this.index_ >= this.size_ ? -1 : (int) this.text_[this.index_++];
}
