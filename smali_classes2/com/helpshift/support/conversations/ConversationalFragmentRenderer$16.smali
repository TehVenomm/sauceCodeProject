.class Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$16;
.super Lcom/helpshift/support/conversations/TextWatcherAdapter;
.source "ConversationalFragmentRenderer.java"


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

    .line 1181
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$16;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-direct {p0}, Lcom/helpshift/support/conversations/TextWatcherAdapter;-><init>()V

    return-void
.end method


# virtual methods
.method public onTextChanged(Ljava/lang/CharSequence;III)V
    .locals 1

    .line 1184
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$16;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object v0, v0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    if-eqz v0, :cond_0

    .line 1185
    iget-object v0, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$16;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object v0, v0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    invoke-interface {v0, p1, p2, p3, p4}, Lcom/helpshift/support/conversations/ConversationalFragmentRouter;->onTextChanged(Ljava/lang/CharSequence;III)V

    :cond_0
    return-void
.end method
