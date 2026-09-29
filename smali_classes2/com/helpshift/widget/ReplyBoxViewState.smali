.class public Lcom/helpshift/widget/ReplyBoxViewState;
.super Lcom/helpshift/widget/BaseViewState;
.source "ReplyBoxViewState.java"


# instance fields
.field protected input:Lcom/helpshift/conversation/activeconversation/message/input/Input;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Lcom/helpshift/widget/BaseViewState;-><init>()V

    return-void
.end method


# virtual methods
.method public getInput()Lcom/helpshift/conversation/activeconversation/message/input/Input;
    .locals 1

    .line 13
    iget-object v0, p0, Lcom/helpshift/widget/ReplyBoxViewState;->input:Lcom/helpshift/conversation/activeconversation/message/input/Input;

    return-object v0
.end method
