.class public interface abstract Lcom/helpshift/conversation/loaders/ConversationsLoader$LoadMoreConversationsCallback;
.super Ljava/lang/Object;
.source "ConversationsLoader.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/loaders/ConversationsLoader;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "LoadMoreConversationsCallback"
.end annotation


# virtual methods
.method public abstract loading()V
.end method

.method public abstract onError()V
.end method

.method public abstract onSuccess(Ljava/util/List;Z)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;Z)V"
        }
    .end annotation
.end method
