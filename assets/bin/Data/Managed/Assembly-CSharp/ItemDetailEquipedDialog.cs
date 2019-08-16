using UnityEngine;

public class ItemDetailEquipedDialog : ItemDetailEquip
{
	private const int SE_ID = 40000021;

	private AudioClip m_AudioClip;

	public override void Initialize()
	{
		base.Initialize();
		StoreAudioClip();
		PlayAudio();
	}

	private void StoreAudioClip()
	{
		string sE = ResourceName.GetSE(40000021);
		if (string.IsNullOrEmpty(sE))
		{
			return;
		}
		Transform child = this.get_transform().GetChild(0);
		if (!(child == null))
		{
			ResourceLink component = child.GetComponent<ResourceLink>();
			if (!(component == null))
			{
				m_AudioClip = component.Get<AudioClip>(sE);
			}
		}
	}

	private void PlayAudio()
	{
		if (m_AudioClip != null)
		{
			SoundManager.PlayOneshotJingle(m_AudioClip, 40000021);
		}
	}
}
