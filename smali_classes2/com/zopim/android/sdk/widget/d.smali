.class Lcom/zopim/android/sdk/widget/d;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Ljava/util/Map;

.field final synthetic b:Lcom/zopim/android/sdk/widget/c;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/widget/c;Ljava/util/Map;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/d;->b:Lcom/zopim/android/sdk/widget/c;

    iput-object p2, p0, Lcom/zopim/android/sdk/widget/d;->a:Ljava/util/Map;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 6
    .annotation build Landroid/annotation/TargetApi;
        value = 0xb
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/d;->a:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->values()Ljava/util/Collection;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v0

    const/4 v1, 0x0

    :cond_0
    const/4 v2, 0x0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_3

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/zopim/android/sdk/model/Agent;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/Agent;->isTyping()Ljava/lang/Boolean;

    move-result-object v4

    if-nez v4, :cond_1

    goto :goto_0

    :cond_1
    if-nez v2, :cond_2

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/Agent;->isTyping()Ljava/lang/Boolean;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v2

    if-eqz v2, :cond_0

    :cond_2
    const/4 v2, 0x1

    goto :goto_0

    :cond_3
    if-eqz v2, :cond_4

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/d;->b:Lcom/zopim/android/sdk/widget/c;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/c;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$000(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    move-result-object v0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->start()V

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/d;->b:Lcom/zopim/android/sdk/widget/c;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/c;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1300(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/widget/TextView;

    move-result-object v0

    const/4 v2, 0x4

    invoke-virtual {v0, v2}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/d;->b:Lcom/zopim/android/sdk/widget/c;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/c;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$000(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    move-result-object v0

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->setVisibility(I)V

    goto :goto_1

    :cond_4
    const-wide/16 v0, 0x0

    sget v2, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v3, 0xb

    if-lt v2, v3, :cond_5

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/d;->b:Lcom/zopim/android/sdk/widget/c;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/c;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1400(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/animation/AnimatorSet;

    move-result-object v0

    invoke-virtual {v0}, Landroid/animation/AnimatorSet;->getDuration()J

    move-result-wide v0

    :cond_5
    iget-object v2, p0, Lcom/zopim/android/sdk/widget/d;->b:Lcom/zopim/android/sdk/widget/c;

    iget-object v2, v2, Lcom/zopim/android/sdk/widget/c;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v2}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1200(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/os/Handler;

    move-result-object v2

    new-instance v3, Lcom/zopim/android/sdk/widget/e;

    invoke-direct {v3, p0}, Lcom/zopim/android/sdk/widget/e;-><init>(Lcom/zopim/android/sdk/widget/d;)V

    const-wide/16 v4, 0x2

    mul-long v0, v0, v4

    invoke-virtual {v2, v3, v0, v1}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    :goto_1
    return-void
.end method
