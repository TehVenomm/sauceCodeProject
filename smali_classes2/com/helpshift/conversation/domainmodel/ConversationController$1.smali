.class Lcom/helpshift/conversation/domainmodel/ConversationController$1;
.super Lcom/helpshift/common/domain/F;
.source "ConversationController.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/domainmodel/ConversationController;->getPoller()Lcom/helpshift/common/domain/Poller;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/domainmodel/ConversationController;)V
    .locals 0

    .line 192
    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$1;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public declared-synchronized f()V
    .locals 1

    monitor-enter p0

    .line 195
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$1;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v0}, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdates()Lcom/helpshift/conversation/dto/ConversationInbox;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 196
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    .line 194
    monitor-exit p0

    throw v0
.end method
