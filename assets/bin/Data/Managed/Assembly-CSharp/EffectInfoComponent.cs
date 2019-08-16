using UnityEngine;

public class EffectInfoComponent : MonoBehaviour
{
	[Tooltip("ル\u30fcプエンドによる削除設定")]
	public bool destroyLoopEnd;

	private AudioObject loopAudioObject;

	[Tooltip("WEATHER CHANGE用のカメラとのZ方向のoffset")]
	public float CameraPosLinkOffsetZ;

	public EffectInfoComponent()
		: this()
	{
	}

	public void SetLoopAudioObject(AudioObject ao)
	{
		if (loopAudioObject != null)
		{
			loopAudioObject.Stop();
		}
		loopAudioObject = ao;
	}
}
