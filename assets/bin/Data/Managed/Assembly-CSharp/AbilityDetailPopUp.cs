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

	public AbilityDetailPopUp()
		: this()
	{
	}

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
		this.get_gameObject().SetActive(false);
	}

	public void PreCacheAbilityDetail(string name, string ap, string desc)
	{
		SetAbilityDetailText(name, ap, desc);
		Hide();
	}

	public void ShowAbilityDetail(Transform targetTrans)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		myTransform.get_gameObject().SetActive(true);
		myTransform.get_transform().set_position(targetTrans.TransformPoint(DETAIL_OFFSET));
		this.StartCoroutine(Follow(targetTrans));
	}

	private IEnumerator Follow(Transform target)
	{
		while (myTransform.get_gameObject().get_activeInHierarchy())
		{
			Vector3 pos = target.TransformPoint(DETAIL_OFFSET);
			pos.x = 0f;
			myTransform.set_position(pos);
			yield return null;
		}
	}
}
