using UnityEngine;

public class ChairPoint : MonoBehaviour
{
	public enum CHAIR_TYPE
	{
		NORMAL,
		BENTCH,
		SOFA
	}

	public CHAIR_TYPE chairType;

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
