.class public Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;
.super Ljava/lang/Object;
.source "CustomTabsClient.java"


# instance fields
.field private final mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

.field private final mServiceComponentName:Landroid/content/ComponentName;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;Landroid/content/ComponentName;)V
    .locals 0

    .line 44
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 45
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    .line 46
    iput-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;->mServiceComponentName:Landroid/content/ComponentName;

    return-void
.end method

.method public static bindCustomTabsService(Landroid/content/Context;Ljava/lang/String;Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection;)Z
    .locals 2

    .line 63
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.support.customtabs.action.CustomTabsService"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 64
    invoke-static {p1}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-nez v1, :cond_0

    invoke-virtual {v0, p1}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    :cond_0
    const/16 p1, 0x21

    .line 65
    invoke-virtual {p0, v0, p2, p1}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    move-result p0

    return p0
.end method

.method public static connectAndInitialize(Landroid/content/Context;Ljava/lang/String;)Z
    .locals 2

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    .line 132
    :cond_0
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    .line 133
    new-instance v1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$1;

    invoke-direct {v1, p0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$1;-><init>(Landroid/content/Context;)V

    .line 148
    :try_start_0
    invoke-static {p0, p1, v1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;->bindCustomTabsService(Landroid/content/Context;Ljava/lang/String;Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection;)Z

    move-result p0
    :try_end_0
    .catch Ljava/lang/SecurityException; {:try_start_0 .. :try_end_0} :catch_0

    return p0

    :catch_0
    return v0
.end method

.method public static getPackageName(Landroid/content/Context;Ljava/util/List;)Ljava/lang/String;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    const/4 v0, 0x0

    .line 75
    invoke-static {p0, p1, v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;->getPackageName(Landroid/content/Context;Ljava/util/List;Z)Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method public static getPackageName(Landroid/content/Context;Ljava/util/List;Z)Ljava/lang/String;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;Z)",
            "Ljava/lang/String;"
        }
    .end annotation

    .line 93
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p0

    if-nez p1, :cond_0

    .line 95
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    goto :goto_0

    :cond_0
    move-object v0, p1

    .line 96
    :goto_0
    new-instance v1, Landroid/content/Intent;

    const-string v2, "android.intent.action.VIEW"

    const-string v3, "http://"

    invoke-static {v3}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v3

    invoke-direct {v1, v2, v3}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    const/4 v2, 0x0

    if-nez p2, :cond_2

    .line 99
    invoke-virtual {p0, v1, v2}, Landroid/content/pm/PackageManager;->resolveActivity(Landroid/content/Intent;I)Landroid/content/pm/ResolveInfo;

    move-result-object p2

    if-eqz p2, :cond_2

    .line 101
    iget-object p2, p2, Landroid/content/pm/ResolveInfo;->activityInfo:Landroid/content/pm/ActivityInfo;

    iget-object p2, p2, Landroid/content/pm/ActivityInfo;->packageName:Ljava/lang/String;

    .line 102
    new-instance v1, Ljava/util/ArrayList;

    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v0

    add-int/lit8 v0, v0, 0x1

    invoke-direct {v1, v0}, Ljava/util/ArrayList;-><init>(I)V

    .line 103
    invoke-interface {v1, p2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    if-eqz p1, :cond_1

    .line 104
    invoke-interface {v1, p1}, Ljava/util/List;->addAll(Ljava/util/Collection;)Z

    :cond_1
    move-object v0, v1

    .line 108
    :cond_2
    new-instance p1, Landroid/content/Intent;

    const-string p2, "android.support.customtabs.action.CustomTabsService"

    invoke-direct {p1, p2}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 109
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p2

    :cond_3
    invoke-interface {p2}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_4

    invoke-interface {p2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 110
    invoke-virtual {p1, v0}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    .line 111
    invoke-virtual {p0, p1, v2}, Landroid/content/pm/PackageManager;->resolveService(Landroid/content/Intent;I)Landroid/content/pm/ResolveInfo;

    move-result-object v1

    if-eqz v1, :cond_3

    return-object v0

    :cond_4
    const/4 p0, 0x0

    return-object p0
.end method


# virtual methods
.method public extraCommand(Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;
    .locals 1

    .line 258
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    invoke-interface {v0, p1, p2}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;->extraCommand(Ljava/lang/String;Landroid/os/Bundle;)Landroid/os/Bundle;

    move-result-object p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return-object p1

    :catch_0
    const/4 p1, 0x0

    return-object p1
.end method

.method public newSession(Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;)Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;
    .locals 3

    .line 183
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$2;-><init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;Lnet/gogame/gowrap/ui/customtabs/CustomTabsCallback;)V

    const/4 p1, 0x0

    .line 249
    :try_start_0
    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    invoke-interface {v1, v0}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;->newSession(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)Z

    move-result v1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    if-nez v1, :cond_0

    return-object p1

    .line 253
    :cond_0
    new-instance p1, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;->mServiceComponentName:Landroid/content/ComponentName;

    invoke-direct {p1, v1, v0, v2}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;-><init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;Landroid/content/ComponentName;)V

    return-object p1

    :catch_0
    return-object p1
.end method

.method public warmup(J)Z
    .locals 1

    .line 165
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;->mService:Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    invoke-interface {v0, p1, p2}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;->warmup(J)Z

    move-result p1
    :try_end_0
    .catch Landroid/os/RemoteException; {:try_start_0 .. :try_end_0} :catch_0

    return p1

    :catch_0
    const/4 p1, 0x0

    return p1
.end method
