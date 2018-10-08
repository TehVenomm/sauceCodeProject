using UnityEngine;

[AddComponentMenu("ProjectUI/UI SE Request")]
public class UISERequest : MonoBehaviour
{
	private const float pitch = 1f;

	public SoundID.UISE SEType = SoundID.UISE.INVALID;

	[Range(0f, 1f)]
	public float volume = 1f;
}
