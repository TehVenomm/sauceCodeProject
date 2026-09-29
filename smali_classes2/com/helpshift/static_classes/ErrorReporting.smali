.class public Lcom/helpshift/static_classes/ErrorReporting;
.super Ljava/lang/Object;
.source "ErrorReporting.java"


# static fields
.field private static enableErrorReporting:Z


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static isEnabled()Z
    .locals 1

    .line 17
    sget-boolean v0, Lcom/helpshift/static_classes/ErrorReporting;->enableErrorReporting:Z

    return v0
.end method

.method public static shouldEnable(Z)V
    .locals 0

    .line 13
    sput-boolean p0, Lcom/helpshift/static_classes/ErrorReporting;->enableErrorReporting:Z

    return-void
.end method
