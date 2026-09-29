.class Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$13;
.super Landroidx/recyclerview/widget/RecyclerView$ItemDecoration;
.source "ConversationalFragmentRenderer.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->createRecyclerViewLastItemDecor()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V
    .locals 0

    .line 787
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$13;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-direct {p0}, Landroidx/recyclerview/widget/RecyclerView$ItemDecoration;-><init>()V

    return-void
.end method


# virtual methods
.method public getItemOffsets(Landroid/graphics/Rect;Landroid/view/View;Landroidx/recyclerview/widget/RecyclerView;Landroidx/recyclerview/widget/RecyclerView$State;)V
    .locals 1

    .line 790
    invoke-virtual {p3, p2}, Landroidx/recyclerview/widget/RecyclerView;->getChildAdapterPosition(Landroid/view/View;)I

    move-result p2

    const/4 p4, -0x1

    if-ne p2, p4, :cond_0

    return-void

    .line 795
    :cond_0
    invoke-virtual {p3}, Landroidx/recyclerview/widget/RecyclerView;->getAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object p4

    if-eqz p4, :cond_1

    .line 796
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$13;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object v0, v0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->skipOutterBubble:Landroid/widget/LinearLayout;

    invoke-virtual {v0}, Landroid/widget/LinearLayout;->getVisibility()I

    move-result v0

    if-nez v0, :cond_1

    .line 797
    invoke-virtual {p4}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->getItemCount()I

    move-result p4

    const/4 v0, 0x1

    sub-int/2addr p4, v0

    if-ne p2, p4, :cond_1

    .line 798
    invoke-virtual {p3}, Landroidx/recyclerview/widget/RecyclerView;->getContext()Landroid/content/Context;

    move-result-object p2

    invoke-virtual {p2}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    invoke-virtual {p2}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object p2

    const/high16 p3, 0x42a00000    # 80.0f

    .line 799
    invoke-static {v0, p3, p2}, Landroid/util/TypedValue;->applyDimension(IFLandroid/util/DisplayMetrics;)F

    move-result p2

    float-to-int p2, p2

    .line 800
    iget p3, p1, Landroid/graphics/Rect;->left:I

    iget p4, p1, Landroid/graphics/Rect;->top:I

    iget v0, p1, Landroid/graphics/Rect;->right:I

    invoke-virtual {p1, p3, p4, v0, p2}, Landroid/graphics/Rect;->set(IIII)V

    :cond_1
    return-void
.end method
