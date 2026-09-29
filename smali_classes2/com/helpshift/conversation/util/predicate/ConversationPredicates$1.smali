.class final Lcom/helpshift/conversation/util/predicate/ConversationPredicates$1;
.super Ljava/lang/Object;
.source "ConversationPredicates.java"

# interfaces
.implements Lcom/helpshift/util/Predicate;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/util/predicate/ConversationPredicates;->newSyncedConversationPredicate(Lcom/helpshift/conversation/activeconversation/ConversationManager;)Lcom/helpshift/util/Predicate;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Lcom/helpshift/util/Predicate<",
        "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic val$conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;)V
    .locals 0

    .line 13
    iput-object p1, p0, Lcom/helpshift/conversation/util/predicate/ConversationPredicates$1;->val$conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public matches(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z
    .locals 1

    .line 16
    iget-object v0, p0, Lcom/helpshift/conversation/util/predicate/ConversationPredicates$1;->val$conversationManager:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/activeconversation/ConversationManager;->isSynced(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result p1

    return p1
.end method

.method public bridge synthetic matches(Ljava/lang/Object;)Z
    .locals 0

    .line 13
    check-cast p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/util/predicate/ConversationPredicates$1;->matches(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result p1

    return p1
.end method
