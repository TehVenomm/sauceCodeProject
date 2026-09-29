.class Lnet/gogame/chat/ChatAdapter$1;
.super Landroid/database/DataSetObserver;
.source "ChatAdapter.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/chat/ChatAdapter;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/ChatAdapter;


# direct methods
.method constructor <init>(Lnet/gogame/chat/ChatAdapter;)V
    .locals 0

    .line 17
    iput-object p1, p0, Lnet/gogame/chat/ChatAdapter$1;->this$0:Lnet/gogame/chat/ChatAdapter;

    invoke-direct {p0}, Landroid/database/DataSetObserver;-><init>()V

    return-void
.end method


# virtual methods
.method public onChanged()V
    .locals 1

    .line 21
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapter$1;->this$0:Lnet/gogame/chat/ChatAdapter;

    invoke-virtual {v0}, Lnet/gogame/chat/ChatAdapter;->notifyDataSetChanged()V

    return-void
.end method
