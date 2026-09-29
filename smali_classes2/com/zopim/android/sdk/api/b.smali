.class Lcom/zopim/android/sdk/api/b;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/api/ChatService;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/ChatService;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/b;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/b;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ChatService;->access$000(Lcom/zopim/android/sdk/api/ChatService;)Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-static {}, Lcom/zopim/android/sdk/api/ChatService;->access$100()Lcom/zopim/android/sdk/api/a;

    move-result-object v0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/api/a;->a()V

    :cond_0
    return-void
.end method
