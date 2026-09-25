.class public Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;
.super Lcom/zopim/android/sdk/api/i;

# interfaces
.implements Ljava/io/Serializable;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/api/ZopimChat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "SessionConfig"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/zopim/android/sdk/api/i<",
        "Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;",
        ">;",
        "Ljava/io/Serializable;"
    }
.end annotation


# static fields
.field private static final serialVersionUID:J = -0x3c469c43452d3728L


# instance fields
.field initializationTimeout:Ljava/lang/Long;

.field sessionTimeout:Ljava/lang/Long;

.field visitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;


# direct methods
.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/i;-><init>()V

    return-void
.end method


# virtual methods
.method public build(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;
    .locals 3

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1400()Z

    move-result v0

    if-nez v0, :cond_0

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$200()Ljava/lang/String;

    move-result-object p1

    const-string v0, "Have you initialized?"

    invoke-static {p1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    new-instance p1, Lcom/zopim/android/sdk/api/v;

    invoke-direct {p1}, Lcom/zopim/android/sdk/api/v;-><init>()V

    return-object p1

    :cond_0
    if-nez p1, :cond_1

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$200()Ljava/lang/String;

    move-result-object p1

    const-string v0, "Can not build the chat. Activity must not be null."

    invoke-static {p1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    new-instance p1, Lcom/zopim/android/sdk/api/v;

    invoke-direct {p1}, Lcom/zopim/android/sdk/api/v;-><init>()V

    return-object p1

    :cond_1
    invoke-static {p1}, Lcom/zopim/android/sdk/store/Storage;->init(Landroid/content/Context;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1100()Z

    move-result v0

    if-eqz v0, :cond_2

    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->visitorInfo()Lcom/zopim/android/sdk/store/VisitorInfoStorage;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/store/VisitorInfoStorage;->disable()V

    :cond_2
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object v0

    const/4 v1, 0x0

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1602(Lcom/zopim/android/sdk/api/ZopimChat;Z)Z

    invoke-virtual {p1}, Landroidx/fragment/app/FragmentActivity;->getSupportFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    const-class v1, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v1

    if-nez v1, :cond_e

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$200()Ljava/lang/String;

    move-result-object v1

    const-string v2, "Adding chat service binder fragment to the host activity"

    invoke-static {v1, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v0

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object v1

    new-instance v2, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    invoke-direct {v2}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;-><init>()V

    invoke-static {v1, v2}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1702(Lcom/zopim/android/sdk/api/ZopimChat;Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;)Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object v1

    invoke-static {v1}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1700(Lcom/zopim/android/sdk/api/ZopimChat;)Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    move-result-object v1

    const-class v2, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    invoke-virtual {v2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroidx/fragment/app/FragmentTransaction;->add(Landroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1800()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    if-eqz v0, :cond_3

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1800()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    :goto_0
    iput-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->visitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

    goto :goto_1

    :cond_3
    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->visitorInfo()Lcom/zopim/android/sdk/store/VisitorInfoStorage;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/store/VisitorInfoStorage;->getVisitorInfo()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    goto :goto_0

    :goto_1
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->department:Ljava/lang/String;

    if-eqz v0, :cond_4

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->department:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_5

    :cond_4
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object v0

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$300(Lcom/zopim/android/sdk/api/ZopimChat;)Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->department:Ljava/lang/String;

    :cond_5
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->preChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    if-nez v0, :cond_6

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object v0

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$400(Lcom/zopim/android/sdk/api/ZopimChat;)Lcom/zopim/android/sdk/prechat/PreChatForm;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->preChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    :cond_6
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->title:Ljava/lang/String;

    if-nez v0, :cond_8

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$600()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_7

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$600()Ljava/lang/String;

    move-result-object v0

    :goto_2
    iput-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->title:Ljava/lang/String;

    goto :goto_3

    :cond_7
    invoke-static {p1}, Lcom/zopim/android/sdk/util/AppInfo;->getApplicationName(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v0

    goto :goto_2

    :cond_8
    :goto_3
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->referrer:Ljava/lang/String;

    if-nez v0, :cond_a

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$700()Ljava/lang/String;

    move-result-object v0

    if-eqz v0, :cond_9

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$700()Ljava/lang/String;

    move-result-object v0

    :goto_4
    iput-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->referrer:Ljava/lang/String;

    goto :goto_5

    :cond_9
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {p1}, Lcom/zopim/android/sdk/util/AppInfo;->getApplicationName(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ", v"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p1}, Lcom/zopim/android/sdk/util/AppInfo;->getApplicationVersionName(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    goto :goto_4

    :cond_a
    :goto_5
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$800()Ljava/lang/Long;

    move-result-object v0

    if-eqz v0, :cond_b

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$800()Ljava/lang/Long;

    move-result-object v0

    :goto_6
    iput-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->initializationTimeout:Ljava/lang/Long;

    goto :goto_7

    :cond_b
    sget-wide v0, Lcom/zopim/android/sdk/api/ChatSession;->DEFAULT_CHAT_INITIALIZATION_TIMEOUT:J

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    goto :goto_6

    :goto_7
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1000()Ljava/lang/Long;

    move-result-object v0

    if-eqz v0, :cond_c

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1000()Ljava/lang/Long;

    move-result-object v0

    :goto_8
    iput-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->sessionTimeout:Ljava/lang/Long;

    goto :goto_9

    :cond_c
    sget-wide v0, Lcom/zopim/android/sdk/api/ChatSession;->DEFAULT_CHAT_SESSION_TIMEOUT:J

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    goto :goto_8

    :goto_9
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object v0

    invoke-static {v0, p0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$002(Lcom/zopim/android/sdk/api/ZopimChat;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;)Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    new-instance v0, Landroid/content/Intent;

    invoke-virtual {p1}, Landroidx/fragment/app/FragmentActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    const-class v2, Lcom/zopim/android/sdk/api/ChatService;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "ACCOUNT_KEY"

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object v2

    invoke-static {v2}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1900(Lcom/zopim/android/sdk/api/ZopimChat;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string v1, "SESSION_CONFIG"

    invoke-virtual {v0, v1, p0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    invoke-static {}, Lcom/zopim/android/sdk/store/Storage;->machineId()Lcom/zopim/android/sdk/store/MachineIdStorage;

    move-result-object v1

    invoke-interface {v1}, Lcom/zopim/android/sdk/store/MachineIdStorage;->getMachineId()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_d

    const-string v2, "MACHINE_ID"

    invoke-virtual {v0, v2, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    :cond_d
    invoke-virtual {p1}, Landroidx/fragment/app/FragmentActivity;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    invoke-virtual {p1, v0}, Landroid/content/Context;->startService(Landroid/content/Intent;)Landroid/content/ComponentName;

    goto :goto_a

    :cond_e
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$200()Ljava/lang/String;

    move-result-object p1

    const-string v0, "Activity is already bound to Chat Service, skipping service start"

    invoke-static {p1, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :goto_a
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object p1

    return-object p1
.end method
