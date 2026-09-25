.class public Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;
.super Ljava/lang/Object;
.source "HistoryLoadingViewBinder.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;,
        Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$HistoryLoadingClickListener;
    }
.end annotation


# instance fields
.field private context:Landroid/content/Context;

.field private historyLoadingClickListener:Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$HistoryLoadingClickListener;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 17
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 18
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;->context:Landroid/content/Context;

    return-void
.end method

.method static synthetic access$300(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;)Landroid/content/Context;
    .locals 0

    .line 13
    iget-object p0, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;->context:Landroid/content/Context;

    return-object p0
.end method

.method static synthetic access$400(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;)Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$HistoryLoadingClickListener;
    .locals 0

    .line 13
    iget-object p0, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;->historyLoadingClickListener:Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$HistoryLoadingClickListener;

    return-object p0
.end method


# virtual methods
.method public bind(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;)V
    .locals 5

    .line 33
    sget-object v0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$1;->$SwitchMap$com$helpshift$conversation$activeconversation$message$HistoryLoadingState:[I

    invoke-virtual {p2}, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->ordinal()I

    move-result p2

    aget p2, v0, p2

    const/4 v0, 0x1

    const/4 v1, 0x0

    packed-switch p2, :pswitch_data_0

    const/4 p2, 0x0

    :goto_0
    const/4 v2, 0x0

    goto :goto_1

    :pswitch_0
    const/4 p2, 0x0

    const/4 v2, 0x1

    goto :goto_1

    :pswitch_1
    const/4 p2, 0x1

    goto :goto_0

    :pswitch_2
    const/4 p2, 0x0

    const/4 v0, 0x0

    goto :goto_0

    .line 45
    :goto_1
    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->access$000(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;)Landroid/view/View;

    move-result-object v3

    const/16 v4, 0x8

    if-eqz v0, :cond_0

    const/4 v0, 0x0

    goto :goto_2

    :cond_0
    const/16 v0, 0x8

    :goto_2
    invoke-virtual {v3, v0}, Landroid/view/View;->setVisibility(I)V

    .line 46
    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->access$100(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;)Landroid/view/View;

    move-result-object v0

    if-eqz p2, :cond_1

    const/4 p2, 0x0

    goto :goto_3

    :cond_1
    const/16 p2, 0x8

    :goto_3
    invoke-virtual {v0, p2}, Landroid/view/View;->setVisibility(I)V

    .line 47
    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->access$200(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;)Landroid/view/View;

    move-result-object p1

    if-eqz v2, :cond_2

    goto :goto_4

    :cond_2
    const/16 v1, 0x8

    :goto_4
    invoke-virtual {p1, v1}, Landroid/view/View;->setVisibility(I)V

    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public createViewHolder(Landroid/view/ViewGroup;)Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;
    .locals 3

    .line 22
    invoke-virtual {p1}, Landroid/view/ViewGroup;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Landroid/view/LayoutInflater;->from(Landroid/content/Context;)Landroid/view/LayoutInflater;

    move-result-object v0

    sget v1, Lcom/helpshift/R$layout;->hs__history_loading_view_layout:I

    const/4 v2, 0x0

    .line 23
    invoke-virtual {v0, v1, p1, v2}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 24
    new-instance v0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;-><init>(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;Landroid/view/View;)V

    return-object v0
.end method

.method public setHistoryLoadingClickListener(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$HistoryLoadingClickListener;)V
    .locals 0

    .line 51
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;->historyLoadingClickListener:Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$HistoryLoadingClickListener;

    return-void
.end method
