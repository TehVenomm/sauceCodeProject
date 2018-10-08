using UnityEngine;

public class CharaMakeColorListItem : MonoBehaviour
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

	public Transform uiEventSender => m_Button.transform;

	private void Awake()
	{
		m_Button.tweenTarget = null;
	}

	public void Init(Color color, int id, UIScrollView scroll)
	{
		m_Sprite.color = color;
		m_Button.defaultColor = color;
		m_Button.hover = color;
		m_Button.pressed = color;
		m_Button.disabledColor = color;
		m_Button.CacheDefaultColor();
		this.id = id;
		m_Button.gameObject.AddComponent<UIDragScrollView>().scrollView = scroll;
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
