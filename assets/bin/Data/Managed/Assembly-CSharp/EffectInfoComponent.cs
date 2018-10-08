using UnityEngine;

public class EffectInfoComponent
{
	[Tooltip("ル\u30fcプエンドによる削除設定")]
	public bool destroyLoopEnd;

	private AudioObject loopAudioObject;

	public EffectInfoComponent()
		: this()
	{
	}

	public void SetLoopAudioObject(AudioObject ao)
	{
		if (loopAudioObject != null)
		{
			loopAudioObject.Stop(0);
		}
		loopAudioObject = ao;
	}
}
