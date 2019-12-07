using System;
using UnityEngine;

public class UIDeadAnnounce : UIAnnounceBase<UIDeadAnnounce>
{
	public enum ANNOUNCE_TYPE
	{
		DEAD,
		RETIRE,
		STONE,
		CONTINUE,
		RESCURE,
		AUTO_REVIVE,
		REACH_NEXT_WAVE,
		RESCUE_STONE,
		MAX
	}

	[Serializable]
	public class LabelSettings
	{
		public string text;

		public Color topColor;

		public Color bottomColor;

		public Color effectColor;
	}

	[SerializeField]
	protected UILabel playerName;

	[SerializeField]
	protected UILabel announceName;

	[SerializeField]
	protected UILabel announceEffect;

	[SerializeField]
	protected GameObject deadBack;

	[SerializeField]
	protected GameObject rescueBack;

	[SerializeField]
	protected GameObject deadEff;

	[SerializeField]
	protected GameObject rescueEff;

	[SerializeField]
	protected LabelSettings[] labelSettings = new LabelSettings[8];

	public void Announce(ANNOUNCE_TYPE type, Player player)
	{
		base.gameObject.SetActive(value: true);
		if (AnnounceStart(player))
		{
			SetupAnnounce(type, player.charaName);
		}
	}

	public void Announce(ANNOUNCE_TYPE type, string charaName)
	{
		base.gameObject.SetActive(value: true);
		if (AnnounceStart())
		{
			SetupAnnounce(type, charaName);
		}
	}

	private void SetupAnnounce(ANNOUNCE_TYPE type, string charaName)
	{
		if (type == ANNOUNCE_TYPE.DEAD || type == ANNOUNCE_TYPE.RETIRE || type == ANNOUNCE_TYPE.STONE)
		{
			deadBack.SetActive(value: true);
			deadEff.SetActive(value: true);
			rescueBack.SetActive(value: false);
			rescueEff.SetActive(value: false);
		}
		else
		{
			deadBack.SetActive(value: false);
			deadEff.SetActive(value: false);
			rescueBack.SetActive(value: true);
			rescueEff.SetActive(value: true);
		}
		announceName.text = labelSettings[(int)type].text;
		announceName.gradientTop = labelSettings[(int)type].topColor;
		announceName.gradientBottom = labelSettings[(int)type].bottomColor;
		announceName.effectColor = labelSettings[(int)type].effectColor;
		announceEffect.text = labelSettings[(int)type].text;
		playerName.text = charaName;
		announceName.fontStyle = style;
		announceEffect.fontStyle = style;
		playerName.fontStyle = style;
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
