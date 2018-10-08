using UnityEngine;

public class ItemDetailEquipDialog : ItemDetailEquip
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
		if (!string.IsNullOrEmpty(sE))
		{
			Transform child = base.transform.GetChild(0);
			if (!((Object)child == (Object)null))
			{
				ResourceLink component = child.GetComponent<ResourceLink>();
				if (!((Object)component == (Object)null))
				{
					m_AudioClip = component.Get<AudioClip>(sE);
				}
			}
		}
	}

	private void PlayAudio()
	{
		if ((Object)m_AudioClip != (Object)null)
		{
			SoundManager.PlayOneshotJingle(m_AudioClip, 40000021, null, null);
		}
	}
}
