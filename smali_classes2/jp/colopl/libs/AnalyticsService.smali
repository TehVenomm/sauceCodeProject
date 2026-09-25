.class public Ljp/colopl/libs/AnalyticsService;
.super Landroid/app/IntentService;
.source "AnalyticsService.java"


# direct methods
.method static constructor <clinit>()V
    .locals 1

    const-string v0, "analytics"

    .line 9
    invoke-static {v0}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    const-string v0, "AnalyticsService"

    .line 19
    invoke-direct {p0, v0}, Landroid/app/IntentService;-><init>(Ljava/lang/String;)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;)V
    .locals 0

    .line 15
    invoke-direct {p0, p1}, Landroid/app/IntentService;-><init>(Ljava/lang/String;)V

    return-void
.end method


# virtual methods
.method public native ana()V
.end method

.method protected onHandleIntent(Landroid/content/Intent;)V
    .locals 0

    .line 24
    invoke-virtual {p0}, Ljp/colopl/libs/AnalyticsService;->ana()V

    return-void
.end method
