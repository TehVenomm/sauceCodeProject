.class Lcom/zopim/android/sdk/widget/g;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:I

.field final synthetic b:Lcom/zopim/android/sdk/widget/f;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/widget/f;I)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/widget/g;->b:Lcom/zopim/android/sdk/widget/f;

    iput p2, p0, Lcom/zopim/android/sdk/widget/g;->a:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/g;->b:Lcom/zopim/android/sdk/widget/f;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/f;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1300(Lcom/zopim/android/sdk/widget/ChatWidgetService;)Landroid/widget/TextView;

    move-result-object v0

    iget v1, p0, Lcom/zopim/android/sdk/widget/g;->a:I

    invoke-static {v1}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/TextView;->setText(Ljava/lang/CharSequence;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/widget/g;->b:Lcom/zopim/android/sdk/widget/f;

    iget-object v0, v0, Lcom/zopim/android/sdk/widget/f;->a:Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-static {v0}, Lcom/zopim/android/sdk/widget/ChatWidgetService;->access$1700(Lcom/zopim/android/sdk/widget/ChatWidgetService;)V

    return-void
.end method
