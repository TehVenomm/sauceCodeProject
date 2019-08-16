using UnityEngine;

public class ChatSlider : MonoBehaviour
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
