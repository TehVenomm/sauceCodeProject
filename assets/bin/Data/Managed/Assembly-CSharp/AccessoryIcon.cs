// Decompiled with JetBrains decompiler
// Type: AccessoryIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
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
    Transform transform = ResourceUtility.Realizes((Object) MonoBehaviourSingleton<GlobalSettingsManager>.I.linkResources.accessoryIconPrefab, (Transform) null);
    AccessoryIcon ai = ((Component) transform).GetComponent<AccessoryIcon>();
    ResourceLoad.LoadIconTexture((MonoBehaviour) ai, RESOURCE_CATEGORY.ICON_ACCESSORY, ResourceName.GetAccessoryIcon((int) accessoryId), (System.Action) null, (Action<Texture>) (tex => ai.texIcon.mainTexture = tex));
    int iconBgid = ItemIcon.GetIconBGID(ITEM_ICON_TYPE.ACCESSORY, (int) accessoryId, new RARITY_TYPE?(rarity));
    ResourceLoad.LoadIconTexture((MonoBehaviour) ai, RESOURCE_CATEGORY.ICON_ITEM, ResourceName.GetItemIcon(iconBgid), (System.Action) null, (Action<Texture>) (tex => ai.texBg.mainTexture = tex));
    string str1 = "RarityText_" + rarity.ToString();
    if (getType != GET_TYPE.PAY)
      str1 += "_Event";
    ai.sprRarity.spriteName = str1;
    string str2 = rarity == RARITY_TYPE.D || rarity == RARITY_TYPE.C ? "EquipIconFrame_CD" : "EquipIconFrame_" + rarity.ToString();
    ai.sprFrame.spriteName = str2;
    return transform;
  }
}
