using UnityEngine;

public class AnimationDirectorProxy : MonoBehaviour
{
	private void __FUNCTION__InstantiatePrefab(string game_object_name)
	{
		if (AnimationDirector.I != null)
		{
			AnimationDirector.I.__FUNCTION__InstantiatePrefab(game_object_name);
		}
	}

	private void __FUNCTION__PlayAudio(string game_object_name)
	{
		if (AnimationDirector.I != null)
		{
			AnimationDirector.I.__FUNCTION__PlayAudio(game_object_name);
		}
	}

	private void __FUNCTION_Command(string command)
	{
		if (AnimationDirector.I != null)
		{
			AnimationDirector.I.__FUNCTION_Command(command);
		}
	}

	public void __FUNCTION__PlayCachedAudio(int se_id)
	{
		if (AnimationDirector.I != null)
		{
			AnimationDirector.I.__FUNCTION__PlayCachedAudio(se_id);
		}
	}
}
