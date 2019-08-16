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
		this.get_gameObject().SetActive(true);
		if (AnnounceStart(player))
		{
			SetupAnnounce(type, player.charaName);
		}
	}

	public void Announce(ANNOUNCE_TYPE type, string charaName)
	{
		this.get_gameObject().SetActive(true);
		if (AnnounceStart())
		{
			SetupAnnounce(type, charaName);
		}
	}

	private void SetupAnnounce(ANNOUNCE_TYPE type, string charaName)
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		if (type == ANNOUNCE_TYPE.DEAD || type == ANNOUNCE_TYPE.RETIRE || type == ANNOUNCE_TYPE.STONE)
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

	protected override void OnStart()
	{
		this.get_gameObject().SetActive(false);
	}

	protected override void OnAfterAnimation()
	{
		this.get_gameObject().SetActive(false);
	}
}
