.class public abstract Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection;
.super Ljava/lang/Object;
.source "CustomTabsServiceConnection.java"

# interfaces
.implements Landroid/content/ServiceConnection;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 28
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public abstract onCustomTabsServiceConnected(Landroid/content/ComponentName;Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;)V
.end method

.method public final onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 1

    .line 32
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection$1;

    .line 33
    invoke-static {p2}, Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService$Stub;->asInterface(Landroid/os/IBinder;)Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;

    move-result-object p2

    invoke-direct {v0, p0, p2, p1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection$1;-><init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection;Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;Landroid/content/ComponentName;)V

    .line 32
    invoke-virtual {p0, p1, v0}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection;->onCustomTabsServiceConnected(Landroid/content/ComponentName;Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;)V

    return-void
.end method
