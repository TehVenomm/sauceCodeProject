.class Lcom/helpshift/util/concurrent/HandlerThreadExecutor$1;
.super Ljava/lang/Object;
.source "HandlerThreadExecutor.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/util/concurrent/HandlerThreadExecutor;->runOnUiThread(Ljava/lang/Runnable;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/util/concurrent/HandlerThreadExecutor;

.field final synthetic val$r:Ljava/lang/Runnable;


# direct methods
.method constructor <init>(Lcom/helpshift/util/concurrent/HandlerThreadExecutor;Ljava/lang/Runnable;)V
    .locals 0

    .line 37
    iput-object p1, p0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor$1;->this$0:Lcom/helpshift/util/concurrent/HandlerThreadExecutor;

    iput-object p2, p0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor$1;->val$r:Ljava/lang/Runnable;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .line 40
    iget-object v0, p0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor$1;->this$0:Lcom/helpshift/util/concurrent/HandlerThreadExecutor;

    iget-object v0, v0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor;->uiHandler:Landroid/os/Handler;

    iget-object v1, p0, Lcom/helpshift/util/concurrent/HandlerThreadExecutor$1;->val$r:Ljava/lang/Runnable;

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
