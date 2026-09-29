.class final Lcom/helpshift/conversation/util/predicate/MessagePredicates$1;
.super Ljava/lang/Object;
.source "MessagePredicates.java"

# interfaces
.implements Lcom/helpshift/util/Predicate;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/util/predicate/MessagePredicates;->olderThanLastDbMessagePredicate(J)Lcom/helpshift/util/Predicate;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Lcom/helpshift/util/Predicate<",
        "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic val$oldestMessageTimeStamp:J


# direct methods
.method constructor <init>(J)V
    .locals 0

    .line 11
    iput-wide p1, p0, Lcom/helpshift/conversation/util/predicate/MessagePredicates$1;->val$oldestMessageTimeStamp:J

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public matches(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z
    .locals 4

    .line 14
    invoke-virtual {p1}, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->getEpochCreatedAtTime()J

    move-result-wide v0

    iget-wide v2, p0, Lcom/helpshift/conversation/util/predicate/MessagePredicates$1;->val$oldestMessageTimeStamp:J

    cmp-long p1, v0, v2

    if-ltz p1, :cond_0

    const/4 p1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return p1
.end method

.method public bridge synthetic matches(Ljava/lang/Object;)Z
    .locals 0

    .line 11
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-virtual {p0, p1}, Lcom/helpshift/conversation/util/predicate/MessagePredicates$1;->matches(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)Z

    move-result p1

    return p1
.end method
