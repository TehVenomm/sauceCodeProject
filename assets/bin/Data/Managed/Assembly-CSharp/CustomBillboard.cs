using UnityEngine;

public class CustomBillboard : MonoBehaviour
{
	public enum FIXED_AXIS
	{
		NONE,
		X,
		Y,
		Z
	}

	[SerializeField]
	private FIXED_AXIS fixedAxis;

	private Transform m_cachedTrans;

	private Transform m_cachedCamTrans;

	private void Awake()
	{
		m_cachedTrans = base.transform;
	}

	private void LateUpdate()
	{
		if (MonoBehaviourSingleton<AppMain>.IsValid() && (Object)m_cachedCamTrans == (Object)null)
		{
			Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
			if ((Object)mainCamera != (Object)null)
			{
				m_cachedCamTrans = mainCamera.transform;
			}
		}
		if (!((Object)m_cachedCamTrans == (Object)null))
		{
			Vector3 position = m_cachedCamTrans.position;
			switch (fixedAxis)
			{
			case FIXED_AXIS.X:
			{
				Vector3 position4 = m_cachedTrans.position;
				position.x = position4.x;
				break;
			}
			case FIXED_AXIS.Y:
			{
				Vector3 position3 = m_cachedTrans.position;
				position.y = position3.y;
				break;
			}
			case FIXED_AXIS.Z:
			{
				Vector3 position2 = m_cachedTrans.position;
				position.z = position2.z;
				break;
			}
			}
			Vector3 up = Vector3.up;
			if (fixedAxis == FIXED_AXIS.NONE)
			{
				up = m_cachedCamTrans.up;
			}
			m_cachedTrans.LookAt(position, up);
		}
	}
}
