using System;
using UnityEngine;

public class ChatHeaderButtonController
{
	public enum STATE
	{
		UNDEFINED,
		SELECTED,
		UNSELECTED,
		DEACTIVATE,
		INVISIBLE
	}

	public class InitParam
	{
		public MainChat.CHAT_TYPE ChatType;

		public int ButtonIndex;

		public Action OnActivateCallBack;

		public Action OnDeactivateCallBack;

		public Action OnInvisibleCallBack;

		public Action OnSelectCallBack;
	}

	private const int DEPTH_OFFSET = 30;

	private const float ANCHOR_LEFT = 0f;

	private const float ANCHOR_RIGHT = 1f;

	private const int WIDTH_SPRITE_NAME_SIDE = 174;

	private const int WIDTH_SPRITE_NAME_LEFT_MIDDLE = 202;

	private static readonly string SPRITE_NAME_SIDE = "ChatPartyTab0_02";

	private static readonly string SPRITE_NAME_LEFT_MIDDLE = "ChatPartyTab1_02";

	private readonly Vector3 BG_SPRITE_POS = new Vector3(0f, -27.8f, 0f);

	private readonly Color BASE_COLOR_ACTIVE = Color.get_white();

	private readonly Color BASE_COLOR_DEACTIVE = new Color(0.5f, 0.5f, 0.5f, 1f);

	private readonly Color OUTLINE_COLOR_ACTIVE = new Color(0f, 0.25f, 0.31f, 1f);

	private readonly Color OUTLINE_COLOR_DEACTIVE = new Color(0.12f, 0.12f, 0.12f, 1f);

	private readonly string SELECTED_SPRITE = "PartyBtn_on_02";

	private readonly string NOT_SELECTED_SPRITE = "PartyBtn_off_02";

	[SerializeField]
	private UISprite m_bgSprite;

	[SerializeField]
	private UIButton m_buttonObject;

	private UIWidget m_buttonWidget;

	[SerializeField]
	private UILabel m_buttonLabel;

	[SerializeField]
	private UISprite m_backGroundSprite;

	private MainChat.CHAT_TYPE m_myType;

	private STATE m_currentState;

	private Action m_onActivateCallBack;

	private Action m_onDeactivateCallBack;

	private Action m_onInvisibleCallBack;

	private Action m_onSelectCallBack;

	private int m_selectedDepth;

	private int m_myButtonIndex;

	private UIWidget ButtonWidget => m_buttonWidget ?? (m_buttonWidget = ((!(m_buttonObject == null)) ? m_buttonObject.GetComponent<UIWidget>() : null));

	public MainChat.CHAT_TYPE MyChatType => m_myType;

	public STATE CurrentState => m_currentState;

	private int ButtonIndex => m_myButtonIndex;

	public ChatHeaderButtonController()
		: this()
	{
	}//IL_0010: Unknown result type (might be due to invalid IL or missing references)
	//IL_0015: Unknown result type (might be due to invalid IL or missing references)
	//IL_001b: Unknown result type (might be due to invalid IL or missing references)
	//IL_0020: Unknown result type (might be due to invalid IL or missing references)
	//IL_003a: Unknown result type (might be due to invalid IL or missing references)
	//IL_003f: Unknown result type (might be due to invalid IL or missing references)
	//IL_0059: Unknown result type (might be due to invalid IL or missing references)
	//IL_005e: Unknown result type (might be due to invalid IL or missing references)
	//IL_0078: Unknown result type (might be due to invalid IL or missing references)
	//IL_007d: Unknown result type (might be due to invalid IL or missing references)


	public bool Initialize(InitParam _param)
	{
		if (_param == null)
		{
			return false;
		}
		m_myType = _param.ChatType;
		m_myButtonIndex = _param.ButtonIndex;
		InitUILabel(m_myType);
		m_selectedDepth = ((m_backGroundSprite != null) ? m_backGroundSprite.depth : 0);
		SetBgSprite(ButtonIndex);
		SetDepth();
		m_onActivateCallBack = _param.OnActivateCallBack;
		m_onDeactivateCallBack = _param.OnDeactivateCallBack;
		m_onInvisibleCallBack = _param.OnInvisibleCallBack;
		m_onSelectCallBack = _param.OnSelectCallBack;
		return true;
	}

	private void InitDepth(int _index)
	{
		if (m_backGroundSprite != null)
		{
			m_backGroundSprite.depth = 0 + _index * 3;
		}
		if (m_bgSprite != null)
		{
			m_bgSprite.depth = 1 + _index * 3;
		}
		if (m_buttonLabel != null)
		{
			m_buttonLabel.depth = 2 + _index * 3;
		}
	}

	public void InitUILabel(MainChat.CHAT_TYPE _t)
	{
		if (!(m_buttonLabel == null))
		{
			switch (_t)
			{
			case MainChat.CHAT_TYPE.HOME:
				m_buttonLabel.text = StringTable.Get(STRING_CATEGORY.CHAT, 1u);
				break;
			case MainChat.CHAT_TYPE.ROOM:
				m_buttonLabel.text = StringTable.Get(STRING_CATEGORY.CHAT, 2u);
				break;
			case MainChat.CHAT_TYPE.FIELD:
				m_buttonLabel.text = StringTable.Get(STRING_CATEGORY.CHAT, 3u);
				break;
			case MainChat.CHAT_TYPE.LOUNGE:
				m_buttonLabel.text = StringTable.Get(STRING_CATEGORY.CHAT, 7u);
				break;
			case MainChat.CHAT_TYPE.PERSONAL:
				m_buttonLabel.text = StringTable.Get(STRING_CATEGORY.CHAT, 8u);
				break;
			case MainChat.CHAT_TYPE.CLAN:
				m_buttonLabel.text = StringTable.Get(STRING_CATEGORY.CHAT, 9u);
				break;
			default:
				m_buttonLabel.text = string.Empty;
				break;
			}
		}
	}

	private void SetBgSprite(int _index)
	{
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		if (!(m_backGroundSprite == null))
		{
			bool flag = 0 < _index && _index < 2;
			bool flag2 = _index <= 0;
			m_backGroundSprite.spriteName = ((!flag) ? SPRITE_NAME_SIDE : SPRITE_NAME_LEFT_MIDDLE);
			m_backGroundSprite.width = ((!flag) ? 174 : 202);
			m_backGroundSprite.flip = (flag2 ? UIBasicSprite.Flip.Horizontally : UIBasicSprite.Flip.Nothing);
			m_backGroundSprite.pivot = (flag ? UIWidget.Pivot.Center : ((!flag2) ? UIWidget.Pivot.Right : UIWidget.Pivot.Left));
			if (!flag)
			{
				ButtonWidget.leftAnchor.Set(0f, (!flag2) ? 44f : 14f);
				ButtonWidget.rightAnchor.Set(1f, (!flag2) ? (-14f) : (-44f));
			}
			m_backGroundSprite.get_transform().set_localPosition(BG_SPRITE_POS);
		}
	}

	private void SetDepth()
	{
		int num = (CurrentState != STATE.SELECTED) ? (ButtonIndex * 3) : (ButtonIndex * 3 + 30);
		if (m_backGroundSprite != null)
		{
			m_backGroundSprite.depth = num;
		}
		if (m_bgSprite != null)
		{
			m_bgSprite.depth = num + 1;
		}
		if (m_buttonLabel != null)
		{
			m_buttonLabel.depth = num + 2;
		}
	}

	public void OnClick()
	{
		Select();
	}

	private void SetNextState(STATE _s)
	{
		if (CurrentState != _s)
		{
			m_currentState = _s;
		}
	}

	public void Activate()
	{
		if (UnSelect() && m_onActivateCallBack != null)
		{
			m_onActivateCallBack.Invoke();
		}
	}

	public bool Select()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (!IsValidObjects())
		{
			return false;
		}
		SetNextState(STATE.SELECTED);
		m_buttonObject.normalSprite = SELECTED_SPRITE;
		m_buttonObject.SetState(UIButtonColor.State.Normal, true);
		m_buttonLabel.color = BASE_COLOR_ACTIVE;
		m_buttonLabel.effectColor = OUTLINE_COLOR_ACTIVE;
		if (m_onSelectCallBack != null)
		{
			m_onSelectCallBack.Invoke();
		}
		SetBgSprite(ButtonIndex);
		SetDepth();
		return true;
	}

	public bool UnSelect()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (!IsValidObjects())
		{
			return false;
		}
		SetNextState(STATE.UNSELECTED);
		m_buttonObject.normalSprite = NOT_SELECTED_SPRITE;
		m_buttonObject.SetState(UIButtonColor.State.Normal, true);
		m_buttonLabel.color = BASE_COLOR_ACTIVE;
		m_buttonLabel.effectColor = OUTLINE_COLOR_ACTIVE;
		SetBgSprite(ButtonIndex);
		SetDepth();
		return true;
	}

	public void Deactivate()
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (IsValidObjects())
		{
			SetNextState(STATE.DEACTIVATE);
			SetBgSprite(ButtonIndex);
			SetDepth();
			m_buttonObject.SetState(UIButtonColor.State.Disabled, true);
			m_buttonLabel.color = BASE_COLOR_DEACTIVE;
			m_buttonLabel.effectColor = OUTLINE_COLOR_DEACTIVE;
			if (m_onDeactivateCallBack != null)
			{
				m_onDeactivateCallBack.Invoke();
			}
		}
	}

	public void Show()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		UnSelect();
		this.get_gameObject().SetActive(true);
	}

	public void Hide()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		SetNextState(STATE.INVISIBLE);
		this.get_gameObject().SetActive(false);
		if (m_onInvisibleCallBack != null)
		{
			m_onInvisibleCallBack.Invoke();
		}
	}

	private bool IsValidObjects()
	{
		return m_bgSprite != null && m_buttonObject != null && m_buttonLabel != null;
	}
}