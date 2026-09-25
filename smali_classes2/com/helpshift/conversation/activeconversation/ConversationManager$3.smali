.class Lcom/helpshift/conversation/activeconversation/ConversationManager$3;
.super Lcom/helpshift/common/domain/F;
.source "ConversationManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/conversation/activeconversation/ConversationManager;->sendMessageWithAutoRetry(Lcom/helpshift/common/domain/F;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

.field final synthetic val$f:Lcom/helpshift/common/domain/F;


# direct methods
.method constructor <init>(Lcom/helpshift/conversation/activeconversation/ConversationManager;Lcom/helpshift/common/domain/F;)V
    .locals 0

    .line 238
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$3;->this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$3;->val$f:Lcom/helpshift/common/domain/F;

    invoke-direct {p0}, Lcom/helpshift/common/domain/F;-><init>()V

    return-void
.end method


# virtual methods
.method public f()V
    .locals 4

    .line 242
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$3;->val$f:Lcom/helpshift/common/domain/F;

    invoke-virtual {v0}, Lcom/helpshift/common/domain/F;->f()V
    :try_end_0
    .catch Lcom/helpshift/common/exception/RootAPIException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 245
    iget-object v1, v0, Lcom/helpshift/common/exception/RootAPIException;->exceptionType:Lcom/helpshift/common/exception/ExceptionType;

    .line 246
    sget-object v2, Lcom/helpshift/common/exception/NetworkException;->NON_RETRIABLE:Lcom/helpshift/common/exception/NetworkException;

    if-eq v1, v2, :cond_1

    sget-object v2, Lcom/helpshift/common/exception/NetworkException;->USER_PRE_CONDITION_FAILED:Lcom/helpshift/common/exception/NetworkException;

    if-ne v1, v2, :cond_0

    goto :goto_0

    .line 247
    :cond_0
    iget-object v1, p0, Lcom/helpshift/conversation/activeconversation/ConversationManager$3;->this$0:Lcom/helpshift/conversation/activeconversation/ConversationManager;

    iget-object v1, v1, Lcom/helpshift/conversation/activeconversation/ConversationManager;->domain:Lcom/helpshift/common/domain/Domain;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/Domain;->getAutoRetryFailedEventDM()Lcom/helpshift/common/AutoRetryFailedEventDM;

    move-result-object v1

    sget-object v2, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->CONVERSATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    .line 248
    invoke-virtual {v0}, Lcom/helpshift/common/exception/RootAPIException;->getServerStatusCode()I

    move-result v3

    invoke-virtual {v1, v2, v3}, Lcom/helpshift/common/AutoRetryFailedEventDM;->scheduleRetryTaskForEventType(Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;I)V

    .line 249
    throw v0

    :cond_1
    :goto_0
    return-void
.end method
