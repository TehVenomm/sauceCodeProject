.class public interface abstract Lcom/helpshift/conversation/dao/ConversationInboxDAO;
.super Ljava/lang/Object;
.source "ConversationInboxDAO.java"


# virtual methods
.method public abstract deleteUserData(J)V
.end method

.method public abstract getConversationArchivalPrefillText(J)Ljava/lang/String;
.end method

.method public abstract getConversationInboxTimestamp(J)Ljava/lang/String;
.end method

.method public abstract getDescriptionDetail(J)Lcom/helpshift/conversation/dto/ConversationDetailDTO;
.end method

.method public abstract getEmail(J)Ljava/lang/String;
.end method

.method public abstract getHasOlderMessages(J)Z
.end method

.method public abstract getImageAttachment(J)Lcom/helpshift/conversation/dto/ImagePickerFile;
.end method

.method public abstract getLastConversationsRedactionTime(J)Ljava/lang/Long;
.end method

.method public abstract getName(J)Ljava/lang/String;
.end method

.method public abstract getPersistMessageBox(J)Z
.end method

.method public abstract getPushNotificationData(Ljava/lang/String;)Lcom/helpshift/conversation/dao/PushNotificationData;
.end method

.method public abstract getUserReplyDraft(J)Ljava/lang/String;
.end method

.method public abstract resetDataAfterConversationsDeletion(J)V
.end method

.method public abstract saveConversationArchivalPrefillText(JLjava/lang/String;)V
.end method

.method public abstract saveConversationInboxTimestamp(JLjava/lang/String;)V
.end method

.method public abstract saveDescriptionDetail(JLcom/helpshift/conversation/dto/ConversationDetailDTO;)V
.end method

.method public abstract saveEmail(JLjava/lang/String;)V
.end method

.method public abstract saveHasOlderMessages(JZ)V
.end method

.method public abstract saveImageAttachment(JLcom/helpshift/conversation/dto/ImagePickerFile;)V
.end method

.method public abstract saveLastConversationsRedactionTime(JJ)V
.end method

.method public abstract saveName(JLjava/lang/String;)V
.end method

.method public abstract savePersistMessageBox(JZ)V
.end method

.method public abstract saveUserReplyDraft(JLjava/lang/String;)V
.end method

.method public abstract setPushNotificationData(Ljava/lang/String;Lcom/helpshift/conversation/dao/PushNotificationData;)V
.end method
