.class Lcom/helpshift/conversation/activeconversation/ConversationManager$6;
.super Lcom/helpshift/common/domain/F;
.source "ConversationManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/activeconversation/ConversationManager;->clearRedactedAttachmentsResources(Ljava/util/List;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

.field final synthetic val$redactedAttachments:Ljava/util/List;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Ljava/util/List;)V
    .locals 0

    .line 636
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$6;->this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$6;->val$redactedAttachments:Ljava/util/List;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 639
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$6;->val$redactedAttachments:Ljava/util/List;

    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_0
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_1

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;

    .line 641
    :try_start_0
    iget-object v2, v1, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->filePath:Ljava/lang/String;

    invoke-static {v2}, Lcom/helpshift/common/util/FileUtil;->deleteFile(Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    const/4 v2, 0x0

    .line 642
    iput-object v2, v1, Lcom/helpshift/conversation/activeconversation/message/AttachmentMessageDM;->filePath:Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v1

    const-string v2, "Helpshift_ConvManager"

    const-string v3, "Exception while deleting redacted AttachmentMessageDM file"

    .line 646
    invoke-static {v2, v3, v1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0

    :cond_1
    return-void
.end method
