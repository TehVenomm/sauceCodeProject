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

	public CustomBillboard()
		: this()
	{
	}

	private void Awake()
	{
		m_cachedTrans = this.get_transform();
	}

	private void LateUpdate()
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		if (MonoBehaviourSingleton<AppMain>.IsValid() && m_cachedCamTrans == null)
		{
			Camera mainCamera = MonoBehaviourSingleton<AppMain>.I.mainCamera;
			if (mainCamera != null)
			{
				m_cachedCamTrans = mainCamera.get_transform();
			}
		}
		if (!(m_cachedCamTrans == null))
		{
			Vector3 position = m_cachedCamTrans.get_position();
			switch (fixedAxis)
			{
			case FIXED_AXIS.X:
			{
				Vector3 position4 = m_cachedTrans.get_position();
				position.x = position4.x;
				break;
			}
			case FIXED_AXIS.Y:
			{
				Vector3 position3 = m_cachedTrans.get_position();
				position.y = position3.y;
				break;
			}
			case FIXED_AXIS.Z:
			{
				Vector3 position2 = m_cachedTrans.get_position();
				position.z = position2.z;
				break;
			}
			}
			Vector3 up = Vector3.get_up();
			if (fixedAxis == FIXED_AXIS.NONE)
			{
				up = m_cachedCamTrans.get_up();
			}
			m_cachedTrans.LookAt(position, up);
		}
	}
}
