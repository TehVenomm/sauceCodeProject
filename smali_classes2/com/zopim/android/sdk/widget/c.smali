.class Lcom/zopim/android/sdk/widget/c;
.super Lcom/zopim/android/sdk/data/observers/AgentsObserver;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/widget/ChatWidgetService;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/widget/ChatWidgetService;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/c;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/observers/AgentsObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public update(Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/Agent;",
            ">;)V"
        }
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/c;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/os/Handler;

    move-result-object v0

    new-instance v1, Lcom/zopim/android/sdk/widget/d;

    invoke-direct {v1, p0, p1}, Lcom/zopim/android/sdk/widget/d;-><init>(Lcom/zopim/android/sdk/widget/c;Ljava/util/Map;)V

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    return-void
.end method
