using System;
using UnityEngine;

public class QuestResultDropIconOpener : MonoBehaviour
{
	public class Info
	{
		public bool IsRare;

		public bool IsBroken;
	}

	[SerializeField]
	private UISprite sprite;

	[SerializeField]
	private TweenAlpha iconParent;

	[SerializeField]
	private UISprite spriteRewardCategory;

	private ItemIcon icon;

	private bool isInitialize;

	private Info m_Info = new Info();

	private Action<Transform, Info, bool> loadEffCallback;

	private const string SPR_RARE_ICON = "ItemOpenerIcon_Gold";

	private const string SPR_NORMAL_ICON = "ItemOpenerIcon_Silver";

	private const string SPR_BREAK_ICON = "ItemOpenerIcon_Red";

	public void Initialized(ItemIcon _icon, Info info, Action<Transform, Info, bool> load_eff_callback)
	{
		if (!(iconParent == null))
		{
			SetIcon(_icon);
			m_Info = info;
			loadEffCallback = load_eff_callback;
			SetSpriteRare();
			SetSpriteBreakReward(visible: false);
			isInitialize = true;
		}
	}

	public void StartEffect(bool is_skip)
	{
		if (isInitialize)
		{
			if (is_skip)
			{
				iconParent.duration = 0f;
				iconParent.delay = 0f;
			}
			loadEffCallback(icon._transform, m_Info, is_skip);
			sprite.enabled = false;
			OpenIcon();
		}
	}

	private void SetIcon(ItemIcon _icon)
	{
		icon = _icon;
		icon.transform.parent = iconParent.transform;
		icon.VisibleIcon(is_visible: false);
	}

	private void SetSpriteRare()
	{
		string spriteName = "ItemOpenerIcon_Silver";
		if (m_Info.IsBroken)
		{
			spriteName = "ItemOpenerIcon_Red";
		}
		else if (m_Info.IsRare)
		{
			spriteName = "ItemOpenerIcon_Gold";
		}
		sprite.spriteName = spriteName;
	}

	private void OpenIcon()
	{
		iconParent.Play(forward: true);
		icon.VisibleIcon(is_visible: true);
		SetSpriteBreakReward(m_Info.IsBroken);
	}

	private void SetSpriteBreakReward(bool visible)
	{
		if (spriteRewardCategory != null)
		{
			spriteRewardCategory.gameObject.SetActive(visible);
		}
	}
}
