using UnityEngine;

public class UISimpleAnnounce : UIAnnounceBase<UISimpleAnnounce>
{
	[SerializeField]
	protected GameObject rootObj;

	[SerializeField]
	protected UILabel titleLabel;

	[SerializeField]
	protected UILabel contentsLabel;

	public void Announce(string title, string contents)
	{
		if (AnnounceStart())
		{
			titleLabel.text = title;
			contentsLabel.text = contents;
		}
	}
}
