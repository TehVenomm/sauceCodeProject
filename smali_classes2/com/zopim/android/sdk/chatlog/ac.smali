.class Lcom/zopim/android/sdk/chatlog/ac;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ac;->a:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ac;->a:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->h:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder$OnClickListener;

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ac;->a:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;

    iget-object p1, p1, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->h:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder$OnClickListener;

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ac;->a:Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->getAdapterPosition()I

    move-result v0

    invoke-interface {p1, v0}, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder$OnClickListener;->onClick(I)V

    goto :goto_0

    :cond_0
    invoke-static {}, Lcom/zopim/android/sdk/chatlog/VisitorMessageHolder;->a()Ljava/lang/String;

    move-result-object p1

    const-string v0, "Failed message click listener not configured. Click events are ignored."

    invoke-static {p1, v0}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method
