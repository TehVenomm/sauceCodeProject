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
		SetActive(this.get_transform(), UI.SPR_STATUS_AFK, is_visible: false);
		SetActive(this.get_transform(), UI.SPR_STATUS_SMITH, is_visible: false);
		SetActive(this.get_transform(), UI.SPR_STATUS_SHOP, is_visible: false);
		SetActive(this.get_transform(), UI.SPR_STATUS_AFK_CENTER, is_visible: false);
		SetActive(this.get_transform(), UI.SPR_STATUS_SMITH_CENTER, is_visible: false);
		SetActive(this.get_transform(), UI.SPR_STATUS_SHOP_CENTER, is_visible: false);
		if (isValidNamePlate)
		{
			switch (actionType)
			{
			case LOUNGE_ACTION_TYPE.TO_EQUIP:
				SetActive(this.get_transform(), UI.SPR_STATUS_SMITH, is_visible: true);
				break;
			case LOUNGE_ACTION_TYPE.TO_GACHA:
				SetActive(this.get_transform(), UI.SPR_STATUS_SHOP, is_visible: true);
				break;
			case LOUNGE_ACTION_TYPE.AFK:
				SetActive(this.get_transform(), UI.SPR_STATUS_AFK, is_visible: true);
				break;
			}
		}
		else
		{
			switch (actionType)
			{
			case LOUNGE_ACTION_TYPE.TO_EQUIP:
				SetActive(this.get_transform(), UI.SPR_STATUS_SMITH_CENTER, is_visible: true);
				break;
			case LOUNGE_ACTION_TYPE.TO_GACHA:
				SetActive(this.get_transform(), UI.SPR_STATUS_SHOP_CENTER, is_visible: true);
				break;
			case LOUNGE_ACTION_TYPE.AFK:
				SetActive(this.get_transform(), UI.SPR_STATUS_AFK_CENTER, is_visible: true);
				break;
			}
		}
		ChangePlayerName(player.LoungeCharaInfo.name);
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
			SetActive(this.get_transform(), UI.OBJ_LOUNGE_NAMEPLATE, is_visible: true);
		}
		SetActive(this.get_transform(), UI.LBL_NAMEPLATE, isActive);
		SetActive(this.get_transform(), UI.SPR_LOUNGE_NAMEPLATE, isActive);
		RefreshUI();
	}

	public void ChangePlayerName(string name)
	{
		SetLabelText(this.get_transform(), UI.LBL_NAMEPLATE, name);
	}

	private void LateUpdate()
	{
		if ((MonoBehaviourSingleton<LoungeManager>.IsValid() || MonoBehaviourSingleton<ClanManager>.IsValid()) && actionType != player.CurrentActionType)
		{
			actionType = player.CurrentActionType;
			RefreshUI();
		}
	}
}
