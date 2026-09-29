.class public Lcom/helpshift/widget/HistoryLoadingViewState;
.super Lcom/helpshift/widget/HSBaseObservable;
.source "HistoryLoadingViewState.java"


# instance fields
.field protected state:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 9
    invoke-direct {p0}, Lcom/helpshift/widget/HSBaseObservable;-><init>()V

    .line 11
    sget-object v0, Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;->NONE:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    iput-object v0, p0, Lcom/helpshift/widget/HistoryLoadingViewState;->state:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    return-void
.end method


# virtual methods
.method public getState()Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;
    .locals 1

    .line 14
    iget-object v0, p0, Lcom/helpshift/widget/HistoryLoadingViewState;->state:Lcom/helpshift/conversation/activeconversation/message/HistoryLoadingState;

    return-object v0
.end method

.method protected notifyInitialState()V
    .locals 0

    .line 19
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/HistoryLoadingViewState;->notifyChange(Ljava/lang/Object;)V

    return-void
.end method
