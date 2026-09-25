.class public Lcom/helpshift/conversation/dto/ConversationInbox;
.super Ljava/lang/Object;
.source "ConversationInbox.java"


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

.field public final cursor:Ljava/lang/String;

.field public final hasOlderMessages:Ljava/lang/Boolean;

.field public final issueExists:Z


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/util/List;ZLjava/lang/Boolean;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;Z",
            "Ljava/lang/Boolean;",
            ")V"
        }
    .end annotation

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 23
    iput-object p1, p0, Lcom/helpshift/conversation/dto/ConversationInbox;->cursor:Ljava/lang/String;

    .line 24
    iput-object p2, p0, Lcom/helpshift/conversation/dto/ConversationInbox;->conversations:Ljava/util/List;

    .line 25
    iput-boolean p3, p0, Lcom/helpshift/conversation/dto/ConversationInbox;->issueExists:Z

    .line 26
    iput-object p4, p0, Lcom/helpshift/conversation/dto/ConversationInbox;->hasOlderMessages:Ljava/lang/Boolean;

    return-void
.end method
