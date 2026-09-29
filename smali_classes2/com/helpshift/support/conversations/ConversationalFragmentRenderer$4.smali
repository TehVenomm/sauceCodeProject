.class Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$4;
.super Lcom/helpshift/support/conversations/TextWatcherAdapter;
.source "ConversationalFragmentRenderer.java"


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

    .line 579
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$4;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-direct {p0}, Lcom/helpshift/support/conversations/TextWatcherAdapter;-><init>()V

    return-void
.end method


# virtual methods
.method public onTextChanged(Ljava/lang/CharSequence;III)V
    .locals 0

    if-eqz p1, :cond_0

    .line 583
    iget-object p2, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$4;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    iget-object p2, p2, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->conversationalFragmentRouter:Lcom/helpshift/support/conversations/ConversationalFragmentRouter;

    invoke-interface {p1}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-interface {p2, p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRouter;->onListPickerSearchQueryChange(Ljava/lang/String;)V

    :cond_0
    return-void
.end method
