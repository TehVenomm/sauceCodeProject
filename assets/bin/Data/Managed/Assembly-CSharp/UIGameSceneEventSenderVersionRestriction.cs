using UnityEngine;

[RequireComponent(typeof(UIGameSceneEventSender))]
public class UIGameSceneEventSenderVersionRestriction : MonoBehaviour
{
	public uint major;

	public uint minor;

	public uint revision;

	public string GetCheckApplicationVersionText()
	{
		return $"{major}.{minor}.{revision}";
	}
}
