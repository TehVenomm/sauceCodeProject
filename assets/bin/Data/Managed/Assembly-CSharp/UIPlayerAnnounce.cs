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
		base.gameObject.SetActive(value: true);
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
		base.gameObject.SetActive(value: true);
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
		base.gameObject.SetActive(value: false);
	}

	protected override void OnAfterAnimation()
	{
		base.gameObject.SetActive(value: false);
	}
}
