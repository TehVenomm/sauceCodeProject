.class Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection$1;
.super Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;
.source "CustomTabsServiceConnection.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection;->onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection;Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;Landroid/content/ComponentName;)V
    .locals 0

    .line 33
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsServiceConnection;

    invoke-direct {p0, p2, p3}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsClient;-><init>(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsService;Landroid/content/ComponentName;)V

    return-void
.end method
