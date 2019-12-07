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
		if (MonoBehaviourSingleton<AppMain>.IsValid() && m_cachedCamTrans == null)
		{
			Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
			if (mainCamera != null)
			{
				m_cachedCamTrans = mainCamera.transform;
			}
		}
		if (!(m_cachedCamTrans == null))
		{
			Vector3 position = m_cachedCamTrans.position;
			switch (fixedAxis)
			{
			case FIXED_AXIS.X:
				position.x = m_cachedTrans.position.x;
				break;
			case FIXED_AXIS.Y:
				position.y = m_cachedTrans.position.y;
				break;
			case FIXED_AXIS.Z:
				position.z = m_cachedTrans.position.z;
				break;
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
