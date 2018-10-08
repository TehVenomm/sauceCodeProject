using UnityEngine;

public class UIInGameSelfAnnounceManager : MonoBehaviourSingleton<UIInGameSelfAnnounceManager>
{
	[SerializeField]
	protected UIInGameSelfAnnounce regionBreak;

	public void PlayRegionBreak()
	{
		if (!(regionBreak == null))
		{
			regionBreak.Play(null);
		}
	}
}
