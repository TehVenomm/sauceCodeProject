.class Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$9;
.super Ljava/lang/Object;
.source "ConversationalFragmentRenderer.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->registerListeners()V
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

    .line 629
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$9;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 632
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$9;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p1, p1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->bottomSheetBehavior:Lcom/google/android/material/bottomsheet/BottomSheetBehavior;

    const/4 v0, 0x4

    invoke-virtual {p1, v0}, Lcom/google/android/material/bottomsheet/BottomSheetBehavior;->setState(I)V

    .line 633
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$9;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-virtual {p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideKeyboard()V

    return-void
.end method
