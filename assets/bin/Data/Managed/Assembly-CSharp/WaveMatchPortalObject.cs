using UnityEngine;

public class WaveMatchPortalObject : MonoBehaviour
{
	public float m_nowTime;

	private float m_maxTime;

	public Renderer m_rend;

	private void Start()
	{
		m_rend = GetComponentInChildren<Renderer>();
		m_maxTime = m_nowTime;
	}

	private void Update()
	{
		float num = m_nowTime / m_maxTime;
		if (num < 0f)
		{
			num = 0f;
		}
		m_nowTime -= Time.deltaTime;
		m_rend.material.SetTextureOffset("_MainTex", new Vector2(0f, 0f - num));
	}
}
