.class public interface abstract Lcom/helpshift/conversation/viewmodel/ConversationVMCallback;
.super Ljava/lang/Object;
.source "ConversationVMCallback.java"

# interfaces
.implements Lcom/helpshift/common/util/HSListObserver;


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Lcom/helpshift/common/util/HSListObserver<",
        "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
        ">;"
    }
.end annotation


# virtual methods
.method public abstract handleIdempotentPreIssueCreationSuccess()V
.end method

.method public abstract handlePreIssueCreationSuccess()V
.end method

.method public abstract isMessageBoxVisible()Z
.end method

.method public abstract isVisibleOnUI()Z
.end method

.method public abstract launchAttachment(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract launchScreenshotAttachment(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract onAgentTypingUpdate(Z)V
.end method

.method public abstract onConversationInboxPollFailure()V
.end method

.method public abstract onConversationInboxPollSuccess()V
.end method

.method public abstract onHistoryLoadingError()V
.end method

.method public abstract onHistoryLoadingStarted()V
.end method

.method public abstract onHistoryLoadingSuccess()V
.end method

.method public abstract onIssueStatusChange(Lcom/helpshift/conversation/dto/IssueState;)V
.end method

.method public abstract prependConversations(Ljava/util/List;Z)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;Z)V"
        }
    .end annotation
.end method
