.class public interface abstract Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;
.super Ljava/lang/Object;
.source "ConversationController.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/domainmodel/ConversationController;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x609
    name = "StartNewConversationListener"
.end annotation


# virtual methods
.method public abstract onCreateConversationFailure(Ljava/lang/Exception;)V
.end method

.method public abstract onCreateConversationSuccess(J)V
.end method
