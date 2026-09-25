.class Lcom/zopim/android/sdk/chatlog/j;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/i;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/i;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/j;->a:Lcom/zopim/android/sdk/chatlog/i;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(I)V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/j;->a:Lcom/zopim/android/sdk/chatlog/i;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/i;->a(Lcom/zopim/android/sdk/chatlog/i;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    if-eqz v0, :cond_2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/j;->a:Lcom/zopim/android/sdk/chatlog/i;

    invoke-virtual {v0, p1}, Lcom/zopim/android/sdk/chatlog/i;->b(I)Lcom/zopim/android/sdk/chatlog/aa;

    move-result-object p1

    instance-of v0, p1, Lcom/zopim/android/sdk/chatlog/ab;

    if-eqz v0, :cond_2

    move-object v0, p1

    check-cast v0, Lcom/zopim/android/sdk/chatlog/ab;

    iget-object v1, v0, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    if-eqz v1, :cond_0

    const/4 v1, 0x1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    if-eqz v1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/j;->a:Lcom/zopim/android/sdk/chatlog/i;

    invoke-static {p1}, Lcom/zopim/android/sdk/chatlog/i;->a(Lcom/zopim/android/sdk/chatlog/i;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object p1

    iget-object v0, v0, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    invoke-interface {p1, v0}, Lcom/zopim/android/sdk/api/Chat;->send(Ljava/io/File;)V

    goto :goto_1

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/j;->a:Lcom/zopim/android/sdk/chatlog/i;

    invoke-static {v0}, Lcom/zopim/android/sdk/chatlog/i;->a(Lcom/zopim/android/sdk/chatlog/i;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    invoke-interface {v0, p1}, Lcom/zopim/android/sdk/api/Chat;->resend(Ljava/lang/String;)V

    :cond_2
    :goto_1
    return-void
.end method
