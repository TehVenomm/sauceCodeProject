.class public Lcom/helpshift/app/AppLifeCycleStateHolder;
.super Ljava/lang/Object;
.source "AppLifeCycleStateHolder.java"


# static fields
.field private static isAppInForeground:Z


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 7
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static isAppInForeground()Z
    .locals 1

    .line 16
    sget-boolean v0, Lcom/helpshift/app/AppLifeCycleStateHolder;->isAppInForeground:Z

    return v0
.end method

.method public static setAppInForeground(Z)V
    .locals 0

    .line 12
    sput-boolean p0, Lcom/helpshift/app/AppLifeCycleStateHolder;->isAppInForeground:Z

    return-void
.end method
