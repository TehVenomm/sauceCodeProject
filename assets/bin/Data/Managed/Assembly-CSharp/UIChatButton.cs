using UnityEngine;

[RequireComponent(typeof(UIButton))]
public class UIChatButton : UIChatButtonBase
{
	private string[] chatSayTexts = new string[6]
	{
		"ナイス！",
		"ありがとう！",
		"Skill使います！",
		"はやっ！",
		"ゴメン！",
		"ヤバい！"
	};

	private void Start()
	{
		int num = chatItem.Length;
		if (num > chatSayTexts.Length)
		{
			num = chatSayTexts.Length;
		}
		for (int i = 0; i < num; i++)
		{
			chatItem[i].SetChatData(this, chatSayTexts[i], i);
		}
	}

	public override string GetChatSayText(int chatID)
	{
		if (chatID < 0 || chatID >= chatSayTexts.Length)
		{
			return string.Empty;
		}
		return chatSayTexts[chatID];
	}

	protected override void chat(int id)
	{
		if (id != -1 && MonoBehaviourSingleton<StageObjectManager>.I.self != null)
		{
			MonoBehaviourSingleton<StageObjectManager>.I.self.ChatSay(id);
		}
	}
}
