using UnityEngine;

public class UIInGameSelfAnnounceManager : MonoBehaviourSingleton<UIInGameSelfAnnounceManager>
{
	[SerializeField]
	protected UIInGameSelfAnnounce regionBreak;

	[SerializeField]
	protected UIInGameSelfAnnounce regionDragonArmor;

	public void PlayRegionBreak()
	{
		if (!(regionBreak == null))
		{
			regionBreak.Play(null);
		}
	}

	public void PlayDragonArmorBreak()
	{
		if (!(regionDragonArmor == null))
		{
			regionDragonArmor.Play(null);
		}
	}
}
