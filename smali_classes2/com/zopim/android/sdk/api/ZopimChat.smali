.class public Lcom/zopim/android/sdk/api/ZopimChat;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/api/Chat;
.implements Lcom/zopim/android/sdk/api/ChatSession;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/api/ZopimChat$ChatTimeoutReceiver;,
        Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;,
        Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;,
        Lcom/zopim/android/sdk/api/ZopimChat$a;,
        Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;
    }
.end annotation


# static fields
.field private static final DATA_SOURCE:Lcom/zopim/android/sdk/data/DataSource;

.field private static final LOG_TAG:Ljava/lang/String; = "ZopimChat"

.field private static mDisableVisitorInfo:Z

.field private static mInitializationTimeout:Ljava/lang/Long;

.field private static mReconnectTimeout:Ljava/lang/Long;

.field private static mReferrer:Ljava/lang/String;

.field private static mSessionTimeout:Ljava/lang/Long;

.field private static mTitle:Ljava/lang/String;

.field private static mVisitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

.field private static singleton:Lcom/zopim/android/sdk/api/ZopimChat;


# instance fields
.field private mAccountKey:Ljava/lang/String;

.field private mChatService:Lcom/zopim/android/sdk/api/Chat;

.field private mChatServiceBinder:Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

.field private mDepartment:Ljava/lang/String;

.field private mEnded:Z

.field private mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

.field private mSessionConfig:Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

.field private mTags:[Ljava/lang/String;

.field mUnsentFiles:Ljava/util/Queue;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Queue<",
            "Ljava/io/File;",
            ">;"
        }
    .end annotation
.end field

.field mUnsentMessages:Ljava/util/Queue;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Queue<",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    new-instance v0, Lcom/zopim/android/sdk/data/PathDataSource;

    invoke-direct {v0}, Lcom/zopim/android/sdk/data/PathDataSource;-><init>()V

    sput-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->DATA_SOURCE:Lcom/zopim/android/sdk/data/DataSource;

    return-void
.end method

.method constructor <init>()V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    new-instance v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Builder;

    invoke-direct {v0}, Lcom/zopim/android/sdk/prechat/PreChatForm$Builder;-><init>()V

    invoke-virtual {v0}, Lcom/zopim/android/sdk/prechat/PreChatForm$Builder;->build()Lcom/zopim/android/sdk/prechat/PreChatForm;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    new-instance v0, Ljava/util/concurrent/ConcurrentLinkedQueue;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentLinkedQueue;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mUnsentMessages:Ljava/util/Queue;

    new-instance v0, Ljava/util/concurrent/ConcurrentLinkedQueue;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentLinkedQueue;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mUnsentFiles:Ljava/util/Queue;

    const/4 v0, 0x0

    invoke-static {v0}, Lcom/zopim/android/sdk/api/Logger;->setEnabled(Z)V

    return-void
.end method

.method static synthetic access$000(Lcom/zopim/android/sdk/api/ZopimChat;)Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mSessionConfig:Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    return-object p0
.end method

.method static synthetic access$002(Lcom/zopim/android/sdk/api/ZopimChat;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;)Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mSessionConfig:Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    return-object p1
.end method

.method static synthetic access$1000()Ljava/lang/Long;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->mSessionTimeout:Ljava/lang/Long;

    return-object v0
.end method

.method static synthetic access$1002(Ljava/lang/Long;)Ljava/lang/Long;
    .locals 0

    sput-object p0, Lcom/zopim/android/sdk/api/ZopimChat;->mSessionTimeout:Ljava/lang/Long;

    return-object p0
.end method

.method static synthetic access$1100()Z
    .locals 1

    sget-boolean v0, Lcom/zopim/android/sdk/api/ZopimChat;->mDisableVisitorInfo:Z

    return v0
.end method

.method static synthetic access$1102(Z)Z
    .locals 0

    sput-boolean p0, Lcom/zopim/android/sdk/api/ZopimChat;->mDisableVisitorInfo:Z

    return p0
.end method

.method static synthetic access$1400()Z
    .locals 1

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->isInitialized()Z

    move-result v0

    return v0
.end method

.method static synthetic access$1500()Lcom/zopim/android/sdk/api/ZopimChat;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    return-object v0
.end method

.method static synthetic access$1602(Lcom/zopim/android/sdk/api/ZopimChat;Z)Z
    .locals 0

    iput-boolean p1, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mEnded:Z

    return p1
.end method

.method static synthetic access$1700(Lcom/zopim/android/sdk/api/ZopimChat;)Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatServiceBinder:Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    return-object p0
.end method

.method static synthetic access$1702(Lcom/zopim/android/sdk/api/ZopimChat;Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;)Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatServiceBinder:Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    return-object p1
.end method

.method static synthetic access$1800()Lcom/zopim/android/sdk/model/VisitorInfo;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->mVisitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

    return-object v0
.end method

.method static synthetic access$1900(Lcom/zopim/android/sdk/api/ZopimChat;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mAccountKey:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$200()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$2002(Lcom/zopim/android/sdk/api/ZopimChat;Lcom/zopim/android/sdk/api/Chat;)Lcom/zopim/android/sdk/api/Chat;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    return-object p1
.end method

.method static synthetic access$2300(Lcom/zopim/android/sdk/api/ZopimChat;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->resendUnsentMessages()V

    return-void
.end method

.method static synthetic access$2400(Lcom/zopim/android/sdk/api/ZopimChat;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->resendUnsentFiles()V

    return-void
.end method

.method static synthetic access$300(Lcom/zopim/android/sdk/api/ZopimChat;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mDepartment:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$302(Lcom/zopim/android/sdk/api/ZopimChat;Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mDepartment:Ljava/lang/String;

    return-object p1
.end method

.method static synthetic access$400(Lcom/zopim/android/sdk/api/ZopimChat;)Lcom/zopim/android/sdk/prechat/PreChatForm;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    return-object p0
.end method

.method static synthetic access$402(Lcom/zopim/android/sdk/api/ZopimChat;Lcom/zopim/android/sdk/prechat/PreChatForm;)Lcom/zopim/android/sdk/prechat/PreChatForm;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    return-object p1
.end method

.method static synthetic access$502(Lcom/zopim/android/sdk/api/ZopimChat;[Ljava/lang/String;)[Ljava/lang/String;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mTags:[Ljava/lang/String;

    return-object p1
.end method

.method static synthetic access$600()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->mTitle:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$602(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    sput-object p0, Lcom/zopim/android/sdk/api/ZopimChat;->mTitle:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$700()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->mReferrer:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$702(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    sput-object p0, Lcom/zopim/android/sdk/api/ZopimChat;->mReferrer:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$800()Ljava/lang/Long;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->mInitializationTimeout:Ljava/lang/Long;

    return-object v0
.end method

.method static synthetic access$802(Ljava/lang/Long;)Ljava/lang/Long;
    .locals 0

    sput-object p0, Lcom/zopim/android/sdk/api/ZopimChat;->mInitializationTimeout:Ljava/lang/Long;

    return-object p0
.end method

.method static synthetic access$902(Ljava/lang/Long;)Ljava/lang/Long;
    .locals 0

    sput-object p0, Lcom/zopim/android/sdk/api/ZopimChat;->mReconnectTimeout:Ljava/lang/Long;

    return-object p0
.end method

.method private canCommunicate()Z
    .locals 4

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatServiceBinder:Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    const/4 v1, 0x1

    const/4 v2, 0x0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatServiceBinder:Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->isBound()Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    iget-object v3, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    if-eqz v3, :cond_1

    const/4 v3, 0x1

    goto :goto_1

    :cond_1
    const/4 v3, 0x0

    :goto_1
    if-eqz v0, :cond_2

    if-eqz v3, :cond_2

    return v1

    :cond_2
    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Can not chat at the moment. Chat is not connected to the chat service."

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->i(Ljava/lang/String;Ljava/lang/String;)V

    return v2
.end method

.method public static getDataSource()Lcom/zopim/android/sdk/data/DataSource;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->DATA_SOURCE:Lcom/zopim/android/sdk/data/DataSource;

    return-object v0
.end method

.method public static getInitializationTimeout()Ljava/lang/Long;
    .locals 2

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->isInitialized()Z

    move-result v0

    if-nez v0, :cond_1

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Chat must be initialized to use initialization timeout configuration. Will return default timeout."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_0
    sget-wide v0, Lcom/zopim/android/sdk/api/ZopimChat;->DEFAULT_CHAT_INITIALIZATION_TIMEOUT:J

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    return-object v0

    :cond_1
    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->mInitializationTimeout:Ljava/lang/Long;

    if-eqz v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->mInitializationTimeout:Ljava/lang/Long;

    return-object v0
.end method

.method public static getReconnectTimeout()Ljava/lang/Long;
    .locals 2

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->isInitialized()Z

    move-result v0

    if-nez v0, :cond_1

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Chat must be initialized to use reconnect timeout configuration. Will return default timeout."

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_0
    sget-wide v0, Lcom/zopim/android/sdk/api/ZopimChat;->DEFAULT_RECONNECT_TIMEOUT:J

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    return-object v0

    :cond_1
    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->mReconnectTimeout:Ljava/lang/Long;

    if-eqz v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->mReconnectTimeout:Ljava/lang/Long;

    return-object v0
.end method

.method public static init(Ljava/lang/String;)Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;
    .locals 2

    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_1

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Account key must not be empty or null. Chat initialization will fail!"

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    if-nez v0, :cond_2

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Initializing Chat SDK"

    invoke-static {v0, v1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat$a;->a()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object v0

    sput-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    :cond_2
    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    iput-object p0, v0, Lcom/zopim/android/sdk/api/ZopimChat;->mAccountKey:Ljava/lang/String;

    sget-object p0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Staring chat configuration"

    invoke-static {p0, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    new-instance p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    invoke-virtual {v0}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    const/4 v1, 0x0

    invoke-direct {p0, v0, v1}, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;-><init>(Lcom/zopim/android/sdk/api/ZopimChat;Lcom/zopim/android/sdk/api/ab;)V

    return-object p0
.end method

.method private static isInitialized()Z
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    if-nez v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Initialization verification failed. Did you initialize?"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    const/4 v0, 0x0

    return v0

    :cond_0
    const/4 v0, 0x1

    return v0
.end method

.method private resendUnsentFiles()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mUnsentFiles:Ljava/util/Queue;

    invoke-interface {v0}, Ljava/util/Queue;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Resending cached unsent files"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mUnsentFiles:Ljava/util/Queue;

    invoke-interface {v0}, Ljava/util/Queue;->poll()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/io/File;

    if-eqz v0, :cond_1

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/api/ZopimChat;->send(Ljava/io/File;)V

    goto :goto_0

    :cond_1
    return-void
.end method

.method private resendUnsentMessages()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mUnsentMessages:Ljava/util/Queue;

    invoke-interface {v0}, Ljava/util/Queue;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Resending cached unsent messages"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mUnsentMessages:Ljava/util/Queue;

    invoke-interface {v0}, Ljava/util/Queue;->poll()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    if-eqz v0, :cond_1

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/api/ZopimChat;->send(Ljava/lang/String;)V

    goto :goto_0

    :cond_1
    return-void
.end method

.method public static declared-synchronized resume(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;
    .locals 5

    const-class v0, Lcom/zopim/android/sdk/api/ZopimChat;

    monitor-enter v0

    :try_start_0
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->isInitialized()Z

    move-result v1

    if-nez v1, :cond_0

    sget-object p0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Have you initialized?"

    invoke-static {p0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    new-instance p0, Lcom/zopim/android/sdk/api/v;

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/v;-><init>()V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object p0

    :cond_0
    if-nez p0, :cond_1

    :try_start_1
    sget-object p0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Chat can not be resumed. Activity must not be null."

    invoke-static {p0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    new-instance p0, Lcom/zopim/android/sdk/api/v;

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/v;-><init>()V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    monitor-exit v0

    return-object p0

    :cond_1
    :try_start_2
    invoke-virtual {p0}, Landroidx/fragment/app/FragmentActivity;->getSupportFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    const-class v2, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    invoke-virtual {v2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v2

    if-nez v2, :cond_2

    sget-object v2, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v3, "Adding chat service binder fragment to the host activity"

    invoke-static {v2, v3}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v1}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v1

    sget-object v2, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    new-instance v3, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    invoke-direct {v3}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;-><init>()V

    iput-object v3, v2, Lcom/zopim/android/sdk/api/ZopimChat;->mChatServiceBinder:Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    sget-object v2, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    iget-object v2, v2, Lcom/zopim/android/sdk/api/ZopimChat;->mChatServiceBinder:Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    const-class v3, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Landroidx/fragment/app/FragmentTransaction;->add(Landroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {v1}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    :cond_2
    sget-object v1, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    iget-object v1, v1, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    if-eqz v1, :cond_3

    sget-object v1, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/api/ZopimChat;->hasEnded()Z

    move-result v1

    if-nez v1, :cond_3

    sget-object p0, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    monitor-exit v0

    return-object p0

    :cond_3
    :try_start_3
    invoke-static {p0}, Lcom/zopim/android/sdk/store/Storage;->init(Landroid/content/Context;)V

    sget-boolean v1, Lcom/zopim/android/sdk/api/ZopimChat;->mDisableVisitorInfo:Z

    if-eqz v1, :cond_4

    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->visitorInfo()Lcom/zopim/android/sdk/store/VisitorInfoStorage;

    move-result-object v1

    invoke-interface {v1}, Lcom/zopim/android/sdk/store/VisitorInfoStorage;->disable()V

    :cond_4
    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->machineId()Lcom/zopim/android/sdk/store/MachineIdStorage;

    move-result-object v1

    invoke-interface {v1}, Lcom/zopim/android/sdk/store/MachineIdStorage;->getMachineId()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_6

    invoke-virtual {v1}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_5

    goto :goto_0

    :cond_5
    new-instance v2, Landroid/content/Intent;

    invoke-virtual {p0}, Landroidx/fragment/app/FragmentActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object v3

    const-class v4, Lcom/zopim/android/sdk/api/ChatService;

    invoke-direct {v2, v3, v4}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v3, "ACCOUNT_KEY"

    sget-object v4, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;

    iget-object v4, v4, Lcom/zopim/android/sdk/api/ZopimChat;->mAccountKey:Ljava/lang/String;

    invoke-virtual {v2, v3, v4}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string v3, "MACHINE_ID"

    invoke-virtual {v2, v3, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string v1, "chat.action.RECONNECT"

    invoke-virtual {v2, v1}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    invoke-virtual {p0}, Landroidx/fragment/app/FragmentActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    invoke-virtual {p0, v2}, Landroid/content/Context;->startService(Landroid/content/Intent;)Landroid/content/ComponentName;

    sget-object p0, Lcom/zopim/android/sdk/api/ZopimChat;->singleton:Lcom/zopim/android/sdk/api/ZopimChat;
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    monitor-exit v0

    return-object p0

    :cond_6
    :goto_0
    :try_start_4
    sget-object p0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Can not resume chat without machine id. Chat either expired or not yet started."

    invoke-static {p0, v1}, Lcom/zopim/android/sdk/api/Logger;->i(Ljava/lang/String;Ljava/lang/String;)V

    new-instance p0, Lcom/zopim/android/sdk/api/v;

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/v;-><init>()V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    monitor-exit v0

    return-object p0

    :catchall_0
    move-exception p0

    monitor-exit v0

    throw p0
.end method

.method public static setVisitorInfo(Lcom/zopim/android/sdk/model/VisitorInfo;)V
    .locals 0

    sput-object p0, Lcom/zopim/android/sdk/api/ZopimChat;->mVisitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

    return-void
.end method

.method public static declared-synchronized start(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;
    .locals 2

    const-class v0, Lcom/zopim/android/sdk/api/ZopimChat;

    monitor-enter v0

    :try_start_0
    new-instance v1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    invoke-direct {v1}, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;-><init>()V

    invoke-virtual {v1, p0}, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->build(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit v0

    return-object p0

    :catchall_0
    move-exception p0

    monitor-exit v0

    throw p0
.end method


# virtual methods
.method public emailTranscript(Ljava/lang/String;)Z
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->emailTranscript(Ljava/lang/String;)Z

    move-result p1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method public endChat()V
    .locals 2

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->endChat()V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatServiceBinder:Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->access$100(Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;)V

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mEnded:Z

    goto :goto_0

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Can not end chat while disconnected from the chat service"

    invoke-static {v0, v1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public getConfig()Lcom/zopim/android/sdk/api/ChatConfig;
    .locals 1

    new-instance v0, Lcom/zopim/android/sdk/api/ab;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/api/ab;-><init>(Lcom/zopim/android/sdk/api/ZopimChat;)V

    return-object v0
.end method

.method public hasEnded()Z
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->hasEnded()Z

    move-result v0

    return v0

    :cond_0
    iget-boolean v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mEnded:Z

    return v0
.end method

.method public resend(Ljava/lang/String;)V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->resend(Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    sget-object p1, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Unable to re-send message at the moment."

    invoke-static {p1, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public resetTimeout()V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->resetTimeout()V

    :cond_0
    return-void
.end method

.method public send(Ljava/io/File;)V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->send(Ljava/io/File;)V

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mUnsentFiles:Ljava/util/Queue;

    invoke-interface {v0, p1}, Ljava/util/Queue;->add(Ljava/lang/Object;)Z

    :goto_0
    return-void
.end method

.method public send(Ljava/lang/String;)V
    .locals 2

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->send(Ljava/lang/String;)V

    goto :goto_0

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Unable to send message at the moment. Caching it for resending."

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mUnsentMessages:Ljava/util/Queue;

    invoke-interface {v0, p1}, Ljava/util/Queue;->add(Ljava/lang/Object;)Z

    :goto_0
    return-void
.end method

.method public sendChatComment(Ljava/lang/String;)V
    .locals 1
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->sendChatComment(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public sendChatRating(Lcom/zopim/android/sdk/model/ChatLog$Rating;)V
    .locals 1
    .param p1    # Lcom/zopim/android/sdk/model/ChatLog$Rating;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->sendChatRating(Lcom/zopim/android/sdk/model/ChatLog$Rating;)V

    :cond_0
    return-void
.end method

.method public sendOfflineMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, p1, p2, p3}, Lcom/zopim/android/sdk/api/Chat;->sendOfflineMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method public setDepartment(Ljava/lang/String;)V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    if-eqz p1, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->setDepartment(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public setEmail(Ljava/lang/String;)V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    if-eqz p1, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->setEmail(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public setName(Ljava/lang/String;)V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    if-eqz p1, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->setName(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public setPhoneNumber(Ljava/lang/String;)V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    if-eqz p1, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat;->mChatService:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->setPhoneNumber(Ljava/lang/String;)V

    :cond_0
    return-void
.end method
