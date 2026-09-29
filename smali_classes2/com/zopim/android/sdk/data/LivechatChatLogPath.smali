.class public Lcom/zopim/android/sdk/data/LivechatChatLogPath;
.super Lcom/zopim/android/sdk/data/Path;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/data/LivechatChatLogPath$a;,
        Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;,
        Lcom/zopim/android/sdk/data/LivechatChatLogPath$ChatTimeoutReceiver;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/zopim/android/sdk/data/Path<",
        "Ljava/util/LinkedHashMap<",
        "Ljava/lang/String;",
        "Lcom/zopim/android/sdk/model/ChatLog;",
        ">;>;"
    }
.end annotation


# static fields
.field private static final INSTANCE:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

.field private static final LOG_TAG:Ljava/lang/String; = "LivechatChatLogPath"


# instance fields
.field mChatRatingEntry:Landroid/util/Pair;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Landroid/util/Pair<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ">;"
        }
    .end annotation
.end field

.field private final mLock:Ljava/lang/Object;

.field private mTimeoutManager:Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;

.field private mUnmatchedAgentQuestionnaire:Ljava/util/LinkedList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/LinkedList<",
            "Landroid/util/Pair<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ">;>;"
        }
    .end annotation
.end field

.field mUploadedFiles:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    invoke-direct {v0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;-><init>()V

    sput-object v0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->INSTANCE:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    return-void
.end method

.method private constructor <init>()V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/Path;-><init>()V

    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mLock:Ljava/lang/Object;

    new-instance v0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;-><init>(Lcom/zopim/android/sdk/data/LivechatChatLogPath;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mTimeoutManager:Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;

    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mUploadedFiles:Ljava/util/Map;

    new-instance v0, Ljava/util/LinkedList;

    invoke-direct {v0}, Ljava/util/LinkedList;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mUnmatchedAgentQuestionnaire:Ljava/util/LinkedList;

    new-instance v0, Ljava/util/LinkedHashMap;

    invoke-direct {v0}, Ljava/util/LinkedHashMap;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    return-void
.end method

.method static synthetic access$000()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->LOG_TAG:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$100()Lcom/zopim/android/sdk/data/LivechatChatLogPath;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->INSTANCE:Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    return-object v0
.end method

.method static synthetic access$200(Lcom/zopim/android/sdk/data/LivechatChatLogPath;Ljava/util/LinkedHashMap;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->updateInternal(Ljava/util/LinkedHashMap;)V

    return-void
.end method

.method private findAgentQuestionnaire(Lcom/zopim/android/sdk/model/ChatLog;)Landroid/util/Pair;
    .locals 14
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ")",
            "Landroid/util/Pair<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ">;"
        }
    .end annotation

    const/4 v0, 0x0

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->LOG_TAG:Ljava/lang/String;

    const-string v1, "RowItem must not be null"

    invoke-static {p1, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-object v0

    :cond_0
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v1

    sget-object v2, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_VISITOR:Lcom/zopim/android/sdk/model/ChatLog$Type;

    if-eq v1, v2, :cond_1

    return-object v0

    :cond_1
    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_7

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-eqz v1, :cond_2

    goto :goto_3

    :cond_2
    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mUnmatchedAgentQuestionnaire:Ljava/util/LinkedList;

    invoke-virtual {v1}, Ljava/util/LinkedList;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_3
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_7

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/util/Pair;

    iget-object v3, v2, Landroid/util/Pair;->second:Ljava/lang/Object;

    check-cast v3, Lcom/zopim/android/sdk/model/ChatLog;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v4

    sget-object v5, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_AGENT:Lcom/zopim/android/sdk/model/ChatLog$Type;

    if-eq v4, v5, :cond_4

    goto :goto_0

    :cond_4
    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getOptions()[Lcom/zopim/android/sdk/model/ChatLog$Option;

    move-result-object v4

    array-length v5, v4

    const/4 v6, 0x0

    const/4 v7, 0x0

    :goto_1
    if-ge v7, v5, :cond_3

    aget-object v8, v4, v7

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v8}, Lcom/zopim/android/sdk/model/ChatLog$Option;->getLabel()Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v9, v8}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v8

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/ChatLog;->getTimestamp()Ljava/lang/Long;

    move-result-object v9

    invoke-virtual {v9}, Ljava/lang/Long;->longValue()J

    move-result-wide v9

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getTimestamp()Ljava/lang/Long;

    move-result-object v11

    invoke-virtual {v11}, Ljava/lang/Long;->longValue()J

    move-result-wide v11

    cmp-long v13, v9, v11

    if-lez v13, :cond_5

    const/4 v9, 0x1

    goto :goto_2

    :cond_5
    const/4 v9, 0x0

    :goto_2
    if-eqz v8, :cond_6

    if-eqz v9, :cond_6

    iget-object p1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mUnmatchedAgentQuestionnaire:Ljava/util/LinkedList;

    invoke-virtual {p1, v2}, Ljava/util/LinkedList;->remove(Ljava/lang/Object;)Z

    return-object v2

    :cond_6
    add-int/lit8 v7, v7, 0x1

    goto :goto_1

    :cond_7
    :goto_3
    return-object v0
.end method

.method public static declared-synchronized getInstance()Lcom/zopim/android/sdk/data/LivechatChatLogPath;
    .locals 2

    const-class v0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    monitor-enter v0

    :try_start_0
    sget-object v1, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->INSTANCE:Lcom/zopim/android/sdk/data/LivechatChatLogPath;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object v1

    :catchall_0
    move-exception v1

    monitor-exit v0

    throw v1
.end method

.method private mergeEntries(Lcom/zopim/android/sdk/model/ChatLog;Lcom/zopim/android/sdk/model/ChatLog;)Lcom/zopim/android/sdk/model/ChatLog;
    .locals 2

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return-object v0

    :cond_0
    if-nez p2, :cond_1

    return-object p1

    :cond_1
    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->PARSER:Lcom/zopim/android/sdk/data/Parser;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/data/Parser;->getMapper()Lcom/fasterxml/jackson/databind/ObjectMapper;

    move-result-object v1

    invoke-virtual {v1, p2}, Lcom/fasterxml/jackson/databind/ObjectMapper;->valueToTree(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/JsonNode;

    move-result-object p2

    invoke-virtual {v1, p1}, Lcom/fasterxml/jackson/databind/ObjectMapper;->readerForUpdating(Ljava/lang/Object;)Lcom/fasterxml/jackson/databind/ObjectReader;

    move-result-object p1

    :try_start_0
    invoke-virtual {p1, p2}, Lcom/fasterxml/jackson/databind/ObjectReader;->readValue(Lcom/fasterxml/jackson/databind/JsonNode;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/model/ChatLog;
    :try_end_0
    .catch Lcom/fasterxml/jackson/core/JsonProcessingException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    move-exception p1

    sget-object p2, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->LOG_TAG:Ljava/lang/String;

    const-string v1, "IO error. Chat log record could not be updated."

    goto :goto_0

    :catch_1
    move-exception p1

    sget-object p2, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Failed to process json. Chat log record could not be updated."

    :goto_0
    invoke-static {p2, v1, p1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    return-object v0
.end method

.method private updateInternal(Ljava/util/LinkedHashMap;)V
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ">;)V"
        }
    .end annotation

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Passed parameter must not be null. Aborting update."

    invoke-static {p1, v0}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mLock:Ljava/lang/Object;

    monitor-enter v0

    :try_start_0
    invoke-virtual {p1}, Ljava/util/LinkedHashMap;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    const/4 v1, 0x0

    const/4 v2, 0x0

    :cond_1
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_16

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/util/Map$Entry;

    invoke-interface {v3}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    invoke-interface {v3}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/zopim/android/sdk/model/ChatLog;

    if-eqz v3, :cond_2

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v5

    sget-object v6, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_RATING:Lcom/zopim/android/sdk/model/ChatLog$Type;

    if-ne v5, v6, :cond_2

    iget-object v5, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mChatRatingEntry:Landroid/util/Pair;

    if-eqz v5, :cond_2

    iget-object v4, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mChatRatingEntry:Landroid/util/Pair;

    iget-object v4, v4, Landroid/util/Pair;->first:Ljava/lang/Object;

    check-cast v4, Ljava/lang/String;

    :cond_2
    iget-object v5, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    check-cast v5, Ljava/util/LinkedHashMap;

    invoke-virtual {v5, v4}, Ljava/util/LinkedHashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v5

    const/4 v6, 0x1

    if-eqz v5, :cond_a

    iget-object v5, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    check-cast v5, Ljava/util/LinkedHashMap;

    invoke-virtual {v5, v4}, Ljava/util/LinkedHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/zopim/android/sdk/model/ChatLog;

    if-nez v3, :cond_4

    iget-object v3, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    check-cast v3, Ljava/util/LinkedHashMap;

    invoke-virtual {v3, v4}, Ljava/util/LinkedHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/zopim/android/sdk/model/ChatLog;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v3

    sget-object v5, Lcom/zopim/android/sdk/model/ChatLog$Type;->ATTACHMENT_UPLOAD:Lcom/zopim/android/sdk/model/ChatLog$Type;

    if-ne v3, v5, :cond_3

    goto :goto_0

    :cond_3
    iget-object v3, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    check-cast v3, Ljava/util/LinkedHashMap;

    invoke-virtual {v3, v4}, Ljava/util/LinkedHashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    add-int/lit8 v2, v2, -0x1

    iget-object v3, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mTimeoutManager:Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;

    invoke-virtual {v3, v4}, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->a(Ljava/lang/String;)V

    goto :goto_0

    :cond_4
    invoke-direct {p0, v5, v3}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mergeEntries(Lcom/zopim/android/sdk/model/ChatLog;Lcom/zopim/android/sdk/model/ChatLog;)Lcom/zopim/android/sdk/model/ChatLog;

    move-result-object v5

    if-nez v5, :cond_5

    iget-object v5, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    check-cast v5, Ljava/util/LinkedHashMap;

    invoke-virtual {v5, v4}, Ljava/util/LinkedHashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    goto/16 :goto_9

    :cond_5
    iget-object v7, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    check-cast v7, Ljava/util/LinkedHashMap;

    invoke-virtual {v7, v4, v5}, Ljava/util/LinkedHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    sget-object v7, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_VISITOR:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v5}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v8

    if-ne v7, v8, :cond_6

    const/4 v7, 0x1

    goto :goto_1

    :cond_6
    const/4 v7, 0x0

    :goto_1
    invoke-virtual {v5}, Lcom/zopim/android/sdk/model/ChatLog;->isFailed()Ljava/lang/Boolean;

    move-result-object v8

    if-nez v8, :cond_7

    const/4 v8, 0x0

    goto :goto_2

    :cond_7
    invoke-virtual {v5}, Lcom/zopim/android/sdk/model/ChatLog;->isFailed()Ljava/lang/Boolean;

    move-result-object v8

    invoke-virtual {v8}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v8

    :goto_2
    if-eqz v7, :cond_15

    if-nez v8, :cond_15

    invoke-virtual {v5}, Lcom/zopim/android/sdk/model/ChatLog;->isUnverified()Ljava/lang/Boolean;

    move-result-object v7

    if-nez v7, :cond_8

    goto :goto_3

    :cond_8
    invoke-virtual {v5}, Lcom/zopim/android/sdk/model/ChatLog;->isUnverified()Ljava/lang/Boolean;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v6

    :goto_3
    if-eqz v6, :cond_9

    iget-object v5, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mTimeoutManager:Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;

    invoke-virtual {v5, v4, v3}, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->a(Ljava/lang/String;Lcom/zopim/android/sdk/model/ChatLog;)V

    goto/16 :goto_9

    :cond_9
    iget-object v5, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mTimeoutManager:Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;

    invoke-virtual {v5, v4}, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->a(Ljava/lang/String;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto/16 :goto_9

    :cond_a
    if-eqz v3, :cond_15

    :try_start_1
    sget-object v5, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_VISITOR:Lcom/zopim/android/sdk/model/ChatLog$Type;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v7

    if-ne v5, v7, :cond_b

    const/4 v5, 0x1

    goto :goto_4

    :cond_b
    const/4 v5, 0x0

    :goto_4
    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object v7

    if-eqz v7, :cond_c

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/String;->isEmpty()Z

    move-result v7

    if-eqz v7, :cond_c

    goto :goto_5

    :cond_c
    const/4 v6, 0x0

    :goto_5
    if-eqz v5, :cond_d

    if-eqz v6, :cond_d

    goto/16 :goto_0

    :cond_d
    invoke-direct {p0, v3}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->findAgentQuestionnaire(Lcom/zopim/android/sdk/model/ChatLog;)Landroid/util/Pair;

    move-result-object v6

    if-eqz v6, :cond_f

    iget-object v5, v6, Landroid/util/Pair;->second:Ljava/lang/Object;

    check-cast v5, Lcom/zopim/android/sdk/model/ChatLog;

    invoke-virtual {v5}, Lcom/zopim/android/sdk/model/ChatLog;->getOptions()[Lcom/zopim/android/sdk/model/ChatLog$Option;

    move-result-object v5

    const/4 v7, 0x0

    :goto_6
    array-length v8, v5

    if-ge v7, v8, :cond_1

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object v8

    aget-object v9, v5, v7

    invoke-virtual {v9}, Lcom/zopim/android/sdk/model/ChatLog$Option;->getLabel()Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v8, v9}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_e

    aget-object v5, v5, v7

    invoke-virtual {v5}, Lcom/zopim/android/sdk/model/ChatLog$Option;->select()V

    iget-object v5, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    check-cast v5, Ljava/util/LinkedHashMap;

    iget-object v7, v6, Landroid/util/Pair;->first:Ljava/lang/Object;

    iget-object v6, v6, Landroid/util/Pair;->second:Ljava/lang/Object;

    invoke-virtual {v5, v7, v6}, Ljava/util/LinkedHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto/16 :goto_0

    :cond_e
    add-int/lit8 v7, v7, 0x1

    goto :goto_6

    :cond_f
    if-eqz v3, :cond_10

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v6

    sget-object v7, Lcom/zopim/android/sdk/model/ChatLog$Type;->ATTACHMENT_UPLOAD:Lcom/zopim/android/sdk/model/ChatLog$Type;

    if-ne v6, v7, :cond_10

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getFileName()Ljava/lang/String;

    move-result-object v6

    if-eqz v6, :cond_10

    sget-object v7, Lcom/zopim/android/sdk/api/FileTransfers;->INSTANCE:Lcom/zopim/android/sdk/api/FileTransfers;

    invoke-virtual {v7, v6}, Lcom/zopim/android/sdk/api/FileTransfers;->findFile(Ljava/lang/String;)Ljava/io/File;

    move-result-object v7

    invoke-virtual {v3, v7}, Lcom/zopim/android/sdk/model/ChatLog;->setFile(Ljava/io/File;)V

    iget-object v7, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mUploadedFiles:Ljava/util/Map;

    invoke-interface {v7, v6, v4}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_10
    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v6

    sget-object v7, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_MSG_VISITOR:Lcom/zopim/android/sdk/model/ChatLog$Type;

    if-ne v6, v7, :cond_12

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v6

    if-eqz v6, :cond_11

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v6

    invoke-virtual {v6}, Lcom/zopim/android/sdk/model/Attachment;->getName()Ljava/lang/String;

    move-result-object v6

    goto :goto_7

    :cond_11
    const/4 v6, 0x0

    :goto_7
    if-eqz v6, :cond_12

    iget-object v5, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mUploadedFiles:Ljava/util/Map;

    invoke-interface {v5, v6}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    iget-object v6, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    check-cast v6, Ljava/util/LinkedHashMap;

    invoke-virtual {v6, v5}, Ljava/util/LinkedHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Lcom/zopim/android/sdk/model/ChatLog;

    if-eqz v5, :cond_1

    const/16 v6, 0x64

    invoke-virtual {v5, v6}, Lcom/zopim/android/sdk/model/ChatLog;->setProgress(I)V

    goto/16 :goto_0

    :cond_12
    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v6

    sget-object v7, Lcom/zopim/android/sdk/model/ChatLog$Type;->CHAT_RATING:Lcom/zopim/android/sdk/model/ChatLog$Type;

    if-ne v6, v7, :cond_13

    new-instance v6, Landroid/util/Pair;

    invoke-direct {v6, v4, v3}, Landroid/util/Pair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V

    iput-object v6, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mChatRatingEntry:Landroid/util/Pair;

    :cond_13
    iget-object v6, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    check-cast v6, Ljava/util/LinkedHashMap;

    invoke-virtual {v6, v4, v3}, Ljava/util/LinkedHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    add-int/lit8 v2, v2, 0x1

    if-eqz v5, :cond_15

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->isUnverified()Ljava/lang/Boolean;

    move-result-object v5

    if-eqz v5, :cond_14

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->isUnverified()Ljava/lang/Boolean;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v5

    goto :goto_8

    :cond_14
    const/4 v5, 0x0

    :goto_8
    if-eqz v5, :cond_15

    iget-object v5, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mTimeoutManager:Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;

    invoke-virtual {v5, v4, v3}, Lcom/zopim/android/sdk/data/LivechatChatLogPath$b;->a(Ljava/lang/String;Lcom/zopim/android/sdk/model/ChatLog;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_9

    :catch_0
    move-exception v5

    :try_start_2
    sget-object v6, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->LOG_TAG:Ljava/lang/String;

    const-string v7, "Failed to process json. Chat log record could not be created."

    invoke-static {v6, v7, v5}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_15
    :goto_9
    if-eqz v3, :cond_1

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getOptions()[Lcom/zopim/android/sdk/model/ChatLog$Option;

    move-result-object v5

    if-eqz v5, :cond_1

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getOptions()[Lcom/zopim/android/sdk/model/ChatLog$Option;

    move-result-object v5

    array-length v5, v5

    if-lez v5, :cond_1

    iget-object v5, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mUnmatchedAgentQuestionnaire:Ljava/util/LinkedList;

    new-instance v6, Landroid/util/Pair;

    invoke-direct {v6, v4, v3}, Landroid/util/Pair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V

    invoke-virtual {v5, v6}, Ljava/util/LinkedList;->addFirst(Ljava/lang/Object;)V

    goto/16 :goto_0

    :cond_16
    if-ltz v2, :cond_17

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getData()Ljava/util/LinkedHashMap;

    move-result-object p1

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->broadcast(Ljava/lang/Object;)V

    :cond_17
    monitor-exit v0

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v0
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw p1
.end method


# virtual methods
.method clear()V
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    check-cast v0, Ljava/util/LinkedHashMap;

    invoke-virtual {v0}, Ljava/util/LinkedHashMap;->clear()V

    :cond_0
    return-void
.end method

.method public varargs countMessages([Lcom/zopim/android/sdk/model/ChatLog$Type;)I
    .locals 7

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getData()Ljava/util/LinkedHashMap;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/LinkedHashMap;->values()Ljava/util/Collection;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v0

    const/4 v1, 0x0

    const/4 v2, 0x0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_2

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/zopim/android/sdk/model/ChatLog;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v3

    array-length v4, p1

    move v5, v2

    const/4 v2, 0x0

    :goto_1
    if-ge v2, v4, :cond_1

    aget-object v6, p1, v2

    invoke-virtual {v6, v3}, Lcom/zopim/android/sdk/model/ChatLog$Type;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_0

    add-int/lit8 v5, v5, 0x1

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_1

    :cond_1
    move v2, v5

    goto :goto_0

    :cond_2
    return v2
.end method

.method public bridge synthetic getData()Ljava/lang/Object;
    .locals 1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getData()Ljava/util/LinkedHashMap;

    move-result-object v0

    return-object v0
.end method

.method public getData()Ljava/util/LinkedHashMap;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ">;"
        }
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    if-eqz v0, :cond_0

    new-instance v0, Ljava/util/LinkedHashMap;

    iget-object v1, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->mData:Ljava/lang/Object;

    check-cast v1, Ljava/util/Map;

    invoke-direct {v0, v1}, Ljava/util/LinkedHashMap;-><init>(Ljava/util/Map;)V

    return-object v0

    :cond_0
    new-instance v0, Ljava/util/LinkedHashMap;

    invoke-direct {v0}, Ljava/util/LinkedHashMap;-><init>()V

    return-object v0
.end method

.method update(Ljava/lang/String;)V
    .locals 2

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->isClearRequired(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->clear()V

    return-void

    :cond_0
    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_1

    return-void

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->PARSER:Lcom/zopim/android/sdk/data/Parser;

    new-instance v1, Lcom/zopim/android/sdk/data/d;

    invoke-direct {v1, p0}, Lcom/zopim/android/sdk/data/d;-><init>(Lcom/zopim/android/sdk/data/LivechatChatLogPath;)V

    invoke-virtual {v0, p1, v1}, Lcom/zopim/android/sdk/data/Parser;->parse(Ljava/lang/String;Lcom/fasterxml/jackson/core/type/TypeReference;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/util/LinkedHashMap;

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->updateInternal(Ljava/util/LinkedHashMap;)V

    return-void
.end method
