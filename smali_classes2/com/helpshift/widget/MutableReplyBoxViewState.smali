.class public Lcom/helpshift/widget/MutableReplyBoxViewState;
.super Lcom/helpshift/widget/ReplyBoxViewState;
.source "MutableReplyBoxViewState.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Lcom/helpshift/widget/ReplyBoxViewState;-><init>()V

    return-void
.end method


# virtual methods
.method public setInput(Lcom/helpshift/conversation/activeconversation/message/input/Input;)V
    .locals 1

    if-eqz p1, :cond_0

    .line 17
    iget-object v0, p0, Lcom/helpshift/widget/MutableReplyBoxViewState;->input:Lcom/helpshift/conversation/activeconversation/message/input/Input;

    invoke-virtual {p1, v0}, Lcom/helpshift/conversation/activeconversation/message/input/Input;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    :cond_0
    const/4 v0, 0x1

    .line 18
    iput-boolean v0, p0, Lcom/helpshift/widget/MutableReplyBoxViewState;->isVisible:Z

    .line 19
    iput-object p1, p0, Lcom/helpshift/widget/MutableReplyBoxViewState;->input:Lcom/helpshift/conversation/activeconversation/message/input/Input;

    .line 20
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/MutableReplyBoxViewState;->notifyChange(Ljava/lang/Object;)V

    :cond_1
    return-void
.end method

.method public setStandardTextInput()V
    .locals 1

    .line 28
    iget-object v0, p0, Lcom/helpshift/widget/MutableReplyBoxViewState;->input:Lcom/helpshift/conversation/activeconversation/message/input/Input;

    if-nez v0, :cond_0

    iget-boolean v0, p0, Lcom/helpshift/widget/MutableReplyBoxViewState;->isVisible:Z

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x0

    .line 32
    iput-object v0, p0, Lcom/helpshift/widget/MutableReplyBoxViewState;->input:Lcom/helpshift/conversation/activeconversation/message/input/Input;

    const/4 v0, 0x1

    .line 33
    iput-boolean v0, p0, Lcom/helpshift/widget/MutableReplyBoxViewState;->isVisible:Z

    .line 34
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/MutableReplyBoxViewState;->notifyChange(Ljava/lang/Object;)V

    return-void
.end method

.method public setVisible(Z)V
    .locals 1

    .line 45
    iget-boolean v0, p0, Lcom/helpshift/widget/MutableReplyBoxViewState;->isVisible:Z

    if-eq p1, v0, :cond_0

    .line 46
    iput-boolean p1, p0, Lcom/helpshift/widget/MutableReplyBoxViewState;->isVisible:Z

    const/4 p1, 0x0

    .line 47
    iput-object p1, p0, Lcom/helpshift/widget/MutableReplyBoxViewState;->input:Lcom/helpshift/conversation/activeconversation/message/input/Input;

    .line 48
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/MutableReplyBoxViewState;->notifyChange(Ljava/lang/Object;)V

    :cond_0
    return-void
.end method
