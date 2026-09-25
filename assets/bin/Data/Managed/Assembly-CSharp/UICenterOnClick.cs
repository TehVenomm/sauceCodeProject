// Decompiled with JetBrains decompiler
// Type: UICenterOnClick
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
[AddComponentMenu("NGUI/Interaction/Center Scroll View on Click")]
public class UICenterOnClick : MonoBehaviour
{
  private void OnClick()
  {
    UICenterOnChild inParents1 = NGUITools.FindInParents<UICenterOnChild>(((Component) this).gameObject);
    UIPanel inParents2 = NGUITools.FindInParents<UIPanel>(((Component) this).gameObject);
    if (Object.op_Inequality((Object) inParents1, (Object) null))
    {
      if (!((Behaviour) inParents1).enabled)
        return;
      inParents1.CenterOn(((Component) this).transform);
    }
    else
    {
      if (!Object.op_Inequality((Object) inParents2, (Object) null) || inParents2.clipping == UIDrawCall.Clipping.None)
        return;
      UIScrollView component = ((Component) inParents2).GetComponent<UIScrollView>();
      Vector3 pos = Vector3.op_UnaryNegation(inParents2.cachedTransform.InverseTransformPoint(((Component) this).transform.position));
      if (!component.canMoveHorizontally)
        pos.x = inParents2.cachedTransform.localPosition.x;
      if (!component.canMoveVertically)
        pos.y = inParents2.cachedTransform.localPosition.y;
      SpringPanel.Begin(inParents2.cachedGameObject, pos, 6f);
    }
  }
}
