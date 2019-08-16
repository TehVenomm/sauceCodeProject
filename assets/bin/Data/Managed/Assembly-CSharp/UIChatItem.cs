using UnityEngine;

[RequireComponent(typeof(UIButton))]
public class UIChatItem : MonoBehaviour
{
	[SerializeField]
	protected UILabel chatText;

	protected int chatID;

	protected UIChatButtonBase chatButton;

	public UIChatItem()
		: this()
	{
	}

	public void SetChatData(UIChatButtonBase parent, string str, int chat_id)
	{
		chatButton = parent;
		chatID = chat_id;
		if (chatText != null)
		{
			chatText.text = str;
		}
		this.get_gameObject().SetActive(false);
	}

	private void OnDragOver(GameObject drag)
	{
		if (chatButton != null)
		{
			chatButton.ChatSay(chatID);
		}
	}

	private void OnDragOut(GameObject drag)
	{
		if (chatButton != null)
		{
			chatButton.ChatCancel(chatID);
		}
	}
}
