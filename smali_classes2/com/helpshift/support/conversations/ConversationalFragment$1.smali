.class Lcom/helpshift/support/conversations/ConversationalFragment$1;
.super Ljava/lang/Object;
.source "ConversationalFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/ConversationalFragment;->initialize(Landroid/view/View;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/ConversationalFragment;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/ConversationalFragment;)V
    .locals 0

    .line 202
    iput-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment$1;->this$0:Lcom/helpshift/support/conversations/ConversationalFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 205
    iget-object p1, p0, Lcom/helpshift/support/conversations/ConversationalFragment$1;->this$0:Lcom/helpshift/support/conversations/ConversationalFragment;

    iget-object p1, p1, Lcom/helpshift/support/conversations/ConversationalFragment;->conversationalVM:Lcom/helpshift/conversation/viewmodel/ConversationalVM;

    const/4 v0, 0x1

    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/viewmodel/ConversationalVM;->markConversationResolutionStatus(Z)V

    return-void
.end method
