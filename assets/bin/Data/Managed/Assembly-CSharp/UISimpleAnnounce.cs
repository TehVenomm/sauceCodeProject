using UnityEngine;

public class UISimpleAnnounce : UIAnnounceBase<UISimpleAnnounce>
{
	[SerializeField]
	protected GameObject rootObj;

	[SerializeField]
	protected UILabel titleLabel;

	[SerializeField]
	protected UILabel contentsLabel;

	private Vector3 LocalPositionOrg;

	public void Announce(string title, string contents)
	{
		base.gameObject.SetActive(value: true);
		if (AnnounceStart())
		{
			titleLabel.text = title;
			contentsLabel.text = contents;
		}
	}

	protected override void OnStart()
	{
		base.gameObject.SetActive(value: false);
	}

	protected override void OnAfterAnimation()
	{
		base.gameObject.SetActive(value: false);
	}
}
