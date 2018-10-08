using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Drag Scroll View")]
public class UIDragScrollView
{
	public UIScrollView scrollView;

	[HideInInspector]
	[SerializeField]
	private UIScrollView draggablePanel;

	private Transform mTrans;

	private UIScrollView mScroll;

	private bool mAutoFind;

	private bool mStarted;

	public UIDragScrollView()
		: this()
	{
	}

	private void OnEnable()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		mTrans = this.get_transform();
		if (scrollView == null && draggablePanel != null)
		{
			scrollView = draggablePanel;
			draggablePanel = null;
		}
		if (mStarted && (mAutoFind || mScroll == null))
		{
			FindScrollView();
		}
	}

	private void Start()
	{
		mStarted = true;
		FindScrollView();
		AttachUIButtonEffect();
	}

	private void FindScrollView()
	{
		UIScrollView uIScrollView = NGUITools.FindInParents<UIScrollView>(mTrans);
		if (scrollView == null || (mAutoFind && uIScrollView != scrollView))
		{
			scrollView = uIScrollView;
			mAutoFind = true;
		}
		else if (scrollView == uIScrollView)
		{
			mAutoFind = true;
		}
		mScroll = scrollView;
	}

	private void AttachUIButtonEffect()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		UIButton component = this.GetComponent<UIButton>();
		if (component != null && this.GetComponent<UINoAuto>() == null && component.GetComponent<UIButtonEffect>() == null)
		{
			UIButtonEffect uIButtonEffect = component.get_gameObject().AddComponent<UIButtonEffect>();
			uIButtonEffect.isSimple = true;
		}
	}

	private void OnPress(bool pressed)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		if (mAutoFind && mScroll != scrollView)
		{
			mScroll = scrollView;
			mAutoFind = false;
		}
		if (Object.op_Implicit(scrollView) && this.get_enabled() && NGUITools.GetActive(this.get_gameObject()))
		{
			scrollView.Press(pressed);
			if (!pressed && mAutoFind)
			{
				scrollView = NGUITools.FindInParents<UIScrollView>(mTrans);
				mScroll = scrollView;
			}
		}
	}

	private void OnDrag(Vector2 delta)
	{
		if (Object.op_Implicit(scrollView) && NGUITools.GetActive(this))
		{
			scrollView.Drag();
		}
	}

	private void OnScroll(float delta)
	{
		if (Object.op_Implicit(scrollView) && NGUITools.GetActive(this))
		{
			scrollView.Scroll(delta);
		}
	}

	public void OnPan(Vector2 delta)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (Object.op_Implicit(scrollView) && NGUITools.GetActive(this))
		{
			scrollView.OnPan(delta);
		}
	}
}
