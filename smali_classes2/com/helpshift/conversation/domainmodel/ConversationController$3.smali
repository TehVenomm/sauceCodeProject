.class Lcom/helpshift/conversation/domainmodel/ConversationController$3;
.super Lcom/helpshift/common/domain/F;
.source "ConversationController.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/domainmodel/ConversationController;->sendUnreadCountUpdate()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

.field final synthetic val$fetchConversationUpdatesListener:Lcom/helpshift/common/FetchDataFromThread;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Lcom/helpshift/common/FetchDataFromThread;)V
    .locals 0

    .line 832
    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$3;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iput-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$3;->val$fetchConversationUpdatesListener:Lcom/helpshift/common/FetchDataFromThread;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 2

    .line 835
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$3;->val$fetchConversationUpdatesListener:Lcom/helpshift/common/FetchDataFromThread;

    iget-object v1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$3;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    invoke-virtual {v1}, Lcom/helpshift/conversation/domainmodel/ConversationController;->getNotificationCountSync()I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-interface {v0, v1}, Lcom/helpshift/common/FetchDataFromThread;->onDataFetched(Ljava/lang/Object;)V

    return-void
.end method
