.class Lcom/helpshift/conversation/domainmodel/ConversationController$2;
.super Lcom/helpshift/common/domain/F;
.source "ConversationController.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/domainmodel/ConversationController;->createPreIssue(Lcom/helpshift/conversation/activeconversation/ViewableConversation;Ljava/lang/String;Ljava/lang/String;Lcom/helpshift/conversation/domainmodel/ConversationController$StartNewConversationListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

.field final synthetic val$activeConversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

.field final synthetic val$createPreIssue:Lcom/helpshift/common/domain/One;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/common/domain/One;Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 0

    .line 511
    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$2;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iput-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$2;->val$createPreIssue:Lcom/helpshift/common/domain/One;

    iput-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$2;->val$activeConversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 3

    .line 518
    :try_start_0
    sget-object v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->fetchConversationUpdatesLock:Ljava/lang/Object;

    monitor-enter v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 519
    :try_start_1
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$2;->val$createPreIssue:Lcom/helpshift/common/domain/One;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/One;->f()V

    .line 520
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 524
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$2;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v0, v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->inProgressPreIssueCreators:Ljava/util/HashMap;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$2;->val$activeConversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v0, v1}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    return-void

    :catchall_0
    move-exception v1

    .line 520
    :try_start_2
    monitor-exit v0
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    :try_start_3
    throw v1
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    :catchall_1
    move-exception v0

    .line 524
    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$2;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v1, v1, Lcom/helpshift/conversation/domainmodel/ConversationController;->inProgressPreIssueCreators:Ljava/util/HashMap;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$2;->val$activeConversation:Lcom/helpshift/conversation/activeconversation/model/Conversation;

    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-virtual {v1, v2}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 525
    throw v0
.end method
