using UnityEngine;

public class AccessoryIcon : MonoBehaviour
{
	[SerializeField]
	public UITexture texIcon;

	[SerializeField]
	public UITexture texBg;

	[SerializeField]
	public UISprite sprFrame;

	[SerializeField]
	public UISprite sprRarity;

	public static Transform Create(uint accessoryId, RARITY_TYPE rarity, GET_TYPE getType)
	{
		Transform transform = ResourceUtility.Realizes(MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.accessoryIconPrefab, null);
		AccessoryIcon ai = transform.GetComponent<AccessoryIcon>();
		ResourceLoad.LoadIconTexture(ai, RESOURCE_CATEGORY.ICON_ACCESSORY, ResourceName.GetAccessoryIcon((int)accessoryId), null, delegate(Texture tex)
		{
			ai.texIcon.mainTexture = tex;
		});
		int iconBGID = ItemIcon.GetIconBGID(ITEM_ICON_TYPE.ACCESSORY, (int)accessoryId, rarity);
		ResourceLoad.LoadIconTexture(ai, RESOURCE_CATEGORY.ICON_ITEM, ResourceName.GetItemIcon(iconBGID), null, delegate(Texture tex)
		{
			ai.texBg.mainTexture = tex;
		});
		string text = "RarityText_" + rarity.ToString();
		if (getType != GET_TYPE.PAY)
		{
			text += "_Event";
		}
		ai.sprRarity.spriteName = text;
		string spriteName = (rarity != 0 && rarity != RARITY_TYPE.C) ? ("EquipIconFrame_" + rarity.ToString()) : "EquipIconFrame_CD";
		ai.sprFrame.spriteName = spriteName;
		return transform;
	}
}
