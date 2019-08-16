using UnityEngine;

public class TabDepthBtn : MonoBehaviour
{
	public enum STATE
	{
		SELECTED,
		UNSELECTED
	}

	[SerializeField]
	private UIButton m_buttonObject;

	[SerializeField]
	private UISprite m_backGroundSprite;

	[SerializeField]
	private UISprite m_labelSprite;

	private int startDepth;

	public STATE currentState;

	public TabDepthBtn()
		: this()
	{
	}

	protected virtual string GetBtnSelectedSprite()
	{
		return "PartyBtn_on_02";
	}

	protected virtual string GetBtnUnSelectedSprite()
	{
		return "PartyBtn_off_02";
	}

	protected virtual string GetTabSelectedSprite()
	{
		string text = m_labelSprite.spriteName.Replace("off", "on");
		return m_labelSprite.spriteName.Replace("off", "on");
	}

	protected virtual string GetTabUnSelectedSprite()
	{
		string text = m_labelSprite.spriteName.Replace("off", "on");
		return m_labelSprite.spriteName.Replace("on", "off");
	}

	protected virtual int GetDepthOffSet()
	{
		return 30;
	}

	public virtual void Initilize()
	{
		startDepth = m_backGroundSprite.depth;
		UnSelect();
	}

	public void SetState(STATE state)
	{
		currentState = state;
	}

	public void Select()
	{
		SetState(STATE.SELECTED);
		SetTab();
	}

	public void UnSelect()
	{
		SetState(STATE.UNSELECTED);
		SetTab();
	}

	protected virtual void SetTab()
	{
		int depth = (currentState != 0) ? startDepth : GetDepthOffSet();
		string normalSprite = (currentState != 0) ? GetBtnUnSelectedSprite() : GetBtnSelectedSprite();
		string spriteName = (currentState != 0) ? GetTabUnSelectedSprite() : GetTabSelectedSprite();
		m_backGroundSprite.depth = depth;
		m_buttonObject.normalSprite = normalSprite;
		m_buttonObject.SetState(UIButtonColor.State.Normal, immediate: true);
		m_labelSprite.spriteName = spriteName;
	}
}
