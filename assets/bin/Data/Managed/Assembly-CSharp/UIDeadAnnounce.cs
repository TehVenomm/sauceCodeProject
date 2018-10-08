using System;
using UnityEngine;

public class UIDeadAnnounce : UIAnnounceBase<UIDeadAnnounce>
{
	public enum ANNOUNCE_TYPE
	{
		DEAD,
		RETIRE,
		CONTINUE,
		RESCURE,
		AUTO_REVIVE,
		REACH_NEXT_WAVE,
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
	protected LabelSettings[] labelSettings = new LabelSettings[6];

	public void Announce(ANNOUNCE_TYPE type, Player player)
	{
		if (AnnounceStart(player))
		{
			SetupAnnounce(type, player.charaName);
		}
	}

	public void Announce(ANNOUNCE_TYPE type, string charaName)
	{
		if (AnnounceStart())
		{
			SetupAnnounce(type, charaName);
		}
	}

	private void SetupAnnounce(ANNOUNCE_TYPE type, string charaName)
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		if (type == ANNOUNCE_TYPE.DEAD || type == ANNOUNCE_TYPE.RETIRE)
		{
			deadBack.SetActive(true);
			deadEff.SetActive(true);
			rescueBack.SetActive(false);
			rescueEff.SetActive(false);
		}
		else
		{
			deadBack.SetActive(false);
			deadEff.SetActive(false);
			rescueBack.SetActive(true);
			rescueEff.SetActive(true);
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
}
