using UnityEngine;

[RequireComponent(typeof(UIButton))]
public class UIChatItem : MonoBehaviour
{
	[SerializeField]
	protected UILabel chatText;

	protected int chatID;

	protected UIChatButtonBase chatButton;

	public void SetChatData(UIChatButtonBase parent, string str, int chat_id)
	{
		chatButton = parent;
		chatID = chat_id;
		if ((Object)chatText != (Object)null)
		{
			chatText.text = str;
		}
		base.gameObject.SetActive(false);
	}

	private void OnDragOver(GameObject drag)
	{
		if ((Object)chatButton != (Object)null)
		{
			chatButton.ChatSay(chatID);
		}
	}

	private void OnDragOut(GameObject drag)
	{
		if ((Object)chatButton != (Object)null)
		{
			chatButton.ChatCancel(chatID);
		}
	}
}
