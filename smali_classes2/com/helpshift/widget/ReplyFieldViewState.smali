.class public Lcom/helpshift/widget/ReplyFieldViewState;
.super Lcom/helpshift/widget/HSBaseObservable;
.source "ReplyFieldViewState.java"


# instance fields
.field protected isEnabled:Z

.field protected replyText:Ljava/lang/String;


# direct methods
.method protected constructor <init>()V
    .locals 1

    .line 16
    invoke-direct {p0}, Lcom/helpshift/widget/HSBaseObservable;-><init>()V

    const-string v0, ""

    .line 10
    iput-object v0, p0, Lcom/helpshift/widget/ReplyFieldViewState;->replyText:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getReplyText()Ljava/lang/String;
    .locals 1

    .line 21
    iget-object v0, p0, Lcom/helpshift/widget/ReplyFieldViewState;->replyText:Ljava/lang/String;

    return-object v0
.end method

.method protected notifyInitialState()V
    .locals 0

    .line 26
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/ReplyFieldViewState;->notifyChange(Ljava/lang/Object;)V

    return-void
.end method
