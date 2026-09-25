// Decompiled with JetBrains decompiler
// Type: AbilityDetailPopUp
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System.Collections;
using UnityEngine;

#nullable disable
public class AbilityDetailPopUp : MonoBehaviour
{
  [SerializeField]
  private UILabel nameLabel;
  [SerializeField]
  private UILabel pointLabel;
  [SerializeField]
  private UILabel descriptionLabel;
  [SerializeField]
  private Transform myTransform;
  private static readonly Vector3 DETAIL_OFFSET = new Vector3(0.0f, 95f, 0.0f);

  public void SetAbilityDetailText(EquipItemAbility ability)
  {
    this.SetAbilityDetailText(ability.GetName(), ability.GetAP(), ability.GetDescription());
  }

  public void SetAbilityDetailText(string name, string ap, string desc)
  {
    this.nameLabel.text = name;
    this.pointLabel.text = ap;
    this.descriptionLabel.text = desc;
  }

  public void Hide() => ((Component) this).gameObject.SetActive(false);

  public void PreCacheAbilityDetail(string name, string ap, string desc)
  {
    this.SetAbilityDetailText(name, ap, desc);
    this.Hide();
  }

  public void ShowAbilityDetail(Transform targetTrans)
  {
    ((Component) this.myTransform).gameObject.SetActive(true);
    ((Component) this.myTransform).transform.position = targetTrans.TransformPoint(AbilityDetailPopUp.DETAIL_OFFSET);
    this.StartCoroutine(this.Follow(targetTrans));
  }

  private IEnumerator Follow(Transform target)
  {
    while (((Component) this.myTransform).gameObject.activeInHierarchy)
    {
      Vector3 vector3 = target.TransformPoint(AbilityDetailPopUp.DETAIL_OFFSET);
      vector3.x = 0.0f;
      this.myTransform.position = vector3;
      yield return (object) null;
    }
  }
}
