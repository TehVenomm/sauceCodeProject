using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{
	public class JointInfo
	{
		public float distance = 1f;

		public Vector3 basisAxis = Vector3.get_zero();
	}

	public const float DEFAULT_LERP_FRAME = 0.8f;

	[SerializeField]
	private float gravity = 9.8f;

	[SerializeField]
	private float angleMax = 30f;

	[SerializeField]
	private float groundHeight;

	[SerializeField]
	private float radius = 1.5f;

	[SerializeField]
	private int uniqueID;

	[SerializeField]
	private bool isUpdate = true;

	[SerializeField]
	private Transform[] pointList;

	private List<JointInfo> m_jointInfoList = new List<JointInfo>();

	private Vector3[] m_prevPositionList;

	private Quaternion[] m_lerpRotationList;

	private Vector3[] m_lerpPositionList;

	private float m_finishLerpTime;

	private float m_lerpTime;

	public Vector3[] PreviousPositionList => m_prevPositionList;

	public int UniqueID => uniqueID;

	public TailController()
		: this()
	{
	}

	private void Awake()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		int num = pointList.Length;
		for (int i = 1; i < num; i++)
		{
			Vector3 localPosition = pointList[i].get_localPosition();
			JointInfo jointInfo = new JointInfo();
			jointInfo.distance = localPosition.get_magnitude();
			jointInfo.basisAxis = localPosition.get_normalized();
			m_jointInfoList.Add(jointInfo);
		}
		m_prevPositionList = (Vector3[])new Vector3[num];
		UpdatePreviousPositionList();
		m_lerpPositionList = (Vector3[])new Vector3[num];
		m_lerpRotationList = (Quaternion[])new Quaternion[num];
		UpdateLerpInfo();
	}

	private void LateUpdate()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		if (isUpdate)
		{
			float num = groundHeight + radius;
			for (int i = 1; i < pointList.Length; i++)
			{
				JointInfo jointInfo = m_jointInfoList[i - 1];
				Transform val = pointList[i - 1];
				Transform val2 = pointList[i];
				Vector3 position = val.get_position();
				Vector3 val3 = val.TransformDirection(jointInfo.basisAxis);
				Vector3 normalized = val3.get_normalized();
				Vector3 val4 = m_prevPositionList[i] - position;
				Vector3 normalized2 = val4.get_normalized();
				normalized2.y -= gravity * Time.get_deltaTime();
				float num2 = Mathf.Min(Vector3.Angle(normalized, normalized2), angleMax);
				Quaternion val5 = Quaternion.AngleAxis(num2, Vector3.Cross(normalized, normalized2));
				Vector3 val6 = val5 * normalized;
				val6.Normalize();
				Vector3 val7 = position + val6 * jointInfo.distance;
				Quaternion val8 = val5 * val.get_rotation();
				Vector3 val9 = val7;
				if (val9.y < num)
				{
					val9.y = num;
					Vector3 val10 = val7 - position;
					Vector3 normalized3 = val10.get_normalized();
					Vector3 val11 = val9 - position;
					Vector3 normalized4 = val11.get_normalized();
					val8 = Quaternion.FromToRotation(normalized3, normalized4) * val8;
					val7 = position + normalized4 * jointInfo.distance;
				}
				val2.set_position(val7);
				val2.set_rotation(val8);
			}
			UpdatePreviousPositionList();
		}
		else if (m_finishLerpTime > 0f)
		{
			m_lerpTime += Time.get_deltaTime();
			float num3 = Mathf.Clamp01(m_lerpTime / m_finishLerpTime);
			int num4 = pointList.Length;
			for (int j = 0; j < num4; j++)
			{
				Transform val12 = pointList[j];
				val12.set_localRotation(Quaternion.Lerp(m_lerpRotationList[j], val12.get_localRotation(), num3));
				val12.set_localPosition(Vector3.Lerp(m_lerpPositionList[j], val12.get_localPosition(), num3));
			}
			if (num3 >= 1f)
			{
				m_finishLerpTime = 0f;
			}
		}
	}

	public void RequestLerp(float lerpFinishTime)
	{
		if (!(lerpFinishTime <= 0f))
		{
			m_finishLerpTime = lerpFinishTime;
			m_lerpTime = 0f;
			UpdateLerpInfo();
		}
	}

	private void UpdateLerpInfo()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < m_prevPositionList.Length; i++)
		{
			m_lerpRotationList[i] = pointList[i].get_localRotation();
			m_lerpPositionList[i] = pointList[i].get_localPosition();
		}
	}

	private void UpdatePreviousPositionList()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < m_prevPositionList.Length; i++)
		{
			m_prevPositionList[i] = pointList[i].get_position();
		}
	}

	public void SetPreviousPositionList(Vector3[] posList)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (posList != null && m_prevPositionList != null && posList.Length == m_prevPositionList.Length)
		{
			int num = posList.Length;
			for (int i = 0; i < num; i++)
			{
				m_prevPositionList[i] = posList[i];
			}
		}
	}

	public void SetUpdateFlag(bool flag, bool isUpdatePreviousPosition = true)
	{
		if (isUpdate != flag)
		{
			isUpdate = flag;
			if (isUpdate && isUpdatePreviousPosition)
			{
				UpdatePreviousPositionList();
			}
		}
	}
}
