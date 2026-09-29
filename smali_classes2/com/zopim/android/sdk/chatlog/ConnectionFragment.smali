.class public Lcom/zopim/android/sdk/chatlog/ConnectionFragment;
.super Landroidx/fragment/app/Fragment;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;
    }
.end annotation


# static fields
.field private static final LOG_TAG:Ljava/lang/String; = "ConnectionFragment"


# instance fields
.field mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

.field private mHandler:Landroid/os/Handler;

.field private mListener:Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 2

    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->mHandler:Landroid/os/Handler;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/u;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/u;-><init>(Lcom/zopim/android/sdk/chatlog/ConnectionFragment;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    return-void
.end method

.method static synthetic access$000(Lcom/zopim/android/sdk/chatlog/ConnectionFragment;Lcom/zopim/android/sdk/model/Connection;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->updateConnection(Lcom/zopim/android/sdk/model/Connection;)V

    return-void
.end method

.method static synthetic access$100(Lcom/zopim/android/sdk/chatlog/ConnectionFragment;)Landroid/os/Handler;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->mHandler:Landroid/os/Handler;

    return-object p0
.end method

.method private updateConnection(Lcom/zopim/android/sdk/model/Connection;)V
    .locals 1

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Connection must not be null. Can not update visibility."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/chatlog/w;->a:[I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/Connection;->getStatus()Lcom/zopim/android/sdk/model/Connection$Status;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/Connection$Status;->ordinal()I

    move-result p1

    aget p1, v0, p1

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    :pswitch_0
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->mListener:Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->mListener:Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;

    invoke-interface {p1}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;->onConnected()V

    goto :goto_0

    :pswitch_1
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->mListener:Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->mListener:Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;

    invoke-interface {p1}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;->onDisconnected()V

    :cond_1
    :goto_0
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public onAttach(Landroid/app/Activity;)V
    .locals 1

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onAttach(Landroid/app/Activity;)V

    instance-of v0, p1, Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;

    if-eqz v0, :cond_0

    check-cast p1, Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->mListener:Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;

    :cond_0
    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object p1

    instance-of p1, p1, Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;

    if-eqz p1, :cond_1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->getParentFragment()Landroidx/fragment/app/Fragment;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->mListener:Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;

    :cond_1
    return-void
.end method

.method public onStart()V
    .locals 2

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStart()V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/data/DataSource;->getConnection()Lcom/zopim/android/sdk/model/Connection;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->updateConnection(Lcom/zopim/android/sdk/model/Connection;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->addConnectionObserver(Ljava/util/Observer;)V

    return-void
.end method

.method public onStop()V
    .locals 2

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStop()V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteConnectionObserver(Ljava/util/Observer;)V

    return-void
.end method
