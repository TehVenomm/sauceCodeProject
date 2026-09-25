// Decompiled with JetBrains decompiler
// Type: ItemIconDetailSetuperBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using UnityEngine;

#nullable disable
public class ItemIconDetailSetuperBase : MonoBehaviour
{
  [SerializeField]
  private UISprite spBG;
  [SerializeField]
  private UILabel lblName;
  public GameObject[] infoRootAry;
  [SerializeField]
  private GameObject[] inActiveRootAry;
  public static readonly string[] SPR_SKILL_MATERIAL_NUMBER = new string[10]
  {
    "MagiSynthNum01",
    "MagiSynthNum02",
    "MagiSynthNum03",
    "MagiSynthNum04",
    "MagiSynthNum05",
    "MagiSynthNum06",
    "MagiSynthNum07",
    "MagiSynthNum08",
    "MagiSynthNum09",
    "MagiSynthNum10"
  };

  public virtual void Set(object[] data = null)
  {
    if (this.inActiveRootAry == null || this.inActiveRootAry.Length == 0)
      return;
    Array.ForEach<GameObject>(this.inActiveRootAry, (Action<GameObject>) (obj =>
    {
      if (!Object.op_Inequality((Object) obj, (Object) null))
        return;
      obj.gameObject.SetActive(false);
    }));
  }

  public void SetName(string text) => this.lblName.text = text;

  public void SetVisibleBG(bool is_visible) => ((Behaviour) this.spBG).enabled = is_visible;

  public virtual void SetupSelectNumberSprite(int select_number)
  {
    if (Object.op_Equality((Object) this.selectSP, (Object) null))
      return;
    int index = select_number - 1;
    bool flag = index >= 0 && index < ItemIconDetailSetuperBase.SPR_SKILL_MATERIAL_NUMBER.Length;
    this.infoRootAry[0].SetActive(flag);
    if (!flag)
      return;
    this.selectSP.spriteName = ItemIconDetailSetuperBase.SPR_SKILL_MATERIAL_NUMBER[index];
    ((Behaviour) this.selectSP).enabled = true;
  }

  protected virtual UISprite selectSP => (UISprite) null;
}
