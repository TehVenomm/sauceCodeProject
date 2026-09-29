.class public Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;
.super Ljava/lang/Object;
.source "BrowserActionsIntent.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;,
        Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$BrowserActionsItemId;,
        Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$BrowserActionsUrlType;
    }
.end annotation


# static fields
.field public static final ACTION_BROWSER_ACTIONS_OPEN:Ljava/lang/String; = "android.support.customtabs.browseractions.browser_action_open"

.field public static final EXTRA_APP_ID:Ljava/lang/String; = "android.support.customtabs.browseractions.APP_ID"

.field public static final EXTRA_MENU_ITEMS:Ljava/lang/String; = "android.support.customtabs.browseractions.extra.MENU_ITEMS"

.field public static final EXTRA_SELECTED_ACTION_PENDING_INTENT:Ljava/lang/String; = "android.support.customtabs.browseractions.extra.SELECTED_ACTION_PENDING_INTENT"

.field public static final EXTRA_TYPE:Ljava/lang/String; = "android.support.customtabs.browseractions.extra.TYPE"

.field public static final ITEM_COPY:I = 0x3

.field public static final ITEM_DOWNLOAD:I = 0x2

.field public static final ITEM_INVALID_ITEM:I = -0x1

.field public static final ITEM_OPEN_IN_INCOGNITO:I = 0x1

.field public static final ITEM_OPEN_IN_NEW_TAB:I = 0x0

.field public static final ITEM_SHARE:I = 0x4

.field public static final KEY_ACTION:Ljava/lang/String; = "android.support.customtabs.browseractions.ACTION"

.field public static final KEY_ICON:Ljava/lang/String; = "android.support.customtabs.browseractions.ICON"

.field public static final KEY_TITLE:Ljava/lang/String; = "android.support.customtabs.browseractions.TITLE"

.field public static final MAX_CUSTOM_ITEMS:I = 0x5

.field private static final TAG:Ljava/lang/String; = "BrowserActions"

.field private static final TEST_URL:Ljava/lang/String; = "https://www.example.com"

.field public static final URL_TYPE_AUDIO:I = 0x3

.field public static final URL_TYPE_FILE:I = 0x4

.field public static final URL_TYPE_IMAGE:I = 0x1

.field public static final URL_TYPE_NONE:I = 0x0

.field public static final URL_TYPE_PLUGIN:I = 0x5

.field public static final URL_TYPE_VIDEO:I = 0x2


# instance fields
.field public final mIntent:Landroid/content/Intent;


# direct methods
.method private constructor <init>(Landroid/content/Intent;)V
    .locals 0

    .line 134
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 135
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;->mIntent:Landroid/content/Intent;

    return-void
.end method

.method synthetic constructor <init>(Landroid/content/Intent;Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$1;)V
    .locals 0

    .line 41
    invoke-direct {p0, p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;-><init>(Landroid/content/Intent;)V

    return-void
.end method

.method private static getBrowserActionsIntentHandlers(Landroid/content/Context;)Ljava/util/List;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            ")",
            "Ljava/util/List<",
            "Landroid/content/pm/ResolveInfo;",
            ">;"
        }
    .end annotation

    .line 305
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.support.customtabs.browseractions.browser_action_open"

    const-string v2, "https://www.example.com"

    .line 306
    invoke-static {v2}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    .line 307
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p0

    const/high16 v1, 0x20000

    .line 308
    invoke-virtual {p0, v0, v1}, Landroid/content/pm/PackageManager;->queryIntentActivities(Landroid/content/Intent;I)Ljava/util/List;

    move-result-object p0

    return-object p0
.end method

.method public static getCreatorPackageName(Landroid/content/Intent;)Ljava/lang/String;
    .locals 2

    const-string v0, "android.support.customtabs.browseractions.APP_ID"

    .line 372
    invoke-virtual {p0, v0}, Landroid/content/Intent;->getParcelableExtra(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object p0

    check-cast p0, Landroid/app/PendingIntent;

    if-eqz p0, :cond_1

    .line 374
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x11

    if-lt v0, v1, :cond_0

    .line 375
    invoke-virtual {p0}, Landroid/app/PendingIntent;->getCreatorPackage()Ljava/lang/String;

    move-result-object p0

    return-object p0

    .line 377
    :cond_0
    invoke-virtual {p0}, Landroid/app/PendingIntent;->getTargetPackage()Ljava/lang/String;

    move-result-object p0

    return-object p0

    :cond_1
    const/4 p0, 0x0

    return-object p0
.end method

.method public static launchIntent(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 5

    .line 274
    invoke-static {p0}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;->getBrowserActionsIntentHandlers(Landroid/content/Context;)Ljava/util/List;

    move-result-object v0

    if-eqz v0, :cond_4

    .line 275
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v1

    if-nez v1, :cond_0

    goto :goto_2

    .line 278
    :cond_0
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v1

    const/4 v2, 0x0

    const/4 v3, 0x1

    if-ne v1, v3, :cond_1

    .line 279
    invoke-interface {v0, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/content/pm/ResolveInfo;

    iget-object v0, v0, Landroid/content/pm/ResolveInfo;->activityInfo:Landroid/content/pm/ActivityInfo;

    iget-object v0, v0, Landroid/content/pm/ActivityInfo;->packageName:Ljava/lang/String;

    invoke-virtual {p1, v0}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    goto :goto_1

    .line 281
    :cond_1
    new-instance v1, Landroid/content/Intent;

    const-string v3, "android.intent.action.VIEW"

    const-string v4, "https://www.example.com"

    invoke-static {v4}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v4

    invoke-direct {v1, v3, v4}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    .line 282
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v3

    const/high16 v4, 0x10000

    .line 284
    invoke-virtual {v3, v1, v4}, Landroid/content/pm/PackageManager;->resolveActivity(Landroid/content/Intent;I)Landroid/content/pm/ResolveInfo;

    move-result-object v1

    if-eqz v1, :cond_3

    .line 286
    iget-object v1, v1, Landroid/content/pm/ResolveInfo;->activityInfo:Landroid/content/pm/ActivityInfo;

    iget-object v1, v1, Landroid/content/pm/ActivityInfo;->packageName:Ljava/lang/String;

    .line 287
    :goto_0
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result v3

    if-ge v2, v3, :cond_3

    .line 288
    invoke-interface {v0, v2}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/content/pm/ResolveInfo;

    iget-object v3, v3, Landroid/content/pm/ResolveInfo;->activityInfo:Landroid/content/pm/ActivityInfo;

    iget-object v3, v3, Landroid/content/pm/ActivityInfo;->packageName:Ljava/lang/String;

    invoke-virtual {v1, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_2

    .line 289
    invoke-virtual {p1, v1}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    goto :goto_1

    :cond_2
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_3
    :goto_1
    const/4 v0, 0x0

    .line 295
    invoke-virtual {p0, p1, v0}, Landroid/content/Context;->startActivity(Landroid/content/Intent;Landroid/os/Bundle;)V

    return-void

    .line 276
    :cond_4
    :goto_2
    invoke-static {p0, p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;->openFallbackBrowserActionsMenu(Landroid/content/Context;Landroid/content/Intent;)V

    return-void
.end method

.method public static openBrowserAction(Landroid/content/Context;Landroid/net/Uri;)V
    .locals 1

    .line 239
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;-><init>(Landroid/content/Context;Landroid/net/Uri;)V

    invoke-virtual {v0}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->build()Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;

    move-result-object p1

    .line 240
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;->getIntent()Landroid/content/Intent;

    move-result-object p1

    invoke-static {p0, p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;->launchIntent(Landroid/content/Context;Landroid/content/Intent;)V

    return-void
.end method

.method public static openBrowserAction(Landroid/content/Context;Landroid/net/Uri;ILjava/util/ArrayList;Landroid/app/PendingIntent;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Landroid/net/Uri;",
            "I",
            "Ljava/util/ArrayList<",
            "Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;",
            ">;",
            "Landroid/app/PendingIntent;",
            ")V"
        }
    .end annotation

    .line 256
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;

    invoke-direct {v0, p0, p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;-><init>(Landroid/content/Context;Landroid/net/Uri;)V

    .line 257
    invoke-virtual {v0, p2}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->setUrlType(I)Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;

    move-result-object p1

    .line 258
    invoke-virtual {p1, p3}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->setCustomItems(Ljava/util/ArrayList;)Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;

    move-result-object p1

    .line 259
    invoke-virtual {p1, p4}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->setOnItemSelectedAction(Landroid/app/PendingIntent;)Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;

    move-result-object p1

    .line 260
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent$Builder;->build()Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;

    move-result-object p1

    .line 261
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;->getIntent()Landroid/content/Intent;

    move-result-object p1

    invoke-static {p0, p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;->launchIntent(Landroid/content/Context;Landroid/content/Intent;)V

    return-void
.end method

.method private static openFallbackBrowserActionsMenu(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 3

    .line 317
    invoke-virtual {p1}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v0

    const-string v1, "android.support.customtabs.browseractions.extra.TYPE"

    const/4 v2, 0x0

    .line 318
    invoke-virtual {p1, v1, v2}, Landroid/content/Intent;->getIntExtra(Ljava/lang/String;I)I

    move-result v1

    const-string v2, "android.support.customtabs.browseractions.extra.MENU_ITEMS"

    .line 319
    invoke-virtual {p1, v2}, Landroid/content/Intent;->getParcelableArrayListExtra(Ljava/lang/String;)Ljava/util/ArrayList;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 320
    invoke-static {p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;->parseBrowserActionItems(Ljava/util/ArrayList;)Ljava/util/List;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    .line 321
    :goto_0
    invoke-static {p0, v0, v1, p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;->openFallbackBrowserActionsMenu(Landroid/content/Context;Landroid/net/Uri;ILjava/util/List;)V

    return-void
.end method

.method private static openFallbackBrowserActionsMenu(Landroid/content/Context;Landroid/net/Uri;ILjava/util/List;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Landroid/net/Uri;",
            "I",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;",
            ">;)V"
        }
    .end annotation

    return-void
.end method

.method private static openFallbackBrowserActionsMenu(Landroid/content/Context;Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;)V
    .locals 0

    .line 313
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;->getIntent()Landroid/content/Intent;

    move-result-object p1

    invoke-static {p0, p1}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;->openFallbackBrowserActionsMenu(Landroid/content/Context;Landroid/content/Intent;)V

    return-void
.end method

.method public static parseBrowserActionItems(Ljava/util/ArrayList;)Ljava/util/List;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList<",
            "Landroid/os/Bundle;",
            ">;)",
            "Ljava/util/List<",
            "Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;",
            ">;"
        }
    .end annotation

    .line 342
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const/4 v1, 0x0

    .line 343
    :goto_0
    invoke-virtual {p0}, Ljava/util/ArrayList;->size()I

    move-result v2

    if-ge v1, v2, :cond_4

    .line 344
    invoke-virtual {p0, v1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/os/Bundle;

    const-string v3, "android.support.customtabs.browseractions.TITLE"

    .line 345
    invoke-virtual {v2, v3}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v4, "android.support.customtabs.browseractions.ACTION"

    .line 346
    invoke-virtual {v2, v4}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object v4

    check-cast v4, Landroid/app/PendingIntent;

    const-string v5, "android.support.customtabs.browseractions.ICON"

    .line 347
    invoke-virtual {v2, v5}, Landroid/os/Bundle;->getParcelable(Ljava/lang/String;)Landroid/os/Parcelable;

    move-result-object v2

    check-cast v2, Landroid/graphics/Bitmap;

    if-eqz v3, :cond_1

    if-eqz v4, :cond_1

    .line 349
    new-instance v5, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;

    invoke-direct {v5, v3, v4}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;-><init>(Ljava/lang/String;Landroid/app/PendingIntent;)V

    if-eqz v2, :cond_0

    .line 351
    invoke-virtual {v5, v2}, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionItem;->setIcon(Landroid/graphics/Bitmap;)V

    .line 353
    :cond_0
    invoke-interface {v0, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    if-nez v3, :cond_3

    if-eqz v4, :cond_2

    .line 357
    new-instance p0, Ljava/lang/IllegalArgumentException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Missing title for item: "

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0

    .line 359
    :cond_2
    new-instance p0, Ljava/lang/IllegalArgumentException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Missing title and action for item: "

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0

    .line 355
    :cond_3
    new-instance p0, Ljava/lang/IllegalArgumentException;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Missing action for item: "

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-direct {p0, v0}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw p0

    :cond_4
    return-object v0
.end method


# virtual methods
.method public getIntent()Landroid/content/Intent;
    .locals 1

    .line 131
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/browseractions/BrowserActionsIntent;->mIntent:Landroid/content/Intent;

    return-object v0
.end method
