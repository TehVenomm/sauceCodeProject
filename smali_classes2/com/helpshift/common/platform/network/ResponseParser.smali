.class public interface abstract Lcom/helpshift/common/platform/network/ResponseParser;
.super Ljava/lang/Object;
.source "ResponseParser.java"


# virtual methods
.method public abstract parseAcceptedAppReviewMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/AcceptedAppReviewMessageDM;
.end method

.method public abstract parseAuthToken(Ljava/lang/String;)Lcom/helpshift/auth/dto/WebSocketAuthData;
.end method

.method public abstract parseBotControlMessage(Ljava/lang/String;Z)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
.end method

.method public abstract parseConfigResponse(Ljava/lang/String;)Lcom/helpshift/configuration/response/RootServerConfig;
.end method

.method public abstract parseConfirmationAcceptedMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/ConfirmationAcceptedMessageDM;
.end method

.method public abstract parseConfirmationRejectedMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/ConfirmationRejectedMessageDM;
.end method

.method public abstract parseConversationHistory(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ConversationHistory;
.end method

.method public abstract parseConversationInbox(Ljava/lang/String;)Lcom/helpshift/conversation/dto/ConversationInbox;
.end method

.method public abstract parseFollowupAcceptedMessage(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/FollowupAcceptedMessageDM;
.end method

.method public abstract parseFollowupRejectedMessage(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/FollowupRejectedMessageDM;
.end method

.method public abstract parseReadableConversation(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
.end method

.method public abstract parseReadableUserMessage(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UserMessageDM;
.end method

.method public abstract parseResponseMessageForOptionInput(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForOptionInput;
.end method

.method public abstract parseResponseMessageForTextInput(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/UserResponseMessageForTextInputDM;
.end method

.method public abstract parseScreenshotMessageDM(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/ScreenshotMessageDM;
.end method

.method public abstract parseSingleFAQ(Ljava/lang/String;)Lcom/helpshift/faq/FaqCore;
.end method

.method public abstract parseWebSocketMessage(Ljava/lang/String;)Lcom/helpshift/conversation/dto/WebSocketMessage;
.end method
