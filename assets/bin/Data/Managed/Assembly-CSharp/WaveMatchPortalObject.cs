using UnityEngine;

public class WaveMatchPortalObject : MonoBehaviour
{
	public float m_nowTime;

	private float m_maxTime;

	public Renderer m_rend;

	public WaveMatchPortalObject()
		: this()
	{
	}

	private void Start()
	{
		m_rend = this.GetComponentInChildren<Renderer>();
		m_maxTime = m_nowTime;
	}

	private void Update()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		float num = m_nowTime / m_maxTime;
		if (num < 0f)
		{
			num = 0f;
		}
		m_nowTime -= Time.get_deltaTime();
		m_rend.get_material().SetTextureOffset("_MainTex", new Vector2(0f, 0f - num));
	}
}
