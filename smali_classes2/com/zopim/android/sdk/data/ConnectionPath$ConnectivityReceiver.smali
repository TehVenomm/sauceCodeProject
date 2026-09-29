.class public Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;
.super Landroid/content/BroadcastReceiver;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/data/ConnectionPath;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "ConnectivityReceiver"
.end annotation


# instance fields
.field private final LOG_TAG:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 1

    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    const-class v0, Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;->LOG_TAG:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 3

    if-eqz p2, :cond_7

    const-string v0, "android.net.conn.CONNECTIVITY_CHANGE"

    invoke-virtual {p2}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    goto/16 :goto_5

    :cond_0
    const-string v0, "noConnectivity"

    invoke-virtual {p2, v0}, Landroid/content/Intent;->hasExtra(Ljava/lang/String;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_1

    invoke-static {}, Lcom/zopim/android/sdk/data/ConnectionPath;->access$000()Lcom/zopim/android/sdk/data/ConnectionPath;

    move-result-object p1

    const-string v0, "noConnectivity"

    invoke-virtual {p2, v0, v1}, Landroid/content/Intent;->getBooleanExtra(Ljava/lang/String;Z)Z

    move-result p2

    invoke-static {p2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p2

    :goto_0
    invoke-static {p1, p2}, Lcom/zopim/android/sdk/data/ConnectionPath;->access$102(Lcom/zopim/android/sdk/data/ConnectionPath;Ljava/lang/Boolean;)Ljava/lang/Boolean;

    goto :goto_3

    :cond_1
    iget-object p2, p0, Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Network change occurred, but no connectivity extras available"

    invoke-static {p2, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    const-string p2, "android.permission.ACCESS_NETWORK_STATE"

    invoke-virtual {p1, p2}, Landroid/content/Context;->checkCallingOrSelfPermission(Ljava/lang/String;)I

    move-result p2

    const/4 v0, 0x1

    if-nez p2, :cond_2

    const/4 p2, 0x1

    goto :goto_1

    :cond_2
    const/4 p2, 0x0

    :goto_1
    if-eqz p2, :cond_5

    iget-object p2, p0, Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;->LOG_TAG:Ljava/lang/String;

    const-string v2, "Looking up active network info..."

    invoke-static {p2, v2}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    const-string p2, "connectivity"

    invoke-virtual {p1, p2}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/net/ConnectivityManager;

    invoke-virtual {p1}, Landroid/net/ConnectivityManager;->getActiveNetworkInfo()Landroid/net/NetworkInfo;

    move-result-object p1

    invoke-static {}, Lcom/zopim/android/sdk/data/ConnectionPath;->access$000()Lcom/zopim/android/sdk/data/ConnectionPath;

    move-result-object p2

    if-nez p1, :cond_3

    goto :goto_2

    :cond_3
    invoke-virtual {p1}, Landroid/net/NetworkInfo;->isConnected()Z

    move-result p1

    if-nez p1, :cond_4

    goto :goto_2

    :cond_4
    const/4 v0, 0x0

    :goto_2
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    invoke-static {p2, p1}, Lcom/zopim/android/sdk/data/ConnectionPath;->access$102(Lcom/zopim/android/sdk/data/ConnectionPath;Ljava/lang/Boolean;)Ljava/lang/Boolean;

    goto :goto_3

    :cond_5
    iget-object p1, p0, Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;->LOG_TAG:Ljava/lang/String;

    const-string p2, "Unable to check device connection state. Assuming device is connected and leaving it to the web widget to verify connection."

    invoke-static {p1, p2}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    invoke-static {}, Lcom/zopim/android/sdk/data/ConnectionPath;->access$000()Lcom/zopim/android/sdk/data/ConnectionPath;

    move-result-object p1

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p2

    goto :goto_0

    :goto_3
    iget-object p1, p0, Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;->LOG_TAG:Ljava/lang/String;

    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v0, "Device "

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Lcom/zopim/android/sdk/data/ConnectionPath;->access$000()Lcom/zopim/android/sdk/data/ConnectionPath;

    move-result-object v0

    invoke-static {v0}, Lcom/zopim/android/sdk/data/ConnectionPath;->access$100(Lcom/zopim/android/sdk/data/ConnectionPath;)Ljava/lang/Boolean;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-eqz v0, :cond_6

    const-string v0, "disconnected"

    goto :goto_4

    :cond_6
    const-string v0, "connected"

    :goto_4
    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    invoke-static {}, Lcom/zopim/android/sdk/data/ConnectionPath;->access$000()Lcom/zopim/android/sdk/data/ConnectionPath;

    move-result-object p1

    invoke-static {}, Lcom/zopim/android/sdk/data/ConnectionPath;->access$000()Lcom/zopim/android/sdk/data/ConnectionPath;

    move-result-object p2

    invoke-virtual {p2}, Lcom/zopim/android/sdk/data/ConnectionPath;->getData()Lcom/zopim/android/sdk/model/Connection;

    move-result-object p2

    invoke-virtual {p1, p2}, Lcom/zopim/android/sdk/data/ConnectionPath;->broadcast(Ljava/lang/Object;)V

    return-void

    :cond_7
    :goto_5
    iget-object p1, p0, Lcom/zopim/android/sdk/data/ConnectionPath$ConnectivityReceiver;->LOG_TAG:Ljava/lang/String;

    const-string p2, "onReceive: intent was null or getAction() was mismatched"

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method
