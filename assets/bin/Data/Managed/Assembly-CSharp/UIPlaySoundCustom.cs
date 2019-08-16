using UnityEngine;

[AddComponentMenu("ProjectUI/UIPlaySoundCustom")]
public class UIPlaySoundCustom : MonoBehaviour
{
	public enum Trigger
	{
		OnClick,
		OnMouseOver,
		OnMouseOut,
		OnPress,
		OnRelease,
		Custom,
		OnEnable,
		OnDisable
	}

	public SoundID.UISE SEType = SoundID.UISE.INVALID;

	public int SEID;

	private string SEName = string.Empty;

	public Trigger trigger;

	public ResourceLink ResourceLink;

	[Range(0f, 1f)]
	public float volume = 1f;

	private const float pitch = 1f;

	public UIPlaySoundCustom()
		: this()
	{
	}

	private bool DoesNeedToFindSource()
	{
		if (SEType != SoundID.UISE.INVALID)
		{
			return false;
		}
		return (SEID > 0) ? true : false;
	}

	private void FindSource()
	{
		SEName = ResourceName.GetSE(SEID);
		if (ResourceLink != null)
		{
			return;
		}
		Transform parent = this.get_gameObject().get_transform().get_parent();
		ResourceLink component;
		while (true)
		{
			if (!(parent != null) || parent.get_name() == "UI Root")
			{
				return;
			}
			component = parent.GetComponent<ResourceLink>();
			if (component != null)
			{
				AudioClip val = component.Get<AudioClip>(SEName);
				if (val != null)
				{
					break;
				}
			}
			parent = parent.get_transform().get_parent();
		}
		ResourceLink = component;
	}

	public void Start()
	{
		if (DoesNeedToFindSource())
		{
			FindSource();
		}
	}

	public void Play()
	{
		if (DoesNeedToFindSource())
		{
			KeyOnById();
		}
		else
		{
			KeyOnSystemSE();
		}
	}

	private void KeyOnById()
	{
		if (!(ResourceLink == null) && !string.IsNullOrEmpty(SEName))
		{
			AudioClip clip = ResourceLink.Get<AudioClip>(SEName);
			KeyOn(clip, SEID);
		}
	}

	private void KeyOnSystemSE()
	{
		if (SEType != SoundID.UISE.INVALID && MonoBehaviourSingleton<SoundManager>.IsValid() && MonoBehaviourSingleton<GlobalSettingsManager>.IsValid())
		{
			SoundManager.PlaySystemSE(SEType);
		}
	}

	private void KeyOn(AudioClip clip, int id)
	{
		if (!(clip == null))
		{
			SoundManager.PlayUISE(clip, volume, loop: false, null, id);
		}
	}
}
