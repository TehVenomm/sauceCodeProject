using UnityEngine;

public class ResidentEffectObject : MonoBehaviour
{
	public int GroupID;

	public string UniqueName = string.Empty;

	public ResidentEffectObject()
		: this()
	{
	}

	public void Initialize(SystemEffectSetting.Data effectData)
	{
		GroupID = effectData.groupID;
		UniqueName = effectData.UniqueName;
	}

	public void Initialize(AnimEventData.ResidentEffectData effectData)
	{
		GroupID = effectData.groupID;
		UniqueName = effectData.UniqueName;
	}
}
