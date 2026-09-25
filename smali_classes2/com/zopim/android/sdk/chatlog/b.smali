.class Lcom/zopim/android/sdk/chatlog/b;
.super Lcom/squareup/picasso/Callback$EmptyCallback;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/b;->a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    invoke-direct {p0}, Lcom/squareup/picasso/Callback$EmptyCallback;-><init>()V

    return-void
.end method


# virtual methods
.method public onError()V
    .locals 2

    invoke-super {p0}, Lcom/squareup/picasso/Callback$EmptyCallback;->onError()V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/b;->a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)Landroid/widget/ProgressBar;

    move-result-object v0

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    return-void
.end method

.method public onSuccess()V
    .locals 2

    invoke-super {p0}, Lcom/squareup/picasso/Callback$EmptyCallback;->onSuccess()V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/b;->a:Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;->a(Lcom/zopim/android/sdk/chatlog/AgentMessageHolder;)Landroid/widget/ProgressBar;

    move-result-object v0

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/widget/ProgressBar;->setVisibility(I)V

    return-void
.end method
