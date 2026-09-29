.class Lcom/helpshift/common/platform/AndroidConversationDAO;
.super Ljava/lang/Object;
.source "AndroidConversationDAO.java"

# interfaces
.implements Lcom/helpshift/conversation/dao/ConversationDAO;
.implements Lcom/helpshift/conversation/dao/FAQSuggestionsDAO;


# instance fields
.field private final TAG:Ljava/lang/String;

.field private final conversationDB:Lcom/helpshift/common/conversation/ConversationDB;


# direct methods
.method constructor <init>(Landroid/content/Context;)V
    .locals 1

    .line 26
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "Helpshift_CnDAO"

    .line 24
    iput-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->TAG:Ljava/lang/String;

    .line 27
    invoke-static {p1}, Lcom/helpshift/common/conversation/ConversationDB;->getInstance(Landroid/content/Context;)Lcom/helpshift/common/conversation/ConversationDB;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    return-void
.end method


# virtual methods
.method public declared-synchronized deleteConversation(J)V
    .locals 3

    monitor-enter p0

    const-wide/16 v0, 0x0

    cmp-long v2, p1, v0

    if-eqz v2, :cond_0

    .line 63
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->deleteConversationWithLocalId(J)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    .line 61
    monitor-exit p0

    throw p1

    .line 65
    :cond_0
    :goto_0
    monitor-exit p0

    return-void
.end method

.method public deleteConversations(J)V
    .locals 3

    const-wide/16 v0, 0x0

    cmp-long v2, p1, v0

    if-lez v2, :cond_0

    .line 309
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->deleteConversations(J)V

    :cond_0
    return-void
.end method

.method public deleteMessagesForConversation(J)Z
    .locals 1

    .line 325
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->deleteMessagesForConversation(J)Z

    move-result p1

    return p1
.end method

.method public dropAndCreateDatabase()V
    .locals 1

    .line 32
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0}, Lcom/helpshift/common/conversation/ConversationDB;->dropAndCreateDatabase()V

    return-void
.end method

.method public getFAQ(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/Object;
    .locals 1

    .line 330
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->getAdminFAQSuggestion(Ljava/lang/String;Ljava/lang/String;)Lcom/helpshift/support/Faq;

    move-result-object p1

    return-object p1
.end method

.method public declared-synchronized getMessagesCountForConversations(Ljava/util/List;)Ljava/util/Map;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/Long;",
            ">;)",
            "Ljava/util/Map<",
            "Ljava/lang/Long;",
            "Ljava/lang/Integer;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 85
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Lcom/helpshift/common/conversation/ConversationDB;->getMessagesCountForConversations(Ljava/util/List;[Ljava/lang/String;)Ljava/util/Map;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized getMessagesCountForConversations(Ljava/util/List;[Ljava/lang/String;)Ljava/util/Map;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/Long;",
            ">;[",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/Map<",
            "Ljava/lang/Long;",
            "Ljava/lang/Integer;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 91
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->getMessagesCountForConversations(Ljava/util/List;[Ljava/lang/String;)Ljava/util/Map;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public getOldestConversationCreatedAtTime(J)Ljava/lang/Long;
    .locals 1

    .line 320
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->getOldestConversationEpochCreatedAtTime(J)Ljava/lang/Long;

    move-result-object p1

    return-object p1
.end method

.method public getOldestMessageCursor(J)Ljava/lang/String;
    .locals 1

    .line 315
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->getOldestMessageCursor(J)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method public insertConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 5

    .line 197
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    .line 198
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    if-nez v0, :cond_0

    if-nez v1, :cond_0

    return-void

    .line 203
    :cond_0
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    if-nez v0, :cond_1

    .line 204
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    .line 207
    :cond_1
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->insertConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)J

    move-result-wide v0

    const-wide/16 v2, -0x1

    cmp-long v4, v0, v2

    if-eqz v4, :cond_2

    .line 209
    invoke-virtual {p1, v0, v1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setLocalId(J)V

    .line 212
    :cond_2
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p0, p1}, Lcom/helpshift/common/platform/AndroidConversationDAO;->insertOrUpdateMessages(Ljava/util/List;)V

    return-void
.end method

.method public insertConversations(Ljava/util/List;)V
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)V"
        }
    .end annotation

    .line 243
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 247
    :cond_0
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 248
    iget-object v2, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    if-nez v2, :cond_1

    .line 249
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v2

    invoke-virtual {v2}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v2

    iput-object v2, v1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    goto :goto_0

    .line 253
    :cond_2
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->insertConversations(Ljava/util/List;)Ljava/util/List;

    move-result-object v0

    .line 254
    new-instance v1, Ljava/util/HashSet;

    invoke-direct {v1}, Ljava/util/HashSet;-><init>()V

    const/4 v2, 0x0

    .line 255
    :goto_1
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v3

    if-ge v2, v3, :cond_4

    .line 256
    invoke-interface {v0, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/Long;

    invoke-virtual {v3}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    .line 257
    invoke-interface {p1, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    const-wide/16 v6, -0x1

    cmp-long v8, v3, v6

    if-nez v8, :cond_3

    .line 259
    invoke-interface {v1, v5}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    goto :goto_2

    .line 262
    :cond_3
    invoke-virtual {v5, v3, v4}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setLocalId(J)V

    :goto_2
    add-int/lit8 v2, v2, 0x1

    goto :goto_1

    .line 266
    :cond_4
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 267
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_5
    :goto_3
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_6

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 268
    invoke-interface {v1, v2}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v3

    if-nez v3, :cond_5

    .line 269
    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-interface {v0, v2}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    goto :goto_3

    .line 272
    :cond_6
    invoke-virtual {p0, v0}, Lcom/helpshift/common/platform/AndroidConversationDAO;->insertOrUpdateMessages(Ljava/util/List;)V

    return-void
.end method

.method public insertOrUpdateFAQ(Ljava/lang/Object;)V
    .locals 1

    .line 335
    check-cast p1, Lcom/helpshift/support/Faq;

    .line 336
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->insertOrUpdateAdminFAQSuggestion(Lcom/helpshift/support/Faq;)V

    return-void
.end method

.method public declared-synchronized insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 5

    monitor-enter p0

    .line 116
    :try_start_0
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    .line 117
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    const-wide/16 v2, -0x1

    if-nez v0, :cond_0

    if-nez v1, :cond_0

    .line 119
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->insertMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)J

    move-result-wide v0

    cmp-long v4, v0, v2

    if-eqz v4, :cond_4

    .line 121
    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    goto :goto_0

    :cond_0
    if-nez v0, :cond_2

    if-eqz v1, :cond_2

    .line 125
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->readMessageWithServerId(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v0

    if-nez v0, :cond_1

    .line 127
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->insertMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)J

    move-result-wide v0

    cmp-long v4, v0, v2

    if-eqz v4, :cond_4

    .line 129
    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    goto :goto_0

    .line 133
    :cond_1
    iget-object v0, v0, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    .line 134
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->updateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    goto :goto_0

    .line 138
    :cond_2
    iget-object v1, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v1, v0}, Lcom/helpshift/common/conversation/ConversationDB;->readMessageWithLocalId(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v0

    if-nez v0, :cond_3

    .line 140
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->insertMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)J

    move-result-wide v0

    cmp-long v4, v0, v2

    if-eqz v4, :cond_4

    .line 142
    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    goto :goto_0

    .line 146
    :cond_3
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->updateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 149
    :cond_4
    :goto_0
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 115
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized insertOrUpdateMessages(Ljava/util/List;)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation

    monitor-enter p0

    .line 153
    :try_start_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v0, :cond_0

    .line 154
    monitor-exit p0

    return-void

    .line 156
    :cond_0
    :try_start_1
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 157
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 158
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_5

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    .line 159
    iget-object v3, v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    .line 160
    iget-object v4, v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->serverId:Ljava/lang/String;

    if-nez v3, :cond_1

    if-nez v4, :cond_1

    .line 162
    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    if-nez v3, :cond_3

    if-eqz v4, :cond_3

    .line 165
    iget-object v3, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v3, v4}, Lcom/helpshift/common/conversation/ConversationDB;->readMessageWithServerId(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v3

    if-nez v3, :cond_2

    .line 167
    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 170
    :cond_2
    iget-object v3, v3, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    iput-object v3, v2, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    .line 171
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 175
    :cond_3
    iget-object v4, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v4, v3}, Lcom/helpshift/common/conversation/ConversationDB;->readMessageWithLocalId(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object v3

    if-nez v3, :cond_4

    .line 177
    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 180
    :cond_4
    invoke-interface {v1, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 184
    :cond_5
    iget-object p1, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1, v0}, Lcom/helpshift/common/conversation/ConversationDB;->insertMessages(Ljava/util/List;)Ljava/util/List;

    move-result-object p1

    const/4 v2, 0x0

    .line 185
    :goto_1
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v3

    if-ge v2, v3, :cond_7

    .line 186
    invoke-interface {p1, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/Long;

    invoke-virtual {v3}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    const-wide/16 v5, -0x1

    cmp-long v7, v3, v5

    if-nez v7, :cond_6

    goto :goto_2

    .line 190
    :cond_6
    invoke-interface {v0, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    iput-object v3, v5, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    :goto_2
    add-int/lit8 v2, v2, 0x1

    goto :goto_1

    .line 192
    :cond_7
    iget-object p1, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1, v1}, Lcom/helpshift/common/conversation/ConversationDB;->updateMessages(Ljava/util/List;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 193
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 152
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized insertPreIssueConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 5

    monitor-enter p0

    .line 74
    :try_start_0
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    if-nez v0, :cond_0

    .line 75
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localUUID:Ljava/lang/String;

    .line 77
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->insertConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)J

    move-result-wide v0

    const-wide/16 v2, -0x1

    cmp-long v4, v0, v2

    if-eqz v4, :cond_1

    .line 79
    invoke-virtual {p1, v0, v1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setLocalId(J)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 81
    :cond_1
    monitor-exit p0

    return-void

    :catchall_0
    move-exception p1

    .line 73
    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized readConversation(J)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 2

    monitor-enter p0

    .line 52
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationWithLocalId(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v0, :cond_0

    const/4 p1, 0x0

    .line 54
    monitor-exit p0

    return-object p1

    .line 56
    :cond_0
    :try_start_1
    iget-object v1, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v1, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readMessages(J)Ljava/util/List;

    move-result-object p1

    invoke-virtual {v0, p1}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setMessageDMs(Ljava/util/List;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 57
    monitor-exit p0

    return-object v0

    :catchall_0
    move-exception p1

    .line 51
    monitor-exit p0

    throw p1
.end method

.method public readConversationWithoutMessages(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 1

    .line 47
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationWithLocalId(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1

    return-object p1
.end method

.method public declared-synchronized readConversationWithoutMessages(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 1

    monitor-enter p0

    .line 37
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationWithServerId(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public declared-synchronized readConversationsWithoutMessages(J)Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(J)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 69
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readConversationsWithLocalId(J)Ljava/util/List;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public readMessage(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
    .locals 1

    .line 111
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->readMessageWithServerId(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    move-result-object p1

    return-object p1
.end method

.method public declared-synchronized readMessages(J)Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(J)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    monitor-enter p0

    .line 96
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->readMessages(J)Ljava/util/List;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public readMessages(JLcom/helpshift/conversation/activeconversation/message/MessageType;)Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(J",
            "Lcom/helpshift/conversation/activeconversation/message/MessageType;",
            ")",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 101
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2, p3}, Lcom/helpshift/common/conversation/ConversationDB;->readMessages(JLcom/helpshift/conversation/activeconversation/message/MessageType;)Ljava/util/List;

    move-result-object p1

    return-object p1
.end method

.method public readMessagesForConversations(Ljava/util/List;)Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/Long;",
            ">;)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation

    .line 106
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->readMessagesForConversations(Ljava/util/Collection;)Ljava/util/List;

    move-result-object p1

    return-object p1
.end method

.method public declared-synchronized readPreConversationWithoutMessages(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
    .locals 1

    monitor-enter p0

    .line 42
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->readPreConversationWithServerId(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-object p1

    :catchall_0
    move-exception p1

    monitor-exit p0

    throw p1
.end method

.method public removeFAQ(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 341
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/common/conversation/ConversationDB;->removeAdminFAQSuggestion(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public updateConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 2

    .line 217
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->serverId:Ljava/lang/String;

    .line 218
    iget-object v1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->preConversationServerId:Ljava/lang/String;

    if-nez v0, :cond_0

    if-nez v1, :cond_0

    return-void

    .line 223
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->updateConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    .line 224
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-virtual {p0, p1}, Lcom/helpshift/common/platform/AndroidConversationDAO;->insertOrUpdateMessages(Ljava/util/List;)V

    return-void
.end method

.method public updateConversationWithoutMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
    .locals 1

    .line 238
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->updateConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V

    return-void
.end method

.method public updateConversations(Ljava/util/List;Ljava/util/Map;)V
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;",
            "Ljava/util/Map<",
            "Ljava/lang/Long;",
            "Lcom/helpshift/conversation/activeconversation/ConversationUpdate;",
            ">;)V"
        }
    .end annotation

    .line 278
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v0

    if-nez v0, :cond_0

    return-void

    .line 282
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/common/conversation/ConversationDB;->updateConversations(Ljava/util/List;)V

    .line 285
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 286
    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    .line 287
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_1
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_2

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;

    .line 288
    iget-object v3, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {p2, v3}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_1

    .line 289
    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/model/Conversation;->localId:Ljava/lang/Long;

    invoke-interface {p2, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;

    .line 290
    iget-object v3, v2, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;->newMessageDMs:Ljava/util/List;

    invoke-interface {v0, v3}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    .line 291
    iget-object v2, v2, Lcom/helpshift/conversation/activeconversation/ConversationUpdate;->updatedMessageDMs:Ljava/util/List;

    invoke-interface {v1, v2}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    goto :goto_0

    .line 296
    :cond_2
    iget-object p1, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1, v0}, Lcom/helpshift/common/conversation/ConversationDB;->insertMessages(Ljava/util/List;)Ljava/util/List;

    move-result-object p1

    const/4 p2, 0x0

    .line 297
    :goto_1
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v2

    if-ge p2, v2, :cond_4

    .line 298
    invoke-interface {p1, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/Long;

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    const-wide/16 v4, -0x1

    cmp-long v6, v2, v4

    if-nez v6, :cond_3

    goto :goto_2

    .line 302
    :cond_3
    invoke-interface {v0, p2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;

    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    iput-object v2, v4, Lcom/helpshift/conversation/activeconversation/message/MessageDM;->localId:Ljava/lang/Long;

    :goto_2
    add-int/lit8 p2, p2, 0x1

    goto :goto_1

    .line 304
    :cond_4
    iget-object p1, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {p1, v1}, Lcom/helpshift/common/conversation/ConversationDB;->updateMessages(Ljava/util/List;)V

    return-void
.end method

.method public updateLastUserActivityTimeInConversation(Ljava/lang/Long;J)V
    .locals 1

    if-nez p1, :cond_0

    const-string p1, "Helpshift_CnDAO"

    const-string p2, "Trying to update last user activity time but localId is null"

    .line 230
    invoke-static {p1, p2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    return-void

    .line 233
    :cond_0
    iget-object v0, p0, Lcom/helpshift/common/platform/AndroidConversationDAO;->conversationDB:Lcom/helpshift/common/conversation/ConversationDB;

    invoke-virtual {v0, p1, p2, p3}, Lcom/helpshift/common/conversation/ConversationDB;->updateLastUserActivityTimeInConversation(Ljava/lang/Long;J)V

    return-void
.end method
