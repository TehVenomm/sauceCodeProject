using UnityEngine;

public class UIInGameSelfAnnounceManager : MonoBehaviourSingleton<UIInGameSelfAnnounceManager>
{
	[SerializeField]
	protected UIInGameSelfAnnounce regionBreak;

	[SerializeField]
	protected UIInGameSelfAnnounce regionDragonArmor;

	public void PlayRegionBreak()
	{
		if (!((Object)regionBreak == (Object)null))
		{
			regionBreak.Play(null);
		}
	}

	public void PlayDragonArmorBreak()
	{
		if (!((Object)regionDragonArmor == (Object)null))
		{
			regionDragonArmor.Play(null);
		}
	}
}
