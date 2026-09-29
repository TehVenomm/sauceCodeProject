.class public Lcom/zopim/android/sdk/prechat/ZopimChatFragment;
.super Landroidx/fragment/app/Fragment;


# static fields
.field private static final EXTRA_CHAT_CONFIG:Ljava/lang/String; = "CHAT_CONFIG"

.field private static final LOG_TAG:Ljava/lang/String; = "ZopimChatFragment"

.field private static final STATE_CHAT_INITIALIZED:Ljava/lang/String; = "CHAT_INITIALIZED"

.field private static final STATE_COULD_NOT_CONNECT_ERROR_VISIBITLITY:Ljava/lang/String; = "COULD_NOT_CONNECT_ERROR_VISIBILITY"

.field private static final STATE_NO_AGENTS_VISIBITLITY:Ljava/lang/String; = "NO_AGENTS_VISIBILITY"

.field private static final STATE_NO_CONNECTION_ERROR_VISIBITLITY:Ljava/lang/String; = "NO_CONNECTION_ERROR_VISIBILITY"

.field private static final STATE_PROGRESS_VISIBITLITY:Ljava/lang/String; = "PROGRESS_VISIBILITY"


# instance fields
.field private mChat:Lcom/zopim/android/sdk/api/Chat;

.field mChatInitializationTimeout:Landroid/content/BroadcastReceiver;

.field private mChatInitialized:Z

.field private mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

.field mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

.field private mCouldNotConnectErrorView:Landroid/view/View;

.field private mHandler:Landroid/os/Handler;

.field private mNoAgentsView:Landroid/view/View;

.field private mNoConnectionErrorView:Landroid/view/View;

.field mOfflineMessageReceiver:Landroid/content/BroadcastReceiver;

.field private mProgressBar:Landroid/view/View;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 2

    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    const/4 v0, 0x0

    iput-boolean v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatInitialized:Z

    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mHandler:Landroid/os/Handler;

    new-instance v0, Lcom/zopim/android/sdk/prechat/i;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/prechat/i;-><init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    new-instance v0, Lcom/zopim/android/sdk/prechat/j;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/prechat/j;-><init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mOfflineMessageReceiver:Landroid/content/BroadcastReceiver;

    new-instance v0, Lcom/zopim/android/sdk/prechat/k;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/prechat/k;-><init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatInitializationTimeout:Landroid/content/BroadcastReceiver;

    return-void
.end method

.method static synthetic access$000(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Landroid/view/View;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mProgressBar:Landroid/view/View;

    return-object p0
.end method

.method static synthetic access$100(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Z
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->showPreChat()Z

    move-result p0

    return p0
.end method

.method static synthetic access$1000(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->showCouldNotConnectError()V

    return-void
.end method

.method static synthetic access$200(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Lcom/zopim/android/sdk/api/Chat;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    return-object p0
.end method

.method static synthetic access$300(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Landroid/view/View;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mNoConnectionErrorView:Landroid/view/View;

    return-object p0
.end method

.method static synthetic access$400(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Landroid/view/View;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mNoAgentsView:Landroid/view/View;

    return-object p0
.end method

.method static synthetic access$500(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Landroid/view/View;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mCouldNotConnectErrorView:Landroid/view/View;

    return-object p0
.end method

.method static synthetic access$600(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Z
    .locals 0

    iget-boolean p0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatInitialized:Z

    return p0
.end method

.method static synthetic access$602(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;Z)Z
    .locals 0

    iput-boolean p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatInitialized:Z

    return p1
.end method

.method static synthetic access$700(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->onChatInitializationFailed()V

    return-void
.end method

.method static synthetic access$800(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->showNoConnectionError()V

    return-void
.end method

.method static synthetic access$900(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->onChatInitialized()V

    return-void
.end method

.method private close()V
    .locals 1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v0

    invoke-virtual {v0, p0}, Landroidx/fragment/app/FragmentTransaction;->remove(Landroidx/fragment/app/Fragment;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    return-void
.end method

.method public static newInstance(Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;)Lcom/zopim/android/sdk/prechat/ZopimChatFragment;
    .locals 3

    new-instance v0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-direct {v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;-><init>()V

    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "CHAT_CONFIG"

    invoke-virtual {v1, v2, p0}, Landroid/os/Bundle;->putSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->setArguments(Landroid/os/Bundle;)V

    return-object v0
.end method

.method private onChatInitializationFailed()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mHandler:Landroid/os/Handler;

    new-instance v1, Lcom/zopim/android/sdk/prechat/e;

    invoke-direct {v1, p0}, Lcom/zopim/android/sdk/prechat/e;-><init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method private onChatInitialized()V
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Chat initialization completed"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    invoke-interface {v0}, Lcom/zopim/android/sdk/prechat/ChatListener;->onChatInitialized()V

    :cond_0
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/data/DataSource;->getAccount()Lcom/zopim/android/sdk/model/Account;

    move-result-object v0

    if-eqz v0, :cond_1

    sget-object v1, Lcom/zopim/android/sdk/model/Account$Status;->OFFLINE:Lcom/zopim/android/sdk/model/Account$Status;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/Account;->getStatus()Lcom/zopim/android/sdk/model/Account$Status;

    move-result-object v0

    if-ne v1, v0, :cond_1

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->showNoAgents()V

    return-void

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mHandler:Landroid/os/Handler;

    new-instance v1, Lcom/zopim/android/sdk/prechat/d;

    invoke-direct {v1, p0}, Lcom/zopim/android/sdk/prechat/d;-><init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method private setViewVisibility(Landroid/view/View;I)V
    .locals 1

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->LOG_TAG:Ljava/lang/String;

    const-string p2, "View must not be null. Can not apply visibility change"

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    if-eqz p2, :cond_2

    const/4 v0, 0x4

    if-eq p2, v0, :cond_1

    const/16 v0, 0x8

    if-eq p2, v0, :cond_1

    goto :goto_0

    :cond_1
    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    :cond_2
    const/4 p2, 0x0

    invoke-virtual {p1, p2}, Landroid/view/View;->setVisibility(I)V

    :goto_0
    return-void
.end method

.method private showCouldNotConnectError()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mHandler:Landroid/os/Handler;

    new-instance v1, Lcom/zopim/android/sdk/prechat/h;

    invoke-direct {v1, p0}, Lcom/zopim/android/sdk/prechat/h;-><init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method private showField(Lcom/zopim/android/sdk/prechat/PreChatForm$Field;Ljava/lang/String;)Z
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->OPTIONAL_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    sget-object v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    sget-object v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->NOT_REQUIRED:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-nez p1, :cond_0

    if-eqz p2, :cond_1

    invoke-virtual {p2}, Ljava/lang/String;->isEmpty()Z

    move-result p1

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    return p1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    return p1
.end method

.method private showNoAgents()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mHandler:Landroid/os/Handler;

    new-instance v1, Lcom/zopim/android/sdk/prechat/g;

    invoke-direct {v1, p0}, Lcom/zopim/android/sdk/prechat/g;-><init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method private showNoConnectionError()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mHandler:Landroid/os/Handler;

    new-instance v1, Lcom/zopim/android/sdk/prechat/f;

    invoke-direct {v1, p0}, Lcom/zopim/android/sdk/prechat/f;-><init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method

.method private showPreChat()Z
    .locals 6

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->getConfig()Lcom/zopim/android/sdk/api/ChatConfig;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/ChatConfig;->getPreChatForm()Lcom/zopim/android/sdk/prechat/PreChatForm;

    move-result-object v1

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/ChatConfig;->getDepartment()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_0

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/ChatConfig;->getDepartment()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    :cond_0
    invoke-virtual {v1}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getDepartment()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v2

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/ChatConfig;->getDepartment()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, v2, v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->showField(Lcom/zopim/android/sdk/prechat/PreChatForm$Field;Ljava/lang/String;)Z

    move-result v0

    const/4 v2, 0x0

    const/4 v3, 0x1

    if-eqz v0, :cond_1

    const/4 v0, 0x1

    goto :goto_0

    :cond_1
    const/4 v0, 0x0

    :goto_0
    if-nez v0, :cond_3

    invoke-virtual {v1}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getMessage()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v0

    const/4 v4, 0x0

    invoke-direct {p0, v0, v4}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->showField(Lcom/zopim/android/sdk/prechat/PreChatForm$Field;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_2

    goto :goto_1

    :cond_2
    const/4 v0, 0x0

    goto :goto_2

    :cond_3
    :goto_1
    const/4 v0, 0x1

    :goto_2
    iget-object v4, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v4}, Lcom/zopim/android/sdk/api/Chat;->getConfig()Lcom/zopim/android/sdk/api/ChatConfig;

    move-result-object v4

    invoke-interface {v4}, Lcom/zopim/android/sdk/api/ChatConfig;->getVisitorInfo()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v4

    if-eqz v4, :cond_d

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/VisitorInfo;->getEmail()Ljava/lang/String;

    move-result-object v5

    if-eqz v5, :cond_4

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/VisitorInfo;->getEmail()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/String;->isEmpty()Z

    move-result v5

    :cond_4
    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/VisitorInfo;->getName()Ljava/lang/String;

    move-result-object v5

    if-eqz v5, :cond_5

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/VisitorInfo;->getName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/String;->isEmpty()Z

    move-result v5

    :cond_5
    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/VisitorInfo;->getPhoneNumber()Ljava/lang/String;

    move-result-object v5

    if-eqz v5, :cond_6

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/VisitorInfo;->getPhoneNumber()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/String;->isEmpty()Z

    move-result v5

    :cond_6
    if-nez v0, :cond_8

    invoke-virtual {v1}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getEmail()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v0

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/VisitorInfo;->getEmail()Ljava/lang/String;

    move-result-object v5

    invoke-direct {p0, v0, v5}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->showField(Lcom/zopim/android/sdk/prechat/PreChatForm$Field;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_7

    goto :goto_3

    :cond_7
    const/4 v0, 0x0

    goto :goto_4

    :cond_8
    :goto_3
    const/4 v0, 0x1

    :goto_4
    if-nez v0, :cond_a

    invoke-virtual {v1}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getName()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v0

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/VisitorInfo;->getName()Ljava/lang/String;

    move-result-object v5

    invoke-direct {p0, v0, v5}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->showField(Lcom/zopim/android/sdk/prechat/PreChatForm$Field;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_9

    goto :goto_5

    :cond_9
    const/4 v0, 0x0

    goto :goto_6

    :cond_a
    :goto_5
    const/4 v0, 0x1

    :goto_6
    if-nez v0, :cond_c

    invoke-virtual {v1}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getPhoneNumber()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v0

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/VisitorInfo;->getPhoneNumber()Ljava/lang/String;

    move-result-object v1

    invoke-direct {p0, v0, v1}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->showField(Lcom/zopim/android/sdk/prechat/PreChatForm$Field;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_b

    goto :goto_7

    :cond_b
    const/4 v0, 0x0

    goto :goto_8

    :cond_c
    :goto_7
    const/4 v0, 0x1

    :cond_d
    :goto_8
    return v0
.end method


# virtual methods
.method public onActivityCreated(Landroid/os/Bundle;)V
    .locals 2
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onActivityCreated(Landroid/os/Bundle;)V

    const/4 v0, 0x1

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->setHasOptionsMenu(Z)V

    const/4 v0, 0x0

    if-nez p1, :cond_2

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mProgressBar:Landroid/view/View;

    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    if-eqz p1, :cond_1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    const-string v0, "CHAT_CONFIG"

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_1

    :try_start_0
    sget-object p1, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Starting chat with session config"

    invoke-static {p1, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getArguments()Landroid/os/Bundle;

    move-result-object p1

    const-string v0, "CHAT_CONFIG"

    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    if-eqz p1, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->build(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object p1

    goto :goto_0

    :cond_0
    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p1

    invoke-static {p1}, Lcom/zopim/android/sdk/api/ZopimChat;->start(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object p1

    :goto_0
    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;
    :try_end_0
    .catch Ljava/lang/ClassCastException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    sget-object p1, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Unexpected configuration extras. Will ignore session configuration."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_1
    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p1

    invoke-static {p1}, Lcom/zopim/android/sdk/api/ZopimChat;->start(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    goto :goto_1

    :cond_2
    const-string v1, "CHAT_INITIALIZED"

    invoke-virtual {p1, v1, v0}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result p1

    iput-boolean p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatInitialized:Z

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p1

    invoke-static {p1}, Lcom/zopim/android/sdk/api/ZopimChat;->resume(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    sget-object p1, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->LOG_TAG:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Restoring states. chat initialized: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatInitialized:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    :goto_1
    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    if-eqz p1, :cond_3

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {p1, v0}, Lcom/zopim/android/sdk/prechat/ChatListener;->onChatLoaded(Lcom/zopim/android/sdk/api/Chat;)V

    :cond_3
    return-void
.end method

.method public onAttach(Landroid/app/Activity;)V
    .locals 2

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onAttach(Landroid/app/Activity;)V

    instance-of v0, p1, Lcom/zopim/android/sdk/prechat/ChatListener;

    if-eqz v0, :cond_0

    check-cast p1, Lcom/zopim/android/sdk/prechat/ChatListener;

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    goto :goto_0

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->LOG_TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p1}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string p1, " should implement "

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-class p1, Lcom/zopim/android/sdk/prechat/ChatListener;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1

    sget p3, Lcom/zopim/android/sdk/R$layout;->zopim_chat_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDetach()V
    .locals 1

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onDetach()V

    const/4 v0, 0x0

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    return-void
.end method

.method public onOptionsItemSelected(Landroid/view/MenuItem;)Z
    .locals 2

    invoke-interface {p1}, Landroid/view/MenuItem;->getItemId()I

    move-result v0

    const v1, 0x102002c

    if-ne v1, v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->endChat()V

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->close()V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    invoke-interface {v0}, Lcom/zopim/android/sdk/prechat/ChatListener;->onChatEnded()V

    :cond_0
    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onOptionsItemSelected(Landroid/view/MenuItem;)Z

    move-result p1

    return p1
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 2

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onSaveInstanceState(Landroid/os/Bundle;)V

    const-string v0, "CHAT_INITIALIZED"

    iget-boolean v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatInitialized:Z

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v0, "NO_CONNECTION_ERROR_VISIBILITY"

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mNoConnectionErrorView:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->getVisibility()I

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v0, "COULD_NOT_CONNECT_ERROR_VISIBILITY"

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mCouldNotConnectErrorView:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->getVisibility()I

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v0, "NO_AGENTS_VISIBILITY"

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mNoAgentsView:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->getVisibility()I

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v0, "PROGRESS_VISIBILITY"

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mProgressBar:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->getVisibility()I

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    sget-object p1, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->LOG_TAG:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Saving states. chat initialized: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatInitialized:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v1, ", no conn visibility: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mNoConnectionErrorView:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->getVisibility()I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v1, ", progress visibility: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mProgressBar:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->getVisibility()I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public onStart()V
    .locals 4

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStart()V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->addConnectionObserver(Ljava/util/Observer;)V

    new-instance v0, Landroid/content/IntentFilter;

    const-string v1, "zopim.action.CREATE_REQUEST"

    invoke-direct {v0, v1}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    const/16 v1, -0x3e8

    invoke-virtual {v0, v1}, Landroid/content/IntentFilter;->setPriority(I)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v2

    iget-object v3, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mOfflineMessageReceiver:Landroid/content/BroadcastReceiver;

    invoke-virtual {v2, v3, v0}, Landroidx/fragment/app/FragmentActivity;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    new-instance v0, Landroid/content/IntentFilter;

    const-string v2, "chat.action.INITIALIZATION_TIMEOUT"

    invoke-direct {v0, v2}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/content/IntentFilter;->setPriority(I)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v1

    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatInitializationTimeout:Landroid/content/BroadcastReceiver;

    invoke-virtual {v1, v2, v0}, Landroidx/fragment/app/FragmentActivity;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    return-void
.end method

.method public onStop()V
    .locals 2

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStop()V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mHandler:Landroid/os/Handler;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacksAndMessages(Ljava/lang/Object;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mConnectionObserver:Lcom/zopim/android/sdk/data/observers/ConnectionObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteConnectionObserver(Ljava/util/Observer;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mOfflineMessageReceiver:Landroid/content/BroadcastReceiver;

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentActivity;->unregisterReceiver(Landroid/content/BroadcastReceiver;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mChatInitializationTimeout:Landroid/content/BroadcastReceiver;

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentActivity;->unregisterReceiver(Landroid/content/BroadcastReceiver;)V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 0
    .param p2    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1, p2}, Landroidx/fragment/app/Fragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    const/4 p2, 0x1

    invoke-virtual {p0, p2}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->setHasOptionsMenu(Z)V

    sget p2, Lcom/zopim/android/sdk/R$id;->progress_container:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mProgressBar:Landroid/view/View;

    sget p2, Lcom/zopim/android/sdk/R$id;->no_connection_error:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mNoConnectionErrorView:Landroid/view/View;

    sget p2, Lcom/zopim/android/sdk/R$id;->no_agents:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mNoAgentsView:Landroid/view/View;

    sget p2, Lcom/zopim/android/sdk/R$id;->could_not_connect_error:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mCouldNotConnectErrorView:Landroid/view/View;

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mNoAgentsView:Landroid/view/View;

    sget p2, Lcom/zopim/android/sdk/R$id;->no_agents_button:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/Button;

    new-instance p2, Lcom/zopim/android/sdk/prechat/b;

    invoke-direct {p2, p0}, Lcom/zopim/android/sdk/prechat/b;-><init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V

    invoke-virtual {p1, p2}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-void
.end method

.method public onViewStateRestored(Landroid/os/Bundle;)V
    .locals 5
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onViewStateRestored(Landroid/os/Bundle;)V

    if-eqz p1, :cond_0

    const-string v0, "NO_CONNECTION_ERROR_VISIBILITY"

    const/16 v1, 0x8

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v0

    const-string v2, "COULD_NOT_CONNECT_ERROR_VISIBILITY"

    invoke-virtual {p1, v2, v1}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v2

    const-string v3, "NO_AGENTS_VISIBILITY"

    invoke-virtual {p1, v3, v1}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result v3

    const-string v4, "PROGRESS_VISIBILITY"

    invoke-virtual {p1, v4, v1}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result p1

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mNoConnectionErrorView:Landroid/view/View;

    invoke-direct {p0, v1, v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->setViewVisibility(Landroid/view/View;I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mCouldNotConnectErrorView:Landroid/view/View;

    invoke-direct {p0, v0, v2}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->setViewVisibility(Landroid/view/View;I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mNoAgentsView:Landroid/view/View;

    invoke-direct {p0, v0, v3}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->setViewVisibility(Landroid/view/View;I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->mProgressBar:Landroid/view/View;

    invoke-direct {p0, v0, p1}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->setViewVisibility(Landroid/view/View;I)V

    :cond_0
    return-void
.end method
