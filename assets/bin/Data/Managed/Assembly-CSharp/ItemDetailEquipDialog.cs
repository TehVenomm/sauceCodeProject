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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		string sE = ResourceName.GetSE(40000021);
		if (!string.IsNullOrEmpty(sE))
		{
			Transform val = this.get_transform().GetChild(0);
			if (!(val == null))
			{
				ResourceLink component = val.GetComponent<ResourceLink>();
				if (!(component == null))
				{
					m_AudioClip = component.Get<AudioClip>(sE);
				}
			}
		}
	}

	private void PlayAudio()
	{
		if (m_AudioClip != null)
		{
			SoundManager.PlayOneshotJingle(m_AudioClip, 40000021, null, null);
		}
	}
}
