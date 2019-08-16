using UnityEngine;

[RequireComponent(typeof(UIButton))]
public class UIChatButtonBase : MonoBehaviourSingleton<UIChatButtonBase>
{
	[SerializeField]
	protected UIChatItem[] chatItem;

	protected int chatID = -1;

	protected int chatCancelID = -1;

	private void Start()
	{
		int num = chatItem.Length;
		for (int i = 0; i < num; i++)
		{
			chatItem[i].SetChatData(this, string.Empty, i);
		}
	}

	private void Update()
	{
		if (chatID != -1 && chatID == chatCancelID)
		{
			chatID = -1;
			chatCancelID = -1;
		}
	}

	public void ChatSay(int chat_id)
	{
		chatID = chat_id;
	}

	public void ChatCancel(int chat_id)
	{
		chatCancelID = chat_id;
	}

	private void OnPress(bool pressed)
	{
		if (!pressed)
		{
			chat(chatID);
		}
		chatID = -1;
		chatCancelID = -1;
		int i = 0;
		for (int num = chatItem.Length; i < num; i++)
		{
			chatItem[i].get_gameObject().SetActive(pressed);
		}
	}

	public virtual string GetChatSayText(int chatID)
	{
		return string.Empty;
	}

	protected virtual void chat(int id)
	{
	}

	public void SetChatItem(UIChatItem[] items)
	{
		chatItem = items;
	}
}
