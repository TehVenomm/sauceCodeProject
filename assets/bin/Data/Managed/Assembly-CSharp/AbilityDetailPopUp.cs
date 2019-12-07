using System.Collections;
using UnityEngine;

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

	private static readonly Vector3 DETAIL_OFFSET = new Vector3(0f, 95f, 0f);

	public void SetAbilityDetailText(EquipItemAbility ability)
	{
		SetAbilityDetailText(ability.GetName(), ability.GetAP(), ability.GetDescription());
	}

	public void SetAbilityDetailText(string name, string ap, string desc)
	{
		nameLabel.text = name;
		pointLabel.text = ap;
		descriptionLabel.text = desc;
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}

	public void PreCacheAbilityDetail(string name, string ap, string desc)
	{
		SetAbilityDetailText(name, ap, desc);
		Hide();
	}

	public void ShowAbilityDetail(Transform targetTrans)
	{
		myTransform.gameObject.SetActive(value: true);
		myTransform.transform.position = targetTrans.TransformPoint(DETAIL_OFFSET);
		StartCoroutine(Follow(targetTrans));
	}

	private IEnumerator Follow(Transform target)
	{
		while (myTransform.gameObject.activeInHierarchy)
		{
			Vector3 position = target.TransformPoint(DETAIL_OFFSET);
			position.x = 0f;
			myTransform.position = position;
			yield return null;
		}
	}
}
