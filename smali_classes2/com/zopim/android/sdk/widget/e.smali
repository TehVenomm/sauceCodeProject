.class Lcom/zopim/android/sdk/widget/e;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/widget/d;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/widget/d;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/e;->a:Lcom/zopim/android/sdk/widget/d;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/e;->a:Lcom/zopim/android/sdk/widget/d;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/d;->b:Lcom/zopim/android/sdk/widget/c;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/c;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$000(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    move-result-object v0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->stop()V

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/e;->a:Lcom/zopim/android/sdk/widget/d;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/d;->b:Lcom/zopim/android/sdk/widget/c;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/c;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1500(Lcom/zopim/android/sdk/widget/ChatWidgetService;)I

    move-result v0

    if-lez v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/e;->a:Lcom/zopim/android/sdk/widget/d;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/d;->b:Lcom/zopim/android/sdk/widget/c;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/c;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1300(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/widget/TextView;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/e;->a:Lcom/zopim/android/sdk/widget/d;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/d;->b:Lcom/zopim/android/sdk/widget/c;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/c;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$000(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;

    move-result-object v0

    const/4 v1, 0x4

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/chatlog/view/TypingIndicatorView;->setVisibility(I)V

    :cond_0
    return-void
.end method
