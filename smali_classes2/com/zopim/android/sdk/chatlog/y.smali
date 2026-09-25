.class Lcom/zopim/android/sdk/chatlog/y;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/model/Connection;

.field final synthetic b:Lcom/zopim/android/sdk/chatlog/x;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/x;Lcom/zopim/android/sdk/model/Connection;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/y;->b:Lcom/zopim/android/sdk/chatlog/x;

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/y;->a:Lcom/zopim/android/sdk/model/Connection;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/y;->b:Lcom/zopim/android/sdk/chatlog/x;

    iget-object v0, v0, Lcom/zopim/android/sdk/chatlog/x;->a:Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/y;->a:Lcom/zopim/android/sdk/model/Connection;

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;->access$000(Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;Lcom/zopim/android/sdk/model/Connection;)V

    return-void
.end method
