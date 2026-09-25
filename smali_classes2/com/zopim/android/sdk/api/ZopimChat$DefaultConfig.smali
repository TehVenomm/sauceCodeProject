.class public Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;
.super Lcom/zopim/android/sdk/api/i;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/api/ZopimChat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "DefaultConfig"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Lcom/zopim/android/sdk/api/i<",
        "Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;",
        ">;"
    }
.end annotation


# static fields
.field private static final serialVersionUID:J = -0x306360a8e62a564dL


# instance fields
.field disableVisitorInfoStorage:Z

.field initializationTimeout:Ljava/lang/Long;

.field reconnectTimeout:Ljava/lang/Long;

.field sessionTimeout:Ljava/lang/Long;

.field final synthetic this$0:Lcom/zopim/android/sdk/api/ZopimChat;


# direct methods
.method private constructor <init>(Lcom/zopim/android/sdk/api/ZopimChat;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->this$0:Lcom/zopim/android/sdk/api/ZopimChat;

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/i;-><init>()V

    return-void
.end method

.method synthetic constructor <init>(Lcom/zopim/android/sdk/api/ZopimChat;Lcom/zopim/android/sdk/api/ab;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;-><init>(Lcom/zopim/android/sdk/api/ZopimChat;)V

    return-void
.end method


# virtual methods
.method public build()Ljava/lang/Void;
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->department:Ljava/lang/String;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->this$0:Lcom/zopim/android/sdk/api/ZopimChat;

    iget-object v1, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->department:Ljava/lang/String;

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/ZopimChat;->access$302(Lcom/zopim/android/sdk/api/ZopimChat;Ljava/lang/String;)Ljava/lang/String;

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->preChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->this$0:Lcom/zopim/android/sdk/api/ZopimChat;

    iget-object v1, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->preChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/ZopimChat;->access$402(Lcom/zopim/android/sdk/api/ZopimChat;Lcom/zopim/android/sdk/prechat/PreChatForm;)Lcom/zopim/android/sdk/prechat/PreChatForm;

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->tags:[Ljava/lang/String;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->this$0:Lcom/zopim/android/sdk/api/ZopimChat;

    iget-object v1, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->tags:[Ljava/lang/String;

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/ZopimChat;->access$502(Lcom/zopim/android/sdk/api/ZopimChat;[Ljava/lang/String;)[Ljava/lang/String;

    :cond_2
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->title:Ljava/lang/String;

    if-eqz v0, :cond_3

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->title:Ljava/lang/String;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$602(Ljava/lang/String;)Ljava/lang/String;

    :cond_3
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->referrer:Ljava/lang/String;

    if-eqz v0, :cond_4

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->referrer:Ljava/lang/String;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$702(Ljava/lang/String;)Ljava/lang/String;

    :cond_4
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->initializationTimeout:Ljava/lang/Long;

    if-eqz v0, :cond_5

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->initializationTimeout:Ljava/lang/Long;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$802(Ljava/lang/Long;)Ljava/lang/Long;

    :cond_5
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->reconnectTimeout:Ljava/lang/Long;

    if-eqz v0, :cond_6

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->reconnectTimeout:Ljava/lang/Long;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$902(Ljava/lang/Long;)Ljava/lang/Long;

    :cond_6
    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->sessionTimeout:Ljava/lang/Long;

    if-eqz v0, :cond_7

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->sessionTimeout:Ljava/lang/Long;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1002(Ljava/lang/Long;)Ljava/lang/Long;

    :cond_7
    iget-boolean v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->disableVisitorInfoStorage:Z

    if-eqz v0, :cond_8

    const/4 v0, 0x1

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1102(Z)Z

    :cond_8
    const/4 v0, 0x0

    return-object v0
.end method

.method public disableVisitorInfoStorage()Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;
    .locals 1

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->disableVisitorInfoStorage:Z

    return-object p0
.end method

.method public initializationTimeout(J)Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;
    .locals 3

    const-wide/16 v0, 0x0

    cmp-long v2, p1, v0

    if-gez v2, :cond_0

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$200()Ljava/lang/String;

    move-result-object p1

    const-string p2, "Can not configure initialization timeout. Timeout must not be less then 0"

    invoke-static {p1, p2}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    return-object p0

    :cond_0
    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->initializationTimeout:Ljava/lang/Long;

    return-object p0
.end method

.method public reconnectTimeout(J)Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;
    .locals 3

    const-wide/16 v0, 0x0

    cmp-long v2, p1, v0

    if-gez v2, :cond_0

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$200()Ljava/lang/String;

    move-result-object p1

    const-string p2, "Can not configure reconnect timeout. Timeout must not be less then 0"

    invoke-static {p1, p2}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    return-object p0

    :cond_0
    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->reconnectTimeout:Ljava/lang/Long;

    return-object p0
.end method

.method public sessionTimeout(J)Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;
    .locals 3

    const-wide/16 v0, 0x0

    cmp-long v2, p1, v0

    if-gez v2, :cond_0

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$200()Ljava/lang/String;

    move-result-object p1

    const-string p2, "Can not configure session timeout. Timeout must not be less then 0"

    invoke-static {p1, p2}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    return-object p0

    :cond_0
    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ZopimChat$DefaultConfig;->sessionTimeout:Ljava/lang/Long;

    return-object p0
.end method
