using UnityEngine;

[AddComponentMenu("ProjectUI/UIPlaySoundCustom")]
public class UIPlaySoundCustom
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

	private const float pitch = 1f;

	public SoundID.UISE SEType = SoundID.UISE.INVALID;

	public int SEID;

	private string SEName = string.Empty;

	public Trigger trigger;

	public ResourceLink ResourceLink;

	[Range(0f, 1f)]
	public float volume = 1f;

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
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected O, but got Unknown
		SEName = ResourceName.GetSE(SEID);
		if (!(ResourceLink != null))
		{
			Transform val = this.get_gameObject().get_transform().get_parent();
			ResourceLink component;
			while (true)
			{
				if (!(val != null) || val.get_name() == "UI Root")
				{
					return;
				}
				component = val.GetComponent<ResourceLink>();
				if (component != null)
				{
					AudioClip val2 = component.Get<AudioClip>(SEName);
					if (val2 != null)
					{
						break;
					}
				}
				val = val.get_transform().get_parent();
			}
			ResourceLink = component;
		}
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
			SoundManager.PlaySystemSE(SEType, 1f);
		}
	}

	private void KeyOn(AudioClip clip, int id)
	{
		if (!(clip == null))
		{
			SoundManager.PlayUISE(clip, volume, false, null, id);
		}
	}
}
