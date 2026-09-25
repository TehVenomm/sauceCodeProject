.class public Lcom/helpshift/widget/MutableBaseViewState;
.super Lcom/helpshift/widget/BaseViewState;
.source "MutableBaseViewState.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Lcom/helpshift/widget/BaseViewState;-><init>()V

    return-void
.end method


# virtual methods
.method public setEnabled(Z)V
    .locals 1

    .line 11
    iget-boolean v0, p0, Lcom/helpshift/widget/MutableBaseViewState;->isEnabled:Z

    if-eq p1, v0, :cond_0

    .line 12
    iput-boolean p1, p0, Lcom/helpshift/widget/MutableBaseViewState;->isEnabled:Z

    .line 13
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/MutableBaseViewState;->notifyChange(Ljava/lang/Object;)V

    :cond_0
    return-void
.end method

.method public setVisible(Z)V
    .locals 1

    .line 18
    iget-boolean v0, p0, Lcom/helpshift/widget/MutableBaseViewState;->isVisible:Z

    if-eq p1, v0, :cond_0

    .line 19
    iput-boolean p1, p0, Lcom/helpshift/widget/MutableBaseViewState;->isVisible:Z

    .line 20
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/MutableBaseViewState;->notifyChange(Ljava/lang/Object;)V

    :cond_0
    return-void
.end method
