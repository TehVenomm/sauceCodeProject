.class public Lcom/helpshift/conversation/util/predicate/ConversationPredicates;
.super Ljava/lang/Object;
.source "ConversationPredicates.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 10
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static allMessagesAfterLastMessageInDbPredicate(Lcom/helpshift/conversation/activeconversation/ConversationManager;)Lcom/helpshift/util/Predicate;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/ConversationManager;",
            ")",
            "Lcom/helpshift/util/Predicate<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    .line 31
    new-instance v0, Lcom/helpshift/conversation/util/predicate/ConversationPredicates$3;

    invoke-direct {v0, p0}, Lcom/helpshift/conversation/util/predicate/ConversationPredicates$3;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;)V

    return-object v0
.end method

.method public static newInProgressConversationPredicate()Lcom/helpshift/util/Predicate;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Lcom/helpshift/util/Predicate<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    .line 22
    new-instance v0, Lcom/helpshift/conversation/util/predicate/ConversationPredicates$2;

    invoke-direct {v0}, Lcom/helpshift/conversation/util/predicate/ConversationPredicates$2;-><init>()V

    return-object v0
.end method

.method public static newSyncedConversationPredicate(Lcom/helpshift/conversation/activeconversation/ConversationManager;)Lcom/helpshift/util/Predicate;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/conversation/activeconversation/ConversationManager;",
            ")",
            "Lcom/helpshift/util/Predicate<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    .line 13
    new-instance v0, Lcom/helpshift/conversation/util/predicate/ConversationPredicates$1;

    invoke-direct {v0, p0}, Lcom/helpshift/conversation/util/predicate/ConversationPredicates$1;-><init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;)V

    return-object v0
.end method
