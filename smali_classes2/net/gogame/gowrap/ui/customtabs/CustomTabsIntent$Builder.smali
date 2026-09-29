.class public final Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
.super Ljava/lang/Object;
.source "CustomTabsIntent.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x19
    name = "Builder"
.end annotation


# instance fields
.field private mActionButtons:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Landroid/os/Bundle;",
            ">;"
        }
    .end annotation
.end field

.field private mInstantAppsEnabled:Z

.field private final mIntent:Landroid/content/Intent;

.field private mMenuItems:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList<",
            "Landroid/os/Bundle;",
            ">;"
        }
    .end annotation
.end field

.field private mStartAnimationBundle:Landroid/os/Bundle;


# direct methods
.method public constructor <init>()V
    .locals 1

    const/4 v0, 0x0

    .line 280
    invoke-direct {p0, v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;-><init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;)V

    return-void
.end method

.method public constructor <init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;)V
    .locals 3

    .line 292
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 269
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.VIEW"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const/4 v0, 0x0

    .line 270
    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mMenuItems:Ljava/util/ArrayList;

    .line 271
    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mStartAnimationBundle:Landroid/os/Bundle;

    .line 272
    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mActionButtons:Ljava/util/ArrayList;

    const/4 v1, 0x1

    .line 273
    iput-boolean v1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mInstantAppsEnabled:Z

    if-eqz p1, :cond_0

    .line 293
    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->getComponentName()Landroid/content/ComponentName;

    move-result-object v2

    invoke-virtual {v2}, Landroid/content/ComponentName;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    .line 294
    :cond_0
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "android.support.customtabs.extra.SESSION"

    if-nez p1, :cond_1

    goto :goto_0

    .line 295
    :cond_1
    invoke-virtual {p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsSession;->getBinder()Landroid/os/IBinder;

    move-result-object v0

    :goto_0
    invoke-virtual {v1, v2, v0}, Landroid/os/Bundle;->putBinder(Ljava/lang/String;Landroid/os/IBinder;)V

    .line 296
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    invoke-virtual {p1, v1}, Landroid/content/Intent;->putExtras(Landroid/os/Bundle;)Landroid/content/Intent;

    return-void
.end method


# virtual methods
.method public addDefaultShareMenuItem()Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 3

    .line 357
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.extra.SHARE_MENU_ITEM"

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    return-object p0
.end method

.method public addMenuItem(Ljava/lang/String;Landroid/app/PendingIntent;)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 2

    .line 345
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mMenuItems:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mMenuItems:Ljava/util/ArrayList;

    .line 346
    :cond_0
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "android.support.customtabs.customaction.MENU_ITEM_TITLE"

    .line 347
    invoke-virtual {v0, v1, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "android.support.customtabs.customaction.PENDING_INTENT"

    .line 348
    invoke-virtual {v0, p1, p2}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 349
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mMenuItems:Ljava/util/ArrayList;

    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    return-object p0
.end method

.method public addToolbarItem(ILandroid/graphics/Bitmap;Ljava/lang/String;Landroid/app/PendingIntent;)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/IllegalStateException;
        }
    .end annotation

    .annotation runtime Ljava/lang/Deprecated;
    .end annotation

    .line 419
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mActionButtons:Ljava/util/ArrayList;

    if-nez v0, :cond_0

    .line 420
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mActionButtons:Ljava/util/ArrayList;

    .line 422
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mActionButtons:Ljava/util/ArrayList;

    invoke-virtual {v0}, Ljava/util/ArrayList;->size()I

    move-result v0

    const/4 v1, 0x5

    if-ge v0, v1, :cond_1

    .line 426
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "android.support.customtabs.customaction.ID"

    .line 427
    invoke-virtual {v0, v1, p1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string p1, "android.support.customtabs.customaction.ICON"

    .line 428
    invoke-virtual {v0, p1, p2}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    const-string p1, "android.support.customtabs.customaction.DESCRIPTION"

    .line 429
    invoke-virtual {v0, p1, p3}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "android.support.customtabs.customaction.PENDING_INTENT"

    .line 430
    invoke-virtual {v0, p1, p4}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 431
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mActionButtons:Ljava/util/ArrayList;

    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    return-object p0

    .line 423
    :cond_1
    new-instance p1, Ljava/lang/IllegalStateException;

    const-string p2, "Exceeded maximum toolbar item count of 5"

    invoke-direct {p1, p2}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw p1
.end method

.method public build()Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent;
    .locals 4

    .line 511
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mMenuItems:Ljava/util/ArrayList;

    if-eqz v0, :cond_0

    .line 512
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.extra.MENU_ITEMS"

    iget-object v2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mMenuItems:Ljava/util/ArrayList;

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putParcelableArrayListExtra(Ljava/lang/String;Ljava/util/ArrayList;)Landroid/content/Intent;

    .line 514
    :cond_0
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mActionButtons:Ljava/util/ArrayList;

    if-eqz v0, :cond_1

    .line 515
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.extra.TOOLBAR_ITEMS"

    iget-object v2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mActionButtons:Ljava/util/ArrayList;

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putParcelableArrayListExtra(Ljava/lang/String;Ljava/util/ArrayList;)Landroid/content/Intent;

    .line 517
    :cond_1
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.extra.EXTRA_ENABLE_INSTANT_APPS"

    iget-boolean v2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mInstantAppsEnabled:Z

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    .line 518
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    iget-object v2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mStartAnimationBundle:Landroid/os/Bundle;

    const/4 v3, 0x0

    invoke-direct {v0, v1, v2, v3}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent;-><init>(Landroid/content/Intent;Landroid/os/Bundle;Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$1;)V

    return-object v0
.end method

.method public enableUrlBarHiding()Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 3

    .line 313
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.extra.ENABLE_URLBAR_HIDING"

    const/4 v2, 0x1

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    return-object p0
.end method

.method public setActionButton(Landroid/graphics/Bitmap;Ljava/lang/String;Landroid/app/PendingIntent;)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 1

    const/4 v0, 0x0

    .line 395
    invoke-virtual {p0, p1, p2, p3, v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->setActionButton(Landroid/graphics/Bitmap;Ljava/lang/String;Landroid/app/PendingIntent;Z)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;

    move-result-object p1

    return-object p1
.end method

.method public setActionButton(Landroid/graphics/Bitmap;Ljava/lang/String;Landroid/app/PendingIntent;Z)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 3

    .line 377
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "android.support.customtabs.customaction.ID"

    const/4 v2, 0x0

    .line 378
    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v1, "android.support.customtabs.customaction.ICON"

    .line 379
    invoke-virtual {v0, v1, p1}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    const-string p1, "android.support.customtabs.customaction.DESCRIPTION"

    .line 380
    invoke-virtual {v0, p1, p2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "android.support.customtabs.customaction.PENDING_INTENT"

    .line 381
    invoke-virtual {v0, p1, p3}, Landroid/os/Bundle;->putParcelable(Ljava/lang/String;Landroid/os/Parcelable;)V

    .line 382
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string p2, "android.support.customtabs.extra.ACTION_BUTTON_BUNDLE"

    invoke-virtual {p1, p2, v0}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    .line 383
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string p2, "android.support.customtabs.extra.TINT_ACTION_BUTTON"

    invoke-virtual {p1, p2, p4}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    return-object p0
.end method

.method public setCloseButtonIcon(Landroid/graphics/Bitmap;)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 2

    .line 323
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.extra.CLOSE_BUTTON_ICON"

    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Parcelable;)Landroid/content/Intent;

    return-object p0
.end method

.method public setExitAnimations(Landroid/content/Context;II)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 0

    .line 500
    invoke-static {p1, p2, p3}, Landroid/app/ActivityOptions;->makeCustomAnimation(Landroid/content/Context;II)Landroid/app/ActivityOptions;

    move-result-object p1

    .line 501
    invoke-virtual {p1}, Landroid/app/ActivityOptions;->toBundle()Landroid/os/Bundle;

    move-result-object p1

    .line 502
    iget-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string p3, "android.support.customtabs.extra.EXIT_ANIMATION_BUNDLE"

    invoke-virtual {p2, p3, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Bundle;)Landroid/content/Intent;

    return-object p0
.end method

.method public setInstantAppsEnabled(Z)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 0

    .line 473
    iput-boolean p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mInstantAppsEnabled:Z

    return-object p0
.end method

.method public setSecondaryToolbarColor(I)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 2

    .line 440
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.extra.SECONDARY_TOOLBAR_COLOR"

    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    return-object p0
.end method

.method public setSecondaryToolbarViews(Landroid/widget/RemoteViews;[ILandroid/app/PendingIntent;)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 2

    .line 461
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.extra.EXTRA_REMOTEVIEWS"

    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Parcelable;)Landroid/content/Intent;

    .line 462
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v0, "android.support.customtabs.extra.EXTRA_REMOTEVIEWS_VIEW_IDS"

    invoke-virtual {p1, v0, p2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;[I)Landroid/content/Intent;

    .line 463
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string p2, "android.support.customtabs.extra.EXTRA_REMOTEVIEWS_PENDINGINTENT"

    invoke-virtual {p1, p2, p3}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Parcelable;)Landroid/content/Intent;

    return-object p0
.end method

.method public setShowTitle(Z)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 2

    .line 333
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.extra.TITLE_VISIBILITY"

    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    return-object p0
.end method

.method public setStartAnimations(Landroid/content/Context;II)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 0

    .line 486
    invoke-static {p1, p2, p3}, Landroid/app/ActivityOptions;->makeCustomAnimation(Landroid/content/Context;II)Landroid/app/ActivityOptions;

    move-result-object p1

    .line 487
    invoke-virtual {p1}, Landroid/app/ActivityOptions;->toBundle()Landroid/os/Bundle;

    move-result-object p1

    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mStartAnimationBundle:Landroid/os/Bundle;

    return-object p0
.end method

.method public setToolbarColor(I)Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;
    .locals 2

    .line 305
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsIntent$Builder;->mIntent:Landroid/content/Intent;

    const-string v1, "android.support.customtabs.extra.TOOLBAR_COLOR"

    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    return-object p0
.end method
