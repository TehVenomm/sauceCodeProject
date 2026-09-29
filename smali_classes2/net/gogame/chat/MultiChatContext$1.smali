.class Lnet/gogame/chat/MultiChatContext$1;
.super Landroid/database/DataSetObserver;
.source "MultiChatContext.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/chat/MultiChatContext;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/MultiChatContext;


# direct methods
.method constructor <init>(Lnet/gogame/chat/MultiChatContext;)V
    .locals 0

    .line 17
    iput-object p1, p0, Lnet/gogame/chat/MultiChatContext$1;->this$0:Lnet/gogame/chat/MultiChatContext;

    invoke-direct {p0}, Landroid/database/DataSetObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public onChanged()V
    .locals 1

    .line 21
    invoke-super {p0}, Landroid/database/DataSetObserver;->onChanged()V

    .line 22
    iget-object v0, p0, Lnet/gogame/chat/MultiChatContext$1;->this$0:Lnet/gogame/chat/MultiChatContext;

    invoke-virtual {v0}, Lnet/gogame/chat/MultiChatContext;->notifyDataSetChanged()V

    return-void
.end method
