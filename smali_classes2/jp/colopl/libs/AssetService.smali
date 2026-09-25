.class public Ljp/colopl/libs/AssetService;
.super Landroid/app/IntentService;
.source "AssetService.java"


# static fields
.field private static a:Z = false


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-string v0, "asset"

    .line 10
    invoke-static {v0}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    const-string v0, "AssetService"

    .line 20
    invoke-direct {p0, v0}, Landroid/app/IntentService;-><init>(Ljava/lang/String;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;)V
    .locals 0

    .line 16
    invoke-direct {p0, p1}, Landroid/app/IntentService;-><init>(Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public native asset()V
.end method

.method protected onHandleIntent(Landroid/content/Intent;)V
    .locals 1

    const-string v0, "asset"

    .line 27
    invoke-virtual {p1, v0}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    const-string v0, "start"

    .line 28
    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-nez p1, :cond_0

    sget-boolean p1, Ljp/colopl/libs/AssetService;->a:Z

    if-nez p1, :cond_1

    .line 29
    :cond_0
    invoke-virtual {p0}, Ljp/colopl/libs/AssetService;->asset()V

    const/4 p1, 0x1

    .line 30
    sput-boolean p1, Ljp/colopl/libs/AssetService;->a:Z

    :cond_1
    return-void
.end method
