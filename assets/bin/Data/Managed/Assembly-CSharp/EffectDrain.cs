// Decompiled with JetBrains decompiler
// Type: EffectDrain
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class EffectDrain : MonoBehaviour
{
  private GameObject m_effect;
  private Transform m_cachedTrans;
  private Vector3 m_beginPos = Vector3.zero;
  private Transform m_endTrans;
  private float m_timer;
  private float m_finishTime = 0.5f;
  private bool m_isDelete;

  public void Initialize(
    StageObject srcObj,
    StageObject dstObj,
    EffectPlayProcessor.EffectSetting setting)
  {
    if (Object.op_Equality((Object) srcObj, (Object) null) || Object.op_Equality((Object) dstObj, (Object) null) || setting == null)
    {
      Object.Destroy((Object) ((Component) this).gameObject);
    }
    else
    {
      this.m_beginPos = ((Component) srcObj.FindNode(setting.nodeName)).transform.position;
      this.m_endTrans = dstObj.FindNode("Hip");
      this.m_timer = 0.0f;
      this.m_cachedTrans = ((Component) this).transform;
      this.m_cachedTrans.parent = MonoBehaviourSingleton<StageObjectManager>.IsValid() ? MonoBehaviourSingleton<StageObjectManager>.I._transform : MonoBehaviourSingleton<EffectManager>.I._transform;
      this.m_cachedTrans.position = this.m_beginPos;
      Transform effect = EffectManager.GetEffect(setting.effectName, this.m_cachedTrans);
      if (Object.op_Equality((Object) effect, (Object) null))
      {
        Object.Destroy((Object) ((Component) this).gameObject);
      }
      else
      {
        effect.localPosition = Vector3.zero;
        effect.localRotation = Quaternion.identity;
        effect.localScale = Vector3.one;
        this.m_effect = ((Component) effect).gameObject;
      }
    }
  }

  private void Update()
  {
    if (this.m_isDelete)
      return;
    this.m_timer += Time.deltaTime;
    float num = Mathf.Clamp(this.m_timer / this.m_finishTime, 0.0f, 1f);
    this.m_cachedTrans.position = Vector3.Slerp(this.m_beginPos, this.m_endTrans.position, num);
    if ((double) num < 1.0)
      return;
    if (Object.op_Inequality((Object) this.m_effect, (Object) null))
    {
      this.m_effect.transform.SetParent((Transform) null);
      EffectManager.ReleaseEffect(this.m_effect, immediate: true);
      this.m_effect = (GameObject) null;
    }
    Object.Destroy((Object) ((Component) this).gameObject);
    this.m_isDelete = true;
  }
}
