.class Lcom/zopim/android/sdk/chatlog/c;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/c;->a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/c;->a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->b(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)Lcom/zopim/android/sdk/chatlog/AgentMessageHolder$OptionClickListener;

    move-result-object v0

    if-nez v0, :cond_0

    invoke-static {}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Agent item click listener not configured. Click events are ignored."

    invoke-static {v0, v1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    :cond_0
    invoke-static {}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a()Ljava/lang/String;

    move-result-object v0

    const-string v1, "Clicked option item"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v0, 0x0

    invoke-virtual {p1, v0}, Landroid/view/View;->setClickable(Z)V

    check-cast p1, Landroid/widget/TextView;

    sget v0, Lcom/zopim/android/sdk/R$drawable;->bg_chat_bubble_visitor:I

    invoke-virtual {p1, v0}, Landroid/widget/TextView;->setBackgroundResource(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/c;->a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    iget-object v0, v0, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->itemView:Landroid/view/View;

    invoke-virtual {v0}, Landroid/view/View;->getContext()Landroid/content/Context;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$style;->chat_bubble_visitor:I

    invoke-virtual {p1, v0, v1}, Landroid/widget/TextView;->setTextAppearance(Landroid/content/Context;I)V

    invoke-virtual {p1}, Landroid/widget/TextView;->getText()Ljava/lang/CharSequence;

    move-result-object p1

    invoke-interface {p1}, Ljava/lang/CharSequence;->toString()Ljava/lang/String;

    move-result-object p1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/c;->a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->b(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)Lcom/zopim/android/sdk/chatlog/AgentMessageHolder$OptionClickListener;

    move-result-object v0

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder$OptionClickListener;->onClick(Ljava/lang/String;)V

    return-void
.end method
