.class public interface abstract Lcom/helpshift/conversation/dao/ConversationDAO;
.super Ljava/lang/Object;
.source "ConversationDAO.java"


# virtual methods
.method public abstract deleteConversation(J)V
.end method

.method public abstract deleteConversations(J)V
.end method

.method public abstract deleteMessagesForConversation(J)Z
.end method

.method public abstract dropAndCreateDatabase()V
.end method

.method public abstract getMessagesCountForConversations(Ljava/util/List;)Ljava/util/Map;
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
.end method

.method public abstract getMessagesCountForConversations(Ljava/util/List;[Ljava/lang/String;)Ljava/util/Map;
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
.end method

.method public abstract getOldestConversationCreatedAtTime(J)Ljava/lang/Long;
.end method

.method public abstract getOldestMessageCursor(J)Ljava/lang/String;
.end method

.method public abstract insertConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
.end method

.method public abstract insertConversations(Ljava/util/List;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract insertOrUpdateMessage(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
.end method

.method public abstract insertOrUpdateMessages(Ljava/util/List;)V
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;)V"
        }
    .end annotation
.end method

.method public abstract insertPreIssueConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
.end method

.method public abstract readConversation(J)Lcom/helpshift/conversation/activeconversation/model/Conversation;
.end method

.method public abstract readConversationWithoutMessages(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
.end method

.method public abstract readConversationWithoutMessages(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
.end method

.method public abstract readConversationsWithoutMessages(J)Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(J)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation
.end method

.method public abstract readMessage(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/message/MessageDM;
.end method

.method public abstract readMessages(J)Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(J)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/MessageDM;",
            ">;"
        }
    .end annotation
.end method

.method public abstract readMessages(JLcom/helpshift/conversation/activeconversation/message/MessageType;)Ljava/util/List;
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
.end method

.method public abstract readMessagesForConversations(Ljava/util/List;)Ljava/util/List;
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
.end method

.method public abstract readPreConversationWithoutMessages(Ljava/lang/String;)Lcom/helpshift/conversation/activeconversation/model/Conversation;
.end method

.method public abstract updateConversation(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
.end method

.method public abstract updateConversationWithoutMessages(Lcom/helpshift/conversation/activeconversation/model/Conversation;)V
.end method

.method public abstract updateConversations(Ljava/util/List;Ljava/util/Map;)V
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
.end method

.method public abstract updateLastUserActivityTimeInConversation(Ljava/lang/Long;J)V
.end method
