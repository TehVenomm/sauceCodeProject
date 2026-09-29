.class Lcom/helpshift/conversation/domainmodel/ConversationController$4;
.super Lcom/helpshift/common/domain/F;
.source "ConversationController.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/domainmodel/ConversationController;->showNotificationOnUI(Ljava/lang/Long;Ljava/lang/String;ILjava/lang/String;Z)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

.field final synthetic val$conversationLocalId:Ljava/lang/Long;

.field final synthetic val$conversationLocalUUID:Ljava/lang/String;

.field final synthetic val$isInApp:Z

.field final synthetic val$messageCount:I

.field final synthetic val$title:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/domainmodel/ConversationController;Ljava/lang/Long;Ljava/lang/String;ILjava/lang/String;Z)V
    .locals 0

    .line 1000
    iput-object p1, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$4;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iput-object p2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$4;->val$conversationLocalId:Ljava/lang/Long;

    iput-object p3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$4;->val$conversationLocalUUID:Ljava/lang/String;

    iput p4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$4;->val$messageCount:I

    iput-object p5, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$4;->val$title:Ljava/lang/String;

    iput-boolean p6, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$4;->val$isInApp:Z

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 7

    .line 1003
    iget-object v0, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$4;->this$0:Lcom/helpshift/conversation/domainmodel/ConversationController;

    iget-object v1, v0, Lcom/helpshift/conversation/domainmodel/ConversationController;->platform:Lcom/helpshift/common/platform/Platform;

    iget-object v2, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$4;->val$conversationLocalId:Ljava/lang/Long;

    iget-object v3, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$4;->val$conversationLocalUUID:Ljava/lang/String;

    iget v4, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$4;->val$messageCount:I

    iget-object v5, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$4;->val$title:Ljava/lang/String;

    iget-boolean v6, p0, Lcom/helpshift/conversation/domainmodel/ConversationController$4;->val$isInApp:Z

    invoke-interface/range {v1 .. v6}, Lcom/helpshift/common/platform/Platform;->showNotification(Ljava/lang/Long;Ljava/lang/String;ILjava/lang/String;Z)V

    return-void
.end method
