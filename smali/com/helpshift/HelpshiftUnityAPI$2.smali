.class final Lcom/helpshift/HelpshiftUnityAPI$2;
.super Ljava/lang/Object;
.source "HelpshiftUnityAPI.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/HelpshiftUnityAPI;->getNotificationCount(Z)I
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    .line 241
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 244
    new-instance v0, Lcom/helpshift/HelpshiftUnityAPI$2$1;

    invoke-direct {v0, p0}, Lcom/helpshift/HelpshiftUnityAPI$2$1;-><init>(Lcom/helpshift/HelpshiftUnityAPI$2;)V

    .line 254
    new-instance v1, Landroid/os/Handler;

    invoke-direct {v1}, Landroid/os/Handler;-><init>()V

    invoke-static {v0, v1}, Lcom/helpshift/support/Support;->getNotificationCount(Landroid/os/Handler;Landroid/os/Handler;)V

    return-void
.end method
