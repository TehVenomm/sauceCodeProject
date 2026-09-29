.class Lcom/zopim/android/sdk/api/ac;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/content/ServiceConnection;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ac;->a:Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onServiceConnected(Landroid/content/ComponentName;Landroid/os/IBinder;)V
    .locals 0

    check-cast p2, Lcom/zopim/android/sdk/api/ChatService$LocalBinder;

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object p1

    invoke-virtual {p2}, Lcom/zopim/android/sdk/api/ChatService$LocalBinder;->getService()Lcom/zopim/android/sdk/api/Chat;

    move-result-object p2

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/ZopimChat;->access$2002(Lcom/zopim/android/sdk/api/ZopimChat;Lcom/zopim/android/sdk/api/Chat;)Lcom/zopim/android/sdk/api/Chat;

    iget-object p1, p0, Lcom/zopim/android/sdk/api/ac;->a:Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    const/4 p2, 0x1

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->access$2102(Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;Z)Z

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->access$2200()Ljava/lang/String;

    move-result-object p1

    const-string p2, "Connected to chat service"

    invoke-static {p1, p2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object p1

    invoke-static {p1}, Lcom/zopim/android/sdk/api/ZopimChat;->access$2300(Lcom/zopim/android/sdk/api/ZopimChat;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object p1

    invoke-static {p1}, Lcom/zopim/android/sdk/api/ZopimChat;->access$2400(Lcom/zopim/android/sdk/api/ZopimChat;)V

    return-void
.end method

.method public onServiceDisconnected(Landroid/content/ComponentName;)V
    .locals 1

    iget-object p1, p0, Lcom/zopim/android/sdk/api/ac;->a:Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    const/4 v0, 0x0

    invoke-static {p1, v0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->access$2102(Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;Z)Z

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object p1

    const/4 v0, 0x0

    invoke-static {p1, v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$2002(Lcom/zopim/android/sdk/api/ZopimChat;Lcom/zopim/android/sdk/api/Chat;)Lcom/zopim/android/sdk/api/Chat;

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->access$2200()Ljava/lang/String;

    move-result-object p1

    const-string v0, "Disconnected from chat service"

    invoke-static {p1, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method
