.class Lnet/gogame/chat/zopim/ZopimChatContext$1;
.super Lcom/zopim/android/sdk/data/observers/ConnectionObserver;
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

    .line 58
    iput-object p1, p0, Lnet/gogame/chat/zopim/ZopimChatContext$1;->this$0:Lnet/gogame/chat/zopim/ZopimChatContext;

    invoke-direct {p0}, Lcom/zopim/android/sdk/data/observers/ConnectionObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public update(Lcom/zopim/android/sdk/model/Connection;)V
    .locals 0

    return-void
.end method
