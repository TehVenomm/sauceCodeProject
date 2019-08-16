using System;
using UnityEngine;

public class UIPlayerAnnounce : UIAnnounceBase<UIPlayerAnnounce>
{
	public enum ANNOUNCE_TYPE
	{
		REGION,
		WEAK,
		DOWN,
		SKILL,
		LEVEL_UP,
		SHIELD_ON,
		SHIELD_OFF,
		DRAGON_ARMOR,
		GIMMICK_EVOLVE,
		MAX
	}

	[Serializable]
	public class LabelSettings
	{
		public string text;

		public Color topColor;

		public Color bottomColor;
	}

	[SerializeField]
	protected UILabel playerName;

	[SerializeField]
	protected UILabel announceName;

	[SerializeField]
	protected UILabel announceEffect;

	[SerializeField]
	protected LabelSettings[] labelSettings = new LabelSettings[9];

	public void Announce(ANNOUNCE_TYPE type, Player player)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(true);
		if (AnnounceStart(player))
		{
			announceName.text = labelSettings[(int)type].text;
			announceName.gradientTop = labelSettings[(int)type].topColor;
			announceName.gradientBottom = labelSettings[(int)type].bottomColor;
			announceEffect.text = labelSettings[(int)type].text;
			playerName.text = player.charaName;
			announceName.fontStyle = style;
			announceEffect.fontStyle = style;
			playerName.fontStyle = style;
		}
	}

	public void StartSkill(string skill_name, Player player)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		this.get_gameObject().SetActive(true);
		if (AnnounceStart(player))
		{
			announceName.text = skill_name;
			announceName.gradientTop = labelSettings[3].topColor;
			announceName.gradientBottom = labelSettings[3].bottomColor;
			announceEffect.text = skill_name;
			playerName.text = player.charaName;
			announceName.fontStyle = style;
			announceEffect.fontStyle = style;
			playerName.fontStyle = style;
		}
	}

	protected override void OnStart()
	{
		this.get_gameObject().SetActive(false);
	}

	protected override void OnAfterAnimation()
	{
		this.get_gameObject().SetActive(false);
	}
}
