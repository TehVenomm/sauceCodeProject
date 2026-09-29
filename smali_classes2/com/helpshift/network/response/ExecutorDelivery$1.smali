.class Lcom/helpshift/network/response/ExecutorDelivery$1;
.super Ljava/lang/Object;
.source "ExecutorDelivery.java"

# interfaces
.implements Ljava/util/concurrent/Executor;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/network/response/ExecutorDelivery;-><init>(Landroid/os/Handler;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/network/response/ExecutorDelivery;

.field final synthetic val$handler:Landroid/os/Handler;


# direct methods
.method constructor <init>(Lcom/helpshift/network/response/ExecutorDelivery;Landroid/os/Handler;)V
    .locals 0

    .line 40
    iput-object p1, p0, Lcom/helpshift/network/response/ExecutorDelivery$1;->this$0:Lcom/helpshift/network/response/ExecutorDelivery;

    iput-object p2, p0, Lcom/helpshift/network/response/ExecutorDelivery$1;->val$handler:Landroid/os/Handler;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public execute(Ljava/lang/Runnable;)V
    .locals 1

    .line 43
    iget-object v0, p0, Lcom/helpshift/network/response/ExecutorDelivery$1;->val$handler:Landroid/os/Handler;

    invoke-virtual {v0, p1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
