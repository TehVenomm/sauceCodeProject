.class public Lnet/gogame/gopay/vip/CustomTaskQueue;
.super Lnet/gogame/gopay/vip/TapeTaskQueue;
.source "SourceFile"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lnet/gogame/gopay/vip/CustomTaskQueue$CustomConverter;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lnet/gogame/gopay/vip/TapeTaskQueue<",
        "Lnet/gogame/gopay/vip/BaseEvent;",
        ">;"
    }
.end annotation


# instance fields
.field private final a:Landroid/net/ConnectivityManager;


# direct methods
.method public constructor <init>(Landroid/content/Context;Ljava/io/File;Lnet/gogame/gopay/vip/TaskQueue$Listener;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 20
    new-instance v0, Lnet/gogame/gopay/vip/CustomTaskQueue$CustomConverter;

    invoke-direct {v0}, Lnet/gogame/gopay/vip/CustomTaskQueue$CustomConverter;-><init>()V

    invoke-direct {p0, p2, v0, p3}, Lnet/gogame/gopay/vip/TapeTaskQueue;-><init>(Ljava/io/File;Lnet/gogame/gopay/vip/tape2/ObjectQueue$Converter;Lnet/gogame/gopay/vip/TaskQueue$Listener;)V

    const-string p2, "connectivity"

    .line 22
    invoke-virtual {p1, p2}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/net/ConnectivityManager;

    iput-object p1, p0, Lnet/gogame/gopay/vip/CustomTaskQueue;->a:Landroid/net/ConnectivityManager;

    return-void
.end method


# virtual methods
.method protected shouldProcess()Z
    .locals 2

    .line 28
    iget-object v0, p0, Lnet/gogame/gopay/vip/CustomTaskQueue;->a:Landroid/net/ConnectivityManager;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 31
    :cond_0
    iget-object v0, p0, Lnet/gogame/gopay/vip/CustomTaskQueue;->a:Landroid/net/ConnectivityManager;

    invoke-virtual {v0}, Landroid/net/ConnectivityManager;->getActiveNetworkInfo()Landroid/net/NetworkInfo;

    move-result-object v0

    if-eqz v0, :cond_1

    .line 32
    invoke-virtual {v0}, Landroid/net/NetworkInfo;->isConnectedOrConnecting()Z

    move-result v0

    if-eqz v0, :cond_1

    const/4 v1, 0x1

    :cond_1
    return v1
.end method
