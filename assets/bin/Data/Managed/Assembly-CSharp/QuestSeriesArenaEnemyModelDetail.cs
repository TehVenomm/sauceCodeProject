using UnityEngine;

public class QuestSeriesArenaEnemyModelDetail : GameSection
{
	private enum UI
	{
		WGT_ENEMY_MODEL,
		TEX_ENEMY_MODEL,
		LBL_ENEMY,
		OBJ_ENEMY,
		LBL_QUEST_NAME,
		SPR_ELEMENT_2,
		STR_NON_ELEMENT_2,
		SPR_WEAK_ELEMENT_2,
		STR_NON_WEAK_ELEMENT_2,
		LBL_LIMIT_TIME,
		LBL_SERIES_ARENA_NAME,
		TEX_ICON
	}

	protected UIModelRenderTexture enemyModelRenderTexture;

	private QuestTable.QuestTableData questData;

	private DeliveryTable.DeliveryData deliveryData;

	protected int enemyIndex;

	public override void Initialize()
	{
		object[] array = GameSection.GetEventData() as object[];
		deliveryData = (array[0] as DeliveryTable.DeliveryData);
		enemyIndex = (int)array[1];
		questData = deliveryData.GetQuestData();
		LoadEnemyModel();
		UpdateTopBar();
		base.Initialize();
	}

	private void UpdateTopBar()
	{
		int num = (int)questData.limitTime;
		SetLabelText(UI.LBL_LIMIT_TIME, $"{num / 60}:{num % 60:D2}");
		SetLabelText(UI.LBL_SERIES_ARENA_NAME, deliveryData.name);
		ResourceLoad.LoadWithSetUITexture(GetCtrl(UI.TEX_ICON).GetComponent<UITexture>(), RESOURCE_CATEGORY.SERIES_ARENA_RANK_ICON, ResourceName.GetSeriesArenaRankIconName(questData.rarity));
	}

	private void LoadEnemyModel()
	{
		EnemyTable.EnemyData enemyData = Singleton<EnemyTable>.I.GetEnemyData((uint)questData.enemyID[enemyIndex]);
		SetRenderEnemyModel(UI.TEX_ENEMY_MODEL, enemyData.id, enemyData.name, OutGameSettingsManager.EnemyDisplayInfo.SCENE.QUEST, null, UIModelRenderTexture.ENEMY_MOVE_TYPE.DONT_MOVE);
		enemyModelRenderTexture = GetComponent<UIModelRenderTexture>(UI.TEX_ENEMY_MODEL);
		GetComponent<UITexture>(UI.TEX_ENEMY_MODEL).color = Color.white;
		ItemIcon itemIcon = ItemIcon.Create(ITEM_ICON_TYPE.QUEST_ITEM, enemyData.iconId, null, GetCtrl(UI.OBJ_ENEMY), enemyData.element);
		itemIcon.SetEnableCollider(is_enable: false);
		itemIcon.rarityFrame.spriteName = "MonsterFrame_CD";
		UIBehaviour.SetRarityColorType(1, itemIcon.rarityFrame);
		SetLabelText(UI.LBL_QUEST_NAME, "Lv" + questData.enemyLv[enemyIndex] + enemyData.name);
		SetElementSprite(UI.SPR_ELEMENT_2, (int)enemyData.element);
		SetActive(UI.STR_NON_ELEMENT_2, enemyData.element == ELEMENT_TYPE.MAX);
		SetElementSprite(UI.SPR_WEAK_ELEMENT_2, (int)enemyData.weakElement);
		SetActive(UI.STR_NON_WEAK_ELEMENT_2, enemyData.weakElement == ELEMENT_TYPE.MAX);
	}
}
