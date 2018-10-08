public class Chat_Model_Base
{
	protected CHAT_PACKET_TYPE m_packetType;

	protected string payload;

	protected static readonly int PAYLOAD_ORIGIN_INDEX = 40;

	protected CHAT_ERROR_TYPE m_ErrorType;

	public CHAT_PACKET_TYPE packetType
	{
		get
		{
			return m_packetType;
		}
		set
		{
			m_packetType = value;
		}
	}

	public int commandId => (int)m_packetType;

	public CHAT_ERROR_TYPE errorType
	{
		get
		{
			return m_ErrorType;
		}
		protected set
		{
			m_ErrorType = value;
		}
	}

	protected void SetErrorType(string errorCode)
	{
		int num = (int)(m_ErrorType = (CHAT_ERROR_TYPE)int.Parse(errorCode));
	}

	public virtual string Serialize()
	{
		return payload;
	}

	public override string ToString()
	{
		return payload;
	}
}
