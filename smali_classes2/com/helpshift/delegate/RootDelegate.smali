.class public interface abstract Lcom/helpshift/delegate/RootDelegate;
.super Ljava/lang/Object;
.source "RootDelegate.java"


# virtual methods
.method public abstract authenticationFailed(Lcom/helpshift/HelpshiftUser;Lcom/helpshift/delegate/AuthenticationFailureReason;)V
.end method

.method public abstract conversationEnded()V
.end method

.method public abstract didReceiveNotification(I)V
.end method

.method public abstract displayAttachmentFile(Ljava/io/File;)V
.end method

.method public abstract newConversationStarted(Ljava/lang/String;)V
.end method

.method public abstract sessionBegan()V
.end method

.method public abstract sessionEnded()V
.end method

.method public abstract userCompletedCustomerSatisfactionSurvey(ILjava/lang/String;)V
.end method

.method public abstract userRepliedToConversation(Ljava/lang/String;)V
.end method
