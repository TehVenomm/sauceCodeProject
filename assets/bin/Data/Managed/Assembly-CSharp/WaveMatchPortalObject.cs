// Decompiled with JetBrains decompiler
// Type: WaveMatchPortalObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class WaveMatchPortalObject : MonoBehaviour
{
  public float m_nowTime;
  private float m_maxTime;
  public Renderer m_rend;

  private void Start()
  {
    this.m_rend = ((Component) this).GetComponentInChildren<Renderer>();
    this.m_maxTime = this.m_nowTime;
  }

  private void Update()
  {
    float num = this.m_nowTime / this.m_maxTime;
    if ((double) num < 0.0)
      num = 0.0f;
    this.m_nowTime -= Time.deltaTime;
    this.m_rend.material.SetTextureOffset("_MainTex", new Vector2(0.0f, -num));
  }
}
