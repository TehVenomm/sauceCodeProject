.class public Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "SystemRedactedConversationDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "ViewHolder"
.end annotation


# instance fields
.field private redactedTextView:Landroid/widget/TextView;

.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder;


# direct methods
.method public constructor <init>(Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder;Landroid/view/View;)V
    .locals 0

    .line 48
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder;

    .line 49
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 50
    sget p1, Lcom/helpshift/R$id;->conversation_redacted_view:I

    invoke-virtual {p2, p1}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;->redactedTextView:Landroid/widget/TextView;

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;)Landroid/widget/TextView;
    .locals 0

    .line 45
    iget-object p0, p0, Lcom/helpshift/support/conversations/messages/SystemRedactedConversationDataBinder$ViewHolder;->redactedTextView:Landroid/widget/TextView;

    return-object p0
.end method
