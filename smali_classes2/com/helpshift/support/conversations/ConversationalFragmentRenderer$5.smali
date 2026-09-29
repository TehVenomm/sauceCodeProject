.class Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$5;
.super Ljava/lang/Object;
.source "ConversationalFragmentRenderer.java"

# interfaces
.implements Landroid/widget/TextView$OnEditorActionListener;


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

    .line 588
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$5;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onEditorAction(Landroid/widget/TextView;ILandroid/view/KeyEvent;)Z
    .locals 0

    const/4 p1, 0x3

    if-ne p2, p1, :cond_0

    .line 592
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$5;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-virtual {p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->hideKeyboard()V

    const/4 p1, 0x1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method
