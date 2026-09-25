.class public Lcom/facebook/CustomTabMainActivity;
.super Landroid/app/Activity;
.source "CustomTabMainActivity.java"


# static fields
.field public static final EXTRA_ACTION:Ljava/lang/String;

.field public static final EXTRA_CHROME_PACKAGE:Ljava/lang/String;

.field public static final EXTRA_PARAMS:Ljava/lang/String;

.field public static final EXTRA_URL:Ljava/lang/String;

.field public static final REFRESH_ACTION:Ljava/lang/String;


# instance fields
.field private redirectReceiver:Landroid/content/BroadcastReceiver;

.field private shouldCloseCustomTab:Z


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 39
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-class v1, Lcom/facebook/CustomTabMainActivity;

    .line 40
    invoke-virtual {v1}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ".extra_action"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/facebook/CustomTabMainActivity;->EXTRA_ACTION:Ljava/lang/String;

    .line 41
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-class v1, Lcom/facebook/CustomTabMainActivity;

    .line 42
    invoke-virtual {v1}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ".extra_params"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/facebook/CustomTabMainActivity;->EXTRA_PARAMS:Ljava/lang/String;

    .line 43
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-class v1, Lcom/facebook/CustomTabMainActivity;

    .line 44
    invoke-virtual {v1}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ".extra_chromePackage"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/facebook/CustomTabMainActivity;->EXTRA_CHROME_PACKAGE:Ljava/lang/String;

    .line 45
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-class v1, Lcom/facebook/CustomTabMainActivity;

    .line 46
    invoke-virtual {v1}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ".extra_url"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/facebook/CustomTabMainActivity;->EXTRA_URL:Ljava/lang/String;

    .line 47
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-class v1, Lcom/facebook/CustomTabMainActivity;

    .line 48
    invoke-virtual {v1}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, ".action_refresh"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/facebook/CustomTabMainActivity;->REFRESH_ACTION:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .line 38
    invoke-direct {p0}, Landroid/app/Activity;-><init>()V

    const/4 v0, 0x1

    .line 50
    iput-boolean v0, p0, Lcom/facebook/CustomTabMainActivity;->shouldCloseCustomTab:Z

    return-void
.end method

.method private static parseResponseUri(Ljava/lang/String;)Landroid/os/Bundle;
    .locals 1

    .line 135
    invoke-static {p0}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p0

    .line 136
    invoke-virtual {p0}, Landroid/net/Uri;->getQuery()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/facebook/internal/Utility;->parseUrlQueryString(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v0

    .line 137
    invoke-virtual {p0}, Landroid/net/Uri;->getFragment()Ljava/lang/String;

    move-result-object p0

    invoke-static {p0}, Lcom/facebook/internal/Utility;->parseUrlQueryString(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object p0

    invoke-virtual {v0, p0}, Landroid/os/Bundle;->putAll(Landroid/os/Bundle;)V

    return-object v0
.end method

.method private sendResult(ILandroid/content/Intent;)V
    .locals 3

    .line 121
    invoke-static {p0}, Landroidx/localbroadcastmanager/content/LocalBroadcastManager;->getInstance(Landroid/content/Context;)Landroidx/localbroadcastmanager/content/LocalBroadcastManager;

    move-result-object v0

    iget-object v1, p0, Lcom/facebook/CustomTabMainActivity;->redirectReceiver:Landroid/content/BroadcastReceiver;

    invoke-virtual {v0, v1}, Landroidx/localbroadcastmanager/content/LocalBroadcastManager;->unregisterReceiver(Landroid/content/BroadcastReceiver;)V

    const/4 v0, 0x0

    if-eqz p2, :cond_2

    .line 123
    sget-object v1, Lcom/facebook/CustomTabMainActivity;->EXTRA_URL:Ljava/lang/String;

    invoke-virtual {p2, v1}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 124
    invoke-static {v1}, Lcom/facebook/CustomTabMainActivity;->parseResponseUri(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v1

    goto :goto_0

    :cond_0
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    .line 125
    :goto_0
    invoke-virtual {p0}, Lcom/facebook/CustomTabMainActivity;->getIntent()Landroid/content/Intent;

    move-result-object v2

    invoke-static {v2, v1, v0}, Lcom/facebook/internal/NativeProtocol;->createProtocolResultIntent(Landroid/content/Intent;Landroid/os/Bundle;Lcom/facebook/FacebookException;)Landroid/content/Intent;

    move-result-object v0

    if-eqz v0, :cond_1

    move-object p2, v0

    .line 127
    :cond_1
    invoke-virtual {p0, p1, p2}, Lcom/facebook/CustomTabMainActivity;->setResult(ILandroid/content/Intent;)V

    goto :goto_1

    .line 129
    :cond_2
    invoke-virtual {p0}, Lcom/facebook/CustomTabMainActivity;->getIntent()Landroid/content/Intent;

    move-result-object p2

    invoke-static {p2, v0, v0}, Lcom/facebook/internal/NativeProtocol;->createProtocolResultIntent(Landroid/content/Intent;Landroid/os/Bundle;Lcom/facebook/FacebookException;)Landroid/content/Intent;

    move-result-object p2

    invoke-virtual {p0, p1, p2}, Lcom/facebook/CustomTabMainActivity;->setResult(ILandroid/content/Intent;)V

    .line 131
    :goto_1
    invoke-virtual {p0}, Lcom/facebook/CustomTabMainActivity;->finish()V

    return-void
.end method


# virtual methods
.method protected onCreate(Landroid/os/Bundle;)V
    .locals 4

    .line 55
    invoke-super {p0, p1}, Landroid/app/Activity;->onCreate(Landroid/os/Bundle;)V

    .line 58
    sget-object v0, Lcom/facebook/CustomTabActivity;->CUSTOM_TAB_REDIRECT_ACTION:Ljava/lang/String;

    invoke-virtual {p0}, Lcom/facebook/CustomTabMainActivity;->getIntent()Landroid/content/Intent;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 59
    invoke-virtual {p0, v1}, Lcom/facebook/CustomTabMainActivity;->setResult(I)V

    .line 60
    invoke-virtual {p0}, Lcom/facebook/CustomTabMainActivity;->finish()V

    return-void

    :cond_0
    if-nez p1, :cond_1

    .line 65
    invoke-virtual {p0}, Lcom/facebook/CustomTabMainActivity;->getIntent()Landroid/content/Intent;

    move-result-object p1

    sget-object v0, Lcom/facebook/CustomTabMainActivity;->EXTRA_ACTION:Ljava/lang/String;

    invoke-virtual {p1, v0}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 66
    invoke-virtual {p0}, Lcom/facebook/CustomTabMainActivity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    sget-object v2, Lcom/facebook/CustomTabMainActivity;->EXTRA_PARAMS:Ljava/lang/String;

    invoke-virtual {v0, v2}, Landroid/content/Intent;->getBundleExtra(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object v0

    .line 67
    invoke-virtual {p0}, Lcom/facebook/CustomTabMainActivity;->getIntent()Landroid/content/Intent;

    move-result-object v2

    sget-object v3, Lcom/facebook/CustomTabMainActivity;->EXTRA_CHROME_PACKAGE:Ljava/lang/String;

    invoke-virtual {v2, v3}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 69
    new-instance v3, Lcom/facebook/internal/CustomTab;

    invoke-direct {v3, p1, v0}, Lcom/facebook/internal/CustomTab;-><init>(Ljava/lang/String;Landroid/os/Bundle;)V

    .line 70
    invoke-virtual {v3, p0, v2}, Lcom/facebook/internal/CustomTab;->openCustomTab(Landroid/app/Activity;Ljava/lang/String;)V

    .line 72
    iput-boolean v1, p0, Lcom/facebook/CustomTabMainActivity;->shouldCloseCustomTab:Z

    .line 75
    new-instance p1, Lcom/facebook/CustomTabMainActivity$1;

    invoke-direct {p1, p0}, Lcom/facebook/CustomTabMainActivity$1;-><init>(Lcom/facebook/CustomTabMainActivity;)V

    iput-object p1, p0, Lcom/facebook/CustomTabMainActivity;->redirectReceiver:Landroid/content/BroadcastReceiver;

    .line 89
    invoke-static {p0}, Landroidx/localbroadcastmanager/content/LocalBroadcastManager;->getInstance(Landroid/content/Context;)Landroidx/localbroadcastmanager/content/LocalBroadcastManager;

    move-result-object p1

    iget-object v0, p0, Lcom/facebook/CustomTabMainActivity;->redirectReceiver:Landroid/content/BroadcastReceiver;

    new-instance v1, Landroid/content/IntentFilter;

    sget-object v2, Lcom/facebook/CustomTabActivity;->CUSTOM_TAB_REDIRECT_ACTION:Ljava/lang/String;

    invoke-direct {v1, v2}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1, v0, v1}, Landroidx/localbroadcastmanager/content/LocalBroadcastManager;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)V

    :cond_1
    return-void
.end method

.method protected onNewIntent(Landroid/content/Intent;)V
    .locals 3

    .line 98
    invoke-super {p0, p1}, Landroid/app/Activity;->onNewIntent(Landroid/content/Intent;)V

    .line 99
    sget-object v0, Lcom/facebook/CustomTabMainActivity;->REFRESH_ACTION:Ljava/lang/String;

    invoke-virtual {p1}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    const/4 v1, -0x1

    if-eqz v0, :cond_0

    .line 101
    new-instance v0, Landroid/content/Intent;

    sget-object v2, Lcom/facebook/CustomTabActivity;->DESTROY_ACTION:Ljava/lang/String;

    invoke-direct {v0, v2}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 102
    invoke-static {p0}, Landroidx/localbroadcastmanager/content/LocalBroadcastManager;->getInstance(Landroid/content/Context;)Landroidx/localbroadcastmanager/content/LocalBroadcastManager;

    move-result-object v2

    invoke-virtual {v2, v0}, Landroidx/localbroadcastmanager/content/LocalBroadcastManager;->sendBroadcast(Landroid/content/Intent;)Z

    .line 103
    invoke-direct {p0, v1, p1}, Lcom/facebook/CustomTabMainActivity;->sendResult(ILandroid/content/Intent;)V

    goto :goto_0

    .line 104
    :cond_0
    sget-object v0, Lcom/facebook/CustomTabActivity;->CUSTOM_TAB_REDIRECT_ACTION:Ljava/lang/String;

    invoke-virtual {p1}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 106
    invoke-direct {p0, v1, p1}, Lcom/facebook/CustomTabMainActivity;->sendResult(ILandroid/content/Intent;)V

    :cond_1
    :goto_0
    return-void
.end method

.method protected onResume()V
    .locals 2

    .line 112
    invoke-super {p0}, Landroid/app/Activity;->onResume()V

    .line 113
    iget-boolean v0, p0, Lcom/facebook/CustomTabMainActivity;->shouldCloseCustomTab:Z

    if-eqz v0, :cond_0

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 115
    invoke-direct {p0, v0, v1}, Lcom/facebook/CustomTabMainActivity;->sendResult(ILandroid/content/Intent;)V

    :cond_0
    const/4 v0, 0x1

    .line 117
    iput-boolean v0, p0, Lcom/facebook/CustomTabMainActivity;->shouldCloseCustomTab:Z

    return-void
.end method
