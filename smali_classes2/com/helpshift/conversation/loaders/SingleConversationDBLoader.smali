.class public Lcom/helpshift/conversation/loaders/SingleConversationDBLoader;
.super Lcom/helpshift/conversation/loaders/ConversationDBLoader;
.source "SingleConversationDBLoader.java"


# instance fields
.field private conversationLocalId:Ljava/lang/Long;


# direct methods
.method public constructor <init>(Lcom/helpshift/conversation/dao/ConversationDAO;Ljava/lang/Long;)V
    .locals 0

    .line 17
    invoke-direct {p0, p1}, Lcom/helpshift/conversation/loaders/ConversationDBLoader;-><init>(Lcom/helpshift/conversation/dao/ConversationDAO;)V

    .line 18
    iput-object p2, p0, Lcom/helpshift/conversation/loaders/SingleConversationDBLoader;->conversationLocalId:Ljava/lang/Long;

    return-void
.end method


# virtual methods
.method public fetchMessages(Ljava/lang/String;Ljava/lang/String;J)Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "J)",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/model/Conversation;",
            ">;"
        }
    .end annotation

    .line 23
    iget-object p1, p0, Lcom/helpshift/conversation/loaders/SingleConversationDBLoader;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v0, p0, Lcom/helpshift/conversation/loaders/SingleConversationDBLoader;->conversationLocalId:Ljava/lang/Long;

    invoke-interface {p1, v0}, Lcom/helpshift/conversation/dao/ConversationDAO;->readConversationWithoutMessages(Ljava/lang/Long;)Lcom/helpshift/conversation/activeconversation/model/Conversation;

    move-result-object p1

    if-nez p1, :cond_0

    .line 28
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    return-object p1

    .line 32
    :cond_0
    iget-object v0, p0, Lcom/helpshift/conversation/loaders/SingleConversationDBLoader;->conversationDAO:Lcom/helpshift/conversation/dao/ConversationDAO;

    iget-object v1, p0, Lcom/helpshift/conversation/loaders/SingleConversationDBLoader;->conversationLocalId:Ljava/lang/Long;

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, v1, v2}, Lcom/helpshift/conversation/dao/ConversationDAO;->readMessages(J)Ljava/util/List;

    move-result-object v0

    .line 35
    invoke-virtual {p0, p2, p3, p4, v0}, Lcom/helpshift/conversation/loaders/SingleConversationDBLoader;->filterMessages(Ljava/lang/String;JLjava/util/List;)Ljava/util/List;

    move-result-object p2

    .line 36
    invoke-virtual {p1, p2}, Lcom/helpshift/conversation/activeconversation/model/Conversation;->setMessageDMs(Ljava/util/List;)V

    .line 38
    iget-object p2, p1, Lcom/helpshift/conversation/activeconversation/model/Conversation;->messageDMs:Lcom/helpshift/common/util/HSObservableList;

    invoke-static {p2}, Lcom/helpshift/conversation/ConversationUtil;->sortMessagesBasedOnCreatedAt(Ljava/util/List;)V

    .line 39
    invoke-static {p1}, Ljava/util/Collections;->singletonList(Ljava/lang/Object;)Ljava/util/List;

    move-result-object p1

    return-object p1
.end method
