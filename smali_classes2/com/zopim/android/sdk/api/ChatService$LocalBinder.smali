.class public Lcom/zopim/android/sdk/api/ChatService$LocalBinder;
.super Landroid/os/Binder;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/api/ChatService;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "LocalBinder"
.end annotation


# instance fields
.field final synthetic this$0:Lcom/zopim/android/sdk/api/ChatService;


# direct methods
.method public constructor <init>(Lcom/zopim/android/sdk/api/ChatService;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ChatService$LocalBinder;->this$0:Lcom/zopim/android/sdk/api/ChatService;

    invoke-direct {p0}, Landroid/os/Binder;-><init>()V

    return-void
.end method


# virtual methods
.method public getService()Lcom/zopim/android/sdk/api/Chat;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ChatService$LocalBinder;->this$0:Lcom/zopim/android/sdk/api/ChatService;

    return-object v0
.end method
