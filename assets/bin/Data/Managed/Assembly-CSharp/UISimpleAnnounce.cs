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
		this.get_gameObject().SetActive(true);
		if (AnnounceStart())
		{
			titleLabel.text = title;
			contentsLabel.text = contents;
		}
	}

	protected override void OnStart()
	{
		this.get_gameObject().SetActive(false);
	}

	protected override void OnAfterAnimation()
	{
		this.get_gameObject().SetActive(false);
	}
}
