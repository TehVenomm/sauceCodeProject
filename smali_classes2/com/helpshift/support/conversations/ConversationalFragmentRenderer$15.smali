.class Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$15;
.super Ljava/lang/Object;
.source "ConversationalFragmentRenderer.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->renderForTextInput(Lcom/helpshift/conversation/activeconversation/message/input/TextInput;)V
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

    .line 882
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$15;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 0

    .line 885
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer$15;->this$0:Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;

    invoke-virtual {p1}, Lcom/helpshift/support/conversations/ConversationalFragmentRenderer;->createDatePickerForReplyField()Landroid/app/DatePickerDialog;

    move-result-object p1

    .line 886
    invoke-virtual {p1}, Landroid/app/DatePickerDialog;->show()V

    return-void
.end method
