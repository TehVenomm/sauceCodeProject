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
				m_Collider = GetComponent<BoxCollider>();
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
				m_Trans = base.transform;
			}
			return m_Trans;
		}
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
