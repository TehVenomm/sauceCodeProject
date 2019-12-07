using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{
	public class JointInfo
	{
		public float distance = 1f;

		public Vector3 basisAxis = Vector3.zero;
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

	private void Awake()
	{
		int num = pointList.Length;
		for (int i = 1; i < num; i++)
		{
			Vector3 localPosition = pointList[i].localPosition;
			JointInfo jointInfo = new JointInfo();
			jointInfo.distance = localPosition.magnitude;
			jointInfo.basisAxis = localPosition.normalized;
			m_jointInfoList.Add(jointInfo);
		}
		m_prevPositionList = new Vector3[num];
		UpdatePreviousPositionList();
		m_lerpPositionList = new Vector3[num];
		m_lerpRotationList = new Quaternion[num];
		UpdateLerpInfo();
	}

	private void LateUpdate()
	{
		if (isUpdate)
		{
			float num = groundHeight + radius;
			for (int i = 1; i < pointList.Length; i++)
			{
				JointInfo jointInfo = m_jointInfoList[i - 1];
				Transform transform = pointList[i - 1];
				Transform obj = pointList[i];
				Vector3 position = transform.position;
				Vector3 normalized = transform.TransformDirection(jointInfo.basisAxis).normalized;
				Vector3 normalized2 = (m_prevPositionList[i] - position).normalized;
				normalized2.y -= gravity * Time.deltaTime;
				Quaternion quaternion = Quaternion.AngleAxis(Mathf.Min(Vector3.Angle(normalized, normalized2), angleMax), Vector3.Cross(normalized, normalized2));
				Vector3 a = quaternion * normalized;
				a.Normalize();
				Vector3 vector = position + a * jointInfo.distance;
				Quaternion quaternion2 = quaternion * transform.rotation;
				Vector3 a2 = vector;
				if (a2.y < num)
				{
					a2.y = num;
					Vector3 normalized3 = (vector - position).normalized;
					Vector3 normalized4 = (a2 - position).normalized;
					quaternion2 = Quaternion.FromToRotation(normalized3, normalized4) * quaternion2;
					vector = position + normalized4 * jointInfo.distance;
				}
				obj.position = vector;
				obj.rotation = quaternion2;
			}
			UpdatePreviousPositionList();
		}
		else if (m_finishLerpTime > 0f)
		{
			m_lerpTime += Time.deltaTime;
			float num2 = Mathf.Clamp01(m_lerpTime / m_finishLerpTime);
			int num3 = pointList.Length;
			for (int j = 0; j < num3; j++)
			{
				Transform transform2 = pointList[j];
				transform2.localRotation = Quaternion.Lerp(m_lerpRotationList[j], transform2.localRotation, num2);
				transform2.localPosition = Vector3.Lerp(m_lerpPositionList[j], transform2.localPosition, num2);
			}
			if (num2 >= 1f)
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
		for (int i = 0; i < m_prevPositionList.Length; i++)
		{
			m_lerpRotationList[i] = pointList[i].localRotation;
			m_lerpPositionList[i] = pointList[i].localPosition;
		}
	}

	private void UpdatePreviousPositionList()
	{
		for (int i = 0; i < m_prevPositionList.Length; i++)
		{
			m_prevPositionList[i] = pointList[i].position;
		}
	}

	public void SetPreviousPositionList(Vector3[] posList)
	{
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
