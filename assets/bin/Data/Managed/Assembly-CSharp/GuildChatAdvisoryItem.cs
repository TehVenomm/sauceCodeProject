using System;
using UnityEngine;

public class GuildChatAdvisoryItem
{
	public Transform close;

	[SerializeField]
	private UILabel full_title;

	private string title;

	private string content;

	public GuildChatAdvisoryItem()
		: this()
	{
	}

	public void Init(string t, string c)
	{
		title = t;
		content = c;
		full_title.text = content;
	}

	public static bool HasReadNew()
	{
		int @int = PlayerPrefs.GetInt("Guild_Chat_Advisory_New", 0);
		return @int != 0 && @int == DateTime.Now.Day;
	}

	public static void SetReadNew()
	{
		PlayerPrefs.SetInt("Guild_Chat_Advisory_New", DateTime.Now.Day);
	}

	public static bool HasReadHomeNew()
	{
		int @int = PlayerPrefs.GetInt("Home_Chat_Advisory_New", 0);
		return @int != 0 && @int == DateTime.Now.Day;
	}

	public static void SetReadHomeNew()
	{
		PlayerPrefs.SetInt("Home_Chat_Advisory_New", DateTime.Now.Day);
	}
}
