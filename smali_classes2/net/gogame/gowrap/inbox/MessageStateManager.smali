.class public interface abstract Lnet/gogame/gowrap/inbox/MessageStateManager;
.super Ljava/lang/Object;
.source "MessageStateManager.java"


# virtual methods
.method public abstract deleteMessageStates(J)V
.end method

.method public abstract deleteMessageStates(Ljava/lang/String;J)V
.end method

.method public abstract getMessageStates(Ljava/lang/String;J)Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "J)",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/inbox/MessageState;",
            ">;"
        }
    .end annotation
.end method

.method public abstract setMessageState(Ljava/lang/String;JJZ)V
.end method
