using UnityEngine;

public class ChairPoint : MonoBehaviour
{
	public ChairPoint dir;

	public HomePlayerCharacterBase sittingChara
	{
		get;
		private set;
	}

	public void SetSittingCharacter(HomePlayerCharacterBase chara)
	{
		sittingChara = chara;
	}

	public void ResetSittingCharacter()
	{
		sittingChara = null;
	}
}
