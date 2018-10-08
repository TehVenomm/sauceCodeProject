using UnityEngine;

public class CharaMakeColorListItem
{
	[SerializeField]
	private UISprite m_Sprite;

	[SerializeField]
	private UIButton m_Button;

	[SerializeField]
	private GameObject m_OnRoot;

	[SerializeField]
	private GameObject m_OffRoot;

	public int id;

	public Transform uiEventSender => m_Button.get_transform();

	public CharaMakeColorListItem()
		: this()
	{
	}

	private void Awake()
	{
		m_Button.tweenTarget = null;
	}

	public void Init(Color color, int id, UIScrollView scroll)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		m_Sprite.color = color;
		m_Button.defaultColor = color;
		m_Button.hover = color;
		m_Button.pressed = color;
		m_Button.disabledColor = color;
		m_Button.CacheDefaultColor();
		this.id = id;
		m_Button.get_gameObject().AddComponent<UIDragScrollView>().scrollView = scroll;
	}

	public void On()
	{
		m_OnRoot.SetActive(true);
		m_OffRoot.SetActive(false);
	}

	public void Off()
	{
		m_OnRoot.SetActive(false);
		m_OffRoot.SetActive(true);
	}
}
