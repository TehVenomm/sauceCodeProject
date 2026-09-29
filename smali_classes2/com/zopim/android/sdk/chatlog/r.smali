.class Lcom/zopim/android/sdk/chatlog/r;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/r;->a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    new-instance p1, Landroid/content/Intent;

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/r;->a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    iget-object v0, v0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->itemView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    const-class v1, Lcom/zopim/android/sdk/chatlog/ZopimCommentActivity;

    invoke-direct {p1, v0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v0, "COMMENT"

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/r;->a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    invoke-static {v1}, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->d(Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;)Landroid/widget/TextView;

    move-result-object v1

    invoke-virtual {v1}, Landroid/widget/TextView;->getText()Ljava/lang/CharSequence;

    move-result-object v1

    invoke-interface {v1}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/r;->a:Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;

    iget-object v0, v0, Lcom/zopim/android/sdk/chatlog/ChatRatingHolder;->itemView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    invoke-virtual {v0, p1}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    return-void
.end method
