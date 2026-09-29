.class public final Ljp/colopl/drapro/AppConsts;
.super Ljava/lang/Object;
.source "AppConsts.java"


# static fields
.field public static final GATrackingID:Ljava/lang/String; = "drapro"

.field public static final SHOPITEM_PREFIX:Ljava/lang/String; = "net.gogame.dragon."

.field public static appContext:Landroid/content/Context;

.field public static itemCodeId:[Ljava/lang/String;

.field public static itemNameId:[Ljava/lang/String;

.field public static versionName:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-string v0, "appconsts"

    .line 11
    invoke-static {v0}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V

    const-string v0, ""

    .line 20
    sput-object v0, Ljp/colopl/drapro/AppConsts;->versionName:Ljava/lang/String;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static SetUpShopItem(Landroid/app/Activity;)V
    .locals 6

    .line 31
    invoke-virtual {p0}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v1, "ShopItemCount"

    const-string v2, "string"

    invoke-virtual {p0}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v1, v2, v3}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 33
    invoke-virtual {p0}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-virtual {v1, v0}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v0

    .line 34
    invoke-static {v0}, Ljava/lang/Integer;->valueOf(Ljava/lang/String;)Ljava/lang/Integer;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    .line 36
    new-array v1, v0, [Ljava/lang/String;

    sput-object v1, Ljp/colopl/drapro/AppConsts;->itemNameId:[Ljava/lang/String;

    .line 37
    new-array v1, v0, [Ljava/lang/String;

    sput-object v1, Ljp/colopl/drapro/AppConsts;->itemCodeId:[Ljava/lang/String;

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_0

    .line 40
    invoke-virtual {p0}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "ShopItemName"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    const-string v4, "string"

    invoke-virtual {p0}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    .line 41
    sget-object v3, Ljp/colopl/drapro/AppConsts;->itemNameId:[Ljava/lang/String;

    invoke-virtual {p0}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    invoke-virtual {v4, v2}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v2

    aput-object v2, v3, v1

    .line 43
    invoke-virtual {p0}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "ShopItem"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    const-string v4, "string"

    invoke-virtual {p0}, Landroid/app/Activity;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    .line 44
    sget-object v3, Ljp/colopl/drapro/AppConsts;->itemCodeId:[Ljava/lang/String;

    invoke-virtual {p0}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    invoke-virtual {v4, v2}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v2

    aput-object v2, v3, v1

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    return-void
.end method

.method public static native getAppLicenseKey()Ljava/lang/String;
.end method

.method public static getProductNameById(Ljava/lang/String;Landroid/app/Activity;)Ljava/lang/String;
    .locals 4

    const/4 v0, 0x0

    const/4 v1, 0x0

    :goto_0
    const/4 v2, 0x0

    .line 50
    :try_start_0
    sget-object v3, Ljp/colopl/drapro/AppConsts;->itemCodeId:[Ljava/lang/String;

    array-length v3, v3

    if-ge v1, v3, :cond_2

    .line 51
    sget-object v3, Ljp/colopl/drapro/AppConsts;->itemCodeId:[Ljava/lang/String;

    aget-object v3, v3, v1

    invoke-virtual {v3, p0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_1

    .line 52
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    if-nez p1, :cond_0

    const/4 v0, 0x1

    :cond_0
    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string p1, "  "

    invoke-virtual {v3, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {v2, p0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 53
    sget-object p0, Ljp/colopl/drapro/AppConsts;->itemNameId:[Ljava/lang/String;

    aget-object p0, p0, v1

    return-object p0

    :cond_1
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_2
    const-string p0, ""
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    return-object p0

    :catch_0
    move-exception p0

    .line 59
    invoke-virtual {p0}, Ljava/lang/Exception;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {v2, p0}, Ljp/colopl/util/Util;->eLog(Ljava/lang/String;Ljava/lang/String;)V

    const-string p0, ""

    return-object p0
.end method
