.class public Lcom/helpshift/conversation/dto/ConversationHistory;
.super Ljava/lang/Object;
.source "ConversationHistory.java"


# instance fields
.field public final conversations:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation
.end field

.field public final hasOlderMessages:Z


# direct methods
.method public constructor <init>(Ljava/util/List;Z)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;Z)V"
        }
    .end annotation

    .line 13
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 14
    iput-object p1, p0, Lcom/helpshift/conversation/dto/ConversationHistory;->conversations:Ljava/util/List;

    .line 15
    iput-boolean p2, p0, Lcom/helpshift/conversation/dto/ConversationHistory;->hasOlderMessages:Z

    return-void
.end method
