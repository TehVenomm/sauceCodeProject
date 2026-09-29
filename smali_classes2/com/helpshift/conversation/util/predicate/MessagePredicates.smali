.class public Lcom/helpshift/conversation/util/predicate/MessagePredicates;
.super Ljava/lang/Object;
.source "MessagePredicates.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 9
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static olderThanLastDbMessagePredicate(J)Lcom/helpshift/util/Predicate;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(J)",
            "Lcom/helpshift/util/Predicate<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 11
    new-instance v0, Lcom/helpshift/conversation/util/predicate/MessagePredicates$1;

    invoke-direct {v0, p0, p1}, Lcom/helpshift/conversation/util/predicate/MessagePredicates$1;-><init>(J)V

    return-object v0
.end method
