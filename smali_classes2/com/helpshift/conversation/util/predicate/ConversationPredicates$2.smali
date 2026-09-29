.class final Lcom/helpshift/conversation/util/predicate/ConversationPredicates$2;
.super Ljava/lang/Object;
.source "ConversationPredicates.java"

# interfaces
.implements Lcom/helpshift/util/Predicate;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/util/predicate/ConversationPredicates;->newInProgressConversationPredicate()Lcom/helpshift/util/Predicate;
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


# direct methods
.method constructor <init>()V
    .locals 0

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public matches(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z
    .locals 0

    .line 25
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->isIssueInProgress()Z

    move-result p1

    return p1
.end method

.method public bridge synthetic matches(Ljava/lang/Object;)Z
    .locals 0

    .line 22
    check-cast p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/util/predicate/ConversationPredicates$2;->matches(Lcom/helpshift/conversation/activeconversation/model/Conversation;)Z

    move-result p1

    return p1
.end method
