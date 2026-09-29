.class Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$17;
.super Ljava/lang/Object;
.source "ConversationalFragmentRenderer.java"

# interfaces
.implements Landroid/widget/TextView$OnEditorActionListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->setReplyboxListeners()V
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

    .line 1190
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$17;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onEditorAction(Landroid/widget/TextView;ILandroid/view/KeyEvent;)Z
    .locals 0

    const/4 p1, 0x4

    if-ne p2, p1, :cond_0

    .line 1193
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$17;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p1, p1, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->replyButton:Landroid/widget/ImageButton;

    invoke-virtual {p1}, Landroid/widget/ImageButton;->performClick()Z

    :cond_0
    const/4 p1, 0x0

    return p1
.end method
