.class public Lcom/helpshift/common/conversation/ConversationDBInfo;
.super Ljava/lang/Object;
.source "ConversationDBInfo.java"


# static fields
.field public static final DATABASE_NAME:Ljava/lang/String; = "__hs__db_issues"

.field public static final DATABASE_VERSION:Ljava/lang/Integer;

.field static final TABLE_FAQ_LIST_SUGGESTIONS:Ljava/lang/String; = "faq_suggestions"


# instance fields
.field final ARCHIVAL_TEXT:Ljava/lang/String;

.field final ATTACHMENT_DRAFT:Ljava/lang/String;

.field public final AUTHOR_NAME:Ljava/lang/String;

.field public final BODY:Ljava/lang/String;

.field public final CONVERSATION_ID:Ljava/lang/String;

.field public final CONVERSATION_META:Ljava/lang/String;

.field public final CREATED_AT:Ljava/lang/String;

.field public CREATE_CONVERSATION_INBOX_TABLE:Ljava/lang/String;

.field public CREATE_CONVERSATION_TABLE:Ljava/lang/String;

.field private CREATE_FAQ_LIST_SUGGESTIONS_CACHE_TABLE:Ljava/lang/String;

.field public CREATE_MESSAGES_TABLE:Ljava/lang/String;

.field private final CREATE_SERVER_ID_INDEX_MESSAGES_TABLE:Ljava/lang/String;

.field public final DELIVERY_STATE:Ljava/lang/String;

.field final DESCRIPTION_DRAFT:Ljava/lang/String;

.field final DESCRIPTION_DRAFT_TIMESTAMP:Ljava/lang/String;

.field final DESCRIPTION_TYPE:Ljava/lang/String;

.field public final EPOCH_TIME_CREATE_AT:Ljava/lang/String;

.field final FORM_EMAIL:Ljava/lang/String;

.field final FORM_NAME:Ljava/lang/String;

.field public final FULL_PRIVACY_ENABLED:Ljava/lang/String;

.field public final HAS_OLDER_MESSAGES:Ljava/lang/String;

.field public final ID:Ljava/lang/String;

.field public final ISSUE_TYPE:Ljava/lang/String;

.field public final IS_REDACTED_CONVERSATION:Ljava/lang/String;

.field public final IS_REDACTED_MESSAGE:Ljava/lang/String;

.field public final IS_START_NEW_CONVERSATION_CLICKED:Ljava/lang/String;

.field public final LAST_CONVERSATIONS_REDACTION_TIME:Ljava/lang/String;

.field final LAST_SYNC_TIMESTAMP:Ljava/lang/String;

.field public final LAST_USER_ACTIVITY_TIME:Ljava/lang/String;

.field public final LOCAL_UUID:Ljava/lang/String;

.field public final MESSAGE_CURSOR:Ljava/lang/String;

.field public final MESSAGE_META:Ljava/lang/String;

.field final PERSIST_MESSAGE_BOX:Ljava/lang/String;

.field public final PRE_CONVERSATION_SERVER_ID:Ljava/lang/String;

.field public final PUBLISH_ID:Ljava/lang/String;

.field final REPLY_TEXT:Ljava/lang/String;

.field public final SERVER_ID:Ljava/lang/String;

.field public final SHOW_AGENT_NAME:Ljava/lang/String;

.field public final STATE:Ljava/lang/String;

.field public final TABLE_CONVERSATIONS:Ljava/lang/String;

.field public final TABLE_CONVERSATION_INBOX:Ljava/lang/String;

.field public final TABLE_MESSAGES:Ljava/lang/String;

.field public final TITLE:Ljava/lang/String;

.field public final TYPE:Ljava/lang/String;

.field public final UPDATED_AT:Ljava/lang/String;

.field public final USER_LOCAL_ID:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const/16 v0, 0x8

    .line 14
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    sput-object v0, Lcom/helpshift/common/conversation/ConversationDBInfo;->DATABASE_VERSION:Ljava/lang/Integer;

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 11
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "_id"

    .line 17
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->ID:Ljava/lang/String;

    const-string v0, "created_at"

    .line 18
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->CREATED_AT:Ljava/lang/String;

    const-string v0, "epoch_time_created_at"

    .line 19
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->EPOCH_TIME_CREATE_AT:Ljava/lang/String;

    const-string v0, "issues"

    .line 22
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->TABLE_CONVERSATIONS:Ljava/lang/String;

    const-string v0, "messages"

    .line 23
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->TABLE_MESSAGES:Ljava/lang/String;

    const-string v0, "conversation_inbox"

    .line 24
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->TABLE_CONVERSATION_INBOX:Ljava/lang/String;

    const-string v0, "server_id"

    .line 29
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->SERVER_ID:Ljava/lang/String;

    const-string v0, "publish_id"

    .line 30
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->PUBLISH_ID:Ljava/lang/String;

    const-string v0, "uuid"

    .line 31
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->LOCAL_UUID:Ljava/lang/String;

    const-string v0, "user_local_id"

    .line 32
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->USER_LOCAL_ID:Ljava/lang/String;

    const-string v0, "title"

    .line 33
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->TITLE:Ljava/lang/String;

    const-string v0, "updated_at"

    .line 34
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->UPDATED_AT:Ljava/lang/String;

    const-string v0, "state"

    .line 35
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->STATE:Ljava/lang/String;

    const-string v0, "show_agent_name"

    .line 36
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->SHOW_AGENT_NAME:Ljava/lang/String;

    const-string v0, "message_cursor"

    .line 37
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->MESSAGE_CURSOR:Ljava/lang/String;

    const-string v0, "start_new_conversation_action"

    .line 38
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->IS_START_NEW_CONVERSATION_CLICKED:Ljava/lang/String;

    const-string v0, "is_redacted"

    .line 39
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->IS_REDACTED_CONVERSATION:Ljava/lang/String;

    const-string v0, "meta"

    .line 40
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->CONVERSATION_META:Ljava/lang/String;

    const-string v0, "pre_conv_server_id"

    .line 41
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->PRE_CONVERSATION_SERVER_ID:Ljava/lang/String;

    const-string v0, "last_user_activity_time"

    .line 42
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->LAST_USER_ACTIVITY_TIME:Ljava/lang/String;

    const-string v0, "issue_type"

    .line 43
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->ISSUE_TYPE:Ljava/lang/String;

    const-string v0, "full_privacy_enabled"

    .line 44
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->FULL_PRIVACY_ENABLED:Ljava/lang/String;

    const-string v0, "conversation_id"

    .line 48
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->CONVERSATION_ID:Ljava/lang/String;

    const-string v0, "body"

    .line 49
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->BODY:Ljava/lang/String;

    const-string v0, "author_name"

    .line 50
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->AUTHOR_NAME:Ljava/lang/String;

    const-string v0, "type"

    .line 51
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->TYPE:Ljava/lang/String;

    const-string v0, "meta"

    .line 52
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->MESSAGE_META:Ljava/lang/String;

    const-string v0, "md_state"

    .line 53
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->DELIVERY_STATE:Ljava/lang/String;

    const-string v0, "is_redacted"

    .line 54
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->IS_REDACTED_MESSAGE:Ljava/lang/String;

    const-string v0, "has_older_messages"

    .line 55
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->HAS_OLDER_MESSAGES:Ljava/lang/String;

    const-string v0, "last_conv_redaction_time"

    .line 56
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->LAST_CONVERSATIONS_REDACTION_TIME:Ljava/lang/String;

    const-string v0, "form_name"

    .line 60
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->FORM_NAME:Ljava/lang/String;

    const-string v0, "form_email"

    .line 61
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->FORM_EMAIL:Ljava/lang/String;

    const-string v0, "description_draft"

    .line 62
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->DESCRIPTION_DRAFT:Ljava/lang/String;

    const-string v0, "description_draft_timestamp"

    .line 63
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->DESCRIPTION_DRAFT_TIMESTAMP:Ljava/lang/String;

    const-string v0, "attachment_draft"

    .line 64
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->ATTACHMENT_DRAFT:Ljava/lang/String;

    const-string v0, "description_type"

    .line 65
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->DESCRIPTION_TYPE:Ljava/lang/String;

    const-string v0, "archival_text"

    .line 66
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->ARCHIVAL_TEXT:Ljava/lang/String;

    const-string v0, "reply_text"

    .line 67
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->REPLY_TEXT:Ljava/lang/String;

    const-string v0, "persist_message_box"

    .line 68
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->PERSIST_MESSAGE_BOX:Ljava/lang/String;

    const-string v0, "since"

    .line 69
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->LAST_SYNC_TIMESTAMP:Ljava/lang/String;

    const-string v0, "CREATE INDEX SERVER_IDX ON messages(server_id)"

    .line 70
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->CREATE_SERVER_ID_INDEX_MESSAGES_TABLE:Ljava/lang/String;

    const-string v0, "CREATE TABLE issues ( _id INTEGER PRIMARY KEY AUTOINCREMENT,server_id TEXT UNIQUE, pre_conv_server_id TEXT UNIQUE, publish_id TEXT, uuid TEXT NOT NULL, user_local_id TEXT NOT NULL, title TEXT NOT NULL,issue_type TEXT NOT NULL, state INTEGER NOT NULL, show_agent_name INTEGER,message_cursor TEXT,start_new_conversation_action INTEGER, is_redacted INTEGER, meta TEXT,last_user_activity_time INTEGER, full_privacy_enabled INTEGER, epoch_time_created_at INTEGER NOT NULL, created_at TEXT NOT NULL,updated_at TEXT NOT NULL  );"

    .line 72
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->CREATE_CONVERSATION_TABLE:Ljava/lang/String;

    const-string v0, "CREATE TABLE conversation_inbox ( user_local_id TEXT PRIMARY KEY NOT NULL, form_name TEXT,form_email TEXT,description_draft TEXT,description_draft_timestamp TEXT,attachment_draft TEXT,description_type TEXT,archival_text TEXT, reply_text TEXT, persist_message_box INT, since TEXT, has_older_messages INT, last_conv_redaction_time INT );"

    .line 93
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->CREATE_CONVERSATION_INBOX_TABLE:Ljava/lang/String;

    const-string v0, "CREATE TABLE messages ( _id INTEGER PRIMARY KEY AUTOINCREMENT, server_id TEXT, conversation_id TEXT, body TEXT, author_name TEXT, type TEXT, meta TEXT, is_redacted INTEGER, created_at TEXT, epoch_time_created_at INTEGER NOT NULL, md_state INTEGER  );"

    .line 108
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->CREATE_MESSAGES_TABLE:Ljava/lang/String;

    const-string v0, "CREATE TABLE faq_suggestions ( _id INTEGER PRIMARY KEY AUTOINCREMENT,question_id TEXT NOT NULL,publish_id TEXT NOT NULL,language TEXT NOT NULL,section_id TEXT NOT NULL,title TEXT NOT NULL,body TEXT NOT NULL,helpful INTEGER,rtl INTEGER,tags TEXT,c_tags TEXT );"

    .line 121
    iput-object v0, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->CREATE_FAQ_LIST_SUGGESTIONS_CACHE_TABLE:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getMinimumDBVersionSupportedForMigration()I
    .locals 1

    const/4 v0, 0x6

    return v0
.end method

.method public getQueriesForDropAndCreate()Ljava/util/List;
    .locals 2
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 148
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const-string v1, "DROP TABLE IF EXISTS issues"

    .line 149
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "DROP TABLE IF EXISTS conversation_inbox"

    .line 150
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "DROP TABLE IF EXISTS messages"

    .line 151
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "DROP TABLE IF EXISTS faq_suggestions"

    .line 152
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    const-string v1, "DROP TABLE IF EXISTS issues_old"

    .line 153
    invoke-interface {v0, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 154
    invoke-virtual {p0}, Lcom/helpshift/common/conversation/ConversationDBInfo;->getQueriesForOnCreate()Ljava/util/List;

    move-result-object v1

    invoke-interface {v0, v1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    return-object v0
.end method

.method public getQueriesForOnCreate()Ljava/util/List;
    .locals 4
    .annotation build Landroidx/annotation/NonNull;
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 138
    new-instance v0, Ljava/util/ArrayList;

    const/4 v1, 0x5

    new-array v1, v1, [Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->CREATE_CONVERSATION_TABLE:Ljava/lang/String;

    const/4 v3, 0x0

    aput-object v2, v1, v3

    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->CREATE_CONVERSATION_INBOX_TABLE:Ljava/lang/String;

    const/4 v3, 0x1

    aput-object v2, v1, v3

    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->CREATE_MESSAGES_TABLE:Ljava/lang/String;

    const/4 v3, 0x2

    aput-object v2, v1, v3

    const-string v2, "CREATE INDEX SERVER_IDX ON messages(server_id)"

    const/4 v3, 0x3

    aput-object v2, v1, v3

    iget-object v2, p0, Lcom/helpshift/common/conversation/ConversationDBInfo;->CREATE_FAQ_LIST_SUGGESTIONS_CACHE_TABLE:Ljava/lang/String;

    const/4 v3, 0x4

    aput-object v2, v1, v3

    invoke-static {v1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    return-object v0
.end method
