.class public Lcom/helpshift/conversation/activeconversation/message/UIViewState;
.super Ljava/lang/Object;
.source "UIViewState.java"


# instance fields
.field private isFooterVisible:Z

.field private isRoundedBackground:Z


# direct methods
.method public constructor <init>()V
    .locals 1

    const/4 v0, 0x0

    .line 11
    invoke-direct {p0, v0, v0}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;-><init>(ZZ)V

    return-void
.end method

.method public constructor <init>(ZZ)V
    .locals 0

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 15
    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible:Z

    .line 16
    iput-boolean p2, p0, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isRoundedBackground:Z

    return-void
.end method


# virtual methods
.method public equals(Ljava/lang/Object;)Z
    .locals 2

    .line 37
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/UIViewState;

    if-eqz p1, :cond_0

    .line 38
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible()Z

    move-result v0

    iget-boolean v1, p0, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible:Z

    if-ne v0, v1, :cond_0

    .line 39
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isRoundedBackground()Z

    move-result p1

    iget-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isRoundedBackground:Z

    if-ne p1, v0, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method public isFooterVisible()Z
    .locals 1

    .line 20
    iget-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible:Z

    return v0
.end method

.method public isRoundedBackground()Z
    .locals 1

    .line 24
    iget-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isRoundedBackground:Z

    return v0
.end method

.method public updateViewState(Lcom/helpshift/conversation/activeconversation/message/UIViewState;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    .line 31
    :cond_0
    iget-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible:Z

    iput-boolean v0, p0, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isFooterVisible:Z

    .line 32
    iget-boolean p1, p1, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isRoundedBackground:Z

    iput-boolean p1, p0, Lcom/helpshift/conversation/activeconversation/message/UIViewState;->isRoundedBackground:Z

    return-void
.end method
