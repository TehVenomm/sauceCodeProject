.class public Lnet/gogame/gowrap/ui/customtabs/PostMessageService;
.super Landroid/app/Service;
.source "PostMessageService.java"


# instance fields
.field private mBinder:Lnet/gogame/gowrap/ui/customtabs/IPostMessageService$Stub;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 28
    invoke-direct {p0}, Landroid/app/Service;-><init>()V

    .line 29
    new-instance v0, Lnet/gogame/gowrap/ui/customtabs/PostMessageService$1;

    invoke-direct {v0, p0}, Lnet/gogame/gowrap/ui/customtabs/PostMessageService$1;-><init>(Lnet/gogame/gowrap/ui/customtabs/PostMessageService;)V

    iput-object v0, p0, Lnet/gogame/gowrap/ui/customtabs/PostMessageService;->mBinder:Lnet/gogame/gowrap/ui/customtabs/IPostMessageService$Stub;

    return-void
.end method


# virtual methods
.method public onBind(Landroid/content/Intent;)Landroid/os/IBinder;
    .locals 0

    .line 46
    iget-object p1, p0, Lnet/gogame/gowrap/ui/customtabs/PostMessageService;->mBinder:Lnet/gogame/gowrap/ui/customtabs/IPostMessageService$Stub;

    return-object p1
.end method
