using UnityEngine;

public class CheckAnim : MonoBehaviour
{
	public Animation m_Target;

	private void OnGUI()
	{
		if (!((Object)m_Target == (Object)null) && GUILayout.Button("play"))
		{
			m_Target.Stop();
			m_Target.Play();
		}
	}
}
