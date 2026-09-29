.class public Lcom/helpshift/common/platform/AndroidConversationInboxDAO;
.super Ljava/lang/Object;
.source "AndroidConversationInboxDAO.java"

# interfaces
.implements Lcom/helpshift/conversation/dao/ConversationInboxDAO;


# static fields
.field private static final KEY_NOTIFICATION_COUNT:Ljava/lang/String; = "notification_count"

.field private static final KEY_NOTIFICATION_TITLE:Ljava/lang/String; = "notification_title"

.field private static final KEY_PUSH_NOTIFICATION_DATA:Ljava/lang/String; = "push_notification_data"


# instance fields
.field private conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

.field private kvStore:Lcom/helpshift/common/platform/KVStore;


# direct methods
.method public constructor <init>(Landroid/content/Context;Lcom/helpshift/common/platform/KVStore;)V
    .locals 0

    .line 22
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 23
    invoke-static {p1}, Lcom/helpshift/common/conversation/ConversationDB;->getInstance(Landroid/content/Context;)Lcom/helpshift/common/conversation/ConversationDB;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    .line 24
    iput-object p2, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    return-void
.end method

.method private declared-synchronized getConversationInboxRecordBuilder(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;
    .locals 1

    monitor-enter p0

    .line 28
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationInboxRecord(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object v0

    if-nez v0, :cond_0

    .line 31
    new-instance v0, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    invoke-direct {v0, p1, p2}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;-><init>(J)V

    move-object p1, v0

    goto :goto_0

    .line 34
    :cond_0
    new-instance p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    invoke-direct {p1, v0}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;-><init>(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 36
    :goto_0
    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    .line 27
    monitor-exit p0

    throw p1
.end method


# virtual methods
.method public deleteUserData(J)V
    .locals 3

    const-wide/16 v0, 0x0

    cmp-long v2, p1, v0

    if-lez v2, :cond_0

    .line 238
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->deleteConversationInboxData(J)V

    :cond_0
    return-void
.end method

.method public declared-synchronized getConversationArchivalPrefillText(J)Ljava/lang/String;
    .locals 1

    monitor-enter p0

    .line 140
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationInboxRecord(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    const/4 p2, 0x0

    if-eqz p1, :cond_0

    .line 143
    iget-object p2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->archivalText:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 145
    :cond_0
    monitor-exit p0

    return-object p2

    :catchall_0
    move-exception p1

    .line 139
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized getConversationInboxTimestamp(J)Ljava/lang/String;
    .locals 1

    monitor-enter p0

    .line 123
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationInboxRecord(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    const/4 p2, 0x0

    if-eqz p1, :cond_0

    .line 126
    iget-object p2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->lastSyncTimestamp:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 128
    :cond_0
    monitor-exit p0

    return-object p2

    :catchall_0
    move-exception p1

    .line 122
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized getDescriptionDetail(J)Lcom/helpshift/conversation/dto/ConversationDetailDTO;
    .locals 4

    monitor-enter p0

    .line 50
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationInboxRecord(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    const/4 p2, 0x0

    if-eqz p1, :cond_0

    .line 53
    iget-object v0, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->description:Ljava/lang/String;

    .line 54
    iget-wide v1, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->descriptionTimeStamp:J

    .line 55
    iget p1, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->descriptionType:I

    .line 56
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_0

    .line 57
    new-instance p2, Lcom/helpshift/conversation/dto/ConversationDetailDTO;

    invoke-direct {p2, v0, v1, v2, p1}, Lcom/helpshift/conversation/dto/ConversationDetailDTO;-><init>(Ljava/lang/String;JI)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 60
    :cond_0
    monitor-exit p0

    return-object p2

    :catchall_0
    move-exception p1

    .line 49
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized getEmail(J)Ljava/lang/String;
    .locals 1

    monitor-enter p0

    .line 89
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationInboxRecord(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    const/4 p2, 0x0

    if-eqz p1, :cond_0

    .line 92
    iget-object p2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->formEmail:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 94
    :cond_0
    monitor-exit p0

    return-object p2

    :catchall_0
    move-exception p1

    .line 88
    monitor-exit p0

    throw p1
.end method

.method public getHasOlderMessages(J)Z
    .locals 1

    .line 251
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationInboxRecord(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 254
    iget-object p2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->hasOlderMessages:Ljava/lang/Boolean;

    if-eqz p2, :cond_0

    .line 255
    iget-object p1, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->hasOlderMessages:Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x1

    :goto_0
    return p1
.end method

.method public declared-synchronized getImageAttachment(J)Lcom/helpshift/conversation/dto/ImagePickerFile;
    .locals 1

    monitor-enter p0

    .line 106
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationInboxRecord(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    const/4 p2, 0x0

    if-eqz p1, :cond_0

    .line 109
    iget-object p2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->imageAttachmentDraft:Lcom/helpshift/conversation/dto/ImagePickerFile;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 111
    :cond_0
    monitor-exit p0

    return-object p2

    :catchall_0
    move-exception p1

    .line 105
    monitor-exit p0

    throw p1
.end method

.method public getLastConversationsRedactionTime(J)Ljava/lang/Long;
    .locals 1

    .line 269
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationInboxRecord(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 272
    iget-object p1, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->lastConversationsRedactionTime:Ljava/lang/Long;

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    return-object p1
.end method

.method public declared-synchronized getName(J)Ljava/lang/String;
    .locals 1

    monitor-enter p0

    .line 72
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationInboxRecord(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    const/4 p2, 0x0

    if-eqz p1, :cond_0

    .line 75
    iget-object p2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->formName:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 77
    :cond_0
    monitor-exit p0

    return-object p2

    :catchall_0
    move-exception p1

    .line 71
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized getPersistMessageBox(J)Z
    .locals 1

    monitor-enter p0

    .line 171
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationInboxRecord(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    const/4 p2, 0x0

    if-eqz p1, :cond_0

    .line 174
    iget-boolean p2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->persistMessageBox:Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 176
    :cond_0
    monitor-exit p0

    return p2

    :catchall_0
    move-exception p1

    .line 170
    monitor-exit p0

    throw p1
.end method

.method public getPushNotificationData(Ljava/lang/String;)Lcom/helpshift/conversation/dao/PushNotificationData;
    .locals 3

    .line 188
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "push_notification_data"

    invoke-interface {v0, v1}, Lcom/helpshift/common/platform/KVStore;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 189
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    const/4 v2, 0x0

    if-eqz v1, :cond_0

    return-object v2

    .line 194
    :cond_0
    :try_start_0
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, v0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 195
    invoke-virtual {v1, p1}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 196
    invoke-virtual {v1, p1}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object p1

    const-string v0, "notification_count"

    .line 197
    invoke-virtual {p1, v0}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v0

    const-string v1, "notification_title"

    .line 198
    invoke-virtual {p1, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 199
    new-instance v1, Lcom/helpshift/conversation/dao/PushNotificationData;

    invoke-direct {v1, v0, p1}, Lcom/helpshift/conversation/dao/PushNotificationData;-><init>(ILjava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    move-object v2, v1

    :catch_0
    :cond_1
    return-object v2
.end method

.method public declared-synchronized getUserReplyDraft(J)Ljava/lang/String;
    .locals 1

    monitor-enter p0

    .line 161
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationInboxRecord(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    const-string p2, ""

    if-eqz p1, :cond_0

    .line 164
    iget-object p2, p1, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;->replyText:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 166
    :cond_0
    monitor-exit p0

    return-object p2

    :catchall_0
    move-exception p1

    .line 160
    monitor-exit p0

    throw p1
.end method

.method public resetDataAfterConversationsDeletion(J)V
    .locals 0

    .line 279
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->getConversationInboxRecordBuilder(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    move-result-object p1

    const/4 p2, 0x1

    .line 282
    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setHasOlderMessages(Z)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    const/4 p2, 0x0

    .line 286
    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setLastSyncTimestamp(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 287
    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->build()Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/helpshift/common/conversation/ConversationDB;->storeConversationInboxRecord(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    return-void
.end method

.method public declared-synchronized saveConversationArchivalPrefillText(JLjava/lang/String;)V
    .locals 0

    monitor-enter p0

    .line 133
    :try_start_0
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->getConversationInboxRecordBuilder(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    move-result-object p1

    .line 134
    invoke-virtual {p1, p3}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setArchivalText(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 135
    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->build()Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/helpshift/common/conversation/ConversationDB;->storeConversationInboxRecord(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 136
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 132
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized saveConversationInboxTimestamp(JLjava/lang/String;)V
    .locals 0

    monitor-enter p0

    .line 116
    :try_start_0
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->getConversationInboxRecordBuilder(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    move-result-object p1

    .line 117
    invoke-virtual {p1, p3}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setLastSyncTimestamp(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 118
    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->build()Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/helpshift/common/conversation/ConversationDB;->storeConversationInboxRecord(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 119
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 115
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized saveDescriptionDetail(JLcom/helpshift/conversation/dto/ConversationDetailDTO;)V
    .locals 2

    monitor-enter p0

    .line 41
    :try_start_0
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->getConversationInboxRecordBuilder(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    move-result-object p1

    .line 42
    iget-object p2, p3, Lcom/helpshift/conversation/dto/ConversationDetailDTO;->title:Ljava/lang/String;

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setDescription(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 43
    iget-wide v0, p3, Lcom/helpshift/conversation/dto/ConversationDetailDTO;->timestamp:J

    invoke-virtual {p1, v0, v1}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setDescriptionTimeStamp(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 44
    iget p2, p3, Lcom/helpshift/conversation/dto/ConversationDetailDTO;->type:I

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setDescriptionType(I)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 45
    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->build()Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/helpshift/common/conversation/ConversationDB;->storeConversationInboxRecord(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 46
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 40
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized saveEmail(JLjava/lang/String;)V
    .locals 0

    monitor-enter p0

    .line 82
    :try_start_0
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->getConversationInboxRecordBuilder(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    move-result-object p1

    .line 83
    invoke-virtual {p1, p3}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setFormEmail(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 84
    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->build()Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/helpshift/common/conversation/ConversationDB;->storeConversationInboxRecord(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 85
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 81
    monitor-exit p0

    throw p1
.end method

.method public saveHasOlderMessages(JZ)V
    .locals 0

    .line 244
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->getConversationInboxRecordBuilder(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    move-result-object p1

    .line 245
    invoke-virtual {p1, p3}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setHasOlderMessages(Z)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 246
    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->build()Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/helpshift/common/conversation/ConversationDB;->storeConversationInboxRecord(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    return-void
.end method

.method public declared-synchronized saveImageAttachment(JLcom/helpshift/conversation/dto/ImagePickerFile;)V
    .locals 0

    monitor-enter p0

    .line 99
    :try_start_0
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->getConversationInboxRecordBuilder(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    move-result-object p1

    .line 100
    invoke-virtual {p1, p3}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setImageAttachmentDraft(Lcom/helpshift/conversation/dto/ImagePickerFile;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 101
    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->build()Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/helpshift/common/conversation/ConversationDB;->storeConversationInboxRecord(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 102
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 98
    monitor-exit p0

    throw p1
.end method

.method public saveLastConversationsRedactionTime(JJ)V
    .locals 0

    .line 262
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->getConversationInboxRecordBuilder(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    move-result-object p1

    .line 263
    invoke-static {p3, p4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p2

    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setLastConversationsRedactionTime(Ljava/lang/Long;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 264
    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->build()Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/helpshift/common/conversation/ConversationDB;->storeConversationInboxRecord(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    return-void
.end method

.method public declared-synchronized saveName(JLjava/lang/String;)V
    .locals 0

    monitor-enter p0

    .line 65
    :try_start_0
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->getConversationInboxRecordBuilder(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    move-result-object p1

    .line 66
    invoke-virtual {p1, p3}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setFormName(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 67
    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->build()Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/helpshift/common/conversation/ConversationDB;->storeConversationInboxRecord(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 68
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 64
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized savePersistMessageBox(JZ)V
    .locals 0

    monitor-enter p0

    .line 181
    :try_start_0
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->getConversationInboxRecordBuilder(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    move-result-object p1

    .line 182
    invoke-virtual {p1, p3}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setPersistMessageBox(Z)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 183
    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->build()Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/helpshift/common/conversation/ConversationDB;->storeConversationInboxRecord(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 184
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 180
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized saveUserReplyDraft(JLjava/lang/String;)V
    .locals 0

    monitor-enter p0

    if-nez p3, :cond_0

    :try_start_0
    const-string p3, ""

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    .line 154
    :cond_0
    :goto_0
    invoke-direct {p0, p1, p2}, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->getConversationInboxRecordBuilder(J)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    move-result-object p1

    .line 155
    invoke-virtual {p1, p3}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->setReplyText(Ljava/lang/String;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;

    .line 156
    iget-object p2, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1}, Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord$Builder;->build()Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;

    move-result-object p1

    invoke-virtual {p2, p1}, Lcom/helpshift/common/conversation/ConversationDB;->storeConversationInboxRecord(Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;)Lcom/helpshift/conversation/dto/dao/ConversationInboxRecord;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 157
    monitor-exit p0

    return-void

    .line 149
    :goto_1
    monitor-exit p0

    throw p1
.end method

.method public setPushNotificationData(Ljava/lang/String;Lcom/helpshift/conversation/dao/PushNotificationData;)V
    .locals 4

    .line 212
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string v1, "push_notification_data"

    invoke-interface {v0, v1}, Lcom/helpshift/common/platform/KVStore;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 213
    invoke-static {v0}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    const-string v0, "{}"

    .line 218
    :cond_0
    :try_start_0
    new-instance v1, Lorg/json/JSONObject;

    invoke-direct {v1, v0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    if-nez p2, :cond_1

    .line 220
    invoke-virtual {v1, p1}, Lorg/json/JSONObject;->remove(Ljava/lang/String;)Ljava/lang/Object;

    goto :goto_0

    .line 223
    :cond_1
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0}, Lorg/json/JSONObject;-><init>()V

    const-string v2, "notification_count"

    .line 224
    iget v3, p2, Lcom/helpshift/conversation/dao/PushNotificationData;->count:I

    invoke-virtual {v0, v2, v3}, Lorg/json/JSONObject;->put(Ljava/lang/String;I)Lorg/json/JSONObject;

    const-string v2, "notification_title"

    .line 225
    iget-object p2, p2, Lcom/helpshift/conversation/dao/PushNotificationData;->title:Ljava/lang/String;

    invoke-virtual {v0, v2, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 226
    invoke-virtual {v1, p1, v0}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 228
    :goto_0
    iget-object p1, p0, Lcom/helpshift/common/platform/AndroidConversationInboxDAO;->kvStore:Lcom/helpshift/common/platform/KVStore;

    const-string p2, "push_notification_data"

    invoke-virtual {v1}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-interface {p1, p2, v0}, Lcom/helpshift/common/platform/KVStore;->setString(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method
