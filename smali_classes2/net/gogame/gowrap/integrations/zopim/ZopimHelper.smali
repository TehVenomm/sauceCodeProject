.class public final Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;
.super Ljava/lang/Object;
.source "ZopimHelper.java"


# direct methods
.method private constructor <init>()V
    .locals 0

    .line 18
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static encodeTagComponent(Ljava/lang/String;)Ljava/lang/String;
    .locals 6

    .line 35
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const/4 v1, 0x0

    const/4 v2, 0x0

    .line 36
    :goto_0
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v3

    if-ge v2, v3, :cond_6

    .line 37
    invoke-virtual {p0, v2}, Ljava/lang/String;->charAt(I)C

    move-result v3

    const/16 v4, 0x30

    const/16 v5, 0x5f

    if-lt v3, v4, :cond_0

    const/16 v4, 0x39

    if-le v3, v4, :cond_4

    :cond_0
    const/16 v4, 0x41

    if-lt v3, v4, :cond_1

    const/16 v4, 0x5a

    if-le v3, v4, :cond_4

    :cond_1
    const/16 v4, 0x61

    if-lt v3, v4, :cond_2

    const/16 v4, 0x7a

    if-le v3, v4, :cond_4

    :cond_2
    const/16 v4, 0x2d

    if-eq v3, v4, :cond_4

    if-ne v3, v5, :cond_3

    goto :goto_1

    :cond_3
    const/4 v4, 0x0

    goto :goto_2

    :cond_4
    :goto_1
    const/4 v4, 0x1

    :goto_2
    if-eqz v4, :cond_5

    .line 41
    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    goto :goto_3

    .line 43
    :cond_5
    invoke-virtual {v0, v5}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :goto_3
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    .line 46
    :cond_6
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method

.method private static getAppLabel(Landroid/content/Context;)Ljava/lang/String;
    .locals 3

    .line 54
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    .line 57
    :try_start_0
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationInfo()Landroid/content/pm/ApplicationInfo;

    move-result-object v1

    iget-object v1, v1, Landroid/content/pm/ApplicationInfo;->packageName:Ljava/lang/String;

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    move-result-object v1
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    const/4 v1, 0x0

    :goto_0
    if-eqz v1, :cond_0

    .line 63
    invoke-virtual {v0, v1}, Landroid/content/pm/PackageManager;->getApplicationLabel(Landroid/content/pm/ApplicationInfo;)Ljava/lang/CharSequence;

    move-result-object p0

    goto :goto_1

    .line 64
    :cond_0
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p0

    :goto_1
    check-cast p0, Ljava/lang/String;

    return-object p0
.end method

.method private static getAppVersion(Landroid/content/Context;)Ljava/lang/String;
    .locals 3

    .line 68
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    const/4 v1, 0x0

    .line 71
    :try_start_0
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p0

    const/4 v2, 0x0

    invoke-virtual {v0, p0, v2}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object p0
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-object p0, v1

    :goto_0
    if-eqz p0, :cond_0

    .line 75
    iget-object v1, p0, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;

    :cond_0
    return-object v1
.end method

.method public static getSessionConfig(Landroid/content/Context;Ljava/lang/String;)Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;
    .locals 4

    .line 79
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object p0

    .line 81
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    .line 82
    invoke-static {p0}, Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;->getAppLabel(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v1

    .line 83
    invoke-static {p0}, Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;->getAppVersion(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p0

    .line 85
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    if-eqz v0, :cond_0

    const-string v3, "packageName"

    .line 87
    invoke-static {v3, v0}, Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;->makeTag(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-interface {v2, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_0
    if-eqz v1, :cond_1

    const-string v0, "appName"

    .line 90
    invoke-static {v0, v1}, Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;->makeTag(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-interface {v2, v0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_1
    if-eqz p0, :cond_2

    const-string v0, "appVersion"

    .line 93
    invoke-static {v0, p0}, Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;->makeTag(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-interface {v2, p0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_2
    if-eqz p1, :cond_3

    const-string p0, "guid"

    .line 96
    invoke-static {p0, p1}, Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;->makeTag(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-interface {v2, p0}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 98
    :cond_3
    invoke-interface {v2}, Ljava/util/List;->size()I

    move-result p0

    new-array p0, p0, [Ljava/lang/String;

    invoke-interface {v2, p0}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object p0

    check-cast p0, [Ljava/lang/String;

    .line 100
    new-instance p1, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    invoke-direct {p1}, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;-><init>()V

    .line 101
    invoke-virtual {p1, p0}, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->tags([Ljava/lang/String;)Lcom/zopim/android/sdk/api/i;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    return-object p0
.end method

.method public static initChat(Landroid/content/Context;Ljava/lang/String;)V
    .locals 3

    .line 26
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    .line 27
    invoke-static {p0}, Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;->getAppLabel(Landroid/content/Context;)Ljava/lang/String;

    move-result-object p0

    .line 28
    invoke-static {p1}, Lcom/zopim/android/sdk/api/ZopimChat;->init(Ljava/lang/String;)Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;

    move-result-object p1

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "http://www.gogame.net/support/android/"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 29
    invoke-virtual {p1, v0}, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->visitorPathOne(Ljava/lang/String;)Lcom/zopim/android/sdk/api/i;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;

    .line 30
    invoke-virtual {p1, p0}, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->visitorPathTwo(Ljava/lang/String;)Lcom/zopim/android/sdk/api/i;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;

    .line 31
    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->build()Ljava/lang/Void;

    return-void
.end method

.method public static isIntegrated()Z
    .locals 1

    const-string v0, "com.zopim.android.sdk.api.ZopimChat"

    .line 22
    invoke-static {v0}, Lnet/gogame/gowrap/support/ClassUtils;->hasClass(Ljava/lang/String;)Z

    move-result v0

    return v0
.end method

.method private static makeTag(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    .line 50
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {p0}, Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;->encodeTagComponent(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p0, "-"

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {p1}, Lnet/gogame/gowrap/integrations/zopim/ZopimHelper;->encodeTagComponent(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0
.end method
