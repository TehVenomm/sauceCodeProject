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
		SetActive(base.transform, UI.SPR_STATUS_AFK, false);
		SetActive(base.transform, UI.SPR_STATUS_SMITH, false);
		SetActive(base.transform, UI.SPR_STATUS_SHOP, false);
		SetActive(base.transform, UI.SPR_STATUS_AFK_CENTER, false);
		SetActive(base.transform, UI.SPR_STATUS_SMITH_CENTER, false);
		SetActive(base.transform, UI.SPR_STATUS_SHOP_CENTER, false);
		if (isValidNamePlate)
		{
			switch (actionType)
			{
			case LOUNGE_ACTION_TYPE.TO_EQUIP:
				SetActive(base.transform, UI.SPR_STATUS_SMITH, true);
				break;
			case LOUNGE_ACTION_TYPE.TO_GACHA:
				SetActive(base.transform, UI.SPR_STATUS_SHOP, true);
				break;
			case LOUNGE_ACTION_TYPE.AFK:
				SetActive(base.transform, UI.SPR_STATUS_AFK, true);
				break;
			}
		}
		else
		{
			switch (actionType)
			{
			case LOUNGE_ACTION_TYPE.TO_EQUIP:
				SetActive(base.transform, UI.SPR_STATUS_SMITH_CENTER, true);
				break;
			case LOUNGE_ACTION_TYPE.TO_GACHA:
				SetActive(base.transform, UI.SPR_STATUS_SHOP_CENTER, true);
				break;
			case LOUNGE_ACTION_TYPE.AFK:
				SetActive(base.transform, UI.SPR_STATUS_AFK_CENTER, true);
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
		isValidNamePlate = isActive;
		if (isActive)
		{
			SetActive(base.transform, UI.OBJ_LOUNGE_NAMEPLATE, true);
		}
		SetActive(base.transform, UI.LBL_NAMEPLATE, isActive);
		SetActive(base.transform, UI.SPR_LOUNGE_NAMEPLATE, isActive);
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
