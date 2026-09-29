.class Lcom/zopim/android/sdk/chatlog/v;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/model/Connection;

.field final synthetic b:Lcom/zopim/android/sdk/chatlog/u;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/u;Lcom/zopim/android/sdk/model/Connection;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/v;->b:Lcom/zopim/android/sdk/chatlog/u;

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/v;->a:Lcom/zopim/android/sdk/model/Connection;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/v;->b:Lcom/zopim/android/sdk/chatlog/u;

    iget-object v0, v0, Lcom/zopim/android/sdk/chatlog/u;->a:Lcom/zopim/android/sdk/chatlog/ConnectionFragment;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/v;->a:Lcom/zopim/android/sdk/model/Connection;

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;->access$000(Lcom/zopim/android/sdk/chatlog/ConnectionFragment;Lcom/zopim/android/sdk/model/Connection;)V

    return-void
.end method
