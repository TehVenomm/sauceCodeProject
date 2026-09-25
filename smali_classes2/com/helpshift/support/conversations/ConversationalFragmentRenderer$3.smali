.class Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$3;
.super Lcom/google/android/material/bottomsheet/BottomSheetBehavior$BottomSheetCallback;
.source "ConversationalFragmentRenderer.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->initBottomSheetCallback()V
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

    .line 470
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$3;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-direct {p0}, Lcom/google/android/material/bottomsheet/BottomSheetBehavior$BottomSheetCallback;-><init>()V

    return-void
.end method


# virtual methods
.method public onSlide(Landroid/view/View;F)V
    .locals 3
    .param p1    # Landroid/view/View;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    float-to-double p1, p2

    const-wide/high16 v0, 0x3fe0000000000000L    # 0.5

    cmpl-double v2, p1, v0

    const/4 p1, 0x2

    if-lez v2, :cond_0

    .line 486
    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$3;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p2, p2, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->bottomSheetBehavior:Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    invoke-virtual {p2}, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;->getState()I

    move-result p2

    if-ne p2, p1, :cond_0

    .line 487
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$3;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-static {p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->access$100(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    goto :goto_0

    .line 489
    :cond_0
    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$3;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p2, p2, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->bottomSheetBehavior:Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    invoke-virtual {p2}, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;->getState()I

    move-result p2

    if-ne p2, p1, :cond_1

    .line 490
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$3;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-static {p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->access$000(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    :cond_1
    :goto_0
    return-void
.end method

.method public onStateChanged(Landroid/view/View;I)V
    .locals 0
    .param p1    # Landroid/view/View;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    const/4 p1, 0x4

    if-ne p1, p2, :cond_0

    .line 474
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$3;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-static {p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->access$000(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    goto :goto_0

    :cond_0
    const/4 p1, 0x3

    if-ne p1, p2, :cond_1

    .line 477
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$3;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-static {p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->access$100(Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;)V

    :cond_1
    :goto_0
    return-void
.end method
