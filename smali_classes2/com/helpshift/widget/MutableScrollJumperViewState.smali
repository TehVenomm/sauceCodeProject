.class public Lcom/helpshift/widget/MutableScrollJumperViewState;
.super Lcom/helpshift/widget/ScrollJumperViewState;
.source "MutableScrollJumperViewState.java"


# direct methods
.method constructor <init>(ZZ)V
    .locals 0

    .line 10
    invoke-direct {p0, p1, p2}, Lcom/helpshift/widget/ScrollJumperViewState;-><init>(ZZ)V

    return-void
.end method


# virtual methods
.method public setShouldShowUnreadMessagesIndicator(Z)V
    .locals 1

    .line 21
    iget-boolean v0, p0, Lcom/helpshift/widget/MutableScrollJumperViewState;->shouldShowUnreadMessagesIndicator:Z

    if-eq v0, p1, :cond_0

    .line 22
    iput-boolean p1, p0, Lcom/helpshift/widget/MutableScrollJumperViewState;->shouldShowUnreadMessagesIndicator:Z

    .line 23
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/MutableScrollJumperViewState;->notifyChange(Ljava/lang/Object;)V

    :cond_0
    return-void
.end method

.method public setVisible(Z)V
    .locals 1

    .line 14
    iget-boolean v0, p0, Lcom/helpshift/widget/MutableScrollJumperViewState;->isVisible:Z

    if-eq v0, p1, :cond_0

    .line 15
    iput-boolean p1, p0, Lcom/helpshift/widget/MutableScrollJumperViewState;->isVisible:Z

    .line 16
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/MutableScrollJumperViewState;->notifyChange(Ljava/lang/Object;)V

    :cond_0
    return-void
.end method
