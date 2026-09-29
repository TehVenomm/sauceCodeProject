.class Lcom/zopim/android/sdk/chatlog/u;
.super Lcom/zopim/android/sdk/data/observers/ConnectionObserver;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/ConnectionFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ConnectionFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/u;->a:Lcom/zopim/android/sdk/chatlog/ConnectionFragment;

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/observers/ConnectionObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public update(Lcom/zopim/android/sdk/model/Connection;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/u;->a:Lcom/zopim/android/sdk/chatlog/ConnectionFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->access$100(Lcom/zopim/android/sdk/chatlog/ConnectionFragment;)Landroid/os/Handler;

    move-result-object v0

    new-instance v1, Lcom/zopim/android/sdk/chatlog/v;

    invoke-direct {v1, p0, p1}, Lcom/zopim/android/sdk/chatlog/v;-><init>(Lcom/zopim/android/sdk/chatlog/u;Lcom/zopim/android/sdk/model/Connection;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
