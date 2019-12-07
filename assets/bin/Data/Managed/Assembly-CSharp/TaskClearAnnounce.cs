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
		string sE = ResourceName.GetSE(40000158);
		if (string.IsNullOrEmpty(sE))
		{
			return;
		}
		Transform child = base.transform.GetChild(0);
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
			SoundManager.PlayOneshotJingle(m_AudioClip, 40000158);
		}
	}

	private void Start()
	{
		Transform ctrl = GetCtrl(UI.OBJ_EFFECT);
		if (ctrl != null)
		{
			ctrl.localScale = Vector3.zero;
		}
		StoreAudioClip();
	}

	public void Play(string announce, string reward, Action onComplete)
	{
		UIWidget component = GetComponent<UIWidget>(UI.WGT_ANCHOR_POINT);
		UITweenCtrl component2 = GetComponent<UITweenCtrl>(UI.OBJ_TWEENCTRL);
		if (component == null || component2 == null)
		{
			if (onComplete != null)
			{
				onComplete();
			}
			return;
		}
		component.leftAnchor.Set(1f, 150f);
		component.rightAnchor.Set(1f, 300f);
		component.bottomAnchor.Set(1f, -130f);
		component.topAnchor.Set(1f, -105f);
		component.UpdateAnchors();
		SetLabelText(UI.LBL_ANNOUNCE, announce);
		SetLabelText(UI.LBL_REWARD, reward);
		component2.Reset();
		component2.Play(forward: true, delegate
		{
			if (onComplete != null)
			{
				onComplete();
			}
		});
		PlayAudio();
	}
}
