using System.Text;
using UnityEngine;

public class UIEnemyAnnounce : UIAnnounceBase<UIEnemyAnnounce>
{
	[SerializeField]
	protected UILabel announce;

	[SerializeField]
	protected UILabel announceEffect;

	protected StringBuilder stateNameBuilder = new StringBuilder(1024);

	protected override float GetDispSec()
	{
		return 4.5f;
	}

	public void RequestAnnounce(string enemyName, STRING_CATEGORY category, uint stringID)
	{
		if (AnnounceStart())
		{
			SetupAnnounceInfo(enemyName, category, stringID);
		}
	}

	public void StartBuff(string enemy_name, BuffParam.BUFFTYPE type)
	{
		if (IsAnnounce(type) && AnnounceStart())
		{
			SetupAnnounceInfo(enemy_name, STRING_CATEGORY.BUFF, (uint)type);
		}
	}

	public void EndBuff(string enemy_name, BuffParam.BUFFTYPE type)
	{
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		if (IsAnnounce(type) && AnnounceStart())
		{
			stateNameBuilder.Length = 0;
			stateNameBuilder.Append(enemy_name);
			stateNameBuilder.Append(" ");
			stateNameBuilder.Append(StringTable.Get(STRING_CATEGORY.BUFF, (uint)type));
			stateNameBuilder.Append(StringTable.Get(STRING_CATEGORY.BUFF, 9999u));
			string text = stateNameBuilder.ToString();
			announce.text = text;
			announceEffect.text = text;
			announce.fontStyle = style;
			announceEffect.fontStyle = style;
		}
	}

	private void SetupAnnounceInfo(string enemyName, STRING_CATEGORY category, uint stringID)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		stateNameBuilder.Length = 0;
		stateNameBuilder.Append(enemyName);
		stateNameBuilder.Append(" ");
		stateNameBuilder.Append(StringTable.Get(category, stringID));
		string text = stateNameBuilder.ToString();
		announce.text = text;
		announceEffect.text = text;
		announce.fontStyle = style;
		announceEffect.fontStyle = style;
	}

	private bool IsAnnounce(BuffParam.BUFFTYPE type)
	{
		if (type == BuffParam.BUFFTYPE.POISON || type == BuffParam.BUFFTYPE.BURNING || type == BuffParam.BUFFTYPE.DEADLY_POISON || type == BuffParam.BUFFTYPE.GHOST_FORM || type == BuffParam.BUFFTYPE.ELECTRIC_SHOCK || type == BuffParam.BUFFTYPE.INK_SPLASH || type == BuffParam.BUFFTYPE.DEFDOWN_RATE_NORMAL || type == BuffParam.BUFFTYPE.DEFDOWN_RATE_FIRE || type == BuffParam.BUFFTYPE.DEFDOWN_RATE_WATER || type == BuffParam.BUFFTYPE.DEFDOWN_RATE_THUNDER || type == BuffParam.BUFFTYPE.DEFDOWN_RATE_SOIL || type == BuffParam.BUFFTYPE.DEFDOWN_RATE_LIGHT || type == BuffParam.BUFFTYPE.DEFDOWN_RATE_DARK || type == BuffParam.BUFFTYPE.DEFDOWN_RATE_ALLELEMENT || type == BuffParam.BUFFTYPE.MAD_MODE || type == BuffParam.BUFFTYPE.ATTACK_SPEED_DOWN || type == BuffParam.BUFFTYPE.MOVE_SPEED_DOWN)
		{
			return false;
		}
		return true;
	}

	public void RequestFieldBuffAnnounce()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		uint currentFieldBuffId = MonoBehaviourSingleton<FieldManager>.I.currentFieldBuffId;
		if (currentFieldBuffId != 0)
		{
			FieldBuffTable.FieldBuffData data = Singleton<FieldBuffTable>.I.GetData(currentFieldBuffId);
			if (!object.ReferenceEquals(data, null) && AnnounceStart())
			{
				string format = StringTable.Get(STRING_CATEGORY.IN_GAME, 150u);
				string text = string.Format(format, data.name);
				announce.text = text;
				announceEffect.text = text;
				announce.fontStyle = style;
				announceEffect.fontStyle = style;
			}
		}
	}

	public void RequestTextAnnounce(string text)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (AnnounceStart())
		{
			announce.text = text;
			announceEffect.text = text;
			announce.fontStyle = style;
			announceEffect.fontStyle = style;
		}
	}
}
