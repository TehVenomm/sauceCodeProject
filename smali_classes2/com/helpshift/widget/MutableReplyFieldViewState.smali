.class public Lcom/helpshift/widget/MutableReplyFieldViewState;
.super Lcom/helpshift/widget/ReplyFieldViewState;
.source "MutableReplyFieldViewState.java"


# direct methods
.method constructor <init>()V
    .locals 0

    .line 13
    invoke-direct {p0}, Lcom/helpshift/widget/ReplyFieldViewState;-><init>()V

    return-void
.end method


# virtual methods
.method public clearReplyText()V
    .locals 1

    const-string v0, ""

    .line 20
    iput-object v0, p0, Lcom/helpshift/widget/MutableReplyFieldViewState;->replyText:Ljava/lang/String;

    .line 21
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/MutableReplyFieldViewState;->notifyChange(Ljava/lang/Object;)V

    return-void
.end method

.method public setReplyText(Ljava/lang/String;)V
    .locals 1

    if-eqz p1, :cond_0

    .line 25
    iget-object v0, p0, Lcom/helpshift/widget/MutableReplyFieldViewState;->replyText:Ljava/lang/String;

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 26
    iput-object p1, p0, Lcom/helpshift/widget/MutableReplyFieldViewState;->replyText:Ljava/lang/String;

    .line 27
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/MutableReplyFieldViewState;->notifyChange(Ljava/lang/Object;)V

    :cond_0
    return-void
.end method
