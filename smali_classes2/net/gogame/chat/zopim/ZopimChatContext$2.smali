.class Lnet/gogame/chat/zopim/ZopimChatContext$2;
.super Lcom/zopim/android/sdk/data/observers/AgentsObserver;
.source "ZopimChatContext.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/chat/zopim/ZopimChatContext;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/zopim/ZopimChatContext;


# direct methods
.method constructor <init>(Lnet/gogame/chat/zopim/ZopimChatContext;)V
    .locals 0

    .line 67
    iput-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext$2;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/observers/AgentsObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public update(Ljava/util/Map;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/Agent;",
            ">;)V"
        }
    .end annotation

    .line 71
    iget-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext$2;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    invoke-static {p1}, Lnet/gogame/chat/zopim/ZopimChatContext;->access$000(Lnet/gogame/chat/zopim/ZopimChatContext;)V

    return-void
.end method
