.class public Lcom/zopim/android/sdk/api/ChatService;
.super Landroid/app/Service;

# interfaces
.implements Lcom/zopim/android/sdk/api/Chat;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/api/ChatService$LocalBinder;
    }
.end annotation


# static fields
.field static final ACTION_CHAT_RECONNECT:Ljava/lang/String; = "chat.action.RECONNECT"

.field static final EXTRA_ACCOUNT_KEY:Ljava/lang/String; = "ACCOUNT_KEY"

.field static final EXTRA_MACHINE_ID:Ljava/lang/String; = "MACHINE_ID"

.field static final EXTRA_SESSION_CONFIG:Ljava/lang/String; = "SESSION_CONFIG"

.field private static final LOG_TAG:Ljava/lang/String; = "ChatService"

.field private static mChat:Lcom/zopim/android/sdk/api/a;


# instance fields
.field private mChatInitializationTimeout:J

.field private mChatInitialized:Z

.field mChatLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

.field private mChatSessionTimeout:J

.field private final mChatTimeoutReceiver:Lcom/zopim/android/sdk/data/LivechatChatLogPath$ChatTimeoutReceiver;

.field mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

.field private final mConnectivityReceiver:Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;

.field private mDepartment:Ljava/lang/String;

.field private mEnded:Z

.field mKeepAliveRunner:Ljava/util/concurrent/ScheduledFuture;

.field private mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

.field private mReferrer:Ljava/lang/String;

.field private final mServiceBinder:Landroid/os/IBinder;

.field private mTags:[Ljava/lang/String;

.field private mTitle:Ljava/lang/String;

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

.field private mVisitorEmail:Ljava/lang/String;

.field private mVisitorName:Ljava/lang/String;

.field private mVisitorPhoneNumber:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    invoke-direct {p0}, Landroid/app/Service;-><init>()V

    new-instance v0, Ljava/util/concurrent/ConcurrentLinkedQueue;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentLinkedQueue;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mUnsentMessages:Ljava/util/Queue;

    new-instance v0, Ljava/util/concurrent/ConcurrentLinkedQueue;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentLinkedQueue;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mUnsentFiles:Ljava/util/Queue;

    new-instance v0, Lcom/zopim/android/sdk/api/ChatService$LocalBinder;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/api/ChatService$LocalBinder;-><init>(Lcom/zopim/android/sdk/api/ChatService;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mServiceBinder:Landroid/os/IBinder;

    new-instance v0, Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;

    invoke-direct {v0}, Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mConnectivityReceiver:Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;

    new-instance v0, Lcom/zopim/android/sdk/data/LivechatChatLogPath$ChatTimeoutReceiver;

    invoke-direct {v0}, Lcom/zopim/android/sdk/data/LivechatChatLogPath$ChatTimeoutReceiver;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatTimeoutReceiver:Lcom/zopim/android/sdk/data/LivechatChatLogPath$ChatTimeoutReceiver;

    new-instance v0, Lcom/zopim/android/sdk/api/c;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/api/c;-><init>(Lcom/zopim/android/sdk/api/ChatService;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

    new-instance v0, Lcom/zopim/android/sdk/api/f;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/api/f;-><init>(Lcom/zopim/android/sdk/api/ChatService;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    return-void
.end method

.method static synthetic access$000(Lcom/zopim/android/sdk/api/ChatService;)Z
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->canCommunicate()Z

    move-result p0

    return p0
.end method

.method static synthetic access$100()Lcom/zopim/android/sdk/api/a;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    return-object v0
.end method

.method static synthetic access$1000(Lcom/zopim/android/sdk/api/ChatService;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/api/ChatService;->mVisitorName:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$200()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$300(Lcom/zopim/android/sdk/api/ChatService;)Z
    .locals 0

    iget-boolean p0, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatInitialized:Z

    return p0
.end method

.method static synthetic access$400(Lcom/zopim/android/sdk/api/ChatService;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->onChatInitialized()V

    return-void
.end method

.method static synthetic access$500(Lcom/zopim/android/sdk/api/ChatService;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/api/ChatService;->mDepartment:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$600(Lcom/zopim/android/sdk/api/ChatService;)[Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/api/ChatService;->mTags:[Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$700(Lcom/zopim/android/sdk/api/ChatService;)Lcom/zopim/android/sdk/prechat/PreChatForm;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/api/ChatService;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    return-object p0
.end method

.method static synthetic access$800(Lcom/zopim/android/sdk/api/ChatService;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/api/ChatService;->mVisitorPhoneNumber:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$900(Lcom/zopim/android/sdk/api/ChatService;)Ljava/lang/String;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/api/ChatService;->mVisitorEmail:Ljava/lang/String;

    return-object p0
.end method

.method private canCommunicate()Z
    .locals 2

    iget-boolean v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatInitialized:Z

    if-eqz v0, :cond_1

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/data/DataSource;->getConnection()Lcom/zopim/android/sdk/model/Connection;

    move-result-object v0

    if-eqz v0, :cond_0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/Connection;->getStatus()Lcom/zopim/android/sdk/model/Connection$Status;

    move-result-object v0

    goto :goto_0

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/model/Connection$Status;->UNKNOWN:Lcom/zopim/android/sdk/model/Connection$Status;

    :goto_0
    sget-object v1, Lcom/zopim/android/sdk/model/Connection$Status;->CONNECTED:Lcom/zopim/android/sdk/model/Connection$Status;

    if-ne v0, v1, :cond_1

    const/4 v0, 0x1

    return v0

    :cond_1
    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Can not communicate at the moment. Chat is either not initialized or not connected."

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->i(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v0, 0x0

    return v0
.end method

.method private configureInitializationTimeout(Z)V
    .locals 6

    const-string v0, "alarm"

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/api/ChatService;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/app/AlarmManager;

    new-instance v1, Landroid/content/Intent;

    const-class v2, Lcom/zopim/android/sdk/api/ChatService;

    invoke-direct {v1, p0, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v2, "chat.action.INITIALIZATION_TIMEOUT"

    invoke-virtual {v1, v2}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    const/4 v2, 0x0

    const/high16 v3, 0x8000000

    invoke-static {p0, v2, v1, v3}, Landroid/app/PendingIntent;->getService(Landroid/content/Context;ILandroid/content/Intent;I)Landroid/app/PendingIntent;

    move-result-object v1

    if-eqz v0, :cond_1

    sget-object v2, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v3, "Alarm manager acquired, scheduling chat initialization timeout"

    invoke-static {v2, v3}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-static {}, Landroid/os/SystemClock;->elapsedRealtime()J

    move-result-wide v2

    iget-wide v4, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatInitializationTimeout:J

    add-long/2addr v2, v4

    if-eqz p1, :cond_0

    const/4 p1, 0x3

    invoke-virtual {v0, p1, v2, v3, v1}, Landroid/app/AlarmManager;->set(IJLandroid/app/PendingIntent;)V

    goto :goto_0

    :cond_0
    invoke-virtual {v0, v1}, Landroid/app/AlarmManager;->cancel(Landroid/app/PendingIntent;)V

    goto :goto_0

    :cond_1
    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Could not get the Alarm manager, will not set chat initialization timeout"

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method private onChatInitialized()V
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Chat initialization completed"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/api/a;->b()V

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatInitialized:Z

    const/4 v0, 0x0

    invoke-direct {p0, v0}, Lcom/zopim/android/sdk/api/ChatService;->configureInitializationTimeout(Z)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/data/DataSource;->getProfile()Lcom/zopim/android/sdk/model/Profile;

    move-result-object v0

    if-eqz v0, :cond_0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/Profile;->getMachineId()Ljava/lang/String;

    move-result-object v0

    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->machineId()Lcom/zopim/android/sdk/store/MachineIdStorage;

    move-result-object v1

    invoke-interface {v1, v0}, Lcom/zopim/android/sdk/store/MachineIdStorage;->setMachineId(Ljava/lang/String;)V

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mVisitorEmail:Ljava/lang/String;

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/api/ChatService;->setEmail(Ljava/lang/String;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mVisitorName:Ljava/lang/String;

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/api/ChatService;->setName(Ljava/lang/String;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mVisitorPhoneNumber:Ljava/lang/String;

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/api/ChatService;->setPhoneNumber(Ljava/lang/String;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mDepartment:Ljava/lang/String;

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/api/ChatService;->setDepartment(Ljava/lang/String;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mTags:[Ljava/lang/String;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mTags:[Ljava/lang/String;

    array-length v0, v0

    if-lez v0, :cond_1

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    iget-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mTags:[Ljava/lang/String;

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/api/a;->a([Ljava/lang/String;)V

    :cond_1
    return-void
.end method

.method private prepareTimeout()V
    .locals 6

    const-string v0, "alarm"

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/api/ChatService;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/app/AlarmManager;

    new-instance v1, Landroid/content/Intent;

    const-class v2, Lcom/zopim/android/sdk/api/ChatService;

    invoke-direct {v1, p0, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v2, "chat.action.TIMEOUT"

    invoke-virtual {v1, v2}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    const/4 v2, 0x0

    const/high16 v3, 0x8000000

    invoke-static {p0, v2, v1, v3}, Landroid/app/PendingIntent;->getService(Landroid/content/Context;ILandroid/content/Intent;I)Landroid/app/PendingIntent;

    move-result-object v1

    if-eqz v0, :cond_0

    sget-object v2, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v3, "Alarm manager acquired, scheduling chat timeout"

    invoke-static {v2, v3}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-static {}, Landroid/os/SystemClock;->elapsedRealtime()J

    move-result-wide v2

    iget-wide v4, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatSessionTimeout:J

    add-long/2addr v2, v4

    const/4 v4, 0x3

    invoke-virtual {v0, v4, v2, v3, v1}, Landroid/app/AlarmManager;->set(IJLandroid/app/PendingIntent;)V

    goto :goto_0

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Could not get the Alarm manager, will not set chat timeout"

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method


# virtual methods
.method public emailTranscript(Ljava/lang/String;)Z
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/a;->emailTranscript(Ljava/lang/String;)Z

    move-result p1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method public endChat()V
    .locals 2

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/api/a;->endChat()V

    :cond_0
    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mEnded:Z

    iget-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mKeepAliveRunner:Ljava/util/concurrent/ScheduledFuture;

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mKeepAliveRunner:Ljava/util/concurrent/ScheduledFuture;

    invoke-interface {v1, v0}, Ljava/util/concurrent/ScheduledFuture;->cancel(Z)Z

    :cond_1
    const/4 v0, 0x0

    iput-boolean v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatInitialized:Z

    sget-object v0, Lcom/zopim/android/sdk/attachment/SdkCache;->INSTANCE:Lcom/zopim/android/sdk/attachment/SdkCache;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ChatService;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/attachment/SdkCache;->deleteCache(Landroid/content/Context;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ChatService;->stopSelf()V

    return-void
.end method

.method protected finalize()V
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Service cleared from memory by GC"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-super {p0}, Ljava/lang/Object;->finalize()V

    return-void
.end method

.method public getConfig()Lcom/zopim/android/sdk/api/ChatConfig;
    .locals 1

    new-instance v0, Lcom/zopim/android/sdk/api/g;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/api/g;-><init>(Lcom/zopim/android/sdk/api/ChatService;)V

    return-object v0
.end method

.method public hasEnded()Z
    .locals 1

    iget-boolean v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mEnded:Z

    return v0
.end method

.method public onBind(Landroid/content/Intent;)Landroid/os/IBinder;
    .locals 0

    iget-object p1, p0, Lcom/zopim/android/sdk/api/ChatService;->mServiceBinder:Landroid/os/IBinder;

    return-object p1
.end method

.method public onCreate()V
    .locals 3

    invoke-super {p0}, Landroid/app/Service;->onCreate()V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->addConnectionObserver(Ljava/util/Observer;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->addChatLogObserver(Ljava/util/Observer;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mConnectivityReceiver:Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;

    new-instance v1, Landroid/content/IntentFilter;

    const-string v2, "android.net.conn.CONNECTIVITY_CHANGE"

    invoke-direct {v1, v2}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    invoke-virtual {p0, v0, v1}, Lcom/zopim/android/sdk/api/ChatService;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatTimeoutReceiver:Lcom/zopim/android/sdk/data/LivechatChatLogPath$ChatTimeoutReceiver;

    new-instance v1, Landroid/content/IntentFilter;

    const-string v2, "chat.action.TIMEOUT"

    invoke-direct {v1, v2}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    invoke-virtual {p0, v0, v1}, Lcom/zopim/android/sdk/api/ChatService;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Service created"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method public onDestroy()V
    .locals 2

    invoke-super {p0}, Landroid/app/Service;->onDestroy()V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mKeepAliveRunner:Ljava/util/concurrent/ScheduledFuture;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mKeepAliveRunner:Ljava/util/concurrent/ScheduledFuture;

    const/4 v1, 0x1

    invoke-interface {v0, v1}, Ljava/util/concurrent/ScheduledFuture;->cancel(Z)Z

    :cond_0
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteConnectionObserver(Ljava/util/Observer;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteChatLogObserver(Ljava/util/Observer;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mConnectivityReceiver:Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/api/ChatService;->unregisterReceiver(Landroid/content/BroadcastReceiver;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatTimeoutReceiver:Lcom/zopim/android/sdk/data/LivechatChatLogPath$ChatTimeoutReceiver;

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/api/ChatService;->unregisterReceiver(Landroid/content/BroadcastReceiver;)V

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Chat service destroyed"

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method public onStartCommand(Landroid/content/Intent;II)I
    .locals 10

    const/4 p2, 0x1

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string p3, "Service restarted by the system, will not reinitialize the web binder"

    invoke-static {p1, p3}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    return p2

    :cond_0
    invoke-virtual {p1}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object p3

    const-string v0, "chat.action.INITIALIZATION_TIMEOUT"

    invoke-virtual {v0, p3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x2

    if-eqz v0, :cond_2

    iget-boolean p1, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatInitialized:Z

    if-eqz p1, :cond_1

    return p2

    :cond_1
    new-instance p1, Landroid/content/Intent;

    invoke-direct {p1}, Landroid/content/Intent;-><init>()V

    const-string p2, "chat.action.INITIALIZATION_TIMEOUT"

    invoke-virtual {p1, p2}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ChatService;->getApplicationContext()Landroid/content/Context;

    move-result-object p2

    invoke-virtual {p2}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    const/4 p2, 0x0

    invoke-virtual {p0, p1, p2}, Lcom/zopim/android/sdk/api/ChatService;->sendOrderedBroadcast(Landroid/content/Intent;Ljava/lang/String;)V

    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string p2, "Chat initialization has timed out. Ending chat session."

    invoke-static {p1, p2}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ChatService;->endChat()V

    return v1

    :cond_2
    const-string v0, "chat.action.TIMEOUT"

    invoke-virtual {v0, p3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_3

    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string p2, "Chat has timed out. Ending chat session."

    invoke-static {p1, p2}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    new-instance p1, Landroid/content/Intent;

    invoke-direct {p1}, Landroid/content/Intent;-><init>()V

    const-string p2, "chat.action.TIMEOUT"

    invoke-virtual {p1, p2}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ChatService;->getApplicationContext()Landroid/content/Context;

    move-result-object p2

    invoke-virtual {p2}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/api/ChatService;->sendBroadcast(Landroid/content/Intent;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ChatService;->endChat()V

    return v1

    :cond_3
    const-string v0, "chat.action.RECONNECT"

    invoke-virtual {v0, p3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p3

    if-eqz p3, :cond_4

    iget-boolean p3, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatInitialized:Z

    if-eqz p3, :cond_4

    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string p3, "Chat service already running and initialized, no need to re-initialize the web widget"

    invoke-static {p1, p3}, Lcom/zopim/android/sdk/api/Logger;->i(Ljava/lang/String;Ljava/lang/String;)V

    return p2

    :cond_4
    new-instance p3, Lcom/zopim/android/sdk/api/x;

    invoke-direct {p3, p0}, Lcom/zopim/android/sdk/api/x;-><init>(Landroid/content/Context;)V

    sput-object p3, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    const/4 p3, 0x0

    iput-boolean p3, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatInitialized:Z

    iput-boolean p3, p0, Lcom/zopim/android/sdk/api/ChatService;->mEnded:Z

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object p3

    invoke-interface {p3}, Lcom/zopim/android/sdk/data/DataSource;->clear()V

    const-string p3, "ACCOUNT_KEY"

    invoke-virtual {p1, p3}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p3

    const-string v0, "MACHINE_ID"

    invoke-virtual {p1, v0}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v2, "SESSION_CONFIG"

    invoke-virtual {p1, v2}, Landroid/content/Intent;->getSerializableExtra(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object p1

    if-nez p3, :cond_5

    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string p2, "Can not start chat service without account id. Have you passed account id as extras?"

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ChatService;->stopSelf()V

    return v1

    :cond_5
    instance-of v1, p1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    if-eqz v1, :cond_7

    check-cast p1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    iget-object v1, p1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->visitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

    if-eqz v1, :cond_6

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/VisitorInfo;->getName()Ljava/lang/String;

    move-result-object v2

    iput-object v2, p0, Lcom/zopim/android/sdk/api/ChatService;->mVisitorName:Ljava/lang/String;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/VisitorInfo;->getEmail()Ljava/lang/String;

    move-result-object v2

    iput-object v2, p0, Lcom/zopim/android/sdk/api/ChatService;->mVisitorEmail:Ljava/lang/String;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/model/VisitorInfo;->getPhoneNumber()Ljava/lang/String;

    move-result-object v1

    iput-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mVisitorPhoneNumber:Ljava/lang/String;

    :cond_6
    iget-object v1, p1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->department:Ljava/lang/String;

    iput-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mDepartment:Ljava/lang/String;

    iget-object v1, p1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->preChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    iput-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    iget-object v1, p1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->title:Ljava/lang/String;

    iput-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mTitle:Ljava/lang/String;

    iget-object v1, p1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->referrer:Ljava/lang/String;

    iput-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mReferrer:Ljava/lang/String;

    iget-object v1, p1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->tags:[Ljava/lang/String;

    iput-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mTags:[Ljava/lang/String;

    iget-object v1, p1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->initializationTimeout:Ljava/lang/Long;

    invoke-virtual {v1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    iput-wide v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatInitializationTimeout:J

    iget-object p1, p1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->sessionTimeout:Ljava/lang/Long;

    invoke-virtual {p1}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    iput-wide v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatSessionTimeout:J

    goto :goto_0

    :cond_7
    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Error getting chat session configuration. Chat will not be configured."

    invoke-static {p1, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    iget-wide v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatInitializationTimeout:J

    const-wide/16 v3, 0x0

    cmp-long p1, v1, v3

    if-gez p1, :cond_8

    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Configured chat initialization timeout is below the minimum threshold. Will use default timeout"

    invoke-static {p1, v1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    sget-wide v1, Lcom/zopim/android/sdk/api/ChatSession;->DEFAULT_CHAT_INITIALIZATION_TIMEOUT:J

    iput-wide v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatInitializationTimeout:J

    :cond_8
    iget-wide v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatSessionTimeout:J

    cmp-long p1, v1, v3

    if-gez p1, :cond_9

    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Configured chat session timeout is below the minimum threshold. Will use default timeout"

    invoke-static {p1, v1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    sget-wide v1, Lcom/zopim/android/sdk/api/ChatSession;->DEFAULT_CHAT_SESSION_TIMEOUT:J

    iput-wide v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mChatSessionTimeout:J

    :cond_9
    invoke-direct {p0, p2}, Lcom/zopim/android/sdk/api/ChatService;->configureInitializationTimeout(Z)V

    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadScheduledExecutor()Ljava/util/concurrent/ScheduledExecutorService;

    move-result-object v3

    new-instance v4, Lcom/zopim/android/sdk/api/b;

    invoke-direct {v4, p0}, Lcom/zopim/android/sdk/api/b;-><init>(Lcom/zopim/android/sdk/api/ChatService;)V

    const-wide/16 v5, 0x1

    const-wide/16 v7, 0x1

    sget-object v9, Ljava/util/concurrent/TimeUnit;->MINUTES:Ljava/util/concurrent/TimeUnit;

    invoke-interface/range {v3 .. v9}, Ljava/util/concurrent/ScheduledExecutorService;->scheduleAtFixedRate(Ljava/lang/Runnable;JJLjava/util/concurrent/TimeUnit;)Ljava/util/concurrent/ScheduledFuture;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ChatService;->mKeepAliveRunner:Ljava/util/concurrent/ScheduledFuture;

    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    iget-object v1, p0, Lcom/zopim/android/sdk/api/ChatService;->mTitle:Ljava/lang/String;

    iget-object v2, p0, Lcom/zopim/android/sdk/api/ChatService;->mReferrer:Ljava/lang/String;

    invoke-virtual {p1, p3, v0, v1, v2}, Lcom/zopim/android/sdk/api/a;->a(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string p3, "Chat service started"

    invoke-static {p1, p3}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    return p2
.end method

.method public resend(Ljava/lang/String;)V
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/a;->resend(Ljava/lang/String;)V

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->prepareTimeout()V

    goto :goto_0

    :cond_0
    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Unable to re-send message at the moment."

    invoke-static {p1, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public resetTimeout()V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->prepareTimeout()V

    return-void
.end method

.method public send(Ljava/io/File;)V
    .locals 3

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_1

    sget-object v0, Lcom/zopim/android/sdk/api/FileTransfers;->INSTANCE:Lcom/zopim/android/sdk/api/FileTransfers;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/FileTransfers;->find(Ljava/io/File;)Lcom/zopim/android/sdk/api/FileTransfers$a;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v1, v0, Lcom/zopim/android/sdk/api/FileTransfers$a;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    sget-object v2, Lcom/zopim/android/sdk/api/FileTransfers$b;->e:Lcom/zopim/android/sdk/api/FileTransfers$b;

    if-ne v1, v2, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Re-sending file"

    invoke-static {p1, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    sget-object p1, Lcom/zopim/android/sdk/api/FileTransfers$b;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    iput-object p1, v0, Lcom/zopim/android/sdk/api/FileTransfers$a;->b:Lcom/zopim/android/sdk/api/FileTransfers$b;

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatChatLogPath;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/data/LivechatChatLogPath;->broadcast()V

    goto :goto_0

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/a;->send(Ljava/io/File;)V

    goto :goto_0

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mUnsentFiles:Ljava/util/Queue;

    invoke-interface {v0, p1}, Ljava/util/Queue;->add(Ljava/lang/Object;)Z

    :goto_0
    return-void
.end method

.method public send(Ljava/lang/String;)V
    .locals 2

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/a;->send(Ljava/lang/String;)V

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->prepareTimeout()V

    goto :goto_0

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Unable to send message at the moment. Caching it for resending."

    invoke-static {v0, v1}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService;->mUnsentMessages:Ljava/util/Queue;

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

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/a;->sendChatComment(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public sendChatRating(Lcom/zopim/android/sdk/model/ChatLog$Rating;)V
    .locals 1
    .param p1    # Lcom/zopim/android/sdk/model/ChatLog$Rating;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/a;->sendChatRating(Lcom/zopim/android/sdk/model/ChatLog$Rating;)V

    :cond_0
    return-void
.end method

.method public sendOfflineMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z
    .locals 1

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ChatService;->canCommunicate()Z

    move-result v0

    if-eqz v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0, p1, p2, p3}, Lcom/zopim/android/sdk/api/a;->sendOfflineMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result p1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method public setDepartment(Ljava/lang/String;)V
    .locals 1

    if-eqz p1, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/a;->setDepartment(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public setEmail(Ljava/lang/String;)V
    .locals 1

    if-eqz p1, :cond_1

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/a;->setEmail(Ljava/lang/String;)V

    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->visitorInfo()Lcom/zopim/android/sdk/store/VisitorInfoStorage;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/store/VisitorInfoStorage;->getVisitorInfo()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    if-nez v0, :cond_0

    new-instance v0, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    invoke-direct {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;-><init>()V

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->email(Ljava/lang/String;)Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->build()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    goto :goto_0

    :cond_0
    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/model/VisitorInfo;->setEmail(Ljava/lang/String;)V

    :goto_0
    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->visitorInfo()Lcom/zopim/android/sdk/store/VisitorInfoStorage;

    move-result-object p1

    invoke-interface {p1, v0}, Lcom/zopim/android/sdk/store/VisitorInfoStorage;->setVisitorInfo(Lcom/zopim/android/sdk/model/VisitorInfo;)V

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->setVisitorInfo(Lcom/zopim/android/sdk/model/VisitorInfo;)V

    :cond_1
    return-void
.end method

.method public setName(Ljava/lang/String;)V
    .locals 1

    if-eqz p1, :cond_1

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/a;->setName(Ljava/lang/String;)V

    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->visitorInfo()Lcom/zopim/android/sdk/store/VisitorInfoStorage;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/store/VisitorInfoStorage;->getVisitorInfo()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    if-nez v0, :cond_0

    new-instance v0, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    invoke-direct {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;-><init>()V

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->name(Ljava/lang/String;)Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->build()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    goto :goto_0

    :cond_0
    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/model/VisitorInfo;->setName(Ljava/lang/String;)V

    :goto_0
    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->visitorInfo()Lcom/zopim/android/sdk/store/VisitorInfoStorage;

    move-result-object p1

    invoke-interface {p1, v0}, Lcom/zopim/android/sdk/store/VisitorInfoStorage;->setVisitorInfo(Lcom/zopim/android/sdk/model/VisitorInfo;)V

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->setVisitorInfo(Lcom/zopim/android/sdk/model/VisitorInfo;)V

    :cond_1
    return-void
.end method

.method public setPhoneNumber(Ljava/lang/String;)V
    .locals 1

    if-eqz p1, :cond_1

    sget-object v0, Lcom/zopim/android/sdk/api/ChatService;->mChat:Lcom/zopim/android/sdk/api/a;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/api/a;->setPhoneNumber(Ljava/lang/String;)V

    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->visitorInfo()Lcom/zopim/android/sdk/store/VisitorInfoStorage;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/store/VisitorInfoStorage;->getVisitorInfo()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    if-nez v0, :cond_0

    new-instance v0, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    invoke-direct {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;-><init>()V

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->phoneNumber(Ljava/lang/String;)Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->build()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    goto :goto_0

    :cond_0
    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/model/VisitorInfo;->setPhoneNumber(Ljava/lang/String;)V

    :goto_0
    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->visitorInfo()Lcom/zopim/android/sdk/store/VisitorInfoStorage;

    move-result-object p1

    invoke-interface {p1, v0}, Lcom/zopim/android/sdk/store/VisitorInfoStorage;->setVisitorInfo(Lcom/zopim/android/sdk/model/VisitorInfo;)V

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->setVisitorInfo(Lcom/zopim/android/sdk/model/VisitorInfo;)V

    :cond_1
    return-void
.end method
