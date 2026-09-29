.class public interface abstract Lcom/helpshift/conversation/activeconversation/ConversationalRenderer;
.super Ljava/lang/Object;
.source "ConversationalRenderer.java"


# virtual methods
.method public abstract appendMessages(II)V
.end method

.method public abstract destroy()V
.end method

.method public abstract disableSendReplyButton()V
.end method

.method public abstract enableSendReplyButton()V
.end method

.method public abstract getReply()Ljava/lang/String;
.end method

.method public abstract hideAgentTypingIndicator()V
.end method

.method public abstract hideKeyboard()V
.end method

.method public abstract hideListPicker(Z)V
.end method

.method public abstract hideNetworkErrorFooter()V
.end method

.method public abstract hidePickerClearButton()V
.end method

.method public abstract hideReplyValidationFailedError()V
.end method

.method public abstract hideSendReplyUI()V
.end method

.method public abstract hideSkipButton()V
.end method

.method public abstract initializeMessages(Ljava/util/List;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract isReplyBoxVisible()Z
.end method

.method public abstract launchAttachment(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract launchScreenshotAttachment(Ljava/lang/String;Ljava/lang/String;)V
.end method

.method public abstract notifyRefreshList()V
.end method

.method public abstract onAuthenticationFailure()V
.end method

.method public abstract onBackPressed()Z
.end method

.method public abstract onFocusChanged(Z)V
.end method

.method public abstract openAppReviewStore(Ljava/lang/String;)V
.end method

.method public abstract openFreshConversationScreen(Ljava/util/Map;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Boolean;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract removeMessages(II)V
.end method

.method public abstract requestReplyFieldFocus()V
.end method

.method public abstract scrollToBottom()V
.end method

.method public abstract setReply(Ljava/lang/String;)V
.end method

.method public abstract setReplyboxListeners()V
.end method

.method public abstract showAgentTypingIndicator()V
.end method

.method public abstract showCSATSubmittedView()V
.end method

.method public abstract showEmptyListPickerView()V
.end method

.method public abstract showErrorView(Lcom/helpshift/common/exception/ExceptionType;)V
.end method

.method public abstract showKeyboard()V
.end method

.method public abstract showListPicker(Ljava/util/List;Ljava/lang/String;ZLjava/lang/String;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/OptionUIModel;",
            ">;",
            "Ljava/lang/String;",
            "Z",
            "Ljava/lang/String;",
            ")V"
        }
    .end annotation
.end method

.method public abstract showNetworkErrorFooter(I)V
.end method

.method public abstract showOptionInput(Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;)V
.end method

.method public abstract showPickerClearButton()V
.end method

.method public abstract showReplyValidationFailedError(I)V
.end method

.method public abstract showSkipButton()V
.end method

.method public abstract updateConversationResolutionQuestionUI(Z)V
.end method

.method public abstract updateImageAttachmentButtonView(Z)V
.end method

.method public abstract updateListPickerOptions(Ljava/util/List;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/viewmodel/OptionUIModel;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract updateMessages(II)V
.end method

.method public abstract updateSendReplyButton(Z)V
.end method

.method public abstract updateSendReplyUI(ZLcom/helpshift/conversation/activeconversation/message/input/Input;)V
.end method
