.class public Lnet/gogame/chat/AgentTypingEntry;
.super Lnet/gogame/chat/ChatEntry;
.source "AgentTypingEntry.java"


# instance fields
.field private typing:Z


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Lnet/gogame/chat/ChatEntry;-><init>()V

    return-void
.end method

.method public constructor <init>(Z)V
    .locals 0

    .line 12
    invoke-direct {p0}, Lnet/gogame/chat/ChatEntry;-><init>()V

    .line 14
    iput-boolean p1, p0, Lnet/gogame/chat/AgentTypingEntry;->typing:Z

    return-void
.end method


# virtual methods
.method public isTyping()Z
    .locals 1

    .line 18
    iget-boolean v0, p0, Lnet/gogame/chat/AgentTypingEntry;->typing:Z

    return v0
.end method

.method public setTyping(Z)V
    .locals 0

    .line 22
    iput-boolean p1, p0, Lnet/gogame/chat/AgentTypingEntry;->typing:Z

    return-void
.end method
