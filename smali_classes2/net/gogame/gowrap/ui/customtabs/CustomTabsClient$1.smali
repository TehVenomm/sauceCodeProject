.class final Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$1;
.super Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection;
.source "CustomTabsClient.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;->connectAndInitialize(Landroid/content/Context;Ljava/lang/String;)Z
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$applicationContext:Landroid/content/Context;


# direct methods
.method constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 133
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$1;->val$applicationContext:Landroid/content/Context;

    invoke-direct {p0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection;-><init>()V

    return-void
.end method


# virtual methods
.method public final onCustomTabsServiceConnected(Landroid/content/ComponentName;Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;)V
    .locals 2

    const-wide/16 v0, 0x0

    .line 137
    invoke-virtual {p2, v0, v1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;->warmup(J)Z

    .line 141
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient$1;->val$applicationContext:Landroid/content/Context;

    invoke-virtual {p1, p0}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    return-void
.end method

.method public final onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 0

    return-void
.end method
