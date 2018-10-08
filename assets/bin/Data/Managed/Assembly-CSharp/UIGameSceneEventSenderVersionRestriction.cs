using UnityEngine;

[RequireComponent(typeof(UIGameSceneEventSender))]
public class UIGameSceneEventSenderVersionRestriction
{
	public uint major;

	public uint minor;

	public uint revision;

	public UIGameSceneEventSenderVersionRestriction()
		: this()
	{
	}

	public string GetCheckApplicationVersionText()
	{
		return $"{major}.{minor}.{revision}";
	}
}
