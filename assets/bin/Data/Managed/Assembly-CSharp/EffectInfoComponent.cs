using UnityEngine;

public class EffectInfoComponent : MonoBehaviour
{
	[Tooltip("ル\u30fcプエンドによる削除設定")]
	public bool destroyLoopEnd;

	private AudioObject loopAudioObject;

	public void SetLoopAudioObject(AudioObject ao)
	{
		if ((Object)loopAudioObject != (Object)null)
		{
			loopAudioObject.Stop(0);
		}
		loopAudioObject = ao;
	}
}
