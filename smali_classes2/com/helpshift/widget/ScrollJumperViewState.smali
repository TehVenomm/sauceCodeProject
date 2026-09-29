.class public Lcom/helpshift/widget/ScrollJumperViewState;
.super Lcom/helpshift/widget/BaseViewState;
.source "ScrollJumperViewState.java"


# instance fields
.field protected shouldShowUnreadMessagesIndicator:Z


# direct methods
.method protected constructor <init>(ZZ)V
    .locals 0

    .line 11
    invoke-direct {p0}, Lcom/helpshift/widget/BaseViewState;-><init>()V

    .line 12
    iput-boolean p1, p0, Lcom/helpshift/widget/ScrollJumperViewState;->isVisible:Z

    .line 13
    iput-boolean p2, p0, Lcom/helpshift/widget/ScrollJumperViewState;->shouldShowUnreadMessagesIndicator:Z

    return-void
.end method


# virtual methods
.method public shouldShowUnreadMessagesIndicator()Z
    .locals 1

    .line 17
    iget-boolean v0, p0, Lcom/helpshift/widget/ScrollJumperViewState;->shouldShowUnreadMessagesIndicator:Z

    return v0
.end method
