using UnityEngine;

public class UIInGameSelfAnnounceManager : MonoBehaviourSingleton<UIInGameSelfAnnounceManager>
{
	[SerializeField]
	protected UIInGameSelfAnnounce regionBreak;

	[SerializeField]
	protected UIInGameSelfAnnounce regionDragonArmor;

	[SerializeField]
	protected UIInGameSelfAnnounce supplyInformation;

	public void PlayRegionBreak()
	{
		if (!(regionBreak == null))
		{
			regionBreak.Play();
		}
	}

	public void PlayDragonArmorBreak()
	{
		if (!(regionDragonArmor == null))
		{
			regionDragonArmor.Play();
		}
	}

	public void PlaySupplyInformation()
	{
		if (!(supplyInformation == null))
		{
			supplyInformation.Play();
		}
	}
}
