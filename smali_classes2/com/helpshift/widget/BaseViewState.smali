.class public Lcom/helpshift/widget/BaseViewState;
.super Lcom/helpshift/widget/HSBaseObservable;
.source "BaseViewState.java"


# instance fields
.field protected isEnabled:Z

.field protected isVisible:Z


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 7
    invoke-direct {p0}, Lcom/helpshift/widget/HSBaseObservable;-><init>()V

    const/4 v0, 0x1

    .line 9
    iput-boolean v0, p0, Lcom/helpshift/widget/BaseViewState;->isEnabled:Z

    .line 10
    iput-boolean v0, p0, Lcom/helpshift/widget/BaseViewState;->isVisible:Z

    return-void
.end method


# virtual methods
.method public isEnabled()Z
    .locals 1

    .line 17
    iget-boolean v0, p0, Lcom/helpshift/widget/BaseViewState;->isEnabled:Z

    return v0
.end method

.method public isVisible()Z
    .locals 1

    .line 13
    iget-boolean v0, p0, Lcom/helpshift/widget/BaseViewState;->isVisible:Z

    return v0
.end method

.method protected notifyInitialState()V
    .locals 0

    .line 22
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/BaseViewState;->notifyChange(Ljava/lang/Object;)V

    return-void
.end method
