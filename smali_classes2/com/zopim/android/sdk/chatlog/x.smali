.class Lcom/zopim/android/sdk/chatlog/x;
.super Lcom/zopim/android/sdk/data/observers/ConnectionObserver;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/x;->a:Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/observers/ConnectionObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public update(Lcom/zopim/android/sdk/model/Connection;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/x;->a:Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->access$100(Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;)Landroid/os/Handler;

    move-result-object v0

    new-instance v1, Lcom/zopim/android/sdk/chatlog/y;

    invoke-direct {v1, p0, p1}, Lcom/zopim/android/sdk/chatlog/y;-><init>(Lcom/zopim/android/sdk/chatlog/x;Lcom/zopim/android/sdk/model/Connection;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
