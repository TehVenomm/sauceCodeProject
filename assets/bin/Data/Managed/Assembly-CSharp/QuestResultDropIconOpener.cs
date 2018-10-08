using System;
using UnityEngine;

public class QuestResultDropIconOpener
{
	public class Info
	{
		public bool IsRare;

		public bool IsBroken;
	}

	private const string SPR_RARE_ICON = "ItemOpenerIcon_Gold";

	private const string SPR_NORMAL_ICON = "ItemOpenerIcon_Silver";

	private const string SPR_BREAK_ICON = "ItemOpenerIcon_Red";

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

	public QuestResultDropIconOpener()
		: this()
	{
	}

	public void Initialized(ItemIcon _icon, Info info, Action<Transform, Info, bool> load_eff_callback)
	{
		if (!(iconParent == null))
		{
			SetIcon(_icon);
			m_Info = info;
			loadEffCallback = load_eff_callback;
			SetSpriteRare();
			SetSpriteBreakReward(false);
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
			loadEffCallback.Invoke(icon._transform, m_Info, is_skip);
			sprite.set_enabled(false);
			OpenIcon();
		}
	}

	private void SetIcon(ItemIcon _icon)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		icon = _icon;
		icon.transform.set_parent(iconParent.get_transform());
		icon.VisibleIcon(false, true);
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
		iconParent.Play(true);
		icon.VisibleIcon(true, true);
		SetSpriteBreakReward(m_Info.IsBroken);
	}

	private void SetSpriteBreakReward(bool visible)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (spriteRewardCategory != null)
		{
			spriteRewardCategory.get_gameObject().SetActive(visible);
		}
	}
}
