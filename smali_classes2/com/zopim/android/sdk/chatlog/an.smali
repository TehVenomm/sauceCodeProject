.class Lcom/zopim/android/sdk/chatlog/an;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Ljava/util/LinkedHashMap;

.field final synthetic b:Lcom/zopim/android/sdk/chatlog/am;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/am;Ljava/util/LinkedHashMap;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/an;->b:Lcom/zopim/android/sdk/chatlog/am;

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/an;->a:Ljava/util/LinkedHashMap;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/an;->b:Lcom/zopim/android/sdk/chatlog/am;

    iget-object v0, v0, Lcom/zopim/android/sdk/chatlog/am;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/an;->a:Ljava/util/LinkedHashMap;

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$1300(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;Ljava/util/LinkedHashMap;)V

    return-void
.end method
