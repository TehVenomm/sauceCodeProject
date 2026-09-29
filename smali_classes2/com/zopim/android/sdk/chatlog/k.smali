.class Lcom/zopim/android/sdk/chatlog/k;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/chatlog/AgentMessageHolder$OptionClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/i;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/i;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/k;->a:Lcom/zopim/android/sdk/chatlog/i;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Ljava/lang/String;)V
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/k;->a:Lcom/zopim/android/sdk/chatlog/i;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/i;->a(Lcom/zopim/android/sdk/chatlog/i;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/k;->a:Lcom/zopim/android/sdk/chatlog/i;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/i;->a(Lcom/zopim/android/sdk/chatlog/i;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->send(Ljava/lang/String;)V

    :cond_0
    return-void
.end method
