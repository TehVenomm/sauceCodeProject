// Decompiled with JetBrains decompiler
// Type: Chat_Model_Base
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class Chat_Model_Base
{
  protected CHAT_PACKET_TYPE m_packetType;
  protected string payload;
  protected static readonly int PAYLOAD_ORIGIN_INDEX = 40;
  protected CHAT_ERROR_TYPE m_ErrorType;

  public CHAT_PACKET_TYPE packetType
  {
    set => this.m_packetType = value;
    get => this.m_packetType;
  }

  public int commandId => (int) this.m_packetType;

  public CHAT_ERROR_TYPE errorType
  {
    get => this.m_ErrorType;
    protected set => this.m_ErrorType = value;
  }

  protected void SetErrorType(string errorCode)
  {
    this.m_ErrorType = (CHAT_ERROR_TYPE) int.Parse(errorCode);
  }

  public virtual string Serialize() => this.payload;

  public override string ToString() => this.payload;
}
