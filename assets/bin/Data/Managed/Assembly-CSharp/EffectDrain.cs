using UnityEngine;

public class EffectDrain : MonoBehaviour
{
	private GameObject m_effect;

	private Transform m_cachedTrans;

	private Vector3 m_beginPos = Vector3.zero;

	private Transform m_endTrans;

	private float m_timer;

	private float m_finishTime = 0.5f;

	private bool m_isDelete;

	public void Initialize(StageObject srcObj, StageObject dstObj, EffectPlayProcessor.EffectSetting setting)
	{
		if ((Object)srcObj == (Object)null || (Object)dstObj == (Object)null || setting == null)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			m_beginPos = srcObj.FindNode(setting.nodeName).transform.position;
			m_endTrans = dstObj.FindNode("Hip");
			m_timer = 0f;
			m_cachedTrans = base.transform;
			m_cachedTrans.parent = ((!MonoBehaviourSingleton<StageObjectManager>.IsValid()) ? MonoBehaviourSingleton<EffectManager>.I._transform : MonoBehaviourSingleton<StageObjectManager>.I._transform);
			m_cachedTrans.position = m_beginPos;
			Transform effect = EffectManager.GetEffect(setting.effectName, m_cachedTrans);
			if ((Object)effect == (Object)null)
			{
				Object.Destroy(base.gameObject);
			}
			else
			{
				effect.localPosition = Vector3.zero;
				effect.localRotation = Quaternion.identity;
				effect.localScale = Vector3.one;
				m_effect = effect.gameObject;
			}
		}
	}

	private void Update()
	{
		if (!m_isDelete)
		{
			m_timer += Time.deltaTime;
			float num = Mathf.Clamp(m_timer / m_finishTime, 0f, 1f);
			m_cachedTrans.position = Vector3.Slerp(m_beginPos, m_endTrans.position, num);
			if (num >= 1f)
			{
				if ((Object)m_effect != (Object)null)
				{
					m_effect.transform.SetParent(null);
					EffectManager.ReleaseEffect(m_effect, true, true);
					m_effect = null;
				}
				Object.DestroyObject(base.gameObject);
				m_isDelete = true;
			}
		}
	}
}
