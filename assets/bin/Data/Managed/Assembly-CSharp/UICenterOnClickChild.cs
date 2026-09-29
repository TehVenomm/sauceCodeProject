// Decompiled with JetBrains decompiler
// Type: UICenterOnClickChild
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UICenterOnClickChild : MonoBehaviour
{
  private void OnClick()
  {
    UICenterOnClick inParents1 = NGUITools.FindInParents<UICenterOnClick>(((Component) this).gameObject);
    if (Object.op_Equality((Object) inParents1, (Object) null))
      return;
    Transform transform = ((Component) inParents1).transform;
    UICenterOnChild inParents2 = NGUITools.FindInParents<UICenterOnChild>(((Component) this).gameObject);
    UIPanel inParents3 = NGUITools.FindInParents<UIPanel>(((Component) this).gameObject);
    if (Object.op_Inequality((Object) inParents2, (Object) null))
    {
      if (!((Behaviour) inParents2).enabled)
        return;
      inParents2.CenterOn(transform);
    }
    else
    {
      if (!Object.op_Inequality((Object) inParents3, (Object) null) || inParents3.clipping == UIDrawCall.Clipping.None)
        return;
      UIScrollView component = ((Component) inParents3).GetComponent<UIScrollView>();
      Vector3 pos = Vector3.op_UnaryNegation(inParents3.cachedTransform.InverseTransformPoint(transform.position));
      if (!component.canMoveHorizontally)
        pos.x = inParents3.cachedTransform.localPosition.x;
      if (!component.canMoveVertically)
        pos.y = inParents3.cachedTransform.localPosition.y;
      SpringPanel.Begin(inParents3.cachedGameObject, pos, 6f);
    }
  }
}
