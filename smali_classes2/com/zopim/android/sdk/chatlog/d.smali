.class Lcom/zopim/android/sdk/chatlog/d;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/d;->a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 3

    :try_start_0
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/d;->a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->itemView:Landroid/view/View;

    invoke-virtual {p1}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object p1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/d;->a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->c(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)Landroid/content/Intent;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V
    :try_end_0
    .catch Landroid/content/ActivityNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    invoke-static {}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a()Ljava/lang/String;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can\'t open attachment. No application can handle this uri. "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/d;->a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    invoke-static {v2}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->c(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)Landroid/content/Intent;

    move-result-object v2

    invoke-virtual {v2}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1, p1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/d;->a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->itemView:Landroid/view/View;

    invoke-virtual {p1}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object p1

    sget v0, Lcom/zopim/android/sdk/R$string;->attachment_open_error_message:I

    const/4 v1, 0x0

    invoke-static {p1, v0, v1}, Landroid/widget/Toast;->makeText(Landroid/content/Context;II)Landroid/widget/Toast;

    move-result-object p1

    invoke-virtual {p1}, Landroid/widget/Toast;->show()V

    :goto_0
    return-void
.end method
