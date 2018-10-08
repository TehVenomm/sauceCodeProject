public class LoungeNamePlateStatus : UIBehaviour
{
	private enum UI
	{
		OBJ_LOUNGE_NAMEPLATE,
		SPR_LOUNGE_NAMEPLATE,
		LBL_NAMEPLATE,
		SPR_STATUS_AFK,
		SPR_STATUS_SMITH,
		SPR_STATUS_SHOP,
		SPR_STATUS_AFK_CENTER,
		SPR_STATUS_SMITH_CENTER,
		SPR_STATUS_SHOP_CENTER
	}

	private bool isValidNamePlate;

	private LOUNGE_ACTION_TYPE actionType;

	private LoungePlayer player;

	public override void UpdateUI()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Expected O, but got Unknown
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Expected O, but got Unknown
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Expected O, but got Unknown
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Expected O, but got Unknown
		SetActive(this.get_transform(), UI.SPR_STATUS_AFK, false);
		SetActive(this.get_transform(), UI.SPR_STATUS_SMITH, false);
		SetActive(this.get_transform(), UI.SPR_STATUS_SHOP, false);
		SetActive(this.get_transform(), UI.SPR_STATUS_AFK_CENTER, false);
		SetActive(this.get_transform(), UI.SPR_STATUS_SMITH_CENTER, false);
		SetActive(this.get_transform(), UI.SPR_STATUS_SHOP_CENTER, false);
		if (isValidNamePlate)
		{
			switch (actionType)
			{
			case LOUNGE_ACTION_TYPE.TO_EQUIP:
				SetActive(this.get_transform(), UI.SPR_STATUS_SMITH, true);
				break;
			case LOUNGE_ACTION_TYPE.TO_GACHA:
				SetActive(this.get_transform(), UI.SPR_STATUS_SHOP, true);
				break;
			case LOUNGE_ACTION_TYPE.AFK:
				SetActive(this.get_transform(), UI.SPR_STATUS_AFK, true);
				break;
			}
		}
		else
		{
			switch (actionType)
			{
			case LOUNGE_ACTION_TYPE.TO_EQUIP:
				SetActive(this.get_transform(), UI.SPR_STATUS_SMITH_CENTER, true);
				break;
			case LOUNGE_ACTION_TYPE.TO_GACHA:
				SetActive(this.get_transform(), UI.SPR_STATUS_SHOP_CENTER, true);
				break;
			case LOUNGE_ACTION_TYPE.AFK:
				SetActive(this.get_transform(), UI.SPR_STATUS_AFK_CENTER, true);
				break;
			}
		}
	}

	public void SetPlayer(LoungePlayer player)
	{
		this.player = player;
		actionType = player.CurrentActionType;
	}

	public void SetActiveNamePlate(bool isActive)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Expected O, but got Unknown
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		isValidNamePlate = isActive;
		if (isActive)
		{
			SetActive(this.get_transform(), UI.OBJ_LOUNGE_NAMEPLATE, true);
		}
		SetActive(this.get_transform(), UI.LBL_NAMEPLATE, isActive);
		SetActive(this.get_transform(), UI.SPR_LOUNGE_NAMEPLATE, isActive);
		RefreshUI();
	}

	private void LateUpdate()
	{
		if (MonoBehaviourSingleton<LoungeManager>.IsValid() && actionType != player.CurrentActionType)
		{
			actionType = player.CurrentActionType;
			RefreshUI();
		}
	}
}
