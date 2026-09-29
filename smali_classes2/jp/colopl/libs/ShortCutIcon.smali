.class public Ljp/colopl/libs/ShortCutIcon;
.super Ljava/lang/Object;
.source "ShortCutIcon.java"


# static fields
.field public static final DEFAULT_ACTIVITY:Ljava/lang/String; = "StartActivity"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 9
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static createAppShortCutIcon(Landroid/content/Context;)V
    .locals 6

    .line 14
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    .line 15
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "app_name"

    const-string v3, "string"

    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    .line 16
    invoke-virtual {p0, v1}, Landroid/content/Context;->getString(I)Ljava/lang/String;

    move-result-object v1

    .line 17
    invoke-virtual {p0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    const-string v3, "app_icon"

    const-string v4, "drawable"

    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    const-string v3, "StartActivity"

    .line 18
    invoke-static {p0, v0, v3}, Ljp/colopl/libs/ShortCutIcon;->createMainIntent(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object v0

    .line 19
    invoke-static {p0, v0, v1, v2}, Ljp/colopl/libs/ShortCutIcon;->createShortCutIcon(Landroid/content/Context;Landroid/content/Intent;Ljava/lang/String;I)V

    return-void
.end method

.method private static createMainIntent(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;
    .locals 0

    .line 53
    new-instance p1, Landroid/content/Intent;

    const-class p2, Ljp/colopl/drapro/StartActivity;

    invoke-direct {p1, p0, p2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string p0, "android.intent.action.MAIN"

    .line 54
    invoke-virtual {p1, p0}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    const/high16 p0, 0x10000000

    .line 55
    invoke-virtual {p1, p0}, Landroid/content/Intent;->addFlags(I)Landroid/content/Intent;

    const/high16 p0, 0x4000000

    .line 56
    invoke-virtual {p1, p0}, Landroid/content/Intent;->addFlags(I)Landroid/content/Intent;

    return-object p1
.end method

.method public static createShortCutIcon(Landroid/content/Context;Landroid/content/Intent;Ljava/lang/String;I)V
    .locals 2

    .line 23
    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    const-string v1, "android.intent.extra.shortcut.INTENT"

    .line 24
    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Parcelable;)Landroid/content/Intent;

    const-string p1, "android.intent.extra.shortcut.NAME"

    .line 26
    invoke-virtual {v0, p1, p2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string p1, "android.intent.extra.shortcut.ICON_RESOURCE"

    .line 28
    invoke-static {p0, p3}, Landroid/content/Intent$ShortcutIconResource;->fromContext(Landroid/content/Context;I)Landroid/content/Intent$ShortcutIconResource;

    move-result-object p2

    invoke-virtual {v0, p1, p2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Parcelable;)Landroid/content/Intent;

    const-string p1, "com.android.launcher.action.INSTALL_SHORTCUT"

    .line 30
    invoke-virtual {v0, p1}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    .line 31
    invoke-virtual {p0, v0}, Landroid/content/Context;->sendBroadcast(Landroid/content/Intent;)V

    return-void
.end method

.method public static createShortCutIcon(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;I)V
    .locals 2

    .line 35
    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    const-string v1, "StartActivity"

    .line 36
    invoke-static {p0, v0, v1, p2}, Ljp/colopl/libs/ShortCutIcon;->createShortCutIntent(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object p2

    .line 37
    invoke-static {p0, p2, p1, p3}, Ljp/colopl/libs/ShortCutIcon;->createShortCutIcon(Landroid/content/Context;Landroid/content/Intent;Ljava/lang/String;I)V

    return-void
.end method

.method private static createShortCutIntent(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;
    .locals 0

    if-nez p3, :cond_0

    const/4 p0, 0x0

    return-object p0

    .line 44
    :cond_0
    new-instance p1, Landroid/content/Intent;

    const-class p2, Ljp/colopl/drapro/StartActivity;

    invoke-direct {p1, p0, p2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string p0, "android.intent.action.VIEW"

    .line 45
    invoke-virtual {p1, p0}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    const/high16 p0, 0x10000000

    .line 46
    invoke-virtual {p1, p0}, Landroid/content/Intent;->addFlags(I)Landroid/content/Intent;

    const/high16 p0, 0x4000000

    .line 47
    invoke-virtual {p1, p0}, Landroid/content/Intent;->addFlags(I)Landroid/content/Intent;

    .line 48
    invoke-static {p3}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p0

    invoke-virtual {p1, p0}, Landroid/content/Intent;->setData(Landroid/net/Uri;)Landroid/content/Intent;

    return-object p1
.end method
