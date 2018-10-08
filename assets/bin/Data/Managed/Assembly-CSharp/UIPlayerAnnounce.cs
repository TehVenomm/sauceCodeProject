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
	protected LabelSettings[] labelSettings = new LabelSettings[7];

	public void Announce(ANNOUNCE_TYPE type, Player player)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
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
}
