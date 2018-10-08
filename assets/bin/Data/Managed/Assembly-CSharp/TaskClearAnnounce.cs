using System;
using UnityEngine;

public class TaskClearAnnounce : UIBehaviour
{
	public enum UI
	{
		WGT_ANCHOR_POINT,
		OBJ_TWEENCTRL,
		OBJ_EFFECT,
		LBL_ANNOUNCE,
		LBL_REWARD
	}

	private const int SE_ID = 40000158;

	private AudioClip m_AudioClip;

	private void StoreAudioClip()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		string sE = ResourceName.GetSE(40000158);
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
			SoundManager.PlayOneshotJingle(m_AudioClip, 40000158, null, null);
		}
	}

	private void Start()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		Transform ctrl = GetCtrl(UI.OBJ_EFFECT);
		if (ctrl != null)
		{
			ctrl.set_localScale(Vector3.get_zero());
		}
		StoreAudioClip();
	}

	public void Play(string announce, string reward, Action onComplete)
	{
		UIWidget component = base.GetComponent<UIWidget>((Enum)UI.WGT_ANCHOR_POINT);
		UITweenCtrl component2 = base.GetComponent<UITweenCtrl>((Enum)UI.OBJ_TWEENCTRL);
		if (component == null || component2 == null)
		{
			if (onComplete != null)
			{
				onComplete();
			}
		}
		else
		{
			component.leftAnchor.Set(1f, 150f);
			component.rightAnchor.Set(1f, 300f);
			component.bottomAnchor.Set(1f, -130f);
			component.topAnchor.Set(1f, -105f);
			component.UpdateAnchors();
			SetLabelText((Enum)UI.LBL_ANNOUNCE, announce);
			SetLabelText((Enum)UI.LBL_REWARD, reward);
			component2.Reset();
			component2.Play(true, delegate
			{
				if (onComplete != null)
				{
					onComplete();
				}
			});
			PlayAudio();
		}
	}
}
