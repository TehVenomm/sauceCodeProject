public class RoomEmotion : UIChatButtonBase
{
	public delegate void OnEmotion(int index);

	public OnEmotion onEmotion;

	public void SetOnEmotion(OnEmotion on_emotion)
	{
		if (on_emotion != null)
		{
			onEmotion = on_emotion;
		}
	}

	protected override void chat(int id)
	{
		if (chatID != -1 && onEmotion != null)
		{
			onEmotion(chatID);
		}
	}
}
