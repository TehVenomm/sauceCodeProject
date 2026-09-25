.class Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1$1;
.super Ljava/lang/Object;
.source "CustomTabsService.java"

# interfaces
.implements Landroid/os/IBinder$DeathRecipient;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->newSession(Lnet/gogame/gowrap/ui/customtabs/ICustomTabsCallback;)Z
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$1:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;

.field final synthetic val$sessionToken:Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)V
    .locals 0

    .line 107
    iput-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1$1;->this$1:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;

    iput-object p2, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1$1;->val$sessionToken:Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public binderDied()V
    .locals 2

    .line 110
    iget-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1$1;->this$1:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;

    iget-object v0, v0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1;->this$0:Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService$1$1;->val$sessionToken:Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;

    invoke-virtual {v0, v1}, Lnet/gogame/gowrap/ui/customtabs/CustomTabsService;->cleanUpSession(Lnet/gogame/gowrap/ui/customtabs/CustomTabsSessionToken;)Z

    return-void
.end method
