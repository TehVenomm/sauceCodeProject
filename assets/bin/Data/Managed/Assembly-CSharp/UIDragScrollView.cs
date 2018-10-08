using UnityEngine;

[AddComponentMenu("NGUI/Interaction/Drag Scroll View")]
public class UIDragScrollView : MonoBehaviour
{
	public UIScrollView scrollView;

	[HideInInspector]
	[SerializeField]
	private UIScrollView draggablePanel;

	private Transform mTrans;

	private UIScrollView mScroll;

	private bool mAutoFind;

	private bool mStarted;

	private void OnEnable()
	{
		mTrans = base.transform;
		if ((Object)scrollView == (Object)null && (Object)draggablePanel != (Object)null)
		{
			scrollView = draggablePanel;
			draggablePanel = null;
		}
		if (mStarted && (mAutoFind || (Object)mScroll == (Object)null))
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
		if ((Object)scrollView == (Object)null || (mAutoFind && (Object)uIScrollView != (Object)scrollView))
		{
			scrollView = uIScrollView;
			mAutoFind = true;
		}
		else if ((Object)scrollView == (Object)uIScrollView)
		{
			mAutoFind = true;
		}
		mScroll = scrollView;
	}

	private void AttachUIButtonEffect()
	{
		UIButton component = GetComponent<UIButton>();
		if ((Object)component != (Object)null && (Object)GetComponent<UINoAuto>() == (Object)null && (Object)component.GetComponent<UIButtonEffect>() == (Object)null)
		{
			UIButtonEffect uIButtonEffect = component.gameObject.AddComponent<UIButtonEffect>();
			uIButtonEffect.isSimple = true;
		}
	}

	private void OnPress(bool pressed)
	{
		if (mAutoFind && (Object)mScroll != (Object)scrollView)
		{
			mScroll = scrollView;
			mAutoFind = false;
		}
		if ((bool)scrollView && base.enabled && NGUITools.GetActive(base.gameObject))
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
		if ((bool)scrollView && NGUITools.GetActive(this))
		{
			scrollView.Drag();
		}
	}

	private void OnScroll(float delta)
	{
		if ((bool)scrollView && NGUITools.GetActive(this))
		{
			scrollView.Scroll(delta);
		}
	}

	public void OnPan(Vector2 delta)
	{
		if ((bool)scrollView && NGUITools.GetActive(this))
		{
			scrollView.OnPan(delta);
		}
	}
}
