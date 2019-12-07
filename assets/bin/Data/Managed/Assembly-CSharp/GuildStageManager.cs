using System.Collections;

public class GuildStageManager : MonoBehaviourSingleton<GuildStageManager>
{
	public HomePeople HomePeople
	{
		get;
		private set;
	}

	public HomeCamera HomeCamera
	{
		get;
		private set;
	}

	public bool IsInitialized
	{
		get;
		private set;
	}

	private IEnumerator Start()
	{
		while (!MonoBehaviourSingleton<StageManager>.IsValid() || MonoBehaviourSingleton<StageManager>.I.isLoading)
		{
			yield return null;
		}
		HomeCamera = base.gameObject.AddComponent<HomeCamera>();
		HomePeople = base.gameObject.AddComponent<HomePeople>();
		while (!HomeCamera.isInitialized || !HomePeople.isInitialized)
		{
			yield return null;
		}
		IsInitialized = true;
	}
}
