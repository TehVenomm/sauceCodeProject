using UnityEngine;

public class ChatSlider
{
	private BoxCollider m_Collider;

	private Transform m_Trans;

	public BoxCollider Collider
	{
		get
		{
			if (m_Collider == null)
			{
				m_Collider = this.GetComponent<BoxCollider>();
			}
			return m_Collider;
		}
	}

	public Transform Trans
	{
		get
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			if (m_Trans == null)
			{
				m_Trans = this.get_transform();
			}
			return m_Trans;
		}
	}

	public ChatSlider()
		: this()
	{
	}

	private void OnDrag(Vector2 delta)
	{
	}

	private void OnDragStart()
	{
	}

	private void OnClick()
	{
	}
}
