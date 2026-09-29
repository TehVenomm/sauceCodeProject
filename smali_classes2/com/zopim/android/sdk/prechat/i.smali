.class Lcom/zopim/android/sdk/prechat/i;
.super Lcom/zopim/android/sdk/data/observers/ConnectionObserver;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/i;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/observers/ConnectionObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public update(Lcom/zopim/android/sdk/model/Connection;)V
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/i;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$200(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->hasEnded()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    :cond_0
    sget-object v0, Lcom/zopim/android/sdk/prechat/c;->a:[I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/Connection;->getStatus()Lcom/zopim/android/sdk/model/Connection$Status;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/Connection$Status;->ordinal()I

    move-result p1

    aget p1, v0, p1

    packed-switch p1, :pswitch_data_0

    goto :goto_0

    :pswitch_0
    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/i;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$600(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Z

    move-result p1

    if-nez p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/i;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    const/4 v0, 0x1

    invoke-static {p1, v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$602(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;Z)Z

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/i;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$900(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V

    goto :goto_0

    :pswitch_1
    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/i;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$600(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Z

    move-result p1

    if-nez p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/i;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$700(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/i;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {p1}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$800(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V

    :cond_1
    :goto_0
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method
